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
using System.Linq;
using Xunit.Internal;

namespace Xunit.Reporting.Internal.Configuration
{
    /// <summary>
    /// A specialized <see cref="ArgumentKey{TValue}"/> for primitive types.
    /// </summary>
    /// <typeparam name="TValue">
    /// Specifies the primitive property value.
    /// </typeparam>
    public class SimpleArgumentKey<TValue> : ArgumentKey<TValue> where TValue : IConvertible
    {
        private readonly Func<TValue> _defaultValueCreator;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleArgumentKey{TValue}"></see> class.
        /// </summary>
        public SimpleArgumentKey(string key, Func<TValue> defaultValueCreator) : base(key)
        {
            _defaultValueCreator = defaultValueCreator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleArgumentKey{TValue}"></see> class.
        /// </summary>
        public SimpleArgumentKey(string key) : this(key, null)
        {
        }

        #region Overrides of ArgumentKey<TValue>

        /// <summary>
        /// Creates the default value which is returned when 
        /// no target value was found.
        /// </summary>
        protected override TValue DefaultValue()
        {
            return _defaultValueCreator != null ? _defaultValueCreator() : default(TValue);
        }

        /// <summary>
        /// Hook method must be overridden in order to perform the actual extraction.
        /// </summary>
        /// <param name="collection">The collection to extract the values from.</param>
        /// <returns>The extracted value.</returns>
        protected override TValue ExtractValue(ICollection<string> collection)
        {
            Debug.Assert(collection.Count > 0);

            return (TValue) Convert.ChangeType(
                                collection.ElementAt(0),
                                typeof (TValue));
        }

        #endregion
    }
}