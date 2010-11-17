using System;
using Xunit.Internal;

namespace Xunit.Specs
{
	[Concern(typeof(AutoFakeContainer<>))]
	public class Given_the_target_constructor_throws_an_exception : StaticContextSpecification
	{
		private AutoFakeContainer<ClassThatThrows> _autoFakeContainer;
		private XbxException _exception;

		protected override void EstablishContext()
		{
			_autoFakeContainer = new AutoFakeContainer<ClassThatThrows>();
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
		public void Should_indicate_that_the_target_constructor_threw_an_exception()
		{
			_exception.Message.ShouldBeEqualTo(
				"Unable to create an instance of the target type ClassThatThrows.\r\nThe constructor threw an exception.");
		}
	}

	public class ClassThatThrows
	{
		public ClassThatThrows()
		{
			throw new NullReferenceException();
		}
	}
}