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
        private IServiceProvider dependency;
        private Action theAssertion;

        protected override void EstablishContext()
        {
            dependency = An<IServiceProvider>();
            dependency.GetService(typeof (ISite));
        }

        protected override void Because()
        {
            theAssertion = () => dependency.WasToldTo(x => x.GetService(typeof (ISite)));
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (RhinoMocksExtensions))]
    public class When_method_was_expected_to_be_called_and_it_actually_has_not_been_called :
        StaticContextSpecification
    {
        private IServiceProvider dependency;
        private Action theAssertion;

        protected override void EstablishContext()
        {
            dependency = An<IServiceProvider>();
        }

        protected override void Because()
        {
            theAssertion = () => dependency.WasToldTo(x => x.GetService(typeof (ISite)));
        }

        [Observation]
        public void Should_throw_an__ExpectationViolationException__()
        {
            theAssertion.ShouldThrowAn<ExpectationViolationException>();
        }
    }

    [Concern(typeof (RhinoMocksExtensions))]
    public class When_a_method_was_not_expected_to_be_called_but_it_actually_was_ :
        StaticContextSpecification
    {
        private IServiceProvider dependency;
        private Action theAssertion;

        protected override void EstablishContext()
        {
            dependency = An<IServiceProvider>();
            dependency.GetService(typeof (ISite));
        }

        protected override void Because()
        {
            theAssertion = () => dependency.WasNotToldTo(x => x.GetService(typeof (ISite)));
        }

        [Observation]
        public void Should_throw_an__ExpectationViolationException__()
        {
            theAssertion.ShouldThrowAn<ExpectationViolationException>();
        }
    }

    [Concern(typeof (RhinoMocksExtensions))]
    public class When_a_method_was_not_expected_to_be_called_and_it_was_not_called :
        StaticContextSpecification
    {
        private IServiceProvider dependency;
        private Action theAssertion;

        protected override void EstablishContext()
        {
            dependency = An<IServiceProvider>();
        }

        protected override void Because()
        {
            theAssertion = () => dependency.WasNotToldTo(x => x.GetService(typeof (ISite)));
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (RhinoMocksExtensions))]
    public class When_setting_up_a_method_result_on_a_generated__Stub__ :
        StaticContextSpecification
    {
        private IServiceProvider dependency;
        private object methodInvokationResult;
        private object recievedMethodResult;

        protected override void EstablishContext()
        {
            dependency = An<IServiceProvider>();
            methodInvokationResult = An<ISite>();
            dependency.WhenToldTo(x => x.GetService(typeof (ISite))).Return(methodInvokationResult);
        }

        protected override void Because()
        {
            recievedMethodResult = dependency.GetService(typeof (ISite));
        }

        [Observation]
        public void Should_behave_like_it_has_been_configured()
        {
            recievedMethodResult.ShouldBeEqualTo(methodInvokationResult);
        }
    }

    [Concern(typeof (RhinoMocksExtensions))]
    public class When_setting_up_a_property_result_on_a_generated__Stub__ :
        StaticContextSpecification
    {
        private ISite dependency;
        private IContainer propertyResult;
        private object recievedPropertyResult;

        protected override void EstablishContext()
        {
            dependency = An<ISite>();
            propertyResult = An<IContainer>();
            dependency.WhenToldTo(x => x.Container).Return(propertyResult);
        }

        protected override void Because()
        {
            recievedPropertyResult = dependency.Container;
        }

        [Observation]
        public void Should_behave_like_it_has_been_configured()
        {
            recievedPropertyResult.ShouldBeEqualTo(propertyResult);
        }
    }

    [Concern(typeof (RhinoMocksExtensions))]
    public class When_configuring_an__Exception__to_be_thrown_when_an_interaction_is_executed :
            StaticContextSpecification
    {
        private IServiceProvider dependency;
        private Action theAssertion;

        protected override void EstablishContext()
        {
            dependency = An<IServiceProvider>();
            dependency.WhenToldTo(x => x.GetService(typeof (ISite))).Throw(new InvalidOperationException());
        }

        protected override void Because()
        {
            theAssertion = () => dependency.GetService(typeof (ISite));
        }

        [Observation]
        public void Should_throw_the_configured__Exception__()
        {
            theAssertion.ShouldThrowAn<InvalidOperationException>();
        }
    }

    [Concern(typeof (RhinoMocksExtensions))]
    public class When_a_method_is_expected_to_be_called_only_once_and_the_method_was_called_at_least_twice :
            StaticContextSpecification
    {
        private IServiceProvider dependency;
        private Action theAssertion;

        protected override void EstablishContext()
        {
            dependency = An<IServiceProvider>();
            dependency.GetService(typeof (ISite));
            dependency.GetService(typeof (ISite));
        }

        protected override void Because()
        {
            theAssertion = () => dependency.WasToldTo(x => x.GetService(typeof (ISite))).OnlyOnce();
        }

        [Observation]
        public void Should_throw_an__ExpectationViolationException__()
        {
            theAssertion.ShouldThrowAn<ExpectationViolationException>();
        }
    }

    [Concern(typeof (RhinoMocksExtensions))]
    public class When_a_method_is_expected_to_be_called_twice_and_the_method_was_called_twice :
            StaticContextSpecification
    {
        private IServiceProvider dependency;
        private Action theAssertion;

        protected override void EstablishContext()
        {
            dependency = An<IServiceProvider>();
            dependency.GetService(typeof (ISite));
            dependency.GetService(typeof (ISite));
        }

        protected override void Because()
        {
            theAssertion = () => dependency.WasToldTo(x => x.GetService(typeof (ISite))).Twice();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (RhinoMocksExtensions))]
    public class When_a_method_is_expected_to_be_called_more_often_than_it_actually_has_been_called :
            StaticContextSpecification
    {
        private IServiceProvider dependency;
        private Action theAssertion;

        protected override void EstablishContext()
        {
            dependency = An<IServiceProvider>();
            dependency.GetService(typeof (ISite));
            dependency.GetService(typeof (ISite));
        }

        protected override void Because()
        {
            theAssertion = () => dependency.WasToldTo(x => x.GetService(typeof (ISite))).Times(3);
        }

        [Observation]
        public void Should_throw_an__ExpectationViolationException__()
        {
            theAssertion.ShouldThrowAn<ExpectationViolationException>();
        }
    }
}