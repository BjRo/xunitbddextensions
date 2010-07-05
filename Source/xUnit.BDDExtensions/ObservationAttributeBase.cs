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
using System.Collections.Generic;
using System.Linq;
using Xunit.Internal;
using Xunit.Sdk;

namespace Xunit
{
    /// <summary>
    /// A specialized <see cref="FactAttribute"/> for integrating the class templates
    /// used in xUnit.BDDExtensions with xUnit.net. The whole purpose of this attribute (besides readability)
    /// is to decorate the original <see cref="ITestCommand"/> recieved from xUnit.net with a <see cref="SpecificationCommand"/>
    /// from xUnit.BDDExtensions.
    /// </summary>
    public abstract class ObservationAttributeBase : FactAttribute
    {
        /// <summary>
        /// Creates all the test command for the method specified by <paramref name="method"/>.
        /// </summary>
        /// <param name="method">The method to create command for.</param>
        /// <returns>
        /// All test commands which should be executed for the particular test method.
        /// </returns>
        protected sealed override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
        {
            return base.EnumerateTestCommands(method).Select(command => new SpecificationCommand(command, method)).Cast<ITestCommand>();
        }
    }
}