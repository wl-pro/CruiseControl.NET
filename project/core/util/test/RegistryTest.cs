using NUnit.Framework;
using System;
using System.IO;

namespace ThoughtWorks.CruiseControl.Core.Util.Test
{
	[TestFixture]
	public class RegistryTest : Assertion
	{
		private const string VALID_REGISTRY_PATH = @"SOFTWARE\Microsoft\Shared Tools";

		[Test]
		public void GetLocalMachineSubKeyValue()
		{
			string sharedPath = new Registry().GetLocalMachineSubKeyValue(VALID_REGISTRY_PATH, "SharedFilesDir");
			Assert("SharedFilesDir does not exist: " + sharedPath, Directory.Exists(sharedPath));
		}

		[Test]
		public void TryToGetInvalidSubKey()
		{
			AssertNull(new Registry().GetLocalMachineSubKeyValue(@"SOFTWARE\BozosSoftwareEmporium\Clowns", "Barrios"));
		}

		[Test]
		public void TryToGetInvalidSubKeyValue()
		{
			AssertNull(new Registry().GetLocalMachineSubKeyValue(VALID_REGISTRY_PATH, "Barrios"));
		}

		[Test, ExpectedException(typeof(CruiseControlException))]
		public void TryToGetExpectedInvalidSubKey()
		{
			new Registry().GetExpectedLocalMachineSubKeyValue(@"SOFTWARE\BozosSoftwareEmporium\Clowns", "Barrios");
		}

		[Test, ExpectedException(typeof(CruiseControlException))]
		public void TryToGetExpectedInvalidSubKeyValue()
		{
			new Registry().GetExpectedLocalMachineSubKeyValue(VALID_REGISTRY_PATH, "Barrios");
		}
	}
}
