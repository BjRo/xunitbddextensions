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
using System.Collections.Generic;

namespace Xunit.Reporting.Internal
{
    /// <summary>
    ///   A report abstraction.
    /// </summary>
    public interface IReport : IEnumerable<Concern>
    {
        /// <summary>
        ///   Gets the name of the reflected assembly.
        /// </summary>
        string ReflectedAssembly { get; }

        /// <summary>
        ///   Gets the total amount of concerns in this report model.
        /// </summary>
        int TotalAmountOfConcerns { get; }

        /// <summary>
        ///   Gets the total amount of contexts in this report model.
        /// </summary>
        int TotalAmountOfContexts { get; }

        /// <summary>
        ///   Gets the total amount of observations in this report model.
        /// </summary>
        int TotalAmountOfObservations { get; }
    }
}