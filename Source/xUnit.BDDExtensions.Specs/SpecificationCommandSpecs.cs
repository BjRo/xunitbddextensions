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
using Xunit.Sdk;

namespace Xunit.Specs
{
    [Concern(typeof (SpecificationCommand))]
    public class When_a_specification_test_command_is_executed_and_the_test_object_does_not_implement_ISpecification : Specification_for_SpecificationCommand
    {
        private Action invokation;
        private object objectNotImplementingITestSpecification;

        protected override void EstablishContext()
        {
            objectNotImplementingITestSpecification = new object();
        }

        protected override void Because()
        {
            invokation = () => Sut.Execute(objectNotImplementingITestSpecification);
        }

        [Observation]
        public void Should_throw_an_invalid_operation_exception()
        {
            invokation.ShouldThrowAn<InvalidOperationException>();
        }
    }

    [Concern(typeof (SpecificationCommand))]
    public class When_a_specification_test_command_is_executed_on_a_ITestSpecification_implementer : Specification_for_SpecificationCommand
    {
        private MethodResult expectedTestResult;
        private ITestCommand innerCommand;
        private IMethodInfo methodInfo;
        private MethodResult testResult;
        private ISpecification _specification;

        protected override void EstablishContext()
        {
            innerCommand = The<ITestCommand>();
            methodInfo = The<IMethodInfo>();
            _specification = An<ISpecification>();
            expectedTestResult = CreateMethodResult();

            innerCommand.WhenToldTo(x => x.Execute(_specification)).Return(expectedTestResult);
        }

        protected override void Because()
        {
            testResult = Sut.Execute(_specification);
        }

        [Observation]
        public void Should_ask_the_test_specification_to_initialize()
        {
            _specification.WasToldTo(x => x.Initialize());
        }

        [Observation]
        public void Should_pass_the_test_specification_to_the_inner_test_command()
        {
            innerCommand.WasToldTo(x => x.Execute(_specification));
        }

        [Observation]
        public void Should_return_the_test_result_from_the_inner_test_command()
        {
            testResult.ShouldBeEqualTo(expectedTestResult);
        }

        [Observation]
        public void Should_ask_the_test_specification_to_cleanup()
        {
            _specification.WasToldTo(x => x.Cleanup());
        }
    }

    public abstract class Specification_for_SpecificationCommand : InstanceContextSpecification<SpecificationCommand>
    {
        protected static MethodResult CreateMethodResult()
        {
            return new PassedResult(new MethodInfoDummy(), "bar");            
        }
    }
}