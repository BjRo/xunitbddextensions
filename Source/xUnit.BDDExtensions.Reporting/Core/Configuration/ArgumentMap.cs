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
using System.Collections.Generic;

namespace Xunit.Reporting.Core.Configuration
{
    /// <summary>
    /// A data container which maps an argument key to a collection of argument
    /// values. 
    /// </summary>
    /// <remarks>
    /// This class was only introduced to have a short cut for ommitting the angle brackets
    /// of a generic <see cref="IDictionary{TKey,TValue}"/>.
    /// </remarks>
    public class ArgumentMap : Dictionary<string, ICollection<string>>, IArgumentMap
    {
    }
}