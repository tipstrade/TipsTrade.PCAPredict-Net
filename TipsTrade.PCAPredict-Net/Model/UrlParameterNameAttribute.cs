using System;

namespace TipsTrade.PCAPredict.Model {
  /// <summary>Represents an attribute used for specifying the PCA Predict service endpoint.</summary>
  [AttributeUsage(AttributeTargets.Property)]
  public class UrlParameterNameAttribute : Attribute {
    /// <summary>Gets or sets the name of the url parameter.</summary>
    public string Name { get; set; }

    /// <summary>Creates an instance of the TipsTrade.PCAPredict.Model.UrlParameterNameAttribute class.</summary>
    /// <param name="name">The name of the url parameter.</param>
    public UrlParameterNameAttribute(string name) {
      Name = name;
    }
  }
}
