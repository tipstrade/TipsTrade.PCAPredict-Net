using System;

namespace TipsTrade.PCAPredict.Model {
  /// <summary>Represents a PCA Predict UK Geocode request.</summary>
  public class UKGeocodeRequest : GeocodeRequest {
    /// <summary>Gets the service endpoint for the request.</summary>
    protected override string ServiceEndPoint => "https://services.postcodeanywhere.co.uk/Geocoding/UK/Geocode/v2.10/json3.ws";

    /// <summary>Only GBR is supported.</summary>
    public new string Country => "GBR";

    /// <summary>
    /// The location to geocode. This can be a full or partial postcode, a place name, street comma town, address (comma separated lines)
    /// or an ID from PostcodeAnywhere/Find web services.
    /// </summary>
    public new string Location { get; set; }
  }

  /// <summary>Represents a PCA Predict UK Geocode result.</summary>
  public class UKGeocodeResult : GeocodeResult {
    #region Enumerations
    /// <summary>Contains the available values for the Type property.</summary>
    public enum AccuractyType {
      Standard,
      Property
    }
    #endregion

    #region Properties
    ///<summary>The name of the location found.</summary>
    public string Location { get; set; }

    ///<summary>The easting coordinate of the location. Represents a distance in meters east from the most south westerly position of the GB mapping grid.</summary>
    public int Easting { get; set; }

    ///<summary>The northing coordinate of the location. Represents a distance in meters north from the most south westerly position of the GB mapping grid.</summary>
    public int Northing { get; set; }

    /// <summary>The OS grid reference for the location.</summary>
    public string OsGrid { get; set; }

    ///<summary>The accuracy of the coordinates returned.</summary>
    public AccuractyType Accuracy { get; set; }
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
