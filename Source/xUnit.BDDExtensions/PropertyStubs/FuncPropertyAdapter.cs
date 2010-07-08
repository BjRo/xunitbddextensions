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

namespace Xunit.PropertyStubs
{
    internal class FuncPropertyAdapter<TMock, TProperty> : IPropertyAdapter
        where TMock : class
        where TProperty : class
    {
        private readonly TMock _mock;
        private readonly Expression<Func<TMock, TProperty>> _accessor;

        public FuncPropertyAdapter(TMock mock, Expression<Func<TMock, TProperty>> accessor)
        {
            _mock = mock;
            _accessor = accessor;
        }

        public Type PropertyType
        {
            get { return typeof(TProperty); }
        }

        public void Stub(object propertyValue)
        {
            _mock.WhenToldTo(_accessor).Return((TProperty)propertyValue);
        }
    }
}
