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
using System.Collections.Generic;
using System.Linq.Expressions;
using StructureMap.AutoMocking;

namespace Xunit.Internal
{
    /// <summary>
    ///   An auto-mocking-container is a container which tries to create stubs for every interface-dependency
    ///   found in the constructor of the type specified via <typeparamref name = "TTargetClass" />.
    /// </summary>
    /// <typeparam name = "TTargetClass">The concrete class being tested</typeparam>
    public sealed class AutoFakeContainer<TTargetClass> :
        AutoMocker<TTargetClass>,
        ServiceLocator,
        IFakeEngine where TTargetClass : class
    {
        private readonly IFabric _fabric;
        private readonly IFakeEngine _fakeEngine;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "AutoFakeContainer{TTargetClass}" /> class.
        /// </summary>
        public AutoFakeContainer() : this(RunnerConfiguration.Instance)
        {
        }

        public AutoFakeContainer(IRunnerConfiguration runnerConfiguration)
        {
            _fabric = new Fabric(runnerConfiguration.FakeEngine, runnerConfiguration.Builders, runnerConfiguration.ConfigurationRules);
            _fakeEngine = runnerConfiguration.FakeEngine;
            _serviceLocator = this;
            _container = new AutoMockedContainer(this);
        }

        #region IFakeEngine Members

        /// <summary>
        ///   Creates a stub of the type specified via <paramref name = "interfaceType" />.
        /// </summary>
        /// <param name = "interfaceType">
        ///   Specifies the interface type to create a stub for.
        /// </param>
        /// <returns>
        ///   The created stub instance.
        /// </returns>
        public object Stub(Type interfaceType)
        {
            return _fakeEngine.Stub(interfaceType);
        }

        /// <summary>
        ///   Configures the behavior of the dependency specified by <paramref name = "dependency" />.
        /// </summary>
        /// <typeparam name = "TDependency">Specifies the type of the dependency.</typeparam>
        /// <typeparam name = "TReturnValue">Specifies the type of the return value.</typeparam>
        /// <param name = "dependency">The dependency to configure behavior on.</param>
        /// <param name = "func">Configures the behavior. This must be a void method.</param>
        /// <returns>
        ///   A <see cref = "IQueryOptions{TReturn}" /> for further configuration.
        /// </returns>
        public IQueryOptions<TReturnValue> SetUpQueryBehaviorFor<TDependency, TReturnValue>(
            TDependency dependency,
            Expression<Func<TDependency, TReturnValue>> func) where TDependency : class
        {
            return _fakeEngine.SetUpQueryBehaviorFor(dependency, func);
        }

        /// <summary>
        ///   Configures the behavior of the dependency specified by <paramref name = "dependency" />.
        /// </summary>
        /// <typeparam name = "TDependency">Specifies the type of the dependency.</typeparam>
        /// <param name = "dependency">The dependency to configure behavior on.</param>
        /// <param name = "func">Configures the behavior. This must be a void method.</param>
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
            return _fakeEngine.SetUpCommandBehaviorFor(dependency, func);
        }

        #endregion

        #region ServiceLocator Members

        /// <summary>
        ///   Is called by the automocking container in order to  resolve a constructor dependency
        ///   of a class under test.
        /// </summary>
        /// <typeparam name = "T">
        ///   Specifies the type of the constructor dependency.
        /// </typeparam>
        /// <returns>
        ///   A created instance of the dependency.
        /// </returns>
        public T Service<T>() where T : class
        {
            return (T)Service(typeof(T));
        }

        /// <summary>
        ///   Is called by the automocking container in order to resolve a constructor dependency
        ///   of a class under test.
        /// </summary>
        /// <returns>
        ///   A created instance of the dependency.
        /// </returns>
        public object Service(Type serviceType)
        {
            return _fabric.Build(serviceType, _container);
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
            return _fakeEngine.PartialMock<T>(args);
        }

        #endregion

        /// <summary>
        ///   Creates a stub.
        /// </summary>
        /// <typeparam name = "T">
        ///   Specifies the type of the stub. This needs to be an interface.
        /// </typeparam>
        /// <returns>
        ///   The created stub instance.
        /// </returns>
        public T Stub<T>() where T : class
        {
            return _fakeEngine.Stub<T>();
        }
    }
}