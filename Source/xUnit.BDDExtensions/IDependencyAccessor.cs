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

namespace Xunit
{
    /// <summary>
    /// Accessor interface for dependencies created and managed by mockframework / automocking container.
    /// </summary>
    public interface IDependencyAccessor
    {
        /// <summary>
        /// Creates a dependency of the type specified by <typeparamref name="TInterfaceType"/>.
        /// </summary>
        /// <typeparam name="TInterfaceType">
        /// The type to create a dependency for. (Should be an interface)
        /// </typeparam>
        /// <returns>
        /// An newly created instance implementing <typeparamref name="TInterfaceType"/>.
        /// </returns>
        TInterfaceType An<TInterfaceType>() where TInterfaceType : class;

        /// <summary>
        /// Creates a dependency of the type specified by <typeparamref name="TInterfaceType"/>.
        /// This method reuses existing instances. If an instance of <typeparamref name="TInterfaceType"/>
        /// was already requested it's returned here. (You can say this is kind of a singleton behavior)
        /// 
        /// Besides that, you can obtain a reference to automatically injected stubs with this 
        /// method.
        /// </summary>
        /// <typeparam name="TInterfaceType">
        /// The type to create a dependency for. (Should be an interface)
        /// </typeparam>
        /// <returns>
        /// An instance implementing <see cref="TInterfaceType"/>.
        /// </returns>
        TInterfaceType The<TInterfaceType>() where TInterfaceType : class;

        /// <summary>
        /// Creates a list of dependencies of the type specified by <typeparamref name="TInterfaceType"/>.
        /// </summary>
        /// <typeparam name="TInterfaceType">
        /// Specifies the dependency type. (Should be an interface).
        /// </typeparam>
        /// <returns>
        /// An newly created instance implementing <typeparamref name="TInterfaceType"/>.
        /// </returns>
        IList<TInterfaceType> Some<TInterfaceType>() where TInterfaceType : class;


        /// <summary>
        /// Uses the instance supplied by <paramref name="instance"/> during the 
        /// creation of the sut. The specified instance will be injected into the constructor.
        /// </summary>
        /// <typeparam name="TInterfaceType">
        /// Specifies the interface type.
        /// </typeparam>
        /// <param name="instance">
        /// Specifies the instance to be used for the specification.
        /// </param>
        void Use<TInterfaceType>(TInterfaceType instance) where TInterfaceType : class;
    }
}