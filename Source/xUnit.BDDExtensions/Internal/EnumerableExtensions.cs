//  Copyright 2010 xUnit.BDDExtensions
//    
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License. 
//  You may obtain a copy of the License at
//    
//        http://www.apache.org/licenses/LICENSE-2.0
//    
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
//  implied. See the License for the specific language governing permissions and
//  limitations under the License.  
//  
using System;
using System.Collections.Generic;
using System.Linq;

namespace Xunit.Internal
{
    public static class EnumerableExtensions
    {
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            Guard.AgainstArgumentNull(action, "action");

            if (enumerable != null)
            {
                foreach (var item in enumerable)
                {
                    action(item);
                }
            }
        }

        public static T FirstOrCustomDefaultValue<T>(this IEnumerable<T> enumerable, T customDefaultValue)
        {
            Guard.AgainstArgumentNull(enumerable, "enumerable");

            var collection = enumerable.AlternativeIfNullOrEmpty(Enumerable.Empty<T>);
            var firstValue = collection.FirstOrDefault();

            return Equals(firstValue, default(T)) ? customDefaultValue : firstValue;
        }

        public static IEnumerable<T> AlternativeIfNullOrEmpty<T>(
            this IEnumerable<T> enumerable,
            Func<IEnumerable<T>> alternativeSelector)
        {
            Guard.AgainstArgumentNull(enumerable, "enumerable");
            Guard.AgainstArgumentNull(alternativeSelector, "alternativeSelector");

            return enumerable == null || !enumerable.Any() ? alternativeSelector() : enumerable;
        }
    }
}