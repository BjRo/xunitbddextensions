using System;
using Rhino.Mocks;

namespace Xunit.Examples.Permission
{
	public class AllPermissionsDenied : BehaviorConfigBase
	{
		public override void EstablishContext(IFakeAccessor accessor)
		{
			var permissionService = accessor.The<IPermissionService>();

			permissionService
				.WhenToldTo(x => x.Demand(Arg<IPermission>.Is.Anything))
				.Throw(new InvalidOperationException());
		}
	}
}