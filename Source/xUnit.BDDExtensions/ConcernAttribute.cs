// Copyright 2010 xUnit.BDDExtensions
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
    /// A concern. This attribute marks a specification to be related to a particular type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ConcernAttribute : Attribute
    {
        /// <summary>
        /// Initializes the <see cref="ConcernAttribute"/> class.
        /// </summary>
        /// <param name="type">
        /// The type related to the concern.
        /// </param>
        public ConcernAttribute(Type type) 
        {
            Type = type;
        }

        /// <summary>
        /// Initializes the <see cref="ConcernAttribute"/> class.
        /// </summary>
        /// <param name="type">
        /// The type related to the concern.
        /// </param>
        /// <param name="scenario">
        /// Specifies an optional scenario in which the type is used.
        /// </param>
        public ConcernAttribute(Type type, string scenario) 
        {
            Type = type;
            Scenario = scenario;
        }

        /// <summary>
        /// Gets the type this specification is for.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Gets the scenario of the type.
        /// </summary>
        public string Scenario { get; private set; }
    }
}