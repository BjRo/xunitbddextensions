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
    /// Internal configuration interface for the core.
    /// </summary>
    internal interface ICore
    {
        /// <summary>
        /// Gets the configured mocking engine.
        /// </summary>
        IMockingEngine MockingEngine { get; }

        /// <summary>
        /// Gets a collection of all configured builders.
        /// </summary>
        IEnumerable<IBuilder> Builders { get; }

        /// <summary>
        /// Gets a collection of all configured configuration rules.
        /// </summary>
        IEnumerable<IConfigurationRule> ConfigurationRules { get; }

        /// <summary>
        /// Veryfies that the configuration is valid.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Is thrown when the configuration is in an invalid state.
        /// </exception>
        void EnsureConfigured();
    }
}