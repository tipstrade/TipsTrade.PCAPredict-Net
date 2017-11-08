namespace TipsTrade.PCAPredict.Model {
  /// <summary>Represents a PCA Predict List Countries request.</summary>
  public class BasicCountryRequest : ApiRequestBase {
    /// <summary>The Filter to find. The Filter can be part of a country name or blank. If blank, the complete list of ISO codes will be returned.</summary>
    public string Filter { get; set; }

    /// <summary>Gets the service endpoint for the request.</summary>
    protected override string ServiceEndPoint => "https://services.postcodeanywhere.co.uk/Extras/Lists/Countries/v1.10/json3.ws";
  }

  /// <summary>Represents a PCA Predict List Countries result.</summary>
  public class BasicCountryResult : ResultBase {
    /// <summary>	The 2 digit ISO code matching the Filter.</summary>
    public string Iso2 { get; set; }

    /// <summary>	The 3 digit ISO code matching the Filter.</summary>
    public string Iso3 { get; set; }

    /// <summary>The country name.</summary>
    public string Name { get; set; }
  }
}
