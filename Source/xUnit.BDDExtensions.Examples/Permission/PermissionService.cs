using System;

namespace Xunit.Examples.Permission
{
	public class PermissionService : IPermissionService
	{
		private readonly IUser _currentUser;

		public PermissionService(IUser currentUser)
		{
			_currentUser = currentUser;
		}

		public void Demand(IPermission permission)
		{
			if (!permission.IsGrantedTo(_currentUser))
			{
				throw new InvalidOperationException(
					"Permission is not granted");
			}
		}
	}
}