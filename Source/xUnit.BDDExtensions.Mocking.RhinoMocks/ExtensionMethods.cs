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
using System.Collections.Generic;
using StructureMap.AutoMocking;
using StructureMap;
using System.Collections;

namespace Xunit
{
    /// <summary>
    /// A container class for extensions on the <see cref="Type"/> class.
    /// </summary>
    internal static class ExtensionMethods
    {
        /// <summary>
        /// Determines whether the type specified by <see cref="serviceType"/>
        /// closes the <see cref="IEnumerable{T}"/> interface.
        /// </summary>
        /// <param name="serviceType">
        /// Specifies a type.
        /// </param>
        /// <returns>
        /// <c>true</c> if the type closes <see cref="IEnumerable{T}"/> otherwise <c>false</c>.
        /// </returns>
        public static bool IsClosingIEnumerable(this Type serviceType)
        {
            return serviceType.IsInterface &&
                serviceType.IsGenericType &&
                serviceType.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        /// <summary>
        /// Creates an array of the itemtype specified by <paramref name="itemType"/>
        /// and prepopulates it with three items.
        /// </summary>
        /// <param name="itemType">
        /// Specifies the item type.
        /// </param>
        /// <param name="serviceLocator">
        /// Specifies the service locator used to create the items.
        /// </param>
        /// <returns>
        /// The created array.
        /// </returns>
        public static IEnumerable CreateItemCollection(this ServiceLocator serviceLocator, Type itemType)
        {
            if (!itemType.IsInterface)
            {
                throw new InvalidOperationException("Works only with interface types");
            }

            var targetArray = Array.CreateInstance(itemType, 3);

            targetArray.SetValue(serviceLocator.Service(itemType), 0);
            targetArray.SetValue(serviceLocator.Service(itemType), 1);
            targetArray.SetValue(serviceLocator.Service(itemType), 2);

            return targetArray;
        }

        /// <summary>
        /// Gets all instances from the container as an array which implements
        /// the correct <see cref="IEnumerable{T}"/> version. SM's weekly typed
        /// API returns only an <see cref="ArrayList"/>.
        /// </summary>
        /// <param name="container">
        /// Specifies the container.
        /// </param>
        /// <param name="itemType">
        /// Specifies the itemType.
        /// </param>
        /// <returns>
        /// An array of the specified item type.
        /// </returns>
        public static IEnumerable GetInstances(this IContainer container, Type itemType)
        {
            var instances = container.GetAllInstances(itemType);

            var targetArray = Array.CreateInstance(itemType, instances.Count);
            instances.CopyTo(targetArray, 0);

            return targetArray;
        }
    }
}