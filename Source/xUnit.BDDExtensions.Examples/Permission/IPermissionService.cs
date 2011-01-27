namespace Xunit.Examples.Permission
{
	public interface IPermissionService
	{
		void Demand(IPermission permission);
	}
}