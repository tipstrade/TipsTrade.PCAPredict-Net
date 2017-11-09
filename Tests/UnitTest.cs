using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TipsTrade.PCAPredict.Tests {
  public class UnitTest {
    private static IConfiguration config;

    /// <summary>Gets or sets the Test Configration.</summary>
    private static IConfiguration Configuration {
      get {
        if (config == null) {
          config = new ConfigurationBuilder()
            .AddJsonFile("client-secrets.json")
            .Build();
        }

        return config;
      }
    }

    /// <summary>Creates an instance of the PCA Predict client.</summary>
    private static PCAPredictClient CreateClient() {
      var section = Configuration.GetSection("pca");
      if (section == null) {
        throw new Exception("A 'pca' configuration section is required.");
      }

      return new PCAPredictClient() {
        ApiKey = section["ApiKey"],
        GeocodeKey = section["GeocodeKey"]
      };
    }

    #region Tests
    [Fact]
    public async Task TestAuth() {
      var client = CreateClient();

      var resp = await client.ListCountriesAsync(new Model.CountryRequest() {
      });

      Assert.NotEqual(0, resp.Count);
    }

    [Fact]
    public async Task TestAuthFail() {
      var client = CreateClient();
      client.ApiKey = null;
      client.GeocodeKey = null;

      try {
        await client.ListCountriesAsync(new Model.CountryRequest() {
        });
      } catch (PCAPredictException ex) {
        Assert.Equal(2, ex.Error);
      }
    }

    [Fact]
    public async Task TestFind() {
      var client = CreateClient();

      var resp = await client.FindAsync(new Model.FindRequest() {
        Countries = "GBR",
        Language = "en-GB",
        Limit = 5,
        Origin = "GBR",
        Text = "buckingham"
      });

      Assert.NotNull(resp);
      Assert.Equal(5, resp.Count);
      Assert.NotNull(resp[0].Description);
      Assert.Equal("0-10", resp[0].Highlight);
      Assert.NotNull(resp[0].Id);
      Assert.NotNull(resp[0].Text);
      Assert.NotNull(resp[0].Type);
    }

    [Fact]
    public async Task TestGeocode() {
      var client = CreateClient();

      var resp = await client.GeocodeAsync(new Model.GeocodeRequest() {
        Country = "DEU",
        Location = "29227"
      });

      Assert.NotNull(resp);
      Assert.NotNull(resp.Name);
      Assert.NotEqual(0, resp.Latitude);
      Assert.NotEqual(0, resp.Longitude);
    }

    [Fact]
    public async Task TestUKGeocode() {
      var client = CreateClient();

      var resp = await client.GeocodeAsync(new Model.UKGeocodeRequest() {
        Location = "EC1V0HB"
      });

      Assert.NotNull(resp);
      Assert.NotNull(resp.Location);
      Assert.NotEqual(0, resp.Easting);
      Assert.NotEqual(0, resp.Northing);
      Assert.NotEqual(0, resp.Latitude);
      Assert.NotEqual(0, resp.Longitude);
      Assert.NotNull(resp.OsGrid);
    }

    [Fact]
    public async Task TestListCountyData() {
      var client = CreateClient();

      var resp = await client.ListCountyDataAsync(new Model.ListCountryDataRequest() {
      });

      Assert.True(resp.Count > 1);
    }

    [Fact]
    public async Task TestListCountries() {
      var client = CreateClient();

      var resp = await client.ListCountriesAsync(new Model.CountryRequest() {
      });

      Assert.True(resp.Count > 1);
    }

    [Fact]
    public async Task TestListCountriesWithFilter() {
      var client = CreateClient();

      var resp = await client.ListCountriesAsync(new Model.BasicCountryRequest() {
        Filter = "United Kingdom"
      });

      Assert.Equal(1, resp.Count);
    }

    [Fact]
    public async Task TestPhoneValidation() {
      var client = CreateClient();

      var resp = await client.ValidatePhoneAsync(new Model.PhoneNumberValidateRequest() {
        Country = "GB",
        Phone = "01844 337 199"
      });

      Assert.NotNull(resp);
      Assert.Equal("+441844337199", resp.PhoneNumber);
      Assert.True(resp.RequestProcessed);
      Assert.Equal(Model.PhoneNumberValidateResult.ValidType.Yes, resp.IsValid);
      Assert.NotNull(resp.NetworkCode);
      Assert.NotNull(resp.NetworkName);
      Assert.Equal("GB", resp.NetworkCountry);
      Assert.Equal("01844 337199", resp.NationalFormat);
      Assert.Equal(44, resp.CountryPrefix);
      Assert.Equal(Model.PhoneNumberValidateResult.NumberType.Landline, resp.Type);
    }

    [Fact]
    public async Task TextRetrieve() {
      var client = CreateClient();

      var find = (await client.FindAsync(new Model.FindRequest() {
        Countries = "GBR",
        Language = "en-GB",
        Limit = 1,
        Origin = "GBR",
        Text = "buckingham palace"
      })).First();

      var resp = await client.RetrieveAsync(new Model.RetrieveRequest() {
         Id = find.Id
      });

      Assert.NotNull(resp);
      Assert.Equal(find.Id, resp.Id);
    }

    [Fact]
    public async Task TestVATValidate() {
      var client = CreateClient();

      var resp = await client.ValidateVATAsync(new Model.VATValidateRequest() {
        Number = "GB 816 0175 52"
      });

      Assert.NotNull(resp);
      Assert.NotNull(resp.Address);
      Assert.True(resp.IsValid);
      Assert.NotNull(resp.Name);
    }

    [Fact]
    public async Task TestVATValidateWithCountry() {
      var client = CreateClient();

      var resp = await client.ValidateVATAsync(new Model.VATValidateRequest() {
        Country = "GBR",
        Number = "816 0175 52"
      });

      Assert.NotNull(resp);
      Assert.NotNull(resp.Address);
      Assert.True(resp.IsValid);
      Assert.NotNull(resp.Name);
    }
    #endregion
  }
}
