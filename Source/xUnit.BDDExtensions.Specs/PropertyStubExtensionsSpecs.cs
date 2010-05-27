// Copyright 2009 
//
// Björn Rochel:     http://www.bjro.de/
// Sergey Shishkin:  http://sergeyshishkin.spaces.live.com/
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

namespace Xunit.Specs
{
    [Concern(typeof (PropertyStubExtensions))]
    public class When_stubbing_properties : StaticContextSpecification
    {
        private IHavePropertiesToStub mock;

        protected override void EstablishContext()
        {
            mock = An<IHavePropertiesToStub>();
        }

        protected override void Because()
        {
            mock.HasProperties();
        }

        [Observation]
        public void Should_have_stub_behavior()
        {
            mock.PrimitiveProperty = "text";

            mock.PrimitiveProperty.ShouldBeEqualTo("text");
        }

        [Observation]
        public void Should_auto_mock_interface_type_properties()
        {
            mock.Component.ShouldNotBeNull();
        }

        [Observation]
        public void Interface_type_properties_should_have_stub_behavior()
        {
            mock.Component.TextProperty = "text";
            mock.Component.TextProperty.ShouldBeEqualTo("text");
        }

        [Observation]
        public void Should_not_generate_stubs_for_properties_of_properties()
        {
            mock.Component.Child.ShouldBeNull();
        }
    }

    #region Test interfaces

    public interface IHavePropertiesToStub
    {
        string PrimitiveProperty { get; set; }

        IWantToBeStubbed Component { get; }

        IList<int> DontStubThis { get; }
    }

    public interface IWantToBeStubbed
    {
        string TextProperty { get; set; }

        IWantToBeStubbed Child { get; }
    }

    #endregion
}