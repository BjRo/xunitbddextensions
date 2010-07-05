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
using System.Linq;

namespace Xunit.Reporting.Internal.Configuration
{
    /// <summary>
    /// A specialized <see cref="ArgumentKey{TValue}"/> for collections
    /// of primitive values.
    /// </summary>
    /// <typeparam name="TValue">
    /// Specifies the primitive collection type.
    /// </typeparam>
    public class CollectionArgumentKey<TValue> : ArgumentKey<IEnumerable<TValue>> where TValue : IConvertible
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionArgumentKey{TValue}"></see> class.
        /// </summary>
        public CollectionArgumentKey(string key) : base(key)
        {
        }

        /// <summary>
        /// Returns the default value. In this case an empty collection.
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<TValue> DefaultValue()
        {
            return new List<TValue>();
        }

        /// <summary>
        /// Hook method must be overridden in order to perform the actual extraction.
        /// </summary>
        /// <param name="collection">The collection to extract the values from.</param>
        /// <returns>The extracted value.</returns>
        protected override IEnumerable<TValue> ExtractValue(ICollection<string> collection)
        {
            return collection
                .Select(entry => (TValue) Convert.ChangeType(entry, typeof (TValue)))
                .ToList();
        }
    }
}