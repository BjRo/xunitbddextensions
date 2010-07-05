// Copyright 2009 Björn Rochel - http://www.bjro.de/ 
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//  
//      http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
using System;
using System.ComponentModel;
using Rhino.Mocks.Exceptions;

namespace Xunit.Specs
{
    [Concern(typeof (RhinoMocksExtensions))]
    public class When_method_was_expected_to_be_called_and_it_actually_has_been_called :
        StaticContextSpecification
    {
        private IServiceProvider _dependency;
        private Action _theAssertion;

        protected override void EstablishContext()
        {
            _dependency = An<IServiceProvider>();
            _dependency.GetService(typeof (ISite));
        }

        protected override void Because()
        {
            _theAssertion = () => _dependency.WasToldTo(x => x.GetService(typeof (ISite)));
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            _theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (RhinoMocksExtensions))]
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
            _theAssertion = () => _dependency.WasToldTo(x => x.GetService(typeof (ISite)));
        }

        [Observation]
        public void Should_throw_an__ExpectationViolationException__()
        {
            _theAssertion.ShouldThrowAn<ExpectationViolationException>();
        }
    }

    [Concern(typeof (RhinoMocksExtensions))]
    public class When_a_method_was_not_expected_to_be_called_but_it_actually_was_ :
        StaticContextSpecification
    {
        private IServiceProvider _dependency;
        private Action _theAssertion;

        protected override void EstablishContext()
        {
            _dependency = An<IServiceProvider>();
            _dependency.GetService(typeof (ISite));
        }

        protected override void Because()
        {
            _theAssertion = () => _dependency.WasNotToldTo(x => x.GetService(typeof (ISite)));
        }

        [Observation]
        public void Should_throw_an__ExpectationViolationException__()
        {
            _theAssertion.ShouldThrowAn<ExpectationViolationException>();
        }
    }

    [Concern(typeof (RhinoMocksExtensions))]
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
            _theAssertion = () => _dependency.WasNotToldTo(x => x.GetService(typeof (ISite)));
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            _theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (RhinoMocksExtensions))]
    public class When_setting_up_a_method_result_on_a_generated__Stub__ :
        StaticContextSpecification
    {
        private IServiceProvider _dependency;
        private object _methodInvokationResult;
        private object _recievedMethodResult;

        protected override void EstablishContext()
        {
            _dependency = An<IServiceProvider>();
            _methodInvokationResult = An<ISite>();
            _dependency.WhenToldTo(x => x.GetService(typeof (ISite))).Return(_methodInvokationResult);
        }

        protected override void Because()
        {
            _recievedMethodResult = _dependency.GetService(typeof (ISite));
        }

        [Observation]
        public void Should_behave_like_it_has_been_configured()
        {
            _recievedMethodResult.ShouldBeEqualTo(_methodInvokationResult);
        }
    }

    [Concern(typeof (RhinoMocksExtensions))]
    public class When_setting_up_a_property_result_on_a_generated__Stub__ :
        StaticContextSpecification
    {
        private ISite _dependency;
        private IContainer _propertyResult;
        private object _recievedPropertyResult;

        protected override void EstablishContext()
        {
            _dependency = An<ISite>();
            _propertyResult = An<IContainer>();
            _dependency.WhenToldTo(x => x.Container).Return(_propertyResult);
        }

        protected override void Because()
        {
            _recievedPropertyResult = _dependency.Container;
        }

        [Observation]
        public void Should_behave_like_it_has_been_configured()
        {
            _recievedPropertyResult.ShouldBeEqualTo(_propertyResult);
        }
    }

    [Concern(typeof (RhinoMocksExtensions))]
    public class When_configuring_an__Exception__to_be_thrown_when_an_interaction_is_executed :
            StaticContextSpecification
    {
        private IServiceProvider _dependency;
        private Action _theAssertion;

        protected override void EstablishContext()
        {
            _dependency = An<IServiceProvider>();
            _dependency.WhenToldTo(x => x.GetService(typeof (ISite))).Throw(new InvalidOperationException());
        }

        protected override void Because()
        {
            _theAssertion = () => _dependency.GetService(typeof (ISite));
        }

        [Observation]
        public void Should_throw_the_configured__Exception__()
        {
            _theAssertion.ShouldThrowAn<InvalidOperationException>();
        }
    }

    [Concern(typeof (RhinoMocksExtensions))]
    public class When_a_method_is_expected_to_be_called_only_once_and_the_method_was_called_at_least_twice :
            StaticContextSpecification
    {
        private IServiceProvider _dependency;
        private Action _theAssertion;

        protected override void EstablishContext()
        {
            _dependency = An<IServiceProvider>();
            _dependency.GetService(typeof (ISite));
            _dependency.GetService(typeof (ISite));
        }

        protected override void Because()
        {
            _theAssertion = () => _dependency.WasToldTo(x => x.GetService(typeof (ISite))).OnlyOnce();
        }

        [Observation]
        public void Should_throw_an__ExpectationViolationException__()
        {
            _theAssertion.ShouldThrowAn<ExpectationViolationException>();
        }
    }

    [Concern(typeof (RhinoMocksExtensions))]
    public class When_a_method_is_expected_to_be_called_twice_and_the_method_was_called_twice :
            StaticContextSpecification
    {
        private IServiceProvider _dependency;
        private Action _theAssertion;

        protected override void EstablishContext()
        {
            _dependency = An<IServiceProvider>();
            _dependency.GetService(typeof (ISite));
            _dependency.GetService(typeof (ISite));
        }

        protected override void Because()
        {
            _theAssertion = () => _dependency.WasToldTo(x => x.GetService(typeof (ISite))).Twice();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            _theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (RhinoMocksExtensions))]
    public class When_a_method_is_expected_to_be_called_more_often_than_it_actually_has_been_called :
            StaticContextSpecification
    {
        private IServiceProvider _dependency;
        private Action _theAssertion;

        protected override void EstablishContext()
        {
            _dependency = An<IServiceProvider>();
            _dependency.GetService(typeof (ISite));
            _dependency.GetService(typeof (ISite));
        }

        protected override void Because()
        {
            _theAssertion = () => _dependency.WasToldTo(x => x.GetService(typeof (ISite))).Times(3);
        }

        [Observation]
        public void Should_throw_an__ExpectationViolationException__()
        {
            _theAssertion.ShouldThrowAn<ExpectationViolationException>();
        }
    }
}