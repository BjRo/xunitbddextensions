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
using System.ComponentModel.Design;
using Xunit.Sdk;

namespace Xunit.Specs.BDDExtensions
{
    public abstract class Concern_for_BDDExtensions : StaticContextSpecification
    {
        protected Action theAssertion;
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_an__Exception__is_expected_to_be_thrown_and_is_actually_thrown :
        Concern_for_BDDExtensions
    {
        private Action operationThatThrows;

        protected override void EstablishContext()
        {
            operationThatThrows = () =>
            {
                throw new InvalidOperationException();
            };
        }

        protected override void Because()
        {
            theAssertion = () => operationThatThrows.ShouldThrowAn<InvalidOperationException>();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_an_instance_is_expected_to_be__null__and_is_actually__null__ : Concern_for_BDDExtensions
    {
        private object existingObject;

        protected override void EstablishContext()
        {
            existingObject = null;
        }

        protected override void Because()
        {
            theAssertion = () => existingObject.ShouldBeNull();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_an_instance_is_expected_to_be__null__but_it_is_not : Concern_for_BDDExtensions
    {
        private object existingObject;

        protected override void EstablishContext()
        {
            existingObject = new object();
        }

        protected override void Because()
        {
            theAssertion = () => existingObject.ShouldBeNull();
        }

        [Observation]
        public void Should_throw_a__NullException__()
        {
            theAssertion.ShouldThrowAn<NullException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_null_to_be_not__null__ : Concern_for_BDDExtensions
    {
        private object existingObject;

        protected override void EstablishContext()
        {
            existingObject = null;
        }

        protected override void Because()
        {
            theAssertion = () => existingObject.ShouldNotBeNull();
        }

        [Observation]
        public void Should_throw_an__NotNullException__()
        {
            theAssertion.ShouldThrowAn<NotNullException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_an_instance_is_expected_to_be_not__null__ : Concern_for_BDDExtensions
    {
        private object existingObject;

        protected override void EstablishContext()
        {
            existingObject = new object();
        }

        protected override void Because()
        {
            theAssertion = () => existingObject.ShouldNotBeNull();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_a_particular__Exception__to_be_thrown_and_no__Exception__was_thrown :
            Concern_for_BDDExtensions
    {
        private Action operationNotThrowingAnException;

        protected override void EstablishContext()
        {
            operationNotThrowingAnException = () =>
            {
            };
        }

        protected override void Because()
        {
            theAssertion = () => operationNotThrowingAnException.ShouldThrowAn<ArgumentNullException>();
        }

        [Observation]
        public void Should_throw_a__ThrowsException__()
        {
            theAssertion.ShouldThrowAn<ThrowsException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_a_particular__Exception__to_be_thrown_and_a_different__Exception__was_thrown :
            Concern_for_BDDExtensions
    {
        private Action operationThatThrowsADifferentException;

        protected override void EstablishContext()
        {
            operationThatThrowsADifferentException = () =>
            {
                throw new InvalidOperationException();
            };
        }

        protected override void Because()
        {
            theAssertion = () => operationThatThrowsADifferentException.ShouldThrowAn<ArgumentNullException>();
        }

        [Observation]
        public void Should_throw_a__ThrowsException__()
        {
            theAssertion.ShouldThrowAn<ThrowsException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting__False__to_be__False__ : Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            theAssertion = () => false.ShouldBeFalse();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting__True__to_be__True__ : Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            theAssertion = () => true.ShouldBeTrue();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting__True__to_be__False__ : Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            theAssertion = () => true.ShouldBeFalse();
        }

        [Observation]
        public void Should_throw_a__FalseException__()
        {
            theAssertion.ShouldThrowAn<FalseException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting__False__to_be__True__ : Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            theAssertion = () => false.ShouldBeTrue();
        }

        [Observation]
        public void Should_throw_a__TrueException__()
        {
            theAssertion.ShouldThrowAn<TrueException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_a_collection_to_contain_only_a_particular_set_of_items_and_a_different_set_is_present :
            Concern_for_BDDExtensions
    {
        private string bar;
        private List<string> collectionToTest;
        private string foo;

        protected override void EstablishContext()
        {
            foo = "foo";
            bar = "bar";
            collectionToTest = new List<string>
            {
                foo,
                bar
            };
        }

        protected override void Because()
        {
            theAssertion = () => collectionToTest.ShouldOnlyContain("SomethingDifferent");
        }

        [Observation]
        public void Should_throw_an__AssertException__()
        {
            theAssertion.ShouldThrowAn<AssertException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_a_collection_to_contain_only_a_particular_set_of_items_and_more_are_present :
        Concern_for_BDDExtensions
    {
        private string bar;
        private List<string> collectionToTest;
        private string foo;
        private string unexpected;

        protected override void EstablishContext()
        {
            foo = "foo";
            bar = "bar";
            unexpected = "unexpected";
            collectionToTest = new List<string>
            {
                foo,
                bar,
                unexpected
            };
        }

        protected override void Because()
        {
            theAssertion = () => collectionToTest.ShouldOnlyContain(foo, bar);
        }

        [Observation]
        public void Should_throw_an__AssertException__()
        {
            theAssertion.ShouldThrowAn<AssertException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_a_collection_to_contain_only_the_items_it_actually_contains :
            Concern_for_BDDExtensions
    {
        private string bar;
        private List<string> collectionToTest;
        private string foo;

        protected override void EstablishContext()
        {
            foo = "foo";
            bar = "bar";
            collectionToTest = new List<string>
            {
                foo,
                bar
            };
        }

        protected override void Because()
        {
            theAssertion = () => collectionToTest.ShouldOnlyContain(foo, bar);
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_a_collection_to_contain_only_a_particular_set_in_a_particular_order_and_different_set_is_present :
            Concern_for_BDDExtensions
    {
        private string bar;
        private List<string> collectionToTest;
        private string foo;

        protected override void EstablishContext()
        {
            foo = "foo";
            bar = "bar";
            collectionToTest = new List<string>
            {
                foo,
                bar
            };
        }

        protected override void Because()
        {
            theAssertion = () => collectionToTest.ShouldOnlyContainInOrder("SomethingDifferent");
        }

        [Observation]
        public void Should_throw_an__AssertException__()
        {
            theAssertion.ShouldThrowAn<AssertException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_a_collection_to_contain_only_a_particular_ordered_set_of_items_and_more_are_present :
            Concern_for_BDDExtensions
    {
        private string bar;
        private List<string> collectionToTest;
        private string foo;
        private string unexpected;

        protected override void EstablishContext()
        {
            foo = "foo";
            bar = "bar";
            unexpected = "unexpected";
            collectionToTest = new List<string>
            {
                foo,
                bar,
                unexpected
            };
        }

        protected override void Because()
        {
            theAssertion = () => collectionToTest.ShouldOnlyContainInOrder(foo, bar);
        }

        [Observation]
        public void Should_throw_an__AssertException__()
        {
            theAssertion.ShouldThrowAn<AssertException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_a_collection_to_contain_only_a_particular_ordered_set_of_items_ordering_does_not_match :
            Concern_for_BDDExtensions
    {
        private string bar;
        private List<string> collectionToTest;
        private string foo;

        protected override void EstablishContext()
        {
            foo = "foo";
            bar = "bar";
            collectionToTest = new List<string>
            {
                foo,
                bar
            };
        }

        protected override void Because()
        {
            theAssertion = () => collectionToTest.ShouldOnlyContainInOrder(bar, foo);
        }

        [Observation]
        public void Should_throw_an__AssertException__()
        {
            theAssertion.ShouldThrowAn<AssertException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_a_collection_to_contain_only_a_particular_ordered_set_of_items_on_a_set_that_matches_the_criteria :
            Concern_for_BDDExtensions
    {
        private string bar;
        private List<string> collectionToTest;
        private string foo;

        protected override void EstablishContext()
        {
            foo = "foo";
            bar = "bar";
            collectionToTest = new List<string>
            {
                foo,
                bar
            };
        }

        protected override void Because()
        {
            theAssertion = () => collectionToTest.ShouldOnlyContainInOrder(foo, bar);
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_an_instance_is_expected_to_be_of_an_unrelated__Type__ :
        Concern_for_BDDExtensions
    {
        private ServiceContainer serviceContainer;

        protected override void EstablishContext()
        {
            serviceContainer = new ServiceContainer();
        }

        protected override void Because()
        {
            theAssertion = () => serviceContainer.ShouldBeAnInstanceOf<bool>();
        }

        [Observation]
        public void Should_throw_a__IsAssignableFromException__()
        {
            theAssertion.ShouldThrowAn<IsAssignableFromException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_an_instance_is_expected_to_assignable_to_a_related__Type__ :
        Concern_for_BDDExtensions
    {
        private ServiceContainer serviceContainer;

        protected override void EstablishContext()
        {
            serviceContainer = new ServiceContainer();
        }

        protected override void Because()
        {
            theAssertion = () => serviceContainer.ShouldBeAnInstanceOf<IServiceProvider>();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_an_instance_to_be_of_its_own__Type__ :
        Concern_for_BDDExtensions
    {
        private ServiceContainer serviceContainer;

        protected override void EstablishContext()
        {
            serviceContainer = new ServiceContainer();
        }

        protected override void Because()
        {
            theAssertion = () => serviceContainer.ShouldBeAnInstanceOf<ServiceContainer>();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_a__Comparable__is_expected_to_be_greater_than_a_second_one_and_it_actually_is_not : Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            theAssertion = () => 1.ShouldBeGreaterThan(2);
        }

        [Observation]
        public void Should_throw_a__AssertException__()
        {
            theAssertion.ShouldThrowAn<AssertException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_a__Comparable__is_expected_to_be_greater_than_a_smaller_second_one : Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            theAssertion = () => 2.ShouldBeGreaterThan(1);
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_a__Comparable__is_expected_to_be_greater_than_itself : Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            theAssertion = () => 2.ShouldBeGreaterThan(2);
        }

        [Observation]
        public void Should_throw_an__AssertException__()
        {
            theAssertion.ShouldThrowAn<AssertException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_an_instance_is_expected_to_be_contained_in_a_collection_and_it_actually_is :
        Concern_for_BDDExtensions
    {
        private List<string> collection;
        private string item;

        protected override void EstablishContext()
        {
            item = "Foo";
            collection = new List<string>
            {
                item
            };
        }

        protected override void Because()
        {
            theAssertion = () => collection.ShouldContain(item);
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_an_instance_is_expected_to_be_contained_in_a_collection_and_it_actually_is_not :
        Concern_for_BDDExtensions
    {
        private List<string> collection;
        private string item;

        protected override void EstablishContext()
        {
            item = "Foo";
            collection = new List<string>();
        }

        protected override void Because()
        {
            theAssertion = () => collection.ShouldContain(item);
        }

        [Observation]
        public void Should_throw_a__ContainsException__()
        {
            theAssertion.ShouldThrowAn<ContainsException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_two_different_instances_to_be_equal :
        Concern_for_BDDExtensions
    {
        private ServiceContainer otherObject;
        private object someObject;

        protected override void EstablishContext()
        {
            someObject = new ServiceContainer();
            otherObject = new ServiceContainer();
        }

        protected override void Because()
        {
            theAssertion = () => someObject.ShouldBeEqualTo(otherObject);
        }

        [Observation]
        public void Should_throw_an__EqualException__()
        {
            theAssertion.ShouldThrowAn<EqualException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_an_instance_to_be_equal_to_itself : Concern_for_BDDExtensions
    {
        private object someObject;

        protected override void EstablishContext()
        {
            someObject = new ServiceContainer();
        }

        protected override void Because()
        {
            theAssertion = () => someObject.ShouldBeEqualTo(someObject);
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_to_differnt_instances_not_to_be_equal :
        Concern_for_BDDExtensions
    {
        private ServiceContainer otherObject;
        private object someObject;

        protected override void EstablishContext()
        {
            someObject = new ServiceContainer();
            otherObject = new ServiceContainer();
        }

        protected override void Because()
        {
            theAssertion = () => someObject.ShouldNotBeEqualTo(otherObject);
        }

        [Observation]
        public void Should_not_throw_and__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_an_instance_not_to_be_equal_to_itself : Concern_for_BDDExtensions
    {
        private object someObject;

        protected override void EstablishContext()
        {
            someObject = new ServiceContainer();
        }

        protected override void Because()
        {
            theAssertion = () => someObject.ShouldNotBeEqualTo(someObject);
        }

        [Observation]
        public void Should_throw_an__NotEqualException__()
        {
            theAssertion.ShouldThrowAn<NotEqualException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_two_differnt__strings__to_be_equal_ignoring_the_casing :
        Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            theAssertion = () => "Foo".ShouldBeEqualIgnoringCase("Bar");
        }

        [Observation]
        public void Should_throw_an__EqualException__()
        {
            theAssertion.ShouldThrowAn<EqualException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_a__string___to_be_equal_to_itself_ignoring_the_casing :
        Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            theAssertion = () => "Foo".ShouldBeEqualIgnoringCase("Foo");
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_a_non_empty_collection_to_be_empty : Concern_for_BDDExtensions
    {
        private ICollection<string> nonEmptyCollection;

        protected override void EstablishContext()
        {
            nonEmptyCollection = new List<string>
            {
                "Foo",
                "Bar"
            };
        }

        protected override void Because()
        {
            theAssertion = () => nonEmptyCollection.ShouldBeEmpty();
        }

        [Observation]
        public void Should_throw_an__EmptyException__()
        {
            theAssertion.ShouldThrowAn<EmptyException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_an_empty_collection_to_be_empty : Concern_for_BDDExtensions
    {
        private ICollection<string> collection;

        protected override void EstablishContext()
        {
            collection = new List<string>();
        }

        protected override void Because()
        {
            theAssertion = () => collection.ShouldBeEmpty();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_a_non_empty_collection_to_contain_items : Concern_for_BDDExtensions
    {
        private ICollection<string> collection;

        protected override void EstablishContext()
        {
            collection = new List<string>
            {
                "Foo",
                "Bar"
            };
        }

        protected override void Because()
        {
            theAssertion = () => collection.ShouldNotBeEmpty();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_expecting_an_empty_collection_not_to_be_empty : Concern_for_BDDExtensions
    {
        private ICollection<string> collection;

        protected override void EstablishContext()
        {
            collection = new List<string>();
        }

        protected override void Because()
        {
            theAssertion = () => collection.ShouldNotBeEmpty();
        }

        [Observation]
        public void Should_throw_a__NotEmptyException__()
        {
            theAssertion.ShouldThrowAn<NotEmptyException>();
        }
    }

    [Concern(typeof (Xunit.BDDExtensions))]
    public class When_a_string_should_be_equal_to_a_differently_cased_string_and_casing_is_ignored :
        Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            theAssertion = () => "Foo".ShouldBeEqualIgnoringCase("fOO");
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            theAssertion.ShouldNotThrowAnyExceptions();
        }
    }
}