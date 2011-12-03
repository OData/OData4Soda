using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace OData4Soda
{
	internal static class JsonExtensions
	{
		public static TValue PrimitivePropertyValue<TValue>(this JObject jObject, string propertyName)
		{
			return (TValue) jObject.PropertyValue<JValue>(propertyName).Value;
		}

		public static IEnumerable<TValue> ArrayPropertyValue<TValue>(this JObject jObject, string propertyName)
			where TValue : JToken
		{
			return jObject.PropertyValue<JArray>(propertyName).Children().Cast<TValue>();
		}

		public static TValue PropertyValue<TValue>(this JObject jObject, string propertyName) where TValue : JToken
		{
			return (TValue) jObject.Property(propertyName).Value;
		}
	}
}