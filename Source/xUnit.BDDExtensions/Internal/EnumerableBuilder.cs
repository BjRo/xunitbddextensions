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
using System.Collections.Generic;
using System.Linq;

namespace Xunit.Internal
{
    public class EnumerableBuilder : IBuilder
    {
        /// <summary>
        /// Determines whether the builder can build the supplied type.
        /// </summary>
        /// <param name="type">
        /// Specifies the type to check.
        /// </param>
        /// <returns>
        /// Returns <c>true</c> if the type can be build; Otherwise <c>false</c>.
        /// </returns>
        public bool KnowsHowToBuild(Type type)
        {
            return type.IsInterface &&
                   type.IsGenericType &&
                   type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        /// <summary>
        /// Builds an instance from/with the data contained in the supplied build context.
        /// </summary>
        /// <param name="fabricContext">
        /// Specifies the build context of the current build operation.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        public object BuildFrom(IFabricContext fabricContext)
        {
            var collectionType = fabricContext.TypeToBuild;
            var itemType = collectionType.GetGenericArguments().First();

            if (fabricContext.ContainerModel.HasImplementationsFor(itemType))
            {
                return fabricContext.ResolveInstancesFromContainer(itemType);
            }

            var targetArray = Array.CreateInstance(itemType, 3);

            if (itemType.IsInterface)
            {              
                targetArray.SetValue(fabricContext.ResolveByFabric(itemType), 0);
                targetArray.SetValue(fabricContext.ResolveByFabric(itemType), 1);
                targetArray.SetValue(fabricContext.ResolveByFabric(itemType), 2);

                foreach (var item in targetArray)
                {
                    fabricContext.InjectExistingInstanceIntoContainer(itemType, item);
                }
            }

            return targetArray;
        }
    }
}