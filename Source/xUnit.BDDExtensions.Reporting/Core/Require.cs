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

namespace Xunit.Reporting.Core
{
    /// <summary>
    /// A simple class for argument checking.
    /// </summary>
    public class Require
    {
        /// <summary>
        /// Checks whether the argument supplied by <paramref name="argument"/>
        /// is <c>null</c>.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="argument"/> is <c>null</c>.
        /// </exception>
        public static void ArgumentNotNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Checks whether the argument supplied by <paramref name="argument"/>
        /// is <c>null</c> or an empty <see cref="string"/>.
        /// </summary>
        /// <param name="argument">The name.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="argument"/> is <c>null</c> or an empty string.
        /// </exception>
        public static void ArgumentNotNullOrEmptyString(string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentException(
                    string.Format(
                        "Argument {0} must not be null or an empty string",
                        argumentName));
            }
        }
    }
}