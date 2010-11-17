using System;
using Xunit.Internal;

namespace Xunit.Specs
{
	[Concern(typeof(AutoFakeContainer<>))]
	public class Given_the_target_type_has_no_public_ctor : StaticContextSpecification
	{
		private AutoFakeContainer<ClassWithoutPublicCtor> _autoFakeContainer;
		private XbxException _exception;

		protected override void EstablishContext()
		{
			_autoFakeContainer = new AutoFakeContainer<ClassWithoutPublicCtor>();
		}

		protected override void Because()
		{
			_exception = Catch.Exception<XbxException>(() => _autoFakeContainer.CreateTarget());
		}

		[Fact]
		public void Should_throw_a_RunnerException()
		{
			_exception.ShouldNotBeNull();
		}

		[Fact]
		public void Should_ask_the_user_to_double_check_the_target_types_ctors()
		{
			_exception.Message.ShouldBeEqualTo(
				"Unable to create an instance of the target type ClassWithoutPublicCtor.\r\nPlease check that the type has at least a single public constructor!");
		}
	}

	public class ClassWithoutPublicCtor
	{
		private ClassWithoutPublicCtor()
		{
		}
	}

}