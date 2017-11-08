using System.Net;
using System.Reflection;

namespace TipsTrade.PCAPredict.Model {
  #region Properties
  /// <summary>Represents a base request mode for accessing the PCA Predict web services.</summary>
  public abstract class ApiRequestBase {
    /// <summary>Gets the service endpoint for the request.</summary>
    protected abstract string ServiceEndPoint { get; }

    /// <summary>The key used to authenticate with the service.</summary>
    public string Key { get; set; }

    /// <summary>Creates a request url for the current option.</summary>
    /// <param name="key">An optional Api Key that can be used to override the current value.</param>
    public virtual string CreateRequestUrl(string key = null) {
      var url = new System.Text.StringBuilder(ServiceEndPoint);

      bool first = true;
      foreach (var prop in GetType().GetProperties( BindingFlags.Public | BindingFlags.Instance)) {
        var val = prop.GetValue(this);

        if ((prop.Name == "Key") && (val == null)) {
          // Allow the key to be overriden
          val = key;
        }

        if (val == null) {
          continue;
        }

        var urlParam = prop.GetCustomAttribute<UrlParameterNameAttribute>();

        if (first) {
          url.Append("?");
        } else {
          url.Append("&");
        }

        url.AppendFormat("{0}={1}",
          urlParam?.Name ?? prop.Name,
          WebUtility.UrlEncode($"{val}")
          );

        first = false;
      }

      return url.ToString();
    }
  }
  #endregion
}
