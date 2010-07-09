// Copyright 2010 xUnit.BDDExtensions
//   
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   
//       http://www.apache.org/licenses/LICENSE-2.0
//   
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//  
using System;
using System.Web.Mvc;

namespace Xunit.Specs
{
// ReSharper disable Asp.NotResolved
    public class TestControllerContextController : Controller
    {
        public bool IndexActionCalled;
            public ActionResult Index()
            {
                IndexActionCalled = true;
                return View();
            }

            [HttpPost]
            public ActionResult Index(int a)
            {
                ParameterIndexA = a;
                return View();
            }

        public int ParameterIndexA;
    }
// ReSharper restore Asp.NotResolved

    [Concern(typeof (ControllerContextSpecification<>))]
    public class When_invoking_the_index_action_of_a_controller : ControllerContextSpecification<TestControllerContextController>
    {
        
        protected override void Because()
        {
            InvokeAction(c => c.Index());
        }

        [Observation]
        public void Should_the_index_action_called()
        {
            Sut.IndexActionCalled.ShouldBeTrue();
        }
    }


    [Concern(typeof(ControllerContextSpecification<>))]
    public class When_invoking_the_index_action_with_parameter_and_post : ControllerContextSpecification<TestControllerContextController>
    {

        protected override void Because()
        {
            InvokePostAction(c => c.Index(815));
        }

        [Observation]
        public void Should_the_given_parameter_815()
        {
            Sut.ParameterIndexA.ShouldBeEqualTo(815);
        }
    }

}