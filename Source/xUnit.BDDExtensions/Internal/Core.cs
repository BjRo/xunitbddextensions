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

namespace Xunit.Internal
{
    /// <summary>
    ///   A configuration endpoint for the internal processes of xunit.bddextensions.
    /// </summary>
    public sealed class Core
    {
        private static ICore _core = new NulloCore();

        /// <summary>
        ///   Gets the <see cref = "IMockingEngine" /> which is used internally.
        /// </summary>
        public static IMockingEngine MockingEngine
        {
            get
            {
                return _core.MockingEngine;
            }
        }

        /// <summary>
        ///   The set of default builders used inside xunit.bddextensions.
        /// </summary>
        public static IEnumerable<IBuilder> Builders
        {
            get
            {
                return _core.Builders;
            }
        }

        /// <summary>
        ///   Gets the set of configuration rules used inside xunit.bddextensions.
        /// </summary>
        public static IEnumerable<IConfigurationRule> ConfigurationRules
        {
            get
            {
                return _core.ConfigurationRules;
            }
        }

        /// <summary>
        ///   Configures the core framwork of xUnit.BDDExtensions.
        /// </summary>
        /// <param name = "configurator">
        ///   Specifies a function which is used for configuring the framework.
        /// </param>
        public static void Configure(Action<ConfigurationExpression> configurator)
        {
            Guard.AgainstArgumentNull(configurator, "configurator");

            var configurationExpression = new ConfigurationExpression();

            configurator(configurationExpression);

            _core = configurationExpression.Build();
        }

        /// <summary>
        ///   Creates the <see cref = "IFabric" /> which is used in the specifications.
        /// </summary>
        /// <returns>
        ///   A <see cref = "IFabric" />.
        /// </returns>
        internal static IFabric BuildFabric()
        {
            _core.EnsureConfigured();

            return new Fabric(MockingEngine, Builders, ConfigurationRules);
        }
    }
}