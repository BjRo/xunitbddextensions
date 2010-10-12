//  Copyright 2010 xUnit.BDDExtensions
//    
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License. 
//  You may obtain a copy of the License at
//    
//        http://www.apache.org/licenses/LICENSE-2.0
//    
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
//  implied. See the License for the specific language governing permissions and
//  limitations under the License.  
//  
using System;
using System.ComponentModel;
using Moq;

namespace Xunit.Faking.Moq.FakeApiSpecs
{
    [Concern(typeof (Xunit.FakeApi))]
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

    [Concern(typeof(Xunit.FakeApi))]
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
        public void Should_throw_an__MockException__()
        {
            _theAssertion.ShouldThrowAn<MockException>();
        }
    }

    [Concern(typeof(Xunit.FakeApi))]
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
        public void Should_throw_an__MockException__()
        {
            _theAssertion.ShouldThrowAn<MockException>();
        }
    }

    [Concern(typeof(Xunit.FakeApi))]
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

    [Concern(typeof(Xunit.FakeApi))]
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
        public void Should_throw_an__MockException__()
        {
            _theAssertion.ShouldThrowAn<MockException>();
        }
    }
}