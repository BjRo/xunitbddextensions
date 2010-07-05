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
using System.Collections.Generic;
using Xunit.Reporting.Internal.Configuration;

namespace Xunit.Reporting.Specs.Internal.Configuration
{
    [Concern(typeof(CollectionArgumentKey<>))]
    public class When_reading_an_existing_set_of_string_values_from_a_map_of_arguments : StaticContextSpecification
    {
        private IArgumentMap argumentMap;
        private IEnumerable<string> value;
        private ArgumentKey<IEnumerable<string>> argumentKey;

        protected override void EstablishContext()
        {
            argumentMap = new ArgumentMap
            {
                { "Go", new[] { "Go", "Gadgetto", "mat" }},
                { "No", new[] { "more", "heroes" }}
            };

            argumentKey = new CollectionArgumentKey<string>("Go");
        }

        protected override void Because()
        {
            value = argumentKey.ParseValue(argumentMap);
        }

        [Observation]
        public void Should_be_able_to_obtain_the_collection_for_the_specified_key_from_the_map()
        {
            value.ShouldOnlyContain("Go", "Gadgetto", "mat");
        }
    }

    [Concern(typeof(CollectionArgumentKey<>))]
    public class When_reading_an_from_an_empty_map_of_arguments : StaticContextSpecification
    {
        private IArgumentMap argumentMap;
        private IEnumerable<string> value;
        private ArgumentKey<IEnumerable<string>> argumentKey;

        protected override void EstablishContext()
        {
            argumentMap = new ArgumentMap();
            argumentKey = new CollectionArgumentKey<string>("Go");
        }

        protected override void Because()
        {
            value = argumentKey.ParseValue(argumentMap);
        }

        [Observation]
        public void Should_return_an_empty_collection()
        {
            value.ShouldNotBeNull();
        }
    }

    [Concern(typeof(CollectionArgumentKey<>))]
    public class When_reading_an_existing_set_of_int_values_from_a_from_a_map_of_arguments : StaticContextSpecification
    {
        private IEnumerable<int> value;
        private ArgumentKey<IEnumerable<int>> argumentKey;
        private IArgumentMap argumentMap;

        protected override void EstablishContext()
        {
            argumentMap = new ArgumentMap
            {
                { "Go", new[] { "1", "2", "3" }}
            };

            argumentKey = new CollectionArgumentKey<int>("Go");
        }

        protected override void Because()
        {
            value = argumentKey.ParseValue(argumentMap);
        }

        [Observation]
        public void Should_be_able_to_obtain_the_values_for_the_specified_key_from_the_map()
        {
            value.ShouldOnlyContain(1, 2, 3);
        }
    }
}