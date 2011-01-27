using System.Collections.Generic;

namespace Xunit.Examples.Permission
{
	public interface IUser
	{
		string Name { get; }
		IEnumerable<string> Roles { get; }
	}
}