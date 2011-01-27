using System;

namespace Xunit.Examples.Permission
{
	public class TestCommand
	{
		private readonly IPermissionService _permissionService;

		public TestCommand(IPermissionService permissionService)
		{
			_permissionService = permissionService;
		}

		public void Execute()
		{
			_permissionService.Demand(new TestPermission());

			//Further stuff.....
		}
	}
}