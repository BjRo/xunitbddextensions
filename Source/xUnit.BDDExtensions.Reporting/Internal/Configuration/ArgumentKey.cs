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
using Xunit.Internal;

namespace Xunit.Reporting.Internal.Configuration
{
    /// <summary>
    ///   Abstract key instance for the <see cref = "IArguments" /> abstraction. Purpose 1 of this class is
    ///   to aid the C# compiler with the type inference in order to omit the generic type arguments when using
    ///   <see cref = "IArguments" /> implementations. Purpose 2 is generic value parsing.
    /// </summary>
    public abstract class ArgumentKey<TValue>
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "ArgumentKey{TValue}"></see> class.
        /// </summary>
        /// <param name = "key">
        ///   The string key.
        /// </param>
        /// <exception cref = "ArgumentException">
        ///   Thrown when <paramref name = "key" /> is <c>null</c> or empty.
        /// </exception>
        protected ArgumentKey(string key)
        {
            Guard.AgainstNullOrEmptyString("key", key);

            Key = key;
        }

        /// <summary>
        ///   Gets the assigned argument key.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        ///   Tries to obtain the value related to this key instance
        ///   from the supplied <see cref = "IArgumentMap" /> instance.
        /// </summary>
        /// <param name = "argumentMap">
        ///   Specifies a one to many map of arguments.
        /// </param>
        /// <returns>
        ///   The parsed value or the default value of the type specified by <typeparamref name = "TValue" />.
        /// </returns>
        public TValue ParseValue(IArgumentMap argumentMap)
        {
            ICollection<string> valueCollection;

            if (!argumentMap.TryGetValue(Key, out valueCollection))
            {
                return DefaultValue();
            }

            return ExtractValue(valueCollection);
        }

        /// <summary>
        ///   Hook method must be overridden in order to determine the default value in case no
        ///   data was found in an <see cref = "IArgumentMap" />.
        /// </summary>
        protected abstract TValue DefaultValue();

        /// <summary>
        ///   Hook method must be overridden in order to obtain the argument value from the supplied
        ///   collection.
        /// </summary>
        /// <param name = "collection">
        ///   A collection of argument values.
        /// </param>
        /// <returns>
        ///   The obtained value.
        /// </returns>
        protected abstract TValue ExtractValue(ICollection<string> collection);
    }
}