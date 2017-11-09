using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TipsTrade.PCAPredict.Model {
  /// <summary>Represents a PCA Predict PhoneNumberValidation Interactive request.</summary>
  public class PhoneNumberValidateRequest : ApiRequestBase {
    /// <summary>The ISO2 country code of the number you are trying to validate (if provided in national format).</summary>
    public string Country { get; set; }

    /// <summary>
    /// The mobile/cell phone number to verify. This must be in international format (+447528471411 or 447528471411) if no country
    /// code is provided or national format with a Country parameter provided (07528471411 and GB as the Country parameter).
    /// </summary>
    public string Phone { get; set; }

    /// <summary>Gets the service endpoint for the request.</summary>
    protected override string ServiceEndPoint => "https://services.postcodeanywhere.co.uk/PhoneNumberValidation/Interactive/Validate/v2.20/json3.ws";
  }

  /// <summary>Represents a PCA Predict PhoneNumberValidation Interactive result.</summary>
  public class PhoneNumberValidateResult : ResultBase {
    #region Enumerations
    /// <summary>Contains the available values for the Type property.</summary>
    public enum NumberType {
      Mobile,
      Landline,
      VOIP
    }

    /// <summary>Contains the available values for the IsValid property.</summary>
    public enum ValidType {
      Maybe,
      Yes,
      No
    }
    #endregion

    #region Properties
    ///<summary>The recipient phone number in international format.</summary>
    public string PhoneNumber { get; set; }

    ///<summary>Returns true if we managed to process the request on the network or false if the validation attempt was unsuccessful.</summary>
    public bool RequestProcessed { get; set; }

    ///<summary>Whether the number is valid or not (Unknown returned if validation wasn't possible).</summary>
    public ValidType IsValid { get; set; }

    ///<summary>The current operator serving the supplied number.</summary>
    public string NetworkCode { get; set; }

    /// <summary>The country code of the operator.</summary>
    public string NetworkCountry { get; set; }

    ///<summary>The name of the current operator serving the supplied number.</summary>
    public string NetworkName { get; set; }

    ///<summary>The domestic network format (useful for dialling from within the same country).</summary>
    public string NationalFormat { get; set; }

    ///<summary>The country prefix that must be prepended to the number when dialling internationally.</summary>
    public int CountryPrefix { get; set; }

    ///<summary>The type of number that was detected in the request (MOBILE, LANDLINE OR VOIP).</summary>
    [JsonProperty("NumberType")]
    [JsonConverter(typeof(StringEnumConverter))]
    public NumberType Type { get; set; }
    #endregion
  }
}
