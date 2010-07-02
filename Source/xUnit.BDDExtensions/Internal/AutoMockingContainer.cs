// Copyright 2010 Björn Rochel - http://www.bjro.de/ 
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
using StructureMap.AutoMocking;

namespace Xunit.Internal
{
    /// <summary>
    /// An auto-mocking-container is a container which tries to create stubs for every interface-dependency
    /// found in the constructor of the type specified via <typeparamref name="TTargetClass"/>.
    /// </summary>
    /// <typeparam name = "TTargetClass">The concrete class being tested</typeparam>
    public sealed class AutoMockingContainer<TTargetClass> :
        AutoMocker<TTargetClass>,
        ServiceLocator,
        IMockFactory where TTargetClass : class
    {
        private readonly Fabric _fabric = new DefaultFabric();
        private readonly IMockFactory _mockFactory;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "AutoMockingContainer{TTargetClass}" /> class.
        /// </summary>
        public AutoMockingContainer(IMockFactory mockFactory)
        {
            _mockFactory = mockFactory;
            _serviceLocator = this;
            _container = new AutoMockedContainer(this);
        }

        /// <summary>
        /// Creates a stub.
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the type of the stub. This needs to be an interface.
        /// </typeparam>
        /// <returns>
        /// The created stub instance.
        /// </returns>
        public T Stub<T>() where T : class
        {
            return _mockFactory.Stub<T>();
        }

        /// <summary>
        /// Creates a stub of the type specified via <paramref name="interfaceType"/>.
        /// </summary>
        /// <param name="interfaceType">
        /// Specifies the interface type to create a stub for.
        /// </param>
        /// <returns>
        /// The created stub instance.
        /// </returns>
        public object Stub(Type interfaceType)
        {
            return _mockFactory.Stub(interfaceType);
        }

        /// <summary>
        /// Is called by the automocking container in order to  resolve a constructor dependency
        /// of a class under test.
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the type of the constructor dependency.
        /// </typeparam>
        /// <returns>
        /// A created instance of the dependency.
        /// </returns>
        public T Service<T>() where T : class
        {
            return (T)Service(typeof(T));
        }

        /// <summary>
        /// Is called by the automocking container in order to resolve a constructor dependency
        /// of a class under test.
        /// </summary>        
        /// <returns>
        /// A created instance of the dependency.
        /// </returns>
        public object Service(Type serviceType)
        {
            return _fabric.Build(serviceType, _mockFactory, _container);
        }

        /// <summary>
        /// Creates a partial mock.
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the type of the partial mock. This needs to be 
        /// an abstract base class.
        /// </typeparam>
        /// <param name="args">
        /// Specifies the constructor parameters.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        public T PartialMock<T>(params object[] args) where T : class
        {
            return _mockFactory.PartialMock<T>(args);
        }
    }
}