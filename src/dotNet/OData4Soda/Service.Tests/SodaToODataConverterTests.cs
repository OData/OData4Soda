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

		[Test]
		public void FeedTest()
		{
			var testMessage = new InMemoryResponse();

			var converter = new SodaToODataConverter(testMessage, new Uri("http://fake"), TestData.TopLevelSodaResponse);
			var payload = new JsonPayload(TestData.SodaResponseFor(TestData.TopLevelSodaResponse));
			converter.ConvertFeed(new Uri("/SomethingOData", UriKind.Relative), new Uri("/SomethingSoda", UriKind.Relative),
			                      payload, FeedUpdateTime);

			Approvals.Approve(Encoding.UTF8.GetString(testMessage.Stream.ToArray()) + "\r\n");
		}

		[Test]
		public void FeedTestJson()
		{
			var testMessage = new InMemoryResponse();
			testMessage.SetHeader("Content-Type", "application/json");

			var converter = new SodaToODataConverter(testMessage, new Uri("http://fake"), TestData.TopLevelSodaResponse);
			var payload = new JsonPayload(TestData.SodaResponseFor(TestData.TopLevelSodaResponse));
			converter.ConvertFeed(new Uri("/SomethingOData", UriKind.Relative), new Uri("/SomethingSoda", UriKind.Relative),
			                      payload, FeedUpdateTime);

			Approvals.Approve(Encoding.UTF8.GetString(testMessage.Stream.ToArray()) + "\r\n");
		}

		[Test]
		public void MetadataTest()
		{
			var testMessage = new InMemoryResponse();

			var converter = new SodaToODataConverter(testMessage, new Uri("http://fake"), TestData.TopLevelSodaResponse);
			var payload = new JsonPayload(TestData.SodaResponseFor(TestData.TopLevelSodaResponse));
			converter.ConvertMetadata(new Uri("/SomethingOData", UriKind.Relative), new Uri("/SomethingSoda", UriKind.Relative),
			                          payload);

			Approvals.Approve(Encoding.UTF8.GetString(testMessage.Stream.ToArray()) + "\r\n");
		}
	}
}