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
using System.Linq;
using Rhino.Mocks;
using StructureMap.AutoMocking;

namespace Xunit
{
    /// <summary>
    ///   Provides an "Auto Mocking Container" for the concrete class TTargetClass using Rhino.Mocks
    /// </summary>
    /// <typeparam name = "TTargetClass">The concrete class being tested</typeparam>
    /// <remarks>
    ///   Just a simple modification of the StructureMap.AutoMocking's version of RhinoAutoMocker.
    ///   Proxybehavior has been removed in order to merge all assemblies to a single xUnit.BDDExtensions dll.
    /// </remarks>
    public class RhinoAutoMocker<TTargetClass> : AutoMocker<TTargetClass>, ServiceLocator, IAutoStubber<TTargetClass>
        where TTargetClass : class
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "RhinoAutoMocker&lt;TTargetClass&gt;" /> class.
        /// </summary>
        public RhinoAutoMocker()
        {
            _serviceLocator = this;
            _container = new AutoMockedContainer(this);
        }

        #region IStubEngine Members

        public T CreateStub<T>() where T : class
        {
            return MockRepository.GenerateStub<T>();
        }

        #endregion

        #region ServiceLocator Members

        public T PartialMock<T>(params object[] args) where T : class
        {
            var mock = MockRepository.GenerateMock<T>(args);
            mock.Replay();
            return mock;
        }

        public T Service<T>() where T : class
        {
            return (T) Service(typeof (T));
        }

        public object Service(Type serviceType)
        {
            if (serviceType.IsClosingIEnumerable())
            {
                var itemType = serviceType.GetGenericArguments().First();

                if (_container.Model.HasImplementationsFor(itemType))
                {
                    return _container.GetInstances(itemType);
                }

                if (itemType.IsInterface)
                {
                    var targetArray = this.CreateItemCollection(itemType);

                    foreach (var item in targetArray)
                    {
                        _container.Inject(itemType, item);
                    }

                    return targetArray;
                }
            }

            var service = MockRepository.GenerateStub(serviceType);
            service.Replay();
            return service;
        }

        #endregion

        public TTargetClass BuildInstance()
        {
            return ClassUnderTest;
        }

        public T GetSingleton<T>() where T : class
        {
            return Service<T>();
        }

        public object GetSingleton(Type serviceType)
        {
            return Service(serviceType);
        }
    }
}