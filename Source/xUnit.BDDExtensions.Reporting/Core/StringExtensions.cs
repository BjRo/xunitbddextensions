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
using System.Text.RegularExpressions;

namespace Xunit.Reporting.Core
{
    /// <summary>
    /// An extension class for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Creates a string in which all underscores contained
        /// in the input string are replaced with spaces.
        /// </summary>
        /// <param name="input">
        /// Specifies the input string.
        /// </param>
        /// <returns>
        /// The formatted string.
        /// </returns>
        public static string ReplaceUnderscoresWithSpaces(this string input)
        {
            return input.Replace('_', ' ');
        }

        /// <summary>
        /// Creates a string in which the first letter of the input string is capitalized.        
        /// </summary>
        /// <param name="input">
        /// Specifies the input string.
        /// </param>
        /// <returns>
        /// The formatted string.
        /// </returns>
        public static string CapitalizeFirstLetter(this string input)
        {
            var firstChar = input[0];

            if (!Char.IsLetter(firstChar) || Char.IsUpper(firstChar))
            {
                return input;
            }

            return string.Concat(
                Char.ToUpper(firstChar),
                input.Substring(1));
        }

        /// <summary>
        /// Creates a string in which the first letter of the input string is lower cased.        
        /// </summary>
        /// <param name="input">
        /// Specifies the input string.
        /// </param>
        /// <returns>
        /// The formatted string.
        /// </returns>
        public static string LowerCaseFirstLetter(this string input)
        {
            var firstChar = input[0];

            if (!Char.IsLetter(firstChar) || Char.IsLower(firstChar))
            {
                return input;
            }

            return string.Concat(
                Char.ToLower(firstChar),
                input.Substring(1));
        }

        /// <summary>
        /// Creates a string in which all double underscores contained
        /// in the input string are replaced with double quotes.
        /// </summary>
        /// <param name="input">
        /// Specifies the input string.
        /// </param>
        /// <returns>
        /// The formatted string.
        /// </returns>
        public static string ReplaceDoubleUnderscoresWithDoubleQuotes(this string input)
        {
            return Regex.Replace(input, @"(?<quoted>__(?<inner>\w+?)__)", " \"${inner}\" ");
        }
    }
}