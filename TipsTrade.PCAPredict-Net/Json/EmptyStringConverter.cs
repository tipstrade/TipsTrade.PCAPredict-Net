using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace TipsTrade.PCAPredict.Json {
  /// <summary>Converts an empty string to and from JSON.</summary>
  public class EmptyStringConverter : JsonConverter {
    /// <summary>Determines whether this instance can convert the specified object type.</summary>
    public override bool CanConvert(Type objectType) {
      return objectType == typeof(string);
    }

    /// <summary>The Newtonsoft.Json.JsonReader to read from.</summary>
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
      string value;
      switch (reader.TokenType) {
        case JsonToken.String:
          value = (string)reader.Value;
          break;

        case JsonToken.Boolean:
        case JsonToken.Date:
        case JsonToken.Float:
        case JsonToken.Integer:
          value = $"{reader.Value}";
          break;

        default:
          throw new JsonException("value cannot be cast to a string.");
      }


      if (value == "") {
        return null;
      } else {
        return value;
      }
    }

    /// <summary>Writes the JSON representation of the object.</summary>
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
      JToken.FromObject(writer).WriteTo(writer);
    }
  }
}
