using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Internal;

namespace Xunit
{
    //TODO: Document me!!!
    public class RunnerConfiguration : IRunnerConfiguration
    {
        private readonly IList<IConfigurationRule> _configurationRules = new List<IConfigurationRule>();
        private IFakeEngine _fakeEngine;
        private readonly IList<IBuilder> _builders = new List<IBuilder>
        {
            new DefaultBuilder(),
            new EnumerableBuilder()
        };

        internal static readonly IRunnerConfiguration Instance = new RunnerConfiguration();

        IEnumerable<IBuilder> IRunnerConfiguration.Builders
        {
            get { return _builders; }
        }

        IEnumerable<IConfigurationRule> IRunnerConfiguration.ConfigurationRules
        {
            get { return _configurationRules; }
        }

        public static IFakeEngine FakeEngine
        {
            get { return Instance.FakeEngine; }
        }

        public static IEnumerable<IBuilder> Builders
        {
            get { return Instance.Builders; }
        }

        public static IEnumerable<IConfigurationRule> ConfigurationRules
        {
            get { return Instance.ConfigurationRules; }
        }

        IFakeEngine IRunnerConfiguration.FakeEngine { get { return _fakeEngine; } }

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


        public void FakeEngineIs<TFakeEngine>() where TFakeEngine : IFakeEngine, new()
        {
            FakeEngineIs(new TFakeEngine());
        }


        public void FakeEngineIs(IFakeEngine fakeEngine)
        {
            Guard.AgainstArgumentNull(fakeEngine, "FakeEngine");

            _fakeEngine = fakeEngine;
        }

        public void FakeEngineIs(Type fakeEngineType)
        {
            Guard.AgainstArgumentNull(fakeEngineType, "fakeEngineType");
            Guard.ArgumentAssignableTo(fakeEngineType, typeof(IFakeEngine));

            _fakeEngine = (IFakeEngine)Activator.CreateInstance(fakeEngineType);
        }
    }
}