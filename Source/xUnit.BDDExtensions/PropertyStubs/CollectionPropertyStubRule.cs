// Copyright 2010 xUnit.BDDExtensions
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit.Internal;

namespace Xunit.PropertyStubs
{
    internal class CollectionPropertyStubRule : IPropertyStubRule
    {
        public bool CanStub(Type propertyType)
        {
            if (!propertyType.IsGenericType)
            {
                return false;
            }

            var generic = propertyType.GetGenericTypeDefinition();
            return generic == typeof(IList<>)
                || generic == typeof(ICollection<>)
                || generic == typeof(IEnumerable<>);
        }

        public void Stub(IPropertyAdapter property)
        {
            var itemType = property.PropertyType.GetGenericArguments()[0];
            var list = CreateListOf(itemType);
            PopulateList(list, itemType);
            property.Stub(list);
        }

        private static IList CreateListOf(Type itemType)
        {
            var listType = typeof(List<>).MakeGenericType(itemType);
            return (IList)Activator.CreateInstance(listType);
        }

        private static void PopulateList(IList list, Type itemType)
        {
            Enumerable.Range(0, 3)
                .Where(x => itemType.IsInterface)
                .Select(x => RunnerConfiguration.FakeEngine.Stub(itemType))
                .ToList()
                .ForEach(x => list.Add(x));
        }
    }
}
