using System;

namespace Xunit.Examples.Permission
{
	public class When_executing_the_test_command : InstanceContextSpecification<TestCommand>
	{
		private InvalidOperationException _exception;

		protected override void EstablishContext()
		{
			With<AllPermissionsDenied>();
			With<AllPermissionsDenied>();
			With<AllPermissionsDenied>();
			With<AllPermissionsDenied>();
		}

		protected override void Because()
		{
			_exception = Catch.Exception<InvalidOperationException>(() => Sut.Execute());
		}

		[Fact]
		public void Should_throw_an_InvalidOperationException()
		{
			_exception.ShouldNotBeNull();			
		}
	}
}