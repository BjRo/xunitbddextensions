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
using System.Collections;
using StructureMap;
using StructureMap.Query;

namespace Xunit.Internal
{
    public class FabricContext : IFabricContext
    {
        private readonly Fabric _fabric;
        private readonly IMockingEngine _mockingEngine;
        private readonly IContainer _container;

        /// <summary>
        /// Gets the type which is build in the current operation.
        /// </summary>
        public Type TypeToBuild { get; private set; }

        /// <summary>
        /// Creates a new instance of the <see cref="FabricContext"/> class.
        /// </summary>
        /// <param name="typeToBuild">
        /// Specifies the type to build.
        /// </param>
        /// <param name="mockingEngine">
        /// Specifies the mock factory.
        /// </param>
        /// <param name="container">
        /// Specifies the container.
        /// </param>
        /// <param name="fabric">
        /// Specifies the fabric.
        /// </param>
        public FabricContext(Type typeToBuild, IMockingEngine mockingEngine, IContainer container, Fabric fabric)
        {
            Guard.AgainstArgumentNull(typeToBuild, "typeToBuild");
            Guard.AgainstArgumentNull(mockingEngine, "mockingEngine");
            Guard.AgainstArgumentNull(container, "container");

            TypeToBuild = typeToBuild;
            _mockingEngine = mockingEngine;
            _container = container;
            _fabric = fabric;
        }

        /// <summary>
        /// Resolves an instance of the specified type from the underlying IoC container.
        /// </summary>
        /// <param name="typeToResolveByContainer">
        /// The type to resolve.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        public object ResolveInstanceFromContainer(Type typeToResolveByContainer)
        {
            return _container.GetInstance(typeToResolveByContainer);
        }

        /// <summary>
        /// Resolves a collection of the itemtype specified by <paramref name="itemType"/> 
        /// from the underlying IoC container.
        /// </summary>
        /// <param name="itemType">
        /// Specifies the item type.
        /// </param>
        /// <returns>
        /// The resolved collection from the IoC container.
        /// </returns>
        public IEnumerable ResolveInstancesFromContainer(Type itemType)
        {
            var instances = _container.GetAllInstances(itemType);

            var targetArray = Array.CreateInstance(itemType, instances.Count);
            instances.CopyTo(targetArray, 0);

            return targetArray;
        }

        /// <summary>
        /// Registers the existing instance specified by <paramref name="instance"/>
        /// under the interface specified by <paramref name="interfaceType"/> at the underlying
        /// IoC type.
        /// </summary>
        /// <param name="interfaceType">
        /// Specifies the interface type.
        /// </param>
        /// <param name="instance">
        /// Specifies an existing instance.
        /// </param>
        public void InjectExistingInstanceIntoContainer(Type interfaceType, object instance)
        {
            _container.Inject(interfaceType, instance);
        }

        /// <summary>
        /// Creates a new stub of the specified interface type.
        /// </summary>
        /// <param name="interfaceType">
        /// Specifies a type to create a stub for.
        /// </param>
        /// <returns>
        /// The created stub.
        /// </returns>
        public object CreateStub(Type interfaceType)
        {
            return _mockingEngine.Stub(interfaceType);
        }

        /// <summary>
        /// Gives full access to the underlying model of the IoC container.
        /// </summary>
        public IModel ContainerModel
        {
            get { return _container.Model; }
        }

        /// <summary>
        /// Resolves the type specified by <paramref name="type"/>
        /// with the root level fabric.
        /// </summary>
        /// <param name="type">
        /// Specifies the type to resolve.
        /// </param>
        /// <returns>
        /// The resolved instance.
        /// </returns>
        public object ResolveByFabric(Type type)
        {
            return _fabric.Build(type, _container);
        }
    }
}