// Copyright 2010 Björn Rochel - http://www.bjro.de/ 
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

namespace Xunit.Internal
{
    /// <summary>
    /// A helper class for argument checking.
    /// </summary>
    public class Guard
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> in case the 
        /// argument specified via <paramref name="argument"/> is <c>null</c>.
        /// </summary>
        /// <param name="argument">Specifies the argument</param>
        /// <param name="argumentName">Specifies the arguments name.</param>
        public static void AgainstArgumentNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}