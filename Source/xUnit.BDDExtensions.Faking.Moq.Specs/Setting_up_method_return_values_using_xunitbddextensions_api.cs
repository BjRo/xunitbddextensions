using System;
using System.ComponentModel;

namespace Xunit.Faking.Moq.FakeApiSpecs
{
    [Concern(typeof(Xunit.FakeApi))]
    public class When_setting_up_a_method_result_on_a_generated__Stub__with_exact_parameters_using_xunitbddextensions_own_api  : StaticContextSpecification
    {
        private IServiceProvider _dependency;
        private object _methodInvokationResult;
        private object _recievedMethodResult;

        protected override void EstablishContext()
        {
            _dependency = An<IServiceProvider>();
            _methodInvokationResult = An<ISite>();
            _dependency.WhenToldTo(x => x.GetService(typeof(ISite))).Return(_methodInvokationResult);
        }

        protected override void Because()
        {
            _recievedMethodResult = _dependency.GetService(typeof(ISite));
        }

        [Observation]
        public void Should_return_the_configured_result_when_the_method_is_called_with_the_parameters_used_when_setting_up_the_behavior()
        {
            _recievedMethodResult.ShouldBeEqualTo(_methodInvokationResult);
        }
    }
}