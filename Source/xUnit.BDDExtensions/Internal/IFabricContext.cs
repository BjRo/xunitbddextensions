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
using StructureMap.Query;

namespace Xunit.Internal
{
    /// <summary>
    /// The context which encapsulates the state of a build operation inside of the fabric.
    /// </summary>
    public interface IFabricContext
    {
        /// <summary>
        /// Creates a new stub of the specified interface type.
        /// </summary>
        /// <param name="interfaceType">
        /// Specifies a type to create a stub for.
        /// </param>
        /// <returns>
        /// The created stub.
        /// </returns>
        object CreateStub(Type interfaceType);

        /// <summary>
        /// Resolves an instance of the specified type from the underlying IoC container.
        /// </summary>
        /// <param name="typeToResolveByContainer">
        /// The type to resolve.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        object ResolveInstanceFromContainer(Type typeToResolveByContainer);

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
        IEnumerable ResolveInstancesFromContainer(Type itemType);

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
        void InjectExistingInstanceIntoContainer(Type interfaceType, object instance);

        /// <summary>
        /// Gives full access to the underlying model of the IoC container.
        /// </summary>
        IModel ContainerModel { get; }

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
        object ResolveByFabric(Type type);

        /// <summary>
        /// Gets the type to build in the current build operation.
        /// </summary>
        Type TypeToBuild { get; }
    }
}