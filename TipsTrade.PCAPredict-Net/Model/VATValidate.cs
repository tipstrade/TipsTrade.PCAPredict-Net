namespace TipsTrade.PCAPredict.Model {
  /// <summary>Represents a PCA Predict VAT Validate request.</summary>
  public class VATValidateRequest : ApiRequestBase {
    /// <summary>The name or ISO 2 or 3 character code for the country to search in. Most country names will be recognised but the use of the ISO country code is recommended for clarity.</summary>
    public string Country { get; set; }

    /// <summary>The company's VAT number. This may contain letters.</summary>
    public string Number { get; set; }

    /// <summary>Gets the service endpoint for the request.</summary>
    protected override string ServiceEndPoint => "https://services.postcodeanywhere.co.uk/Extras/VAT/Validate/v1.10/json3.ws";
  }

  /// <summary>Represents a PCA Predict VAT Validate result.</summary>
  public class VATValidateResult : ResultBase {
    /// <summary>The address of the company the VAT number is registered to (not always available).</summary>
    public string Address { get; set; }

    /// <summary>Indicates whether the VAT number is valid.</summary>
    public bool IsValid { get; set; }

    /// <summary>The name of the company the VAT number is registered to (not always available).</summary>
    public string Name { get; set; }
  }
}
