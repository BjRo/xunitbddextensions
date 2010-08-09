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
using System.Linq;

namespace Xunit.Reporting.Internal
{
    /// <summary>
    ///   An observation in a context specification.
    /// </summary>
    public class Observation
    {
        private readonly string _description;
        private readonly IFormatter _formatter = new ObservationFormatter();

        /// <summary>
        ///   Initializes a new instance of the <see cref = "Observation" /> class.
        /// </summary>
        /// <param name = "description">
        ///   Specifies the unformatted description of the observation.
        /// </param>
        private Observation(string description)
        {
            _description = description;
        }

        /// <summary>
        ///   Builds all <see cref = "Observation" /> instances for the context type specified
        ///   via <paramref name = "specType" />.
        /// </summary>
        /// <param name = "specType">
        ///   Specifies the contex type.
        /// </param>
        /// <returns>
        ///   A collection of all <see cref = "Observation" />s contained in the type supplied by <paramref name = "specType" />.
        /// </returns>
        public static IEnumerable<Observation> BuildAllFrom(Type specType)
        {
            return specType
                .GetMethodsMarkedWith<FactAttribute>()
                .Select(method => new Observation(method.Name));
        }

        /// <summary>
        ///   Returns a <see cref = "System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///   A <see cref = "System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return _formatter.Format(_description);
        }
    }
}