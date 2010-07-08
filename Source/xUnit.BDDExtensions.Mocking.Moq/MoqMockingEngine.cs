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
using Moq;

namespace Xunit
{
    /// <summary>
    ///   An implementation of <see cref = "IMockingEngine" />
    ///   using Rhino.Mocks.
    /// </summary>
    internal class MoqMockingEngine : IMockingEngine
    {
        #region IMockingEngine Members

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
            var closedMockType = typeof (Mock<>).MakeGenericType(interfaceType);
            var objectProperty = closedMockType.GetProperty("Object", closedMockType);
            var instance = Activator.CreateInstance(closedMockType);
            return objectProperty.GetValue(instance, null);
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
            var closedMockType = typeof (Mock<>).MakeGenericType(typeof (T));
            var callBaseProperty = closedMockType.GetProperty("CallBase", typeof (bool));
            var objectProperty = closedMockType.GetProperty("Object", typeof (T));
            var constructor = closedMockType.GetConstructor(new[]
            {
                typeof (object[])
            });
            var instance = constructor.Invoke(new[]
            {
                args
            });
            callBaseProperty.SetValue(instance, true, null);
            return objectProperty.GetValue(instance, null) as T;
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
            var mock = Mock.Get(dependency);

            return new MoqQueryOptions<TDependency, TReturnValue>(mock.Setup(func));
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
            var mock = Mock.Get(dependency);

            return new MoqCommandOptions<TDependency>(mock.Setup(func));
        }

        #endregion
    }
}