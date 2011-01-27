using System.Linq;

namespace Xunit.Examples.Permission
{
	public class TestPermission : IPermission
	{
		public bool IsGrantedTo(IUser currentUser)
		{
			return currentUser.Roles.Contains("Admin");
		}
	}
}