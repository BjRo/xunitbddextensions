//  Copyright 2010 xUnit.BDDExtensions
//    
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License. 
//  You may obtain a copy of the License at
//    
//        http://www.apache.org/licenses/LICENSE-2.0
//    
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
//  implied. See the License for the specific language governing permissions and
//  limitations under the License.  
//  
using System.Collections.Generic;
using System.Linq;
using Xunit.Internal;

namespace Xunit
{
    /// <summary>
    /// A set of extension methods to simplify the strong typed fake creation
    /// which is used at several places in the framework.
    /// </summary>
    public static class FakeEngineExtensions
    {
        /// <summary>
        /// Creates a list containing 3 fake instances of the type specified 
        /// via <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the item type of the list. This should be an interface or an abstract class.
        /// </typeparam>
        /// <param name="fakeEngine">
        /// Specifies the <see cref="IFakeEngine"/> which is used to create the individual items.
        /// </param>
        /// <returns>
        /// An <see cref="IList{T}"/>.
        /// </returns>
        public static IList<T> CreateFakeCollectionOf<T>(this IFakeEngine fakeEngine)
        {
            Guard.AgainstArgumentNull(fakeEngine, "fakeEngine");

            return Enumerable.Range(0, 3)
                .Select(x => (T)fakeEngine.Stub(typeof(T)))
                .ToList();
        }

        /// <summary>
        /// Gives strong typed access to the generic <see cref="IFakeEngine.Stub"/> method.
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the type to stub e.g. to create a fake for.
        /// </typeparam>
        /// <param name="fakeEngine">
        /// Specifies the <see cref="IFakeEngine"/>.
        /// </param>
        /// <returns>
        /// A new fake for the type specified via <typeparamref name="T"/>.
        /// </returns>
        public static T Stub<T>(this IFakeEngine fakeEngine)
        {
            Guard.AgainstArgumentNull(fakeEngine, "fakeEngine");

            return (T)fakeEngine.Stub(typeof(T));
        }
    }
}