using System;

namespace TipsTrade.PCAPredict {
  /// <summary>Represents errors that occur when accessing the PCA Predict web services.</summary>
  public class PCAPredictException : Exception {
    #region Properties
    /// <summary>Gets or sets the cause of the error.</summary>
    public string Cause => Message;

    /// <summary>Gets or sets the description of the error.</summary>
    public string Description { get; private set; }

    /// <summary>Gets or sets the error code.</summary>
    public int? Error { get; private set; }

    /// <summary>Gets or sets a description of how the error can be resolved.</summary>
    public string Resolution { get; private set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the TipsTrade.PCAPredict.PCAPredictException class.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or a null reference
    /// (Nothing in Visual Basic) if no inner exception is specified.
    /// </param>
    public PCAPredictException(string message, Exception innerException = null) : this(null, null, message, null, innerException) {
    }

    /// <summary>
    /// Initializes a new instance of the TipsTrade.PCAPredict.PCAPredictException class.
    /// </summary>
    /// <param name="error">The ID of the error.</param>
    /// <param name="description">A description of the error.</param>
    /// <param name="cause">The cause of the error.</param>
    /// <param name="resolution">How the error can be resolved.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or a null reference
    /// (Nothing in Visual Basic) if no inner exception is specified.
    /// </param>
    public PCAPredictException(int? error, string description, string cause, string resolution, Exception innerException = null) : base(cause, innerException) {
      Description = description;
      Error = error;
      Resolution = resolution;
    }
    #endregion
  }
}
