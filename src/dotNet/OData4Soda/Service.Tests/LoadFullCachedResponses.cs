﻿using System;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace Service.Tests
{
	[TestFixture, UseReporter(typeof (DiffReporter))]
	public class LoadFullCachedResponses
	{
		[Test]
		public void LoadChicagoPoliceDepartments()
		{
			Approvals.Approve(TestData.SodaResponseFor(new Uri("http://data.cityofchicago.org/views/z8bn-74gv.json")));
		}
	}
}