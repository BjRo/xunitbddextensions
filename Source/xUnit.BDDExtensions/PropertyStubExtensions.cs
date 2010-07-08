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
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Xunit.PropertyStubs;

namespace Xunit
{
    public static class PropertyStubExtensions
    {
        private static readonly IPropertyStubRule[] Rules = new IPropertyStubRule[]
        {
            new CollectionPropertyStubRule(),
            new DefaultPropertyStubRule(),
            new TerminalPropertyStubRule()
        };

        public static TMock Has<TMock, TProperty>(
            this TMock mock,
            Expression<Func<TMock, TProperty>> propertySelector)
            where TMock : class
            where TProperty : class
        {
            StubProperty(new FuncPropertyAdapter<TMock, TProperty>(mock, propertySelector));
            return mock;
        }

        public static void HasProperties<TMock>(this TMock mock) where TMock : class
        {
            typeof (TMock)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Select(property => new InfoPropertyAdapter<TMock>(mock, property))
                .ToList()
                .ForEach(StubProperty);
        }

        private static void StubProperty(IPropertyAdapter property)
        {
            Rules
                .First(x => x.CanStub(property.PropertyType))
                .Stub(property);
        }
    }
}