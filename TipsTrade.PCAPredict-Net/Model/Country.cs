namespace TipsTrade.PCAPredict.Model {
  /// <summary>Represents a PCA Predict Country List request.</summary>
  public class CountryRequest : ApiRequestBase {
    /// <summary>Gets the service endpoint for the request.</summary>
    protected override string ServiceEndPoint => "https://services.postcodeanywhere.co.uk/Extras/Lists/CountryList/v1.00/json3.ws";
  }

  /// <summary>Represents a PCA Predict List Country result.</summary>
  public class CountryResult : ResultBase {
    ///<summary>The 2 digit ISO code matching the Filter.</summary>
    public string Iso2 { get; set; }

    ///<summary>The 3 digit ISO code matching the Filter.</summary>
    public string Iso3 { get; set; }

    ///<summary>The country name.</summary>
    public string Name { get; set; }

    ///<summary>A comma separated list of alternate names for this country.</summary>
    public string AlternateNames { get; set; }

    ///<summary>The offset index for the flag image.</summary>
    public int FlagOffset { get; set; }
  }
}
