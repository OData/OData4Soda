using System;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using NUnit.Framework;

namespace Service.Tests
{
	[TestFixture, UseReporter(typeof (DiffReporter))]
	public class LoadFullCachedResponses
	{
		private static void AssertShouldBeCachedReferenceTo(Uri query, Uri expected)
		{
			query.Should().Be(expected);
			TestData.HaveCachedResponseFor(query).Should().BeTrue();
		}

		[Test]
		public void EnsureTheConstantToGetTheTopLevelRequestGivesTheRightUrl()
		{
			AssertShouldBeCachedReferenceTo(TestData.TopLevelSodaResponse,
			                                new Uri("http://data.cityofchicago.org/views/z8bn-74gv.json"));
		}

		[Test]
		public void LoadChicagoPoliceDepartments()
		{
			Approvals.Approve(TestData.SodaResponseFor(new Uri("http://data.cityofchicago.org/views/z8bn-74gv.json")));
		}
	}
}