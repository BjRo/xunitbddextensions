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
using System.Collections.Generic;

namespace Xunit.Internal
{
    /// <summary>
    /// A configuration endpoint for the <see cref="Fabric"/> which controls
    /// the internal processes of xunit.bddextensions.     
    /// </summary>
    public class FrameworkConfig
    {
        /// <summary>
        /// Gets or sets the <see cref="IMockingEngine"/> which is used internally.
        /// </summary>
        public static IMockingEngine MockingEngine { get; set; }

        /// <summary>
        /// The set of default builders used inside xunit.bddextensions.
        /// </summary>
        public static readonly IList<IBuilder> Builders = new List<IBuilder>
        {
            new EnumerableBuilder(),
            new DefaultBuilder()
        };

        /// <summary>
        /// The set of configuration rules used inside xunit.bddextensions.
        /// </summary>
        public static readonly IList<IConfigurationRule> ConfigurationRules = new List<IConfigurationRule>();

        /// <summary>
        /// Creates the <see cref="IFabric"/> which is used in the specifications.
        /// </summary>
        /// <returns>
        /// A <see cref="IFabric"/>.
        /// </returns>
        public static IFabric BuildFabric()
        {
            return new Fabric(MockingEngine, Builders, ConfigurationRules);
        }
    }
}