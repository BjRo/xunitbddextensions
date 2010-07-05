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
using StructureMap;

namespace Xunit.Internal
{
    /// <summary>
    /// A fabric for creating a constructor dependency of the system under test
    /// and applying some configuration rules to it.
    /// </summary>
    public interface IFabric
    {
        /// <summary>
        /// Builds a dependency.
        /// </summary>
        /// <param name="typeToBuild">
        /// Specifies the type to be build.
        /// </param>
        /// <param name="container">
        /// Specifies the automocking container.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        object Build(Type typeToBuild, IContainer container);
    }
}