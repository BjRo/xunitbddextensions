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
namespace Xunit.Reporting.Specs.Core
{
    [Concern(typeof(string))]
    public class After_this__fake__specification_has_been_executed : StaticContextSpecification
    {
        protected override void Because()
        {
        }

        [Observation(Skip = "This is just an example for testing purposes")]
        public void It_should_effectively_do__nothing__()
        {
        }
    }

    public class Foo_bar_derived_concern: After_this__fake__specification_has_been_executed
    {
        [Observation]
        public void Just_for_nothing()
        {
            
        }
    }
}