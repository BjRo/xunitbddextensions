namespace Xunit.Specs
{
    [Concern(typeof(MockingExtensions))]
    public class When_setting_up_a_property_result_on_a_generated__Stub__with_exact_parameters_using_xunitbddextensions_own_api  : StaticContextSpecification
    {
        private IDependencyWithProperty _dependency;
        private object _recievedMethodResult;

        protected override void EstablishContext()
        {
            _dependency = An<IDependencyWithProperty>();
            _dependency.WhenToldTo(x => x.DummyProperty).Return("I want this result!");
        }

        protected override void Because()
        {
            _recievedMethodResult = _dependency.DummyProperty;
        }

        [Observation]
        public void Should_return_the_configured_result_when_the_property_is_read()
        {
            _recievedMethodResult.ShouldBeEqualTo("I want this result!");
        }
    }

    public interface IDependencyWithProperty
    {
        string DummyProperty { get; set; }
    }
}