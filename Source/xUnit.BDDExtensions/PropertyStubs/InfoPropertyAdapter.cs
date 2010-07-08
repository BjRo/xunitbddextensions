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
using System.Linq.Expressions;
using System.Reflection;

namespace Xunit.PropertyStubs
{
    internal class InfoPropertyAdapter<TMock> : IPropertyAdapter where TMock : class
    {
        private readonly TMock _mock;
        private readonly PropertyInfo _property;

        public InfoPropertyAdapter(TMock mock, PropertyInfo property)
        {
            _mock = mock;
            _property = property;
        }

        public Type PropertyType
        {
            get { return _property.PropertyType; }
        }

        public void Stub(object propertyValue)
        {
            var actionParameter = Expression.Parameter(typeof(TMock), "x");
            var propertyGetter = Expression.Property(actionParameter, _property);

            var func = typeof(Func<,>).MakeGenericType(typeof (TMock), _property.PropertyType);
            var lamda = Expression.Lambda(func, propertyGetter, actionParameter);
            var compiledDelegate = lamda.Compile();

            _mock.WhenToldTo(m => compiledDelegate.DynamicInvoke(m)).Return(propertyValue);
        }
    }
}
