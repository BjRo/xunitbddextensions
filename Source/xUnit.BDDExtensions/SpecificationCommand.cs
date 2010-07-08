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
using Xunit.Sdk;

namespace Xunit
{
    /// <summary>
    ///   A <see cref = "ITestCommand" /> decorator which invokes methods on the specification class before and after the real test method executes.
    ///   This requires the specification class to implement <see cref = "ISpecification" />.
    /// </summary>
    public class SpecificationCommand : TestCommand
    {
        private readonly ITestCommand _innerCommand;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "SpecificationCommand" /> class.
        /// </summary>
        /// <param name = "innerCommand">The inner command.</param>
        /// <param name = "testMethod">The test method.</param>
        public SpecificationCommand(ITestCommand innerCommand, IMethodInfo testMethod)
            : base(testMethod, testMethod.Name, 0)
        {
            _innerCommand = innerCommand;
        }

        /// <summary>
        ///   Executes the specified test class.
        /// </summary>
        /// <param name = "testClass">The test class.</param>
        /// <returns>
        ///   The method result of the xUnit.net test execution.
        /// </returns>
        public override MethodResult Execute(object testClass)
        {
            var specification = testClass as ISpecification;

            try
            {
                if (specification == null)
                {
                    throw new InvalidOperationException("Instance doesn't implement ISpecification");
                }

                specification.Initialize();
                return _innerCommand.Execute(testClass);
            }
            finally
            {
                if (specification != null)
                {
                    specification.Cleanup();
                }
            }
        }
    }
}