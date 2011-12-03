using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.OData;
using Socrata;
using Newtonsoft.Json.Linq;
using Microsoft.Data.OData.Atom;

namespace OData4Soda
{
    public class SodaToODataConverter
    {
//        position
//sid
//updated
//created at
//created meta
//updated at
//updated meta
//id

        private static readonly string[] fieldsToIgnore = new string[] 
        { 
            "position",
            "sid",
            "id",
            "created_at",
            "created_meta",
            "updated_at",
            "updated_meta",
        };

        public SodaToODataConverter(ODataMessageWriter writer, Uri odataEndpointUri, Uri sodaEndpointUri)
        {
            this.Writer = writer;
            this.ODataEndpointUri = odataEndpointUri;
            this.SodaEndpointUri = sodaEndpointUri;
        }

        public ODataMessageWriter Writer { get; private set; }

        public Uri ODataEndpointUri { get; private set; }

        public Uri SodaEndpointUri { get; private set; }

        public void ConvertTopLevelJson(Uri relativeODataUri, Uri relativeSodaUri, JsonPayload jsonPayload)
        {
            var jsonObject = jsonPayload.JsonObject;
            var entries = (JArray)jsonObject.Property("entries").Value;

            // TODO: construct type information
            // TODO: infer which specific type this feed is

            var feedWriter = this.Writer.CreateODataFeedWriter();

            var feed = new ODataFeed();

            feed.Id = new Uri(this.ODataEndpointUri, relativeODataUri.OriginalString).OriginalString;

            feedWriter.WriteStart(feed);
            foreach (var entry in entries.Cast<JObject>())
            {
                var entryMetadata = new ODataEntry();
                entryMetadata.Id = (string)((JValue)entry.Property("id").Value).Value;

                entryMetadata.Properties = ConvertProperties(entry);

                entryMetadata.SetAnnotation(new AtomEntryMetadata()
                {
                    Updated = ConvertDateTimeOffset((long)((JValue)entry.Property("updated_at").Value).Value),
                    Published = ConvertDateTimeOffset((long)((JValue)entry.Property("created_at").Value).Value),
                });
                
                feedWriter.WriteStart(entryMetadata);
                feedWriter.WriteEnd();
            }

            feedWriter.WriteEnd();
        }

        private static DateTimeOffset ConvertDateTimeOffset(long secondsSinceEpoch)
        {
            return new DateTimeOffset(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(secondsSinceEpoch), TimeSpan.Zero);
        }

        private static IEnumerable<ODataProperty> ConvertProperties(JObject entry)
        {
            var properties = new List<ODataProperty>();
            foreach (var property in entry.Properties())
            {
                if (fieldsToIgnore.Contains(property.Name))
                {
                    continue;
                }

                object odataPropertyValue;
                var propertyValue = property.Value;
                if (propertyValue.Type == JTokenType.Object)
                {
                    // TODO: recurse
                    var complex = new ODataComplexValue();
                    complex.Properties = ConvertProperties((JObject)propertyValue);
                    odataPropertyValue = complex;
                }
                else
                {
                    odataPropertyValue = ((JValue)propertyValue).Value;
                }

                properties.Add(new ODataProperty() { Name = property.Name, Value = odataPropertyValue });
            }

            return properties;
        }
    }
}
