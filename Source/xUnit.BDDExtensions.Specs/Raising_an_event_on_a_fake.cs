﻿// Copyright 2009 Björn Rochel - http://www.bjro.de/ 
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

namespace Xunit.Faking.RhinoMocks.FakeApiSpecs
{
    [Concern(typeof(FakeApi))]
    public class When_using_the_bddextension_wrapper_to_raise_an_event : StaticContextSpecification
    {
        private bool _wasRaised;
        private IHaveEvent _dependency;

        protected override void EstablishContext()
        {
            _dependency = An<IHaveEvent>();
            _dependency.EventOccurred += (sender, e) => { _wasRaised = true; };
        }

        protected override void Because()
        {
            _dependency
                .Event(x => x.EventOccurred += null)
                .Raise(null, EventArgs.Empty);
        }

        [Observation]
        public void Should_fire_the_event_so_that_registered_clients_recieve_the_event_notification()
        {
            _wasRaised.ShouldBeTrue();
        }
    }

    public interface IHaveEvent
    {
        event EventHandler EventOccurred;
    }
}
