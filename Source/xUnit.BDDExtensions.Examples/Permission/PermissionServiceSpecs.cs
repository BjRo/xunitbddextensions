using System;

namespace Xunit.Examples.Permission.PermissionServiceSpecs
{
	public class When_a_permission_is_demanded : InstanceContextSpecification<PermissionService>
	{
		private IUser _user;
		private IPermission _permission;

		protected override void EstablishContext()
		{
			_user = The<IUser>();
			_permission = An<IPermission>();

			_permission
				.WhenToldTo(x => x.IsGrantedTo(_user))
				.Return(true);
		}

		protected override void Because()
		{
			Sut.Demand(_permission);
		}

		[Fact]
		public void Should_evaluate_the_permission_with_the_current_user()
		{
			_permission.WasToldTo(x => x.IsGrantedTo(_user));
		}
	}
}