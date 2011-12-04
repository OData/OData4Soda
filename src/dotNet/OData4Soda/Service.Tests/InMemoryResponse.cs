using System.Collections.Generic;
using System.IO;
using Microsoft.Data.OData;

namespace Service.Tests
{
	internal class InMemoryResponse : IODataResponseMessage
	{
		public readonly MemoryStream Stream = new MemoryStream();
		private readonly Dictionary<string, string> headers = new Dictionary<string, string>();

		public IEnumerable<KeyValuePair<string, string>> Headers
		{
			get { return headers; }
		}

		public int StatusCode { get; set; }

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

		public void SetHeader(string headerName, string headerValue)
		{
			headers[headerName] = headerValue;
		}
	}
}