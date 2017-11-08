using Microsoft.Extensions.Configuration;
using System;

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
  }
}
