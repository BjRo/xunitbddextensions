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
using System.Collections.Generic;
using System.Diagnostics;
using Xunit.Internal;

namespace Xunit.Reporting.Internal
{
    /// <summary>
    /// A builder which is able to create a report model 
    /// from an assembly.
    /// </summary>
    public class ModelBuilder : IModelBuilder
    {
        private readonly IAssemblyLoader _assemblyLoader;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelBuilder"/> class.
        /// </summary>
        /// <param name="assemblyLoader">
        /// Specifies the assembly loader.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="assemblyLoader"/> is <c>null</c>.
        /// </exception>
        public ModelBuilder(IAssemblyLoader assemblyLoader)
        {
            Guard.AgainstArgumentNull(assemblyLoader, "assemblyLoader");

            _assemblyLoader = assemblyLoader;
        }

        #region IModelBuilder Members

        /// <summary>
        /// Builds a report model from the assembly specified via <paramref name="assemblyName"/>.
        /// </summary>
        /// <param name="assemblyName">Specifies the name of an assembly or the full path to an assembly</param>
        /// <returns>
        /// The <see cref="Report"/> model extracted from the specified assembly.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="assemblyName"/> is <c>null</c> or empty.
        /// </exception>
        public IReport BuildModel(string assemblyName)
        {
            Guard.AgainstNullOrEmptyString(assemblyName, "assemblyName");

            var assembly = _assemblyLoader.Load(assemblyName);

            Debug.Assert(assembly != null);

            return BuildModelFrom(assembly);
        }

        #endregion

        private static IReport BuildModelFrom(IAssembly assembly)
        {
            var allSpecTypes = assembly.AllTypesMatching(Context.Specification);
            var collectedConcerns = new List<Concern>();

            foreach (var specType in allSpecTypes)
            {
                var context = Context.BuildFrom(specType);
                var concern = BuildOrGetConcern(specType, collectedConcerns);

                concern.AddContext(context);
            }

            return new Report(collectedConcerns, assembly);
        }

        private static Concern BuildOrGetConcern(Type specType, List<Concern> existingConcerns)
        {
            var relatedConcern = existingConcerns.Find(concern => concern.IsRelatedTo(specType));

            if (relatedConcern == null)
            {
                relatedConcern = Concern.BuildFrom(specType);
                existingConcerns.Add(relatedConcern);
            }

            return relatedConcern;
        }
    }
}