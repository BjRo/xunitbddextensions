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
    ///   This class is implements the configuration API for xUnit.BDDExtensions.
    /// </summary>
    public sealed class ConfigurationExpression : ICore
    {
        private readonly IList<IBuilder> _builders = new List<IBuilder>
        {
            new DefaultBuilder(),
            new EnumerableBuilder()
        };

        private readonly IList<IConfigurationRule> _configurationRules = new List<IConfigurationRule>();
        private IMockingEngine _mockingEngine;

        #region ICore Members

        /// <summary>
        ///   Gets the configured mocking engine.
        /// </summary>
        IMockingEngine ICore.MockingEngine
        {
            get
            {
                return _mockingEngine;
            }
        }

        /// <summary>
        ///   Gets a collection of all configured builders.
        /// </summary>
        IEnumerable<IBuilder> ICore.Builders
        {
            get
            {
                foreach (var builder in _builders)
                {
                    yield return builder;
                }
            }
        }

        /// <summary>
        ///   Gets a collection of all configured configuration rules.
        /// </summary>
        IEnumerable<IConfigurationRule> ICore.ConfigurationRules
        {
            get
            {
                foreach (var configRule in _configurationRules)
                {
                    yield return configRule;
                }
            }
        }

        /// <summary>
        ///   Veryfies that the configuration is valid.
        /// </summary>
        /// <exception cref = "InvalidOperationException">
        ///   Is thrown when the configuration is in an invalid state.
        /// </exception>
        void ICore.EnsureConfigured()
        {
            if (_mockingEngine == null)
            {
                throw new InvalidOperationException("No mocking engine was configured");
            }
        }

        #endregion

        /// <summary>
        /// Configures the mocking engine to be used.
        /// </summary>
        /// <typeparam name="TMockingEngine">
        /// Specifies the type of the mocking engine.
        /// </typeparam>
        public void MockingEngineIs<TMockingEngine>() where TMockingEngine : IMockingEngine, new()
        {
            MockingEngineIs(new TMockingEngine());
        }

        /// <summary>
        /// Specifies the mocking engine to be used.
        /// </summary>
        /// <param name="mockingEngine">
        /// The mocking engine.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="mockingEngine"/> is <c>null</c>.
        /// </exception>
        public void MockingEngineIs(IMockingEngine mockingEngine)
        {
            Guard.AgainstArgumentNull(mockingEngine, "mockingEngine");

            _mockingEngine = mockingEngine;
        }

        /// <summary>
        /// Adds a builder to the xUnit.BDDExtensions build chain.
        /// A builder is a class which is used by the xUnit.BDDExtensions
        /// core in order to build a dependency of a class under specification
        /// in the automocking container.
        /// </summary>
        /// <param name="externalBuilder">
        /// Specifies the external builder.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="externalBuilder"/> is <c>null</c>.
        /// </exception>
        public void AddBuilder(IBuilder externalBuilder)
        {
            Guard.AgainstArgumentNull(externalBuilder, "externalBuilder");

            _builders.Insert(0, externalBuilder);
        }

        /// <summary>
        /// Adds a builder to the xUnit.BDDExtensions build chain.
        /// A builder is a class which is used by the xUnit.BDDExtensions
        /// core in order to build a dependency of a class under specification
        /// in the automocking container.
        /// </summary>
        /// <typeparam name="TBuilder">
        /// Specifies the type of the external builder.
        /// </typeparam>
        public void AddBuilder<TBuilder>() where TBuilder : IBuilder, new()
        {
            AddBuilder(new TBuilder());
        }

        /// <summary>
        /// Adds a configuration rule to xUnit.BDDExtensions.
        /// A configuration rule is applied to a dependency of a class under
        /// specification after it has been created by a builder. A <see cref="IConfigurationRule"/>
        /// can be used to apply mocking configuration as part of the build process
        /// of the class under specification.
        /// </summary>
        /// <typeparam name="TConfigurationRule">
        /// Specifies the type of the configuration rule.
        /// </typeparam>
        public void AddConfigurationRule<TConfigurationRule>() where TConfigurationRule : IConfigurationRule, new()
        {
            AddConfigurationRule(new TConfigurationRule());
        }

        /// <summary>
        /// Adds a configuration rule to xUnit.BDDExtensions.
        /// A configuration rule is applied to a dependency of a class under
        /// specification after it has been created by a builder. A <see cref="IConfigurationRule"/>
        /// can be used to apply mocking configuration as part of the build process
        /// of the class under specification.
        /// </summary>
        /// <param name="externalRule">
        /// Specifies the external configuration rule.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the supplied configuration rule is <c>null</c>.
        /// </exception>
        public void AddConfigurationRule(IConfigurationRule externalRule)
        {
            Guard.AgainstArgumentNull(externalRule, "externalRule");

            _configurationRules.Add(externalRule);
        }

        /// <summary>
        ///   Builds the <see cref = "ICore" /> of xUnit.BDDExtensions.
        /// </summary>
        /// <returns>
        ///  The created core.
        /// </returns>
        internal ICore Build()
        {
            return this;
        }
    }
}