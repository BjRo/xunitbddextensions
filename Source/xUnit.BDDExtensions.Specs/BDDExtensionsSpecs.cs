//  Copyright 2010 xUnit.BDDExtensions
//    
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License. 
//  You may obtain a copy of the License at
//    
//        http://www.apache.org/licenses/LICENSE-2.0
//    
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
//  implied. See the License for the specific language governing permissions and
//  limitations under the License.  
//  
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Xunit.Sdk;

namespace Xunit.BDDExtensionsSpecs
{
    public abstract class Concern_for_BDDExtensions : StaticContextSpecification
    {
        protected Action TheAssertion;
    }

    [Concern(typeof (BDDExtensions))]
    public class When_an__Exception__is_expected_to_be_thrown_and_is_actually_thrown :
        Concern_for_BDDExtensions
    {
        private Action _operationThatThrows;

        protected override void EstablishContext()
        {
            _operationThatThrows = () => { throw new InvalidOperationException(); };
        }

        protected override void Because()
        {
            TheAssertion = () => _operationThatThrows.ShouldThrowAn<InvalidOperationException>();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            TheAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_an_instance_is_expected_to_be__null__and_is_actually__null__ : Concern_for_BDDExtensions
    {
        private object _existingObject;

        protected override void EstablishContext()
        {
            _existingObject = null;
        }

        protected override void Because()
        {
            TheAssertion = () => _existingObject.ShouldBeNull();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            TheAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_an_instance_is_expected_to_be__null__but_it_is_not : Concern_for_BDDExtensions
    {
        private object _existingObject;

        protected override void EstablishContext()
        {
            _existingObject = new object();
        }

        protected override void Because()
        {
            TheAssertion = () => _existingObject.ShouldBeNull();
        }

        [Observation]
        public void Should_throw_a__NullException__()
        {
            TheAssertion.ShouldThrowAn<NullException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting_null_to_be_not__null__ : Concern_for_BDDExtensions
    {
        private object _existingObject;

        protected override void EstablishContext()
        {
            _existingObject = null;
        }

        protected override void Because()
        {
            TheAssertion = () => _existingObject.ShouldNotBeNull();
        }

        [Observation]
        public void Should_throw_an__NotNullException__()
        {
            TheAssertion.ShouldThrowAn<NotNullException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_an_instance_is_expected_to_be_not__null__ : Concern_for_BDDExtensions
    {
        private object _existingObject;

        protected override void EstablishContext()
        {
            _existingObject = new object();
        }

        protected override void Because()
        {
            TheAssertion = () => _existingObject.ShouldNotBeNull();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            TheAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting_a_particular__Exception__to_be_thrown_and_no__Exception__was_thrown :
        Concern_for_BDDExtensions
    {
        private Action _operationNotThrowingAnException;

        protected override void EstablishContext()
        {
            _operationNotThrowingAnException = () => { };
        }

        protected override void Because()
        {
            TheAssertion = () => _operationNotThrowingAnException.ShouldThrowAn<ArgumentNullException>();
        }

        [Observation]
        public void Should_throw_a__ThrowsException__()
        {
            TheAssertion.ShouldThrowAn<ThrowsException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting_a_particular__Exception__to_be_thrown_and_a_different__Exception__was_thrown :
        Concern_for_BDDExtensions
    {
        private Action _operationThatThrowsADifferentException;

        protected override void EstablishContext()
        {
            _operationThatThrowsADifferentException = () => { throw new InvalidOperationException(); };
        }

        protected override void Because()
        {
            TheAssertion = () => _operationThatThrowsADifferentException.ShouldThrowAn<ArgumentNullException>();
        }

        [Observation]
        public void Should_throw_a__ThrowsException__()
        {
            TheAssertion.ShouldThrowAn<ThrowsException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting__False__to_be__False__ : Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            TheAssertion = () => false.ShouldBeFalse();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            TheAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting__True__to_be__True__ : Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            TheAssertion = () => true.ShouldBeTrue();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            TheAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting__True__to_be__False__ : Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            TheAssertion = () => true.ShouldBeFalse();
        }

        [Observation]
        public void Should_throw_a__FalseException__()
        {
            TheAssertion.ShouldThrowAn<FalseException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting__False__to_be__True__ : Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            TheAssertion = () => false.ShouldBeTrue();
        }

        [Observation]
        public void Should_throw_a__TrueException__()
        {
            TheAssertion.ShouldThrowAn<TrueException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting_a_collection_to_contain_only_a_particular_set_of_items_and_a_different_set_is_present :
        Concern_for_BDDExtensions
    {
        private string _bar;
        private List<string> _collectionToTest;
        private string _foo;

        protected override void EstablishContext()
        {
            _foo = "foo";
            _bar = "bar";
            _collectionToTest = new List<string>
                                {
                                    _foo,
                                    _bar
                                };
        }

        protected override void Because()
        {
            TheAssertion = () => _collectionToTest.ShouldOnlyContain("SomethingDifferent");
        }

        [Observation]
        public void Should_throw_an__AssertException__()
        {
            TheAssertion.ShouldThrowAn<AssertException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting_a_collection_to_contain_only_a_particular_set_of_items_and_more_are_present :
        Concern_for_BDDExtensions
    {
        private string _bar;
        private List<string> _collectionToTest;
        private string _foo;
        private string _unexpected;

        protected override void EstablishContext()
        {
            _foo = "foo";
            _bar = "bar";
            _unexpected = "unexpected";
            _collectionToTest = new List<string>
                                {
                                    _foo,
                                    _bar,
                                    _unexpected
                                };
        }

        protected override void Because()
        {
            TheAssertion = () => _collectionToTest.ShouldOnlyContain(_foo, _bar);
        }

        [Observation]
        public void Should_throw_an__AssertException__()
        {
            TheAssertion.ShouldThrowAn<AssertException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting_a_collection_to_contain_only_the_items_it_actually_contains :
        Concern_for_BDDExtensions
    {
        private string _bar;
        private List<string> _collectionToTest;
        private string _foo;

        protected override void EstablishContext()
        {
            _foo = "foo";
            _bar = "bar";
            _collectionToTest = new List<string>
                                {
                                    _foo,
                                    _bar
                                };
        }

        protected override void Because()
        {
            TheAssertion = () => _collectionToTest.ShouldOnlyContain(_foo, _bar);
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            TheAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class
        When_expecting_a_collection_to_contain_only_a_particular_set_in_a_particular_order_and_different_set_is_present :
            Concern_for_BDDExtensions
    {
        private string _bar;
        private List<string> _collectionToTest;
        private string _foo;

        protected override void EstablishContext()
        {
            _foo = "foo";
            _bar = "bar";
            _collectionToTest = new List<string>
                                {
                                    _foo,
                                    _bar
                                };
        }

        protected override void Because()
        {
            TheAssertion = () => _collectionToTest.ShouldOnlyContainInOrder("SomethingDifferent");
        }

        [Observation]
        public void Should_throw_an__AssertException__()
        {
            TheAssertion.ShouldThrowAn<AssertException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting_a_collection_to_contain_only_a_particular_ordered_set_of_items_and_more_are_present :
        Concern_for_BDDExtensions
    {
        private string _bar;
        private List<string> _collectionToTest;
        private string _foo;
        private string _unexpected;

        protected override void EstablishContext()
        {
            _foo = "foo";
            _bar = "bar";
            _unexpected = "unexpected";
            _collectionToTest = new List<string>
                                {
                                    _foo,
                                    _bar,
                                    _unexpected
                                };
        }

        protected override void Because()
        {
            TheAssertion = () => _collectionToTest.ShouldOnlyContainInOrder(_foo, _bar);
        }

        [Observation]
        public void Should_throw_an__AssertException__()
        {
            TheAssertion.ShouldThrowAn<AssertException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting_a_collection_to_contain_only_a_particular_ordered_set_of_items_ordering_does_not_match :
        Concern_for_BDDExtensions
    {
        private string _bar;
        private List<string> _collectionToTest;
        private string _foo;

        protected override void EstablishContext()
        {
            _foo = "foo";
            _bar = "bar";
            _collectionToTest = new List<string>
                                {
                                    _foo,
                                    _bar
                                };
        }

        protected override void Because()
        {
            TheAssertion = () => _collectionToTest.ShouldOnlyContainInOrder(_bar, _foo);
        }

        [Observation]
        public void Should_throw_an__AssertException__()
        {
            TheAssertion.ShouldThrowAn<AssertException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class
        When_expecting_a_collection_to_contain_only_a_particular_ordered_set_of_items_on_a_set_that_matches_the_criteria :
            Concern_for_BDDExtensions
    {
        private string _bar;
        private List<string> _collectionToTest;
        private string _foo;

        protected override void EstablishContext()
        {
            _foo = "foo";
            _bar = "bar";
            _collectionToTest = new List<string>
                                {
                                    _foo,
                                    _bar
                                };
        }

        protected override void Because()
        {
            TheAssertion = () => _collectionToTest.ShouldOnlyContainInOrder(_foo, _bar);
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            TheAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_an_instance_is_expected_to_be_of_an_unrelated__Type__ :
        Concern_for_BDDExtensions
    {
        private ServiceContainer _serviceContainer;

        protected override void EstablishContext()
        {
            _serviceContainer = new ServiceContainer();
        }

        protected override void Because()
        {
            TheAssertion = () => _serviceContainer.ShouldBeAnInstanceOf<bool>();
        }

        [Observation]
        public void Should_throw_a__IsAssignableFromException__()
        {
            TheAssertion.ShouldThrowAn<IsAssignableFromException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_an_instance_is_expected_to_assignable_to_a_related__Type__ :
        Concern_for_BDDExtensions
    {
        private ServiceContainer _serviceContainer;

        protected override void EstablishContext()
        {
            _serviceContainer = new ServiceContainer();
        }

        protected override void Because()
        {
            TheAssertion = () => _serviceContainer.ShouldBeAnInstanceOf<IServiceProvider>();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            TheAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting_an_instance_to_be_of_its_own__Type__ :
        Concern_for_BDDExtensions
    {
        private ServiceContainer _serviceContainer;

        protected override void EstablishContext()
        {
            _serviceContainer = new ServiceContainer();
        }

        protected override void Because()
        {
            TheAssertion = () => _serviceContainer.ShouldBeAnInstanceOf<ServiceContainer>();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            TheAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_a__Comparable__is_expected_to_be_greater_than_a_second_one_and_it_actually_is_not :
        Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            TheAssertion = () => 1.ShouldBeGreaterThan(2);
        }

        [Observation]
        public void Should_throw_a__AssertException__()
        {
            TheAssertion.ShouldThrowAn<AssertException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_a__Comparable__is_expected_to_be_greater_than_a_smaller_second_one : Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            TheAssertion = () => 2.ShouldBeGreaterThan(1);
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            TheAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_a__Comparable__is_expected_to_be_greater_than_itself : Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            TheAssertion = () => 2.ShouldBeGreaterThan(2);
        }

        [Observation]
        public void Should_throw_an__AssertException__()
        {
            TheAssertion.ShouldThrowAn<AssertException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_an_instance_is_expected_to_be_contained_in_a_collection_and_it_actually_is :
        Concern_for_BDDExtensions
    {
        private List<string> _collection;
        private string _item;

        protected override void EstablishContext()
        {
            _item = "Foo";
            _collection = new List<string>
                          {
                              _item
                          };
        }

        protected override void Because()
        {
            TheAssertion = () => _collection.ShouldContain(_item);
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            TheAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_an_instance_is_expected_to_be_contained_in_a_collection_and_it_actually_is_not :
        Concern_for_BDDExtensions
    {
        private List<string> _collection;
        private string _item;

        protected override void EstablishContext()
        {
            _item = "Foo";
            _collection = new List<string>();
        }

        protected override void Because()
        {
            TheAssertion = () => _collection.ShouldContain(_item);
        }

        [Observation]
        public void Should_throw_a__ContainsException__()
        {
            TheAssertion.ShouldThrowAn<ContainsException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting_two_different_instances_to_be_equal :
        Concern_for_BDDExtensions
    {
        private ServiceContainer _otherObject;
        private object _someObject;

        protected override void EstablishContext()
        {
            _someObject = new ServiceContainer();
            _otherObject = new ServiceContainer();
        }

        protected override void Because()
        {
            TheAssertion = () => _someObject.ShouldBeEqualTo(_otherObject);
        }

        [Observation]
        public void Should_throw_an__EqualException__()
        {
            TheAssertion.ShouldThrowAn<EqualException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting_an_instance_to_be_equal_to_itself : Concern_for_BDDExtensions
    {
        private object _someObject;

        protected override void EstablishContext()
        {
            _someObject = new ServiceContainer();
        }

        protected override void Because()
        {
            TheAssertion = () => _someObject.ShouldBeEqualTo(_someObject);
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            TheAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting_to_differnt_instances_not_to_be_equal :
        Concern_for_BDDExtensions
    {
        private ServiceContainer _otherObject;
        private object _someObject;

        protected override void EstablishContext()
        {
            _someObject = new ServiceContainer();
            _otherObject = new ServiceContainer();
        }

        protected override void Because()
        {
            TheAssertion = () => _someObject.ShouldNotBeEqualTo(_otherObject);
        }

        [Observation]
        public void Should_not_throw_and__Exception__()
        {
            TheAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting_an_instance_not_to_be_equal_to_itself : Concern_for_BDDExtensions
    {
        private object _someObject;

        protected override void EstablishContext()
        {
            _someObject = new ServiceContainer();
        }

        protected override void Because()
        {
            TheAssertion = () => _someObject.ShouldNotBeEqualTo(_someObject);
        }

        [Observation]
        public void Should_throw_an__NotEqualException__()
        {
            TheAssertion.ShouldThrowAn<NotEqualException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting_two_differnt__strings__to_be_equal_ignoring_the_casing :
        Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            TheAssertion = () => "Foo".ShouldBeEqualIgnoringCase("Bar");
        }

        [Observation]
        public void Should_throw_an__EqualException__()
        {
            TheAssertion.ShouldThrowAn<EqualException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting_a__string___to_be_equal_to_itself_ignoring_the_casing :
        Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            TheAssertion = () => "Foo".ShouldBeEqualIgnoringCase("Foo");
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            TheAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting_a_non_empty_collection_to_be_empty : Concern_for_BDDExtensions
    {
        private ICollection<string> _nonEmptyCollection;

        protected override void EstablishContext()
        {
            _nonEmptyCollection = new List<string>
                                  {
                                      "Foo",
                                      "Bar"
                                  };
        }

        protected override void Because()
        {
            TheAssertion = () => _nonEmptyCollection.ShouldBeEmpty();
        }

        [Observation]
        public void Should_throw_an__EmptyException__()
        {
            TheAssertion.ShouldThrowAn<EmptyException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting_an_empty_collection_to_be_empty : Concern_for_BDDExtensions
    {
        private ICollection<string> _collection;

        protected override void EstablishContext()
        {
            _collection = new List<string>();
        }

        protected override void Because()
        {
            TheAssertion = () => _collection.ShouldBeEmpty();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            TheAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting_a_non_empty_collection_to_contain_items : Concern_for_BDDExtensions
    {
        private ICollection<string> _collection;

        protected override void EstablishContext()
        {
            _collection = new List<string>
                          {
                              "Foo",
                              "Bar"
                          };
        }

        protected override void Because()
        {
            TheAssertion = () => _collection.ShouldNotBeEmpty();
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            TheAssertion.ShouldNotThrowAnyExceptions();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_expecting_an_empty_collection_not_to_be_empty : Concern_for_BDDExtensions
    {
        private ICollection<string> _collection;

        protected override void EstablishContext()
        {
            _collection = new List<string>();
        }

        protected override void Because()
        {
            TheAssertion = () => _collection.ShouldNotBeEmpty();
        }

        [Observation]
        public void Should_throw_a__NotEmptyException__()
        {
            TheAssertion.ShouldThrowAn<NotEmptyException>();
        }
    }

    [Concern(typeof (BDDExtensions))]
    public class When_a_string_should_be_equal_to_a_differently_cased_string_and_casing_is_ignored :
        Concern_for_BDDExtensions
    {
        protected override void Because()
        {
            TheAssertion = () => "Foo".ShouldBeEqualIgnoringCase("fOO");
        }

        [Observation]
        public void Should_not_throw_an__Exception__()
        {
            TheAssertion.ShouldNotThrowAnyExceptions();
        }
    }
}