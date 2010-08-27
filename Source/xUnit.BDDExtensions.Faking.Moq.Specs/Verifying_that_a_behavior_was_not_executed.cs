using System;
using System.ComponentModel;
using Moq;

namespace Xunit.Faking.Moq.FakeApiSpecs
{
    [Concern(typeof(Xunit.FakeApi))]
    public class When_a_method_was_not_expected_to_be_called_but_it_actually_was_ :
        StaticContextSpecification
    {
        private IServiceProvider _dependency;
        private Action _theAssertion;

        protected override void EstablishContext()
        {
            _dependency = An<IServiceProvider>();
            _dependency.GetService(typeof(ISite));
        }

        protected override void Because()
        {
            _theAssertion = () => _dependency.WasNotToldTo(x => x.GetService(typeof(ISite)));
        }

        [Observation]
        public void Should_throw_an__MockException__()
        {
            _theAssertion.ShouldThrowAn<MockException>();
        }
    }

    [Concern(typeof(Xunit.FakeApi))]
    public class When_a_method_was_not_expected_to_be_called_and_it_was_not_called :
        StaticContextSpecification
    {
        private IServiceProvider _dependency;
        private Action _theAssertion;

        protected override void EstablishContext()
        {
            _dependency = An<IServiceProvider>();
        }

        protected override void Because()
        {
            _theAssertion = () => _dependency.WasNotToldTo(x => x.GetService(typeof(ISite)));
        }

        [Observation]
        public void Should_not_throw_an_exception()
        {
            _theAssertion.ShouldNotThrowAnyExceptions();
        }
    }
}