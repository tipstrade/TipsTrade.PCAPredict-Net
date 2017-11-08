namespace TipsTrade.PCAPredict.Model {
  /// <summary>Represents a PCA Predict Geocode request.</summary>
  public class GeocodeRequest : ApiRequestBase {
    /// <summary>Gets the service endpoint for the request.</summary>
    protected override string ServiceEndPoint => "https://services.postcodeanywhere.co.uk/Geocoding/International/Geocode/v1.10/json3.ws";

    /// <summary>The name or ISO 2 or 3 character code for the country to search in. Most country names will be recognised but the use of the ISO country code is recommended for clarity.</summary>
    public string Country { get; set; }

    /// <summary>The location to geocode. This can be a postal code or place name.</summary>
    public string Location { get; set; }
  }

  /// <summary>Represents a PCA Predict Geocode result.</summary>
  public class GeocodeResult : ResultBase {
    #region Properties
    /// <summary>The WGS84 latitude of the found location.</summary>
    public float Latitude { get; set; }

    /// <summary>The WGS84 longitude of the found location.</summary>
    public float Longitude { get; set; }

    /// <summary>The name of the location found.</summary>
    public string Name { get; set; }
    #endregion

    #region Methods
    /// <summary>Returns a string that represents the current object.</summary>
    /// <returns></returns>
    public override string ToString() {
      return $"{Name} - {Latitude}, {Longitude}";
    }
    #endregion
  }
}
