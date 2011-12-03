using System;
using System.IO;
using FluentAssertions;

namespace Service.Tests
{
	public static class TestData
	{
		public static readonly Uri TopLevelSodaResponse = new Uri("http://data.cityofchicago.org/views/z8bn-74gv.json");

		public static string SodaResponseFor(Uri query)
		{
			return File.ReadAllText(ToFileName(query)) + "\r\n";
		}

		private static string ToFileName(Uri query)
		{
			query.ShouldHave().Properties(u => u.IsAbsoluteUri).EqualTo(new {IsAbsoluteUri = true});
			return string.Format("cached_responses\\{0}{1}", query.Host, query.PathAndQuery.Replace("/", "__"));
		}

		public static bool HaveCachedResponseFor(Uri query)
		{
			return File.Exists(ToFileName(query));
		}
	}
}