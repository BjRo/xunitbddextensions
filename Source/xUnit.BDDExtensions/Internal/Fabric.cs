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
using StructureMap;
using System.Linq;

namespace Xunit.Internal
{
    /// <summary>
    /// A fabric for creating a constructor dependency of the system under test
    /// and applying some configuration rules to it.
    /// </summary>
    public class Fabric : IFabric
    {
        private readonly IEnumerable<IBuilder> _builders;
        private readonly IEnumerable<IConfigurationRule> _configurationRules;

        /// <summary>
        /// Creates a new instance of the <see cref="Fabric"/> class.
        /// </summary>
        /// <param name="builders">
        /// A collection of all known builders.
        /// </param>
        /// <param name="configurationRules">
        /// A set of rules for configuration of the produced instances.
        /// </param>
        public Fabric(IEnumerable<IBuilder> builders, IEnumerable<IConfigurationRule> configurationRules)
        {
            Guard.AgainstArgumentNull(builders, "builders");
            Guard.AgainstArgumentNull(configurationRules, "configurationRules");

            _builders = builders;
            _configurationRules = configurationRules;
        }

        /// <summary>
        /// Builds a dependency.
        /// </summary>
        /// <param name="typeToBuild">
        /// Specifies the type to be build.
        /// </param>
        /// <param name="mockFactory">
        /// Specifies a facade to a mocking framework.
        /// </param>
        /// <param name="container">
        /// Specifies the automocking container.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        public object Build(Type typeToBuild, IMockFactory mockFactory, IContainer container)
        {
            Guard.AgainstArgumentNull(typeToBuild, "typeToBuild");
            Guard.AgainstArgumentNull(mockFactory, "mockFactory");
            Guard.AgainstArgumentNull(container, "container");

            var buildContext = new FabricContext(typeToBuild, mockFactory, container, this);

            var responsibleBuilder = _builders.FirstOrDefault(x => x.KnowsHowToBuild(buildContext.TypeToBuild));

            if (responsibleBuilder == null)
            {
                throw new InvalidOperationException(
                    string.Format("Unable to build ctor dependency of type {0}." + Environment.NewLine +
                                 "Make sure to use only interfaces or abstract base classes in the constructor!", typeToBuild.FullName));
            }

            var stub = responsibleBuilder.BuildFrom(buildContext);

            Guard.AgainstArgumentNull(stub, "stub");

            _configurationRules.Each(x => x.Configure(stub, buildContext));

            return stub;               
        }
    }
}