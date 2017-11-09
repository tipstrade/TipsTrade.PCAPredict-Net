namespace TipsTrade.PCAPredict.Model {
  /// <summary>Represents a PCA Predict Capture+ Address Find request.</summary>
  public class FindRequest : ApiRequestBase {
    #region Properties
    /// <summary>A container for the search. This should only be another Id previously returned from this service when the Type of the result was not 'Address'.</summary>
    public string Container { get; set; }

    /// <summary>	A comma separated list of ISO 2 or 3 character country codes to limit the search within.</summary>
    public string Countries { get; set; }

    /// <summary>The preferred language for results. This should be a 2 or 4 character language code e.g. (en, fr, en-gb, en-us etc).</summary>
    public string Language { get; set; }

    /// <summary>The maximum number of results to return.</summary>
    public int? Limit { get; set; }

    /// <summary>	A starting location for the search. This can be the name or ISO 2 or 3 character code of a country, WGS84 coordinates (comma separated) or IP address to search from.</summary>
    public string Origin { get; set; }

    /// <summary>The search text to find. Ideally a postcode or the start of the address.</summary>
    public string Text { get; set; }

    /// <summary>Gets the service endpoint for the request.</summary>
    protected override string ServiceEndPoint => "https://services.postcodeanywhere.co.uk/Capture/Interactive/Find/v1.00/json3.ws";
    #endregion
  }

  /// <summary>Represents a PCA Predict Capture+ Address Find result.</summary>
  public class FindResult : ResultBase {
    #region Properties
    ///<summary>This can be an address Id or a container Id for further results.</summary>
    public string Id { get; set; }
    ///<summary>If the Type is 'Address' then the Id can be passed to the Retrieve service. Any other Id should be passed as the Container to a further Find request to get more results.</summary>
    public string Type { get; set; }
    ///<summary>The name of the result.</summary>
    public string Text { get; set; }
    ///<summary>A list of number ranges identifying the matched characters in the Text and Description.</summary>
    public string Highlight { get; set; }
    ///<summary>Descriptive information about the result.</summary>
    public new string Description { get; set; }
    #endregion

    #region Methods
    /// <summary>Returns a string that represents the current object.</summary>
    /// <returns></returns>
    public override string ToString() {
      if (!string.IsNullOrEmpty(Text) && (!string.IsNullOrEmpty(Description))) {
        return $"{Text}, {Description}";
      } else {
        return Text;
      }
    }
    #endregion
  }
}
