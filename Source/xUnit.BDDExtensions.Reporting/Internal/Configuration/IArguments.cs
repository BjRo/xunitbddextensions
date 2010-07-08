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

namespace Xunit.Reporting.Internal.Configuration
{
    /// <summary>
    ///   Interface for strong type argument handling. 
    ///   This abstraction serves together with classes derived from <see cref = "ArgumentKey{TValue}" /> as a 
    ///   simple strong typed property container.
    /// </summary>
    public interface IArguments
    {
        /// <summary>
        ///   Gets the value for the argument key specified by <paramref name = "key" />.
        /// </summary>
        /// <typeparam name = "TValue">
        ///   Specifies the type of the concrete value.
        /// </typeparam>
        /// <param name = "key">
        ///   Specifies the key for the argument value.
        /// </param>
        /// <returns>
        ///   The found value or the default value for <typeparamref name = "TValue" /> when
        ///   no value was specified.
        /// </returns>
        /// <exception cref = "ArgumentNullException">
        ///   Thrown when <paramref name = "key" /> is <c>null</c>.
        /// </exception>
        TValue Get<TValue>(ArgumentKey<TValue> key);
    }
}