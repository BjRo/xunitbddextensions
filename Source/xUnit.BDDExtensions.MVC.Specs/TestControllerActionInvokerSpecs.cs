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
using System.Web.Mvc;

namespace Xunit.Specs
{
    [Concern(typeof (SpecificationControllerActionInvoker))]
    public class When_the_actionresult_is_invoked : InstanceContextSpecification<FakeSpecificationControllerActionInvoker>
    {
        private ActionResult _actionResult;

        protected override void EstablishContext()
        {
            _actionResult = An<ActionResult>();
       } 

        protected override void Because()
        {
            Sut.FakeInvokeActionResult(An<ControllerContext>(), _actionResult);
        }

        [Observation]
        public void Should_the_actionresult_in_the_result_property()
        {
            Sut.Result.ShouldBeTheSame(_actionResult);
        }
    }

    public class FakeSpecificationControllerActionInvoker : SpecificationControllerActionInvoker
    {
        public void FakeInvokeActionResult(ControllerContext context, ActionResult result)
        {
            InvokeActionResult(context, result);
        }
    }
}