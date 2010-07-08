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

namespace Xunit.Reporting.Internal
{
    /// <summary>
    ///   An interface for an assembly.
    /// </summary>
    public interface IAssembly
    {
        /// <summary>
        ///   Gets the name of the assembly.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///   Collects all exported types of the assembly matching the predicate
        ///   specified via <paramref name = "predicate" />.
        /// </summary>
        /// <param name = "predicate">
        ///   Specifies a predicate that evaluates each exported type in the assembly.
        /// </param>
        /// <returns>
        ///   A collection of <see cref = "Type" /> instance matching the predicate
        ///   specified by <paramref name = "predicate" />.
        /// </returns>
        /// <remarks>
        ///   This method never returns <c>null</c>.
        /// </remarks>
        IEnumerable<Type> AllTypesMatching(Func<Type, bool> predicate);
    }
}