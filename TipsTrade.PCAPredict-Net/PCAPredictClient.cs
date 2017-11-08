using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TipsTrade.PCAPredict.Model;

namespace TipsTrade.PCAPredict {
  /// <summary>Represents a strongly typed client for interacting with the PCA Predict web services.</summary>
  /// <see cref="!:https://www.pcapredict.com/support/webservices/"/>
  public class PCAPredictClient {
    #region Properties
    /// <summary>Gets or set the default service key to be used for any API requests.</summary>
    public string ApiKey { get; set; }

    /// <summary>Gets or sets a flag indicating whether empty strings should be converted to null.</summary>
    public bool ConvertEmptyStringToNull { get; set; } = false;

    /// <summary>Gets or set the default service key to be used for geocode requests.</summary>
    public string GeocodeKey { get; set; }

    /// <summary>Gets or sets the Web Proxy to be used for HTTP requests.</summary>
    public IWebProxy Proxy { get; set; }
    #endregion

    #region Internal methods
    private HttpWebRequest CreateRequest(ApiRequestBase request, string key = null) {
      var webRequest = WebRequest.Create(request.CreateRequestUrl(key ?? ApiKey)) as HttpWebRequest;
      webRequest.Method = "GET";
      webRequest.Proxy = Proxy;
      webRequest.Accept = "application/json";

      return webRequest;
    }

    private async Task<List<T>> GetResult<T>(ApiRequestBase request, string key = null) where T : ResultBase {
      var webRequest = CreateRequest(request, key);

      return await GetResult<T>(webRequest);
    }

    private async Task<List<T>> GetResult<T>(HttpWebRequest request) where T : ResultBase {
      ApiResult<T> result;

      var settings = new JsonSerializerSettings();
      if (ConvertEmptyStringToNull) {
        settings.Converters.Add(new Json.EmptyStringConverter());
      }

      using (var webResponse = await request.GetResponseAsync()) {
        using (var reader = new StreamReader(webResponse.GetResponseStream())) {
          result = JsonConvert.DeserializeObject<ApiResult<T>>(await reader.ReadToEndAsync(), settings);
        }
      }

      if (result.HasError) {
        throw result.CreateException();
      }

      return result.Items;
    }
    #endregion

    #region Methods
    /// <summary>Find addresses and places.</summary>
    public async Task<List<FindResult>> FindAsync(FindRequest request) {
      return await GetResult<FindResult>(request);
    }

    /// <summary>Returns the WGS84 latitude and longitude for the given location. Supports most international locations.</summary>
    public async Task<GeocodeResult> GeocodeAsync(GeocodeRequest request) {
      return (await GetResult<GeocodeResult>(request, GeocodeKey)).FirstOrDefault();
    }

    /// <summary>Lists ISO 3166 country codes and names. Also provides additional information about the data available.</summary>
    public async Task<List<ListCountryDataResult>> ListCountyDataAsync(ListCountryDataRequest request) {
      return await GetResult<ListCountryDataResult>(request);
    }

    /// <summary>Lists ISO 3166 country codes, names, alternate names and the offset for the flag image.</summary>
    public async Task<List<CountryResult>> ListCountriesAsync(CountryRequest request) {
      return await GetResult<CountryResult>(request);
    }

    /// <summary>Lists ISO 3166 country codes and names.</summary>
    public async Task<List<BasicCountryResult>> ListCountriesAsync(BasicCountryRequest request) {
      return await GetResult<BasicCountryResult>(request);
    }

    /// <summary>Returns the full address details based on the Id.</summary>
    /// <param name="geocode">A flag indicating whether the address found should be geocoded.</param>
    /// <returns></returns>
    public async Task<RetrieveResult> RetrieveAsync(RetrieveRequest request, bool geocode = false) {
      var result = (await GetResult<RetrieveResult>(request)).FirstOrDefault();

      if (geocode && (result != null) && !string.IsNullOrEmpty(result.PostalCode)) {
        result.Geocode = await GeocodeAsync(new GeocodeRequest() {
          Country = result.CountryIso3,
          Location = result.PostalCode
        });
      }

      return result;
    }

    /// <summary>Starts a new phone number validation request.</summary>
    public async Task<PhoneNumberValidateResult> ValidatePhoneAsync(PhoneNumberValidateRequest request) {
      return (await GetResult<PhoneNumberValidateResult>(request)).FirstOrDefault();
    }

    /// <summary>Verifies an EU VAT number is valid.</summary>
    public async Task<VATValidateResult> ValidateVATAsync(VATValidateRequest request) {
      return (await GetResult<VATValidateResult>(request)).FirstOrDefault();
    }
    #endregion
  }
}
