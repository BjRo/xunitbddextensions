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
namespace Xunit.Reporting.Internal.Generator
{
    /// <summary>
    ///   A simple helper class for pluralizing names.
    /// </summary>
    public class Pluralizer
    {
        /// <summary>
        ///   Takes the name specified via <paramref name = "name" />
        ///   and appends an "s" to it in case the value 
        ///   specified via <paramref name = "value" /> is greater than 1.
        /// </summary>
        /// <param name = "name">
        ///   Specifies the name to pluralize.
        /// </param>
        /// <param name = "value">
        ///   Specifies the value to decide on.
        /// </param>
        /// <returns>
        ///   The formatted string.
        /// </returns>
        public string Pluralize(string name, int value)
        {
            return value > 1 ? string.Concat(name, "s") : name;
        }
    }
}