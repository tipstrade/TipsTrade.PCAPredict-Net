using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TipsTrade.PCAPredict.Model {
  /// <summary>Represents a PCA Predict web service result container.</summary>
  public class ApiResult<T> where T : ResultBase {
    #region Properties
    /// <summary>Gets or sets the list of results.</summary>
    public List<T> Items { get; set; }

    /// <summary>Gets a flag indicating whether there is an error.</summary>
    public bool HasError {
      get {
        return (Items != null) && Items.Any(i => i.HasError);
      }
    }
    #endregion

    #region Methods
    /// <summary>Creates an PCAPredictException from the current error information.</summary>
    public PCAPredictException CreateException() {
      ResultBase item = null;
      if (Items != null) {
        item = (from i in Items where i.HasError select i).FirstOrDefault();
      }

      if (item == null) {
        throw new InvalidOperationException("No error information is available.");
      }

      return new PCAPredictException(item.Error, item.Description, item.Cause, item.Resolution);
    }
    #endregion
  }
}
