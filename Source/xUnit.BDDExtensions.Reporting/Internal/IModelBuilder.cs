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

namespace Xunit.Reporting.Internal
{
    /// <summary>
    ///   Interface for a builder which is able to create a report model 
    ///   from an assembly.
    /// </summary>
    public interface IModelBuilder
    {
        /// <summary>
        ///   Builds a report model from the assembly specified via <paramref name = "assemblyName" />.
        /// </summary>
        /// <param name = "assemblyName">
        ///   Specifies the name of an assembly or the full path to an assembly
        /// </param>
        /// <returns>
        ///   The <see cref = "Report" /> model extracted from the specified assembly.
        /// </returns>
        /// <exception cref = "ArgumentException">
        ///   Thrown when <paramref name = "assemblyName" /> is <c>null</c> or empty.
        /// </exception>
        IReport BuildModel(string assemblyName);
    }
}