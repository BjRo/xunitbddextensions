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

namespace Xunit
{
    /// <summary>
    /// Interface to a mocking library. 
    /// </summary>
    public interface IMockFactory
    {
        /// <summary>
        /// Creates a stub of the type specified via <paramref name="interfaceType"/>.
        /// </summary>
        /// <param name="interfaceType">
        /// Specifies the interface type to create a stub for.
        /// </param>
        /// <returns>
        /// The created stub instance.
        /// </returns>
        object Stub(Type interfaceType);

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
        T PartialMock<T>(params  object[] args) where T : class;
    }
}