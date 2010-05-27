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

namespace Xunit
{
    /// <summary>
    /// A concern. This attribute marks a specification to be related to a particular type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ConcernAttribute : TraitAttribute
    {
        /// <summary>
        /// Initializes the <see cref="ConcernAttribute"/> class.
        /// </summary>
        /// <param name="type">
        /// The type related to the concern.
        /// </param>
        public ConcernAttribute(Type type) : base("Concern", type.FullName)
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
        /// Specifies a scenario.
        /// </param>
        public ConcernAttribute(Type type, string scenario) : base("Concern", type.FullName)
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