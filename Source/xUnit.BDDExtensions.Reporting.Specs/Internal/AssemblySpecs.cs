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
using Xunit.Reporting.Internal;

namespace Xunit.Reporting.Specs.Internal
{
    [Concern(typeof (Assembly))]
    public class When_trying_to_find_all_types_in_an_assembly_matching_a_specified_criteria : StaticContextSpecification
    {
        private Assembly _assembly;
        private Func<Type, bool> _specificationOnlyMatchingThisContextClass;
        private IEnumerable<Type> _allTypesMatchingTheCriteria;
        private Type _theCurrentType;

        protected override void EstablishContext()
        {
            _theCurrentType = GetType();
            _assembly = new Assembly(_theCurrentType.Assembly);
            _specificationOnlyMatchingThisContextClass = (type => Equals(type, _theCurrentType));
        }

        protected override void Because()
        {
            _allTypesMatchingTheCriteria = _assembly.AllTypesMatching(_specificationOnlyMatchingThisContextClass);
        }

        [Observation]
        public void Should_find_all_types_matching_the_criteria()
        {
            _allTypesMatchingTheCriteria.ShouldOnlyContain(_theCurrentType);
        }
    }

    [Concern(typeof (Assembly))]
    public class When_trying_to_read_the_name_of_an_assembly : StaticContextSpecification
    {
        private Assembly _assembly;
        private string _assemblyName;
        private Type _theCurrentType;

        protected override void EstablishContext()
        {
            _theCurrentType = GetType();
            _assembly = new Assembly(_theCurrentType.Assembly);
        }

        protected override void Because()
        {
            _assemblyName = _assembly.Name;
        }

        [Observation]
        public void Should_return_the_short_name_of_the_assembly()
        {
            _assemblyName.ShouldBeEqualTo(_theCurrentType.Assembly.GetName().Name);
        }
    }
}