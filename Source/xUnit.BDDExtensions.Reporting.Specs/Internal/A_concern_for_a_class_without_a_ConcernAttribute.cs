using System;
using Xunit.Reporting.Internal;

namespace Xunit.Reporting.Specs.Internal
{
	[Concern(typeof(Concern))]
	public class Given_a_spec_that_is_not_marked_with_the_concern_attribute_when_generating_a_concern : StaticContextSpecification
	{
		private Concern _concern;

		protected override void Because()
		{
			_concern = Concern.BuildFrom(typeof(Dummy_Spec));
		}

		[Fact]
		public void Should_build_a_concern()
		{
			_concern.ShouldNotBeNull();
		}

		[Fact]
		public void Should_use_the_common_concern_for_uncategorized_elements_for_this()
		{
			_concern.ShouldBeAnInstanceOf<__Uncategorized__>();
		}
	}

	public class Dummy_Spec : StaticContextSpecification
	{
		protected override void Because()
		{
		}

		[Fact(Skip = "This is just a dummy spec")]
		public void Should_do_nothing()
		{
		}
	}
}