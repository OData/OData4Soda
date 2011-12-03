﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.Data.OData;
using NUnit.Framework;
using Socrata;

namespace Service.Tests
{
	[TestFixture, UseReporter(typeof (DiffReporter))]
	public class SodaToODataConverterTests
	{
		private static readonly DateTimeOffset FeedUpdateTime = DateTimeOffset.Parse("2011-12-03T15:50:26-08:00");

		private class TestMessage : IODataResponseMessage
		{
			private readonly Dictionary<string, string> headers = new Dictionary<string, string>();
			public MemoryStream Stream = new MemoryStream();

			#region IODataResponseMessage Members

			public string GetHeader(string headerName)
			{
				string headerValue;
				if (!headers.TryGetValue(headerName, out headerValue))
				{
					headerValue = null;
				}

				return headerValue;
			}

			public Stream GetStream()
			{
				return Stream;
			}

			public IEnumerable<KeyValuePair<string, string>> Headers
			{
				get { return headers; }
			}

			public void SetHeader(string headerName, string headerValue)
			{
				headers[headerName] = headerValue;
			}

			public int StatusCode { get; set; }

			#endregion
		}

		[Test]
		public void FeedTest()
		{
			var testMessage = new TestMessage();

			var converter = new SodaToODataConverter(testMessage, new Uri("http://fake"), TestData.TopLevelSodaResponse);
			var payload = new JsonPayload(TestData.SodaResponseFor(TestData.TopLevelSodaResponse));
			converter.ConvertFeed(new Uri("/SomethingOData", UriKind.Relative), new Uri("/SomethingSoda", UriKind.Relative),
			                      payload, FeedUpdateTime);

			Approvals.Approve(Encoding.UTF8.GetString(testMessage.Stream.ToArray()) + "\r\n");
		}

		[Test]
		public void MetadataTest()
		{
			var testMessage = new TestMessage();

			var converter = new SodaToODataConverter(testMessage, new Uri("http://fake"), TestData.TopLevelSodaResponse);
			var payload = new JsonPayload(TestData.SodaResponseFor(TestData.TopLevelSodaResponse));
			converter.ConvertMetadata(new Uri("/SomethingOData", UriKind.Relative), new Uri("/SomethingSoda", UriKind.Relative),
			                          payload);

			Approvals.Approve(Encoding.UTF8.GetString(testMessage.Stream.ToArray()) + "\r\n");
		}
	}
}