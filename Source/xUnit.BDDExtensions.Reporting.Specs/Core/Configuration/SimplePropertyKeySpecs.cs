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
using Xunit.Reporting.Core.Configuration;

namespace Xunit.Reporting.Specs.Core.Configuration
{
    [Concern(typeof(SimpleArgumentKey<>))]
    public class When_reading_an_existing_string_value_from_a_map_of_arguments : StaticContextSpecification
    {
        private ArgumentKey<string> argumentKey;
        private IArgumentMap argumentMap;
        private string value;

        protected override void EstablishContext()
        {
            argumentMap = new ArgumentMap
            {
                { "Foo", new[] { "Bar" }}
            };

            argumentKey = new SimpleArgumentKey<string>("Foo");
        }

        protected override void Because()
        {
            value = argumentKey.ParseValue(argumentMap);
        }

        [Observation]
        public void Should_be_able_to_obtain_the_correct_value_for_the_specified_key_from_the_map()
        {
            value.ShouldBeEqualTo("Bar");
        }
    }

    [Concern(typeof(SimpleArgumentKey<>))]
    public class When_reading_an_existing_int_value_from_a_map_of_arguments : StaticContextSpecification
    {
        private ArgumentKey<int> argumentKey;
        private IArgumentMap argumentMap;
        private int value;

        protected override void EstablishContext()
        {
            argumentMap = new ArgumentMap
            {
                { "Foo", new[] {"22"}}
            };

            argumentKey = new SimpleArgumentKey<int>("Foo");
        }

        protected override void Because()
        {
            value = argumentKey.ParseValue(argumentMap);
        }

        [Observation]
        public void Should_be_able_to_obtain_the_correct_value_for_the_specified_key_from_the_map()
        {
            value.ShouldBeEqualTo(22);
        }
    }
}