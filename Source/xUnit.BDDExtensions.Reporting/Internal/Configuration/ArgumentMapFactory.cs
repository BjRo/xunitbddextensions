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
using System.Diagnostics;
using System.Text.RegularExpressions;
using Xunit.Internal;

namespace Xunit.Reporting.Internal.Configuration
{
    /// <summary>
    ///   A mapper class which is responsible for mapping console arguments to 
    ///   a one to many map for arguments.
    /// </summary>
    public class ArgumentMapFactory : IArgumentMapFactory
    {
        private readonly Func<IEnumerable<string>> _argsFactory;

        /// <summary>
        ///   Creates a new instance of the <see <see cref = "ArgumentMapFactory" />
        /// </summary>
        /// <param name = "argsFactory">
        ///   Specifies a delegate for obtaining the console arguments.
        /// </param>
        public ArgumentMapFactory(Func<IEnumerable<string>> argsFactory)
        {
            Guard.AgainstArgumentNull(argsFactory, "argsFactory");

            _argsFactory = argsFactory;
        }

        #region IArgumentMapFactory Members

        /// <summary>
        ///   A factory for creating an <see cref = "IArgumentMap" /> instance.
        /// </summary>
        public IArgumentMap Create()
        {
            var consoleArgs = _argsFactory();

            Debug.Assert(consoleArgs != null);

            return Map(consoleArgs);
        }

        #endregion

        /// <summary>
        ///   Maps the collection of strings to a dictionary of arguments
        /// </summary>
        /// <param name = "input">Specifies the argsFactory collection.</param>
        /// <returns>Returns the mapped instance.</returns>
        private static IArgumentMap Map(IEnumerable<string> input)
        {
            var argumentMap = new ArgumentMap();

            foreach (var inputString in input)
            {
                var match = Regex.Match(inputString, @"^/(?<argumentName>\w+):(?<argumentValue>[\w\W]+)");

                if (!match.Success)
                {
                    throw new ArgumentException(
                        "Recieved malformatted arguments. Unable to proceed . . .");
                }

                AddToDictionary(
                    match.Groups["argumentName"].Value,
                    match.Groups["argumentValue"].Value.Trim('\''),
                    argumentMap);
            }

            return argumentMap;
        }

        private static void AddToDictionary(
            string key,
            string value,
            IDictionary<string, ICollection<string>> dictionary)
        {
            ICollection<string> targetCollection;

            if (!dictionary.TryGetValue(key, out targetCollection))
            {
                targetCollection = new List<string>();
                dictionary.Add(key, targetCollection);
            }

            if (!targetCollection.Contains(value))
            {
                targetCollection.Add(value);
            }
        }
    }
}