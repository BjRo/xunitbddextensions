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
    [Concern(typeof(Arguments))]
    public class When_queried_with_a_key_and_the_value_exists_in_the_underlying_data_source : StaticContextSpecification
    {
        private ArgumentKey<string> key;
        private IArguments propertyBag;
        private IArgumentMap propertyDictionary;
        private string value;

        protected override void EstablishContext()
        {
            propertyDictionary = new ArgumentMap
            {
                { "SomeKey", new[] { "SomeValue" }}
            };

            key = new SimpleArgumentKey<string>("SomeKey");
            propertyBag = new Arguments(propertyDictionary);
        }

        protected override void Because()
        {
            value = propertyBag.Get(key);
        }

        [Observation]
        public void Should_be_able_to_retrieve_the_related_value()
        {
            value.ShouldBeEqualTo("SomeValue");
        }
    }
}