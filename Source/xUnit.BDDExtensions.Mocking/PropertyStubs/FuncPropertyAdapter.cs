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
using System.Text;

using Rhino.Mocks;

namespace Xunit.PropertyStubs
{
    internal class FuncPropertyAdapter<TMock, TProperty> : IPropertyAdapter
        where TMock : class
        where TProperty : class
    {
        private readonly TMock _mock;
        private readonly Function<TMock, TProperty> _accessor;

        public FuncPropertyAdapter(TMock mock, Function<TMock, TProperty> accessor)
        {
            this._mock = mock;
            this._accessor = accessor;
        }

        public Type PropertyType
        {
            get { return typeof(TProperty); }
        }

        public void Stub(object propertyValue)
        {
            _mock.Stub(_accessor).Return((TProperty)propertyValue);
        }
    }
}
