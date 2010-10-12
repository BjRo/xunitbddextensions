using System;
using System.ComponentModel;
using Rhino.Mocks;

namespace Xunit.Faking.RhinoMocks.FakeApiSpecs
{
    [Concern(typeof(FakeApi))]
    public class When_setting_up_a_method_result_on_a_generated__Stub__partially_using_RhinoMocks_argument_matchers  : StaticContextSpecification
    {
        private IServiceProvider _dependency;
        private object _methodInvokationResult;
        private object _recievedMethodResult;

        protected override void EstablishContext()
        {
            _dependency = An<IServiceProvider>();
            _methodInvokationResult = An<ISite>();
            _dependency.WhenToldTo(x => x.GetService(Arg<Type>.Is.Anything)).Return(_methodInvokationResult);
        }

        protected override void Because()
        {
            _recievedMethodResult = _dependency.GetService(typeof(ISite));
        }

        [Observation]
        public void Should_return_the_configured_result_when_the_method_is_called()
        {
            _recievedMethodResult.ShouldBeEqualTo(_methodInvokationResult);
        }
    }
}