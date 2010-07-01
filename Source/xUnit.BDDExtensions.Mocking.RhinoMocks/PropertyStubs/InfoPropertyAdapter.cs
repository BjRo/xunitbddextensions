// Copyright 2009 
//
// Björn Rochel:     http://www.bjro.de/
// Maxim Tansin
// Sergey Shishkin:  http://shishkin.org/
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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Rhino.Mocks;

namespace Xunit.PropertyStubs
{
    internal class InfoPropertyAdapter<TMock> : IPropertyAdapter
        where TMock : class
    {
        private readonly TMock _mock;
        private readonly PropertyInfo _property;

        public InfoPropertyAdapter(TMock mock, PropertyInfo property)
        {
            this._mock = mock;
            this._property = property;
        }

        public Type PropertyType
        {
            get { return _property.PropertyType; }
        }

        public void Stub(object propertyValue)
        {
            var actionParameter = Expression.Parameter(typeof(TMock), "x");
            var propertyGetter = Expression.Property(actionParameter, _property);
            var action = Expression.Lambda<Action<TMock>>(propertyGetter, actionParameter);

            _mock.Stub(action.Compile()).Return(propertyValue);
        }
    }
}
