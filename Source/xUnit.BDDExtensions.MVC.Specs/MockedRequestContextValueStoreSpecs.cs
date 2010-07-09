// Copyright 2010 xUnit.BDDExtensions
//   
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   
//       http://www.apache.org/licenses/LICENSE-2.0
//   
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//  
namespace Xunit.Specs
{
    [Concern(typeof (MockedRequestContextValueStore))]
    public class When_setting_a_value_for_an_instance_in_the_dictionary : StaticContextSpecification
    {
        private IMockedRequestContext _instance;
       
        protected override void EstablishContext()
        {
            _instance = An<IMockedRequestContext>();
        }

        protected override void Because()
        {
            _instance.SetValue("Key", "wert1");
        }

        [Observation]
        public void Should_be_able_to_retrieve_the_value_with_the_key_and_the_related_instance()
        {
            _instance.GetValue<string>("Key").ShouldBeEqualTo("wert1");
        }

        [Observation]
        public void Should_not_be_able_to_retrieve_a_value_with_the_key_and_and_an_unrelated_instance()
        {
            var otherInstance = An<IMockedRequestContext>();
            string value;
            otherInstance.TryGetValue("Key",out value).ShouldBeFalse();
            value.ShouldBeNull();
        }
    }
}
