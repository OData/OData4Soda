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
using Microsoft.Data.OData.Metadata;

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

        public SodaToODataConverter(IODataResponseMessage message, Uri odataEndpointUri, Uri sodaEndpointUri)
        {
            this.Message = message;
            this.ODataEndpointUri = odataEndpointUri;
            this.SodaEndpointUri = sodaEndpointUri;
        }

        public IODataResponseMessage Message { get; private set; }

        public Uri ODataEndpointUri { get; private set; }

        public Uri SodaEndpointUri { get; private set; }

        public void ConvertMetadata(Uri relativeODataUri, Uri relativeSodaUri, JsonPayload jsonPayload)
        {
            var jsonObject = jsonPayload.JsonObject;

            var meta = jsonObject.PropertyValue<JObject>("meta");
            var view = meta.PropertyValue<JObject>("view");

            var model = BuildModel(view);

            var settings = new ODataMessageWriterSettings()
            {
                Indent = true,
            };

            using (var writer = new ODataMessageWriter(this.Message, settings, model))
            {
                writer.WriteMetadataDocument();
            }
        }

        public void ConvertFeed(Uri relativeODataUri, Uri relativeSodaUri, JsonPayload jsonPayload)
        {
            var jsonObject = jsonPayload.JsonObject;

            var entries = jsonObject.PropertyValue<JArray>("entries");
            var meta = jsonObject.PropertyValue<JObject>("meta");
            var view = meta.PropertyValue<JObject>("view");

            var model = BuildModel(view);
            var entitySet = model.EntityContainers.Single().EntitySets().Single();

            var settings = new ODataMessageWriterSettings()
            {
                Indent = true,
            };

            using (var writer = new ODataMessageWriter(this.Message, settings, model))
            {
                var feedWriter = writer.CreateODataFeedWriter();

                var feed = new ODataFeed();

                feed.Id = new Uri(this.ODataEndpointUri, relativeODataUri.OriginalString).OriginalString;

                feedWriter.WriteStart(feed);
                foreach (var entry in entries.Cast<JObject>())
                {
                    var entryMetadata = new ODataEntry();
                    entryMetadata.Id = (string)((JValue)entry.Property("id").Value).Value;
                    entryMetadata.TypeName = entitySet.ElementType.FullName();

                    entryMetadata.Properties = ConvertProperties(entry);

                    entryMetadata.SetAnnotation(new AtomEntryMetadata()
                    {
                        Updated = ConvertDateTimeOffset(entry.PrimitivePropertyValue<long>("updated_at")),
                        Published = ConvertDateTimeOffset(entry.PrimitivePropertyValue<long>("created_at")),
                    });

                    feedWriter.WriteStart(entryMetadata);
                    feedWriter.WriteEnd();
                }

                feedWriter.WriteEnd();
            }
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

        private static IEdmModel BuildModel(JObject viewObject)
        {
            var model = new EdmModel();

            var name = viewObject.PrimitivePropertyValue<string>("name");
            name = name.Replace(' ', '_');

            var entityType = new EdmEntityType(false, true, null, "OData4Socrata", name, Enumerable.Empty<IEdmStructuralProperty>());
            var entitySet = new EdmEntitySet(name, entityType);

            foreach (var column in viewObject.ArrayPropertyValue<JObject>("columns"))
            {
                var sodaType = column.PrimitivePropertyValue<string>("dataTypeName");
                IEdmPrimitiveTypeReference typeReference;
                switch (sodaType)
                {
                    case "meta_data":
                        continue;

                    case "text":
                    case "url":
                        typeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(typeof(string));
                        break;

                    case "phone":
                    case "location":
                        // TODO: build a complex
                        typeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(typeof(string));
                        break;

                    default:
                        throw new Exception();
                }

                var property = new EdmStructuralProperty(entityType, column.PrimitivePropertyValue<string>("fieldName"), typeReference, null, EdmConcurrencyMode.None);
            }

            model.AddElement(entityType);

            var entityContainer = new EdmEntityContainer();
            model.AddEntityContainer(entityContainer);
            entityContainer.AddElement(entitySet);

            return model;
        }
    }
}
