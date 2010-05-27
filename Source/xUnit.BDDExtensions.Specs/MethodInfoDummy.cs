// Copyright 2009 Björn Rochel - http://www.bjro.de/ 
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
using System.Reflection;
using Xunit.Sdk;

namespace Xunit.Specs
{
    public class MethodInfoDummy : IMethodInfo
    {
        public object CreateInstance()
        {
            return null;
        }

        public IEnumerable<IAttributeInfo> GetCustomAttributes(Type attributeType)
        {
            return new List<IAttributeInfo>();
        }

        public bool HasAttribute(Type attributeType)
        {
            return false;
        }

        public void Invoke(object testClass, params object[] parameters)
        {
            
        }

        public bool IsAbstract
        {
            get { return false; }
        }

        public bool IsStatic
        {
            get { return false; }
        }

        public MethodInfo MethodInfo
        {
            get { return null; }
        }

        public string Name
        {
            get { return string.Empty; }
        }

        public string ReturnType
        {
            get { return string.Empty; }
        }

        public string TypeName
        {
            get { return string.Empty; }
        }
    }
}