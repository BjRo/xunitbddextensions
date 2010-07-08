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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Xunit.Reporting.Internal
{
    /// <summary>
    ///   A report abstraction which represents the report data extracted from an assembly.
    /// </summary>
    public class Report : IReport
    {
        private readonly IEnumerable<Concern> _collectedConcerns;
        private readonly IAssembly _reflectedAssembly;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "Report" /> class.
        /// </summary>
        /// <param name = "collectedConcerns">The collected concerns.</param>
        /// <param name = "reflectedAssembly">The reflected assembly.</param>
        public Report(IEnumerable<Concern> collectedConcerns, IAssembly reflectedAssembly)
        {
            _collectedConcerns = collectedConcerns;
            _reflectedAssembly = reflectedAssembly;
        }

        #region IReport Members

        /// <summary>
        ///   Gets the name of the reflected assembly.
        /// </summary>
        public string ReflectedAssembly
        {
            get
            {
                return _reflectedAssembly.Name;
            }
        }

        /// <summary>
        ///   Gets the total amount of concerns in this report model.
        /// </summary>
        public int TotalAmountOfConcerns
        {
            get
            {
                return _collectedConcerns.Count();
            }
        }

        /// <summary>
        ///   Gets the total amount of contexts in this report model.
        /// </summary>
        public int TotalAmountOfContexts
        {
            get
            {
                return _collectedConcerns.Sum(concern => concern.AmountOfContexts);
            }
        }

        /// <summary>
        ///   Gets the total amount of observations in this report model.
        /// </summary>
        public int TotalAmountOfObservations
        {
            get
            {
                return _collectedConcerns.Sum(concern => concern.AmountOfObservations);
            }
        }

        public IEnumerator<Concern> GetEnumerator()
        {
            foreach (var collectedConcern in _collectedConcerns)
            {
                yield return collectedConcern;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}