using System;
using System.ComponentModel;

namespace Xunit.Specs
{
    [Concern(typeof(MockingExtensions))]
    public class When_setting_up_an_exception_which_should_be_thrown_when_the_method_is_called_on_a_generated__Stub__with_exact_parameters_using_xunitbddextensions_own_api  : StaticContextSpecification
    {
        private IServiceProvider _dependency;
        private Action _invocation;

        protected override void EstablishContext()
        {
            _dependency = An<IServiceProvider>();
            
            _dependency
                .WhenToldTo(x => x.GetService(typeof(ISite)))
                .Throw(new ArgumentException());
        }

        protected override void Because()
        {
            _invocation = () => _dependency.GetService(typeof(ISite));
        }

        [Observation]
        public void Should_throw_the_configured_exception_when_the_method_is_called_with_the_parameters_used_when_setting_up_the_behavior()
        {
            _invocation.ShouldThrowAn<ArgumentException>();
        }
    }
}