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
using Rhino.Mocks;

namespace Xunit.Internal
{
    /// <summary>
    ///   An implementation of <see cref = "IFakeEngine" />
    ///   using Rhino.Mocks.
    /// </summary>
    internal class RhinoFakeEngine : IFakeEngine
    {
        #region IFakeEngine Members

        /// <summary>
        ///   Creates a dependency of the type specified via <paramref name = "interfaceType" />.
        /// </summary>
        /// <param name = "interfaceType">
        ///   Specifies the interface type to create a dependency for.
        /// </param>
        /// <returns>
        ///   The created dependency instance.
        /// </returns>
        public object Stub(Type interfaceType)
        {
            var stub = MockRepository.GenerateStub(interfaceType);
            stub.Replay();
            return stub;
        }

        /// <summary>
        ///   Creates a partial mock.
        /// </summary>
        /// <typeparam name = "T">
        ///   Specifies the type of the partial mock. This needs to be 
        ///   an abstract base class.
        /// </typeparam>
        /// <param name = "args">
        ///   Specifies the constructor parameters.
        /// </param>
        /// <returns>
        ///   The created instance.
        /// </returns>
        public T PartialMock<T>(params object[] args) where T : class
        {
            var mock = MockRepository.GenerateMock<T>(args);
            mock.Replay();
            return mock;
        }

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
        public IQueryOptions<TReturnValue> SetUpQueryBehaviorFor<TDependency, TReturnValue>(
            TDependency dependency,
            Expression<Func<TDependency, TReturnValue>> func) where TDependency : class
        {
            var compiledFunction = func.Compile();

            Guard.AgainstArgumentNull(compiledFunction, "compiledFunction");

            return new RhinoQueryOptions<TReturnValue>(dependency.Stub(f => compiledFunction(f)));
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
        public ICommandOptions SetUpCommandBehaviorFor<TDependency>(
            TDependency dependency,
            Expression<Action<TDependency>> func) where TDependency : class
        {
            var compiledFunction = func.Compile();

            Guard.AgainstArgumentNull(compiledFunction, "compiledFunction");

            return new RhinoCommandOptions(dependency.Stub(compiledFunction));
        }

        #endregion
    }
}