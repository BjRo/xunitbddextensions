// Copyright 2009 
//
// Björn Rochel:     http://www.bjro.de/
// Sergey Shishkin:  http://sergeyshishkin.spaces.live.com/
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
using System.Linq.Expressions;
using System.Reflection;
using Rhino.Mocks;

namespace Xunit
{
    public static class PropertyStubExtensions
    {
        public static TMock Has<TMock, TProperty>(
            this TMock mock,
            Function<TMock, TProperty> propertySelector)
            where TMock : class
            where TProperty : class
        {
            var propertyStub = MockRepository.GenerateStub<TProperty>();

            mock.Stub(propertySelector).Return(propertyStub);

            return mock;
        }

        public static void HasProperties<TMock>(this TMock mock) where TMock : class
        {
            foreach (var property in GetPropertiesToStub(typeof (TMock)))
            {
                StubProperty(mock, property);
            }
        }

        private static IEnumerable<PropertyInfo> GetPropertiesToStub(Type type)
        {
            foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var propertyType = property.PropertyType;

                if (propertyType.IsInterface && !typeof (IEnumerable).IsAssignableFrom(propertyType))
                {
                    yield return property;
                }
            }
        }

        private static void StubProperty<TMock>(TMock mock, PropertyInfo property) where TMock : class
        {
            var actionParameter = Expression.Parameter(typeof (TMock), "x");
            var propertyGetter = Expression.Property(actionParameter, property);
            var action = Expression.Lambda<Action<TMock>>(propertyGetter, actionParameter);

            var propertyStub = MockRepository.GenerateStub(property.PropertyType);

            mock.Stub(action.Compile()).Return(propertyStub);
        }
    }
}