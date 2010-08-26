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
using System.Linq.Expressions;
using Xunit.Internal;

namespace Xunit
{
    /// <summary>
    ///   A set of extension methods for setting up behavior on stubs in a fashion idependant 
    ///   to a particular mocking framework.
    /// </summary>
    public static class MockingExtensions
    {
        /// <summary>
        ///   Configures the behavior of the dependency specified by <paramref name = "dependency" />.
        /// </summary>
        /// <typeparam name = "TDependency">
        ///   Specifies the type of the dependency.
        /// </typeparam>
        /// <typeparam name = "TReturnValue">
        ///   Specifies the type of the return value.
        /// </typeparam>
        /// <param name = "dependency">
        ///   The dependency to configure behavior on.
        /// </param>
        /// <param name = "func">
        ///   Configures the behavior. This must be a void method.
        /// </param>
        /// <returns>
        ///   A <see cref = "IQueryOptions{TReturn}" /> for further configuration.
        /// </returns>
        public static IQueryOptions<TReturnValue> WhenToldTo<TDependency, TReturnValue>(
            this TDependency dependency,
            Expression<Func<TDependency, TReturnValue>> func) where TDependency : class
        {
            Guard.AgainstArgumentNull(dependency, "dependency");
            Guard.AgainstArgumentNull(func, "func");

            return RunnerConfiguration.FakeEngine.SetUpQueryBehaviorFor(dependency, func);
        }

        /// <summary>
        ///   Configures the behavior of the dependency specified by <paramref name = "dependency" />.
        /// </summary>
        /// <typeparam name = "TDependency">
        ///   Specifies the type of the dependency.
        /// </typeparam>
        /// <param name = "dependency">
        ///   The dependency to configure behavior on.
        /// </param>
        /// <param name = "func">
        ///   Configures the behavior. This must be a void method.
        /// </param>
        /// <returns>
        ///   A <see cref = "ICommandOptions" /> for further configuration.
        /// </returns>
        /// <remarks>
        ///   This method is used for command, e.g. methods returning void.
        /// </remarks>
        public static ICommandOptions WhenToldTo<TDependency>(
            this TDependency dependency,
            Expression<Action<TDependency>> func) where TDependency : class
        {
            Guard.AgainstArgumentNull(dependency, "dependency");
            Guard.AgainstArgumentNull(func, "func");

            return RunnerConfiguration.FakeEngine.SetUpCommandBehaviorFor(dependency, func);
        }
    }
}