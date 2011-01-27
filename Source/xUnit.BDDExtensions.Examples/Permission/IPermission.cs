namespace Xunit.Examples.Permission
{
	public interface IPermission
	{
		bool IsGrantedTo(IUser currentUser);
	}
}