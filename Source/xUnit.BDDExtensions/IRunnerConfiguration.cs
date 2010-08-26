using System;
using System.Collections.Generic;
using Xunit.Internal;

namespace Xunit
{
    public interface IRunnerConfiguration
    {
        IEnumerable<IBuilder> Builders { get; }

        IEnumerable<IConfigurationRule> ConfigurationRules { get; }

        IFakeEngine FakeEngine { get; }

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
        void AddBuilder(IBuilder externalBuilder);

        /// <summary>
        /// Adds a builder to the xUnit.BDDExtensions build chain.
        /// A builder is a class which is used by the xUnit.BDDExtensions
        /// core in order to build a dependency of a class under specification
        /// in the automocking container.
        /// </summary>
        /// <typeparam name="TBuilder">
        /// Specifies the type of the external builder.
        /// </typeparam>
        void AddBuilder<TBuilder>() where TBuilder : IBuilder, new();

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
        void AddConfigurationRule<TConfigurationRule>() where TConfigurationRule : IConfigurationRule, new();

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
        void AddConfigurationRule(IConfigurationRule externalRule);

        /// <summary>
        /// Configures the mocking engine to be used.
        /// </summary>
        /// <typeparam name="TFakeEngine">
        /// Specifies the type of the mocking engine.
        /// </typeparam>
        void FakeEngineIs<TFakeEngine>() where TFakeEngine : IFakeEngine, new();

        /// <summary>
        /// Specifies the mocking engine to be used.
        /// </summary>
        /// <param name="fakeEngine">
        /// The mocking engine.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="fakeEngine"/> is <c>null</c>.
        /// </exception>
        void FakeEngineIs(IFakeEngine fakeEngine);

        void FakeEngineIs(Type fakeEngineType);
    }
}