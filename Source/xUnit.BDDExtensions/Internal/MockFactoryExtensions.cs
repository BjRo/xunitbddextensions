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

using System.Collections.Generic;
using System.Linq;

namespace Xunit.Internal
{
    /// <summary>
    /// A static extension class for the <see cref="IMockingEngine"/> interface.
    /// </summary>
    public static class MockFactoryExtensions
    {
        /// <summary>
        /// Creates list filled with 3 stubs of the type specified via <typeparamref name="TInterfaceType"/>.
        /// </summary>
        /// <typeparam name="TInterfaceType">
        /// Specifies the interface type.
        /// </typeparam>
        /// <param name="mockingEngine">
        /// Specifies the factory for creating stub implementations on-the-fly.
        /// </param>
        /// <returns>
        /// A collection containing three stubs of the specified type.
        /// </returns>
        public static IList<TInterfaceType> CreateStubCollectionOf<TInterfaceType>(this IMockingEngine mockingEngine) where TInterfaceType : class 
        {
            Guard.AgainstArgumentNull(mockingEngine, "mockingEngine");

           return Enumerable.Range(0, 3).Select(x => mockingEngine.Stub<TInterfaceType>()).ToList();
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
        public static T Stub<T>(this IMockingEngine mockingEngine) where T : class
        {
            Guard.AgainstArgumentNull(mockingEngine, "mockingEngine");

            return (T) mockingEngine.Stub(typeof (T));
        }
    }
}