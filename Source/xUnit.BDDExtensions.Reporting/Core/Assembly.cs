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
using System.Diagnostics;
using System.Linq;

namespace Xunit.Reporting.Core
{
    /// <summary>
    /// A simple wrapper class around <see cref="System.Reflection.Assembly"/> for 
    /// the sake of testability.
    /// </summary>
    public class Assembly : IAssembly
    {
        private readonly System.Reflection.Assembly assembly;

        /// <summary>
        /// Creates a new instance of the <see cref="Assembly"/> class.
        /// </summary>
        /// <param name="assembly">
        /// Specifies the <see cref="System.Reflection.Assembly"/> which is wrapped.
        /// </param>
        public Assembly(System.Reflection.Assembly assembly)
        {
            Require.ArgumentNotNull(assembly, "assembly");

            this.assembly = assembly;
        }

        #region IAssembly Members

        /// <summary>
        /// Collects all exported types of the assembly matching the predicate
        /// specified via <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">
        /// Specifies a predicate that evaluates each exported type in the assembly.
        /// </param>
        /// <returns>
        /// A collection of <see cref="Type"/> instance matching the predicate
        /// specified by <paramref name="predicate"/>.
        /// </returns>
        /// <remarks>
        /// This method never returns <c>null</c>.
        /// </remarks>
        public IEnumerable<Type> AllTypesMatching(Func<Type, bool> predicate)
        {
            Debug.Assert(predicate != null);

            return assembly.GetExportedTypes().Where(predicate);
        }

        /// <summary>
        /// Gets the name of the assembly.
        /// </summary>
        public string Name
        {
            get { return assembly.GetName().Name; }
        }

        #endregion
    }
}