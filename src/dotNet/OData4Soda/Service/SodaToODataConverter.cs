using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData;
using Microsoft.Data.OData.Atom;
using Microsoft.Data.OData.Metadata;
using Newtonsoft.Json.Linq;
using Socrata;

namespace Service
{
	public class SodaToODataConverter
	{
		public SodaToODataConverter(IODataResponseMessage message, Uri odataEndpointUri, Uri sodaEndpointUri)
		{
			Message = message;
			ODataEndpointUri = odataEndpointUri;
			SodaEndpointUri = sodaEndpointUri;
		}

		public IODataResponseMessage Message { get; private set; }

		public Uri ODataEndpointUri { get; private set; }

		public Uri SodaEndpointUri { get; private set; }

		public void ConvertMetadata(Uri relativeODataUri, Uri relativeSodaUri, JsonPayload jsonPayload)
		{
			var jsonObject = jsonPayload.JsonObject;

			var meta = jsonObject.PropertyValue<JObject>("meta");
			var view = meta.PropertyValue<JObject>("view");

			IList<string> fieldsToIgnore;
			var model = BuildModel(view, out fieldsToIgnore);

			var settings = new ODataMessageWriterSettings
			               	{
			               		Indent = true,
			               	};

			using (var writer = new ODataMessageWriter(Message, settings, model))
			{
				writer.WriteMetadataDocument();
			}
		}

		public void ConvertFeed(Uri relativeODataUri, Uri relativeSodaUri, JsonPayload jsonPayload,
		                        DateTimeOffset feedUpdateTime)
		{
			var jsonObject = jsonPayload.JsonObject;

			var entries = jsonObject.PropertyValue<JArray>("entries");
			var meta = jsonObject.PropertyValue<JObject>("meta");
			var view = meta.PropertyValue<JObject>("view");

			IList<string> fieldsToIgnore;
			var model = BuildModel(view, out fieldsToIgnore);

			var entitySet = model.EntityContainers.Single().EntitySets().Single();

			var settings = new ODataMessageWriterSettings
			               	{
			               		Indent = true,
			               	};

			using (var writer = new ODataMessageWriter(Message, settings, model))
			{
				var feedWriter = writer.CreateODataFeedWriter();

				var feed = new ODataFeed();

				feed.SetAnnotation(new AtomFeedMetadata
				                   	{
				                   		Updated = feedUpdateTime,
				                   	});

				feed.Id = new Uri(ODataEndpointUri, relativeODataUri.OriginalString).OriginalString;

				feedWriter.WriteStart(feed);
				foreach (var entry in entries.Cast<JObject>())
				{
					var entryMetadata = new ODataEntry();
					entryMetadata.Id = (string) ((JValue) entry.Property("id").Value).Value;
					entryMetadata.TypeName = entitySet.ElementType.FullName();

					entryMetadata.Properties = ConvertProperties(entry, fieldsToIgnore);

					entryMetadata.SetAnnotation(new AtomEntryMetadata
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
			return new DateTimeOffset(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(secondsSinceEpoch),
			                          TimeSpan.Zero);
		}

		private static IEnumerable<ODataProperty> ConvertProperties(JObject entry, IEnumerable<string> fieldsToIgnore)
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
					complex.Properties = ConvertProperties((JObject) propertyValue, fieldsToIgnore);
					odataPropertyValue = complex;
				}
				else
				{
					odataPropertyValue = ((JValue) propertyValue).Value;
				}

				properties.Add(new ODataProperty {Name = property.Name, Value = odataPropertyValue});
			}

			return properties;
		}

		private static IEdmModel BuildModel(JObject viewObject, out IList<string> fieldsToIgnore)
		{
			var model = new EdmModel();
			fieldsToIgnore = new List<string>();

			var name = viewObject.PrimitivePropertyValue<string>("name");
			name = name.Replace(' ', '_');

			var entityType = new EdmEntityType(false, false, null, "OData4Socrata", name,
			                                   Enumerable.Empty<IEdmStructuralProperty>());
			var entitySet = new EdmEntitySet(name, entityType);

			EdmComplexType phoneComplex = null;
			EdmComplexType locationComplex = null;
			EdmComplexType urlComplex = null;

			foreach (var column in viewObject.ArrayPropertyValue<JObject>("columns"))
			{
				var fieldName = column.PrimitivePropertyValue<string>("name");

				var sodaType = column.PrimitivePropertyValue<string>("dataTypeName");
				IEdmTypeReference typeReference;
				switch (sodaType)
				{
					case "meta_data":
						fieldsToIgnore.Add(fieldName);
						continue;

					case "text":

						typeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(typeof (string));
						break;

					case "url":
						if (urlComplex == null)
						{
							urlComplex = new EdmComplexType(false, false, null, entityType.Namespace, "url");
							new EdmStructuralProperty(urlComplex, "url", EdmLibraryExtensions.GetPrimitiveTypeReference(typeof (string)),
							                          null, EdmConcurrencyMode.None);
							model.AddElement(urlComplex);
						}

						typeReference = new EdmComplexTypeReference(urlComplex, false);
						break;

					case "phone":
						if (phoneComplex == null)
						{
							phoneComplex = new EdmComplexType(false, false, null, entityType.Namespace, "phone");
							new EdmStructuralProperty(phoneComplex, "phone_number",
							                          EdmLibraryExtensions.GetPrimitiveTypeReference(typeof (string)), null,
							                          EdmConcurrencyMode.None);
							new EdmStructuralProperty(phoneComplex, "phone_type",
							                          EdmLibraryExtensions.GetPrimitiveTypeReference(typeof (string)), null,
							                          EdmConcurrencyMode.None);
							model.AddElement(phoneComplex);
						}

						typeReference = new EdmComplexTypeReference(phoneComplex, false);
						break;

					case "location":
						if (locationComplex == null)
						{
							locationComplex = new EdmComplexType(false, false, null, entityType.Namespace, "location");
							new EdmStructuralProperty(locationComplex, "human_address",
							                          EdmLibraryExtensions.GetPrimitiveTypeReference(typeof (string)), null,
							                          EdmConcurrencyMode.None);
							new EdmStructuralProperty(locationComplex, "latitude",
							                          EdmLibraryExtensions.GetPrimitiveTypeReference(typeof (string)), null,
							                          EdmConcurrencyMode.None);
							new EdmStructuralProperty(locationComplex, "longitude",
							                          EdmLibraryExtensions.GetPrimitiveTypeReference(typeof (string)), null,
							                          EdmConcurrencyMode.None);
							new EdmStructuralProperty(locationComplex, "machine_address",
							                          EdmLibraryExtensions.GetPrimitiveTypeReference(typeof (string)), null,
							                          EdmConcurrencyMode.None);
							new EdmStructuralProperty(locationComplex, "needs_recoding",
							                          EdmLibraryExtensions.GetPrimitiveTypeReference(typeof (bool)), null,
							                          EdmConcurrencyMode.None);
							model.AddElement(locationComplex);
						}

						typeReference = new EdmComplexTypeReference(locationComplex, false);
						break;

					default:
						throw new Exception();
				}

				new EdmStructuralProperty(entityType, fieldName, typeReference, null, EdmConcurrencyMode.None);
			}

			model.AddElement(entityType);

			var entityContainer = new EdmEntityContainer();
			model.AddEntityContainer(entityContainer);
			entityContainer.AddElement(entitySet);

			return model;
		}
	}
}