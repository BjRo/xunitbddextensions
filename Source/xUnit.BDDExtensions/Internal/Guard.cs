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

namespace Xunit.Internal
{
    public static class Guard
    {
        public static void AgainstArgumentNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void AgainstNullOrEmptyString(string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentException(
                    string.Format(
                        "Argument {0} must not be null or an empty string",
                        argumentName));
            }
        }

        public static void ArgumentAssignableTo(Type argument, Type assignmentTargetType)
        {
            if (!assignmentTargetType.IsAssignableFrom(argument))
            {
                throw new ArgumentException(
                    string.Format("Type {0} is not assignable to the type {1}", 
                        argument.FullName, 
                        assignmentTargetType.FullName));
            }
        }
    }
}