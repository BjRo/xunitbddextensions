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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Xunit.Reporting.Internal
{
    /// <summary>
    ///   A representation of a context specification in the report model.
    /// </summary>
    public class Context : IEnumerable<Observation>
    {
        private readonly string _description;
        private readonly IFormatter _formatter = new ContextFormatter();
        private readonly IEnumerable<Observation> _observations;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "Context" /> class.
        /// </summary>
        /// <param name = "description">
        ///   Specifies the unformatted description of the context. 
        /// </param>
        /// <param name = "observations">
        ///   Specifies the a collection of <see cref = "Observation" />s related to this
        ///   <see cref = "Context" />.
        /// </param>
        private Context(string description, IEnumerable<Observation> observations)
        {
            this._description = description;
            this._observations = observations;
        }

        /// <summary>
        ///   Gets the amount of <see cref = "Observation" /> instances
        ///   related to the <see cref = "Context" />.
        /// </summary>
        public int AmountOfObservations
        {
            get
            {
                return _observations.Count();
            }
        }

        #region IEnumerable<Observation> Members

        /// <summary>
        ///   Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///   A <see cref = "T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Observation> GetEnumerator()
        {
            foreach (var observation in _observations)
            {
                yield return observation;
            }
        }

        /// <summary>
        ///   Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///   An <see cref = "T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        /// <summary>
        ///   Builds a context from the type specified via <paramref name = "specType" />.
        /// </summary>
        /// <param name = "specType">
        ///   A valid spec type.
        /// </param>
        /// <returns>
        ///   The created report model for a context specification.
        /// </returns>
        public static Context BuildFrom(Type specType)
        {
            var description = specType.Name;

            Debug.Assert(Specification(specType));

            return new Context(
                description,
                Observation.BuildAllFrom(specType));
        }

        /// <summary>
        ///   Checks whether the type specified via <paramref name = "type" /> is a valid context type.
        /// </summary>
        /// <param name = "type">
        ///   Specifies the type to be checked.
        /// </param>
        /// <returns>
        ///   <c>true</c> if the type is a valid context specification. Otherwise <c>false</c>.
        /// </returns>
        public static bool Specification(Type type)
        {
            return type.IsMarkedWith<ConcernAttribute>() &&
                   type.Implements<IContextSpecification>() &&
                   type.ContainsMethodsMarkedWith<FactAttribute>();
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