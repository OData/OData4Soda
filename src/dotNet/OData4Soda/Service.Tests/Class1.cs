using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace Service.Tests
{
	[TestFixture, UseReporter(typeof(DiffReporter))]
	public class Class1
	{
		[Test]
		public void AlwaysPasses()
		{
		}

		[Test]
		public void ConstantlySeekingApproval()
		{
		}
	}
}