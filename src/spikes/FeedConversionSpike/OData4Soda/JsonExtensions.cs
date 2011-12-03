using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.OData;
using Socrata;
using Newtonsoft.Json.Linq;
using Microsoft.Data.OData.Atom;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using System.Dynamic;

namespace OData4Soda
{
    internal static class JsonExtensions
    {
        public static TValue PrimitivePropertyValue<TValue>(this JObject jObject, string propertyName)
        {
            return (TValue)jObject.PropertyValue<JValue>(propertyName).Value;
        }

        public static IEnumerable<TValue> ArrayPropertyValue<TValue>(this JObject jObject, string propertyName) where TValue : JToken
        {
            return jObject.PropertyValue<JArray>(propertyName).Children().Cast<TValue>();
        }
        
        public static TValue PropertyValue<TValue>(this JObject jObject, string propertyName) where TValue : JToken
        {
            return (TValue)jObject.Property(propertyName).Value;
        }
    }
}