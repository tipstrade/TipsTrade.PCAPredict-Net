using Microsoft.Extensions.Configuration;
using System;
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
    #endregion
  }
}
