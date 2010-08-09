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
using System.Collections.Generic;

namespace Xunit.Specs
{
    [Concern(typeof(PropertyStubExtensions))]
    public class When_stubbing_properties_of_collection_types : StaticContextSpecification
    {
        private IHaveCollectionsToStub _mock;

        protected override void EstablishContext()
        {
            _mock = An<IHaveCollectionsToStub>();
        }

        protected override void Because()
        {
            _mock.HasProperties();
        }

        [Observation]
        public void Should_not_stub_concrete_collections()
        {
            _mock.ConcreteList.ShouldBeNull();
        }

        [Observation]
        public void Should_stub_generic_collections_of_concrete_types_empty()
        {
            _mock.ListOfInt.Count.ShouldBeEqualTo(0);
        }

        [Observation]
        public void Should_stub_generic_collections_of_interface_types_with_3_items()
        {
            _mock.ListOfInterface.Count.ShouldBeEqualTo(3);
        }

        [Observation]
        public void Should_not_stub_custom_derived_collections()
        {
            _mock.CustomList.ShouldBeNull();
        }
    }

    #region Test interfaces

    public interface IHaveCollectionsToStub
    {
        List<int> ConcreteList { get; }

        IList<int> ListOfInt { get; }

        IList<IWantToBeStubbed> ListOfInterface { get; }

        IImplementList<int> CustomList { get; }
    }

    public interface IImplementList<T> : IList<T> { }

    #endregion
}