using System;
using System.ComponentModel;
using Rhino.Mocks.Exceptions;

namespace Xunit.Faking.RhinoMocks.FakeApiSpecs
{
    [Concern(typeof(FakeApi))]
    public class When_method_was_expected_to_be_called_and_it_actually_has_been_called :
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
            _theAssertion = () => _dependency.WasToldTo(x => x.GetService(typeof(ISite)));
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            _theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof(FakeApi))]
    public class When_method_was_expected_to_be_called_and_it_actually_has_not_been_called :
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
            _theAssertion = () => _dependency.WasToldTo(x => x.GetService(typeof(ISite)));
        }

        [Observation]
        public void Should_throw_an__ExpectationViolationException__()
        {
            _theAssertion.ShouldThrowAn<ExpectationViolationException>();
        }
    }

    [Concern(typeof(FakeApi))]
    public class When_a_method_is_expected_to_be_called_only_once_and_the_method_was_called_at_least_twice :
            StaticContextSpecification
    {
        private IServiceProvider _dependency;
        private Action _theAssertion;

        protected override void EstablishContext()
        {
            _dependency = An<IServiceProvider>();
            _dependency.GetService(typeof(ISite));
            _dependency.GetService(typeof(ISite));
        }

        protected override void Because()
        {
            _theAssertion = () => _dependency.WasToldTo(x => x.GetService(typeof(ISite))).OnlyOnce();
        }

        [Observation]
        public void Should_throw_an__ExpectationViolationException__()
        {
            _theAssertion.ShouldThrowAn<ExpectationViolationException>();
        }
    }

    [Concern(typeof(FakeApi))]
    public class When_a_method_is_expected_to_be_called_twice_and_the_method_was_called_twice :
            StaticContextSpecification
    {
        private IServiceProvider _dependency;
        private Action _theAssertion;

        protected override void EstablishContext()
        {
            _dependency = An<IServiceProvider>();
            _dependency.GetService(typeof(ISite));
            _dependency.GetService(typeof(ISite));
        }

        protected override void Because()
        {
            _theAssertion = () => _dependency.WasToldTo(x => x.GetService(typeof(ISite))).Twice();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            _theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof(FakeApi))]
    public class When_a_method_is_expected_to_be_called_more_often_than_it_actually_has_been_called :
            StaticContextSpecification
    {
        private IServiceProvider _dependency;
        private Action _theAssertion;

        protected override void EstablishContext()
        {
            _dependency = An<IServiceProvider>();
            _dependency.GetService(typeof(ISite));
            _dependency.GetService(typeof(ISite));
        }

        protected override void Because()
        {
            _theAssertion = () => _dependency.WasToldTo(x => x.GetService(typeof(ISite))).Times(3);
        }

        [Observation]
        public void Should_throw_an__ExpectationViolationException__()
        {
            _theAssertion.ShouldThrowAn<ExpectationViolationException>();
        }
    }

}