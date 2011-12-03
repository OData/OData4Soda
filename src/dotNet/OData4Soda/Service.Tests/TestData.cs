using System;
using System.IO;
using FluentAssertions;

namespace Service.Tests
{
	public static class TestData
	{
		public static string SodaResponseFor(Uri uri)
		{
			return File.ReadAllText(ToFileName(uri)) + "\r\n";
		}

		private static string ToFileName(Uri uri)
		{
			uri.ShouldHave().Properties(u => u.IsAbsoluteUri).EqualTo(new {IsAbsoluteUri = true});
			return string.Format("cached_responses\\{0}{1}", uri.Host, uri.PathAndQuery.Replace("/", "__"));
		}

		public static Uri TopLevelSodaResponse
		{
			get { return new Uri("http://data.cityofchicago.org/views/z8bn-74gv.json"); }
		}
	}
}