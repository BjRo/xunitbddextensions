// Copyright 2008 Björn Rochel - http://www.bjro.de/ 
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Xunit.Reporting.Internal
{
    /// <summary>
    /// Interface for a formatter.
    /// </summary>
    public interface IFormatter
    {
        /// <summary>
        /// Formats the string supplied by <paramref name="content"/>.
        /// </summary>
        /// <param name="content">
        /// Specifies the content to format.
        /// </param>
        /// <returns>
        /// The formatted content.
        /// </returns>
        string Format(string content);        
    }
}