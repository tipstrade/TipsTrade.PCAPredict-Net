using System;
using System.Collections.Generic;
using System.Text;

namespace TipsTrade.PCAPredict.Model {
  /// <summary>Represents a PCA Predict web service result.</summary>
  public abstract class ResultBase {
    #region Properties
    /// <summary>Gets or sets the cause of the error.</summary>
    public string Cause { get; set; }

    /// <summary>Gets or sets the description of the error.</summary>
    public string Description { get; set; }

    /// <summary>Gets or sets the error code.</summary>
    public int? Error { get; set; }

    /// <summary>Gets a flag indicating whether there is an error.</summary>
    public bool HasError {
      get {
        return Error.HasValue && (Error != 0);
      }
    }

    /// <summary>Gets or sets a description of how the error can be resolved.</summary>
    public string Resolution { get; set; }
    #endregion
  }
}
