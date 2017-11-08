namespace TipsTrade.PCAPredict.Model {
  /// <summary>Represents a PCA Predict List Countries Data request.</summary>
  public class ListCountryDataRequest : ApiRequestBase {
    /// <summary>Gets the service endpoint for the request.</summary>
    protected override string ServiceEndPoint => "https://services.postcodeanywhere.co.uk/Extras/VAT/Validate/v1.10/json3.ws";
  }

  /// <summary>Represents a PCA Predict List Countries Data result.</summary>
  public class ListCountryDataResult : ResultBase {
    ///<summary>The 2 digit ISO code of the country.</summary>
    public string Country { get; set; }

    ///<summary>The country name.</summary>
    public string Name { get; set; }

    ///<summary>Quality of the addressing data for this country.</summary>
    public int Addressing { get; set; }

    ///<summary>Whether phone validation is available for this country.</summary>
    public bool Phone { get; set; }

    ///<summary>Whether email validation is available for this country.</summary>
    public bool Email { get; set; }

    ///<summary>The continent within which the country resides.</summary>
    public string Continent { get; set; }
  }
}
