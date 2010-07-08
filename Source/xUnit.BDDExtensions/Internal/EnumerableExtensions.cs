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

namespace Xunit.Internal
{
    /// <summary>
    ///   A container class for extensions on the <see cref = "Type" /> class.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///   Executes the action specified by <paramref name = "action" /> on each element of the specified
        ///   collection.
        /// </summary>
        /// <typeparam name = "T">
        ///   Specifies the collections item type.
        /// </typeparam>
        /// <param name = "enumerable">
        ///   Specifies the collection.
        /// </param>
        /// <param name = "action">
        ///   Specifies the operation to perform on each element.
        /// </param>
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            Guard.AgainstArgumentNull(action, "action");

            if (enumerable != null)
            {
                foreach (var item in enumerable)
                {
                    action(item);
                }
            }
        }
    }
}