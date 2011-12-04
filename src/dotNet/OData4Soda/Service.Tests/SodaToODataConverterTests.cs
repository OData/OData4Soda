using System;
using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using Socrata;

namespace Service.Tests
{
	[TestFixture, UseReporter(typeof (DiffReporter))]
	public class SodaToODataConverterTests
	{
		private static readonly DateTimeOffset FeedUpdateTime = DateTimeOffset.Parse("2011-12-03T15:50:26-08:00");
		private static readonly Uri RelativeUriOData = new Uri("/SomethingOData", UriKind.Relative);
		private static readonly Uri RelativeUriSoda = new Uri("/SomethingSoda", UriKind.Relative);

		[Test]
		public void FeedTest()
		{
			var testMessage = new InMemoryResponse();

			var converter = new SodaToODataConverter(testMessage, new Uri("http://fake"), TestData.TopLevelSodaResponse);
			var payload = new JsonPayload(TestData.SodaResponseFor(TestData.TopLevelSodaResponse));
			converter.ConvertFeed(RelativeUriOData, RelativeUriSoda, payload, FeedUpdateTime);

			ApproveResponse(testMessage);
		}

		[Test]
		public void FeedTestJson()
		{
			var testMessage = new InMemoryResponse();
			testMessage.SetHeader("Content-Type", "application/json");

			var converter = new SodaToODataConverter(testMessage, new Uri("http://fake"), TestData.TopLevelSodaResponse);
			var payload = new JsonPayload(TestData.SodaResponseFor(TestData.TopLevelSodaResponse));
			converter.ConvertFeed(RelativeUriOData, RelativeUriSoda, payload, FeedUpdateTime);

			ApproveResponse(testMessage);
		}

		[Test]
		public void MetadataTest()
		{
			var testMessage = new InMemoryResponse();

			var converter = new SodaToODataConverter(testMessage, new Uri("http://fake"), TestData.TopLevelSodaResponse);
			var payload = new JsonPayload(TestData.SodaResponseFor(TestData.TopLevelSodaResponse));
			converter.ConvertMetadata(RelativeUriOData, RelativeUriSoda, payload);

			ApproveResponse(testMessage);
		}

		private static void ApproveResponse(InMemoryResponse testMessage)
		{
			Approvals.Approve(Encoding.UTF8.GetString(testMessage.Stream.ToArray()) + "\r\n");
		}
	}
}