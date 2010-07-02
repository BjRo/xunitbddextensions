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
using System.Linq;
using Xunit;

namespace Xunit.Specs
{
    [Concern(typeof(InstanceContextSpecification<>))]
    public class When_a_an_external_instance_is_used_in_a_specification : InstanceContextSpecification<ClassWithSingleDependency>
    {
        private IDependency _arbitraryDependency;
        private IDependency _injectedDependency;

        protected override void EstablishContext()
        {
            _arbitraryDependency = An<IDependency>();
            Use(_arbitraryDependency);
        }

        protected override void Because()
        {
            _injectedDependency = Sut.Dependency;
        }

        [Observation]
        public void Should_inject_the_external_instance_into_the_sut()
        {
            _injectedDependency.ShouldBeTheSame(_arbitraryDependency);
        }
    }

    [Concern(typeof(InstanceContextSpecification<>))]
    public class When_a_behavior_configuration_is_used_to_setup_behaviors : InstanceContextSpecification<ClassWithGenericEnumerableDependency>
    {
        private DependencyConfiguration _configuration;
        private IDependencyAccessor _accessor;

        protected override void EstablishContext()
        {
            _configuration = new DependencyConfiguration();

            With(_configuration);
        }

        protected override void Because()
        {
            _accessor = _configuration.Accessor;
        }

        [Observation]
        public void It_should_pass_the_accessor_for_dependencies_into_the_configuration_before_the_because_method_is_run()
        {
            _accessor.ShouldNotBeNull();
        }
    }

    public class FooClass
    {
        public IEnumerable<string> En;

        public FooClass(IEnumerable<string> en)
        {
            En = en;
        }
    }

    [Concern(typeof(InstanceContextSpecification<>))]
    public class When_an_instance_with_a_dependency_on_a_generic__IEnumerable__is_resolved_and_the_item_type_can_be_stubbed : InstanceContextSpecification<ClassWithGenericEnumerableDependency>
    {
        private IEnumerable<IDependency> _dependencies;
        private IEnumerable<IDependency> _injectedDependencies;

        protected override void EstablishContext()
        {
            _dependencies = The<IEnumerable<IDependency>>();
        }

        protected override void Because()
        {
            _injectedDependencies = Sut.Dependencies;
        }

        [Observation]
        public void Should_prefill_the_enumerable_with_three_stubs()
        {
            Sut.Dependencies.Count().ShouldBeEqualTo(3);
        }

        [Observation]
        public void Should_inject_the_stubs_into_the_SUT_that_where_returned_by_the_THE_call()
        {
            foreach (var dependency in _dependencies)
            {
                _injectedDependencies.ShouldContain(dependency);
            }
        }

        [Observation]
        public void Should_match_the_items_but_not_the_collection()
        {
            _injectedDependencies.ShouldNotBeTheSame(_dependencies);
        }
    }

    [Concern(typeof(InstanceContextSpecification<>))]
    public class When_an_instance_with_a_dependency_on_a_generic__IEnumerable__is_resolved_and_the_item_type_cannot_be_stubbed : InstanceContextSpecification<ClassWithGenericEnumerableWhoseItemTypeIsNotAnInterface>
    {
        private IEnumerable<string> _dependencies;
        private IEnumerable<string> _injectedDependencies;

        protected override void EstablishContext()
        {
            Use("SomeString");
            
            _dependencies = The<IEnumerable<string>>();
        }

        protected override void Because()
        {
            _injectedDependencies = Sut.Dependencies;
        }

        [Observation]
        public void Needs_to_be_populated_manually()
        {
            _injectedDependencies.ShouldContain("SomeString");
        }
    }

    [Concern(typeof(InstanceContextSpecification<>))]
    public class When_an_instance_with_a_dependency_on_some_interface_type_is_resolved : InstanceContextSpecification<ClassWithSingleDependency>
    {
        private IDependency _dependency;

        protected override void Because()
        {
            _dependency = The<IDependency>();
        }

        [Observation]
        public void Should_be_accessible_via_the__The__accessor()
        {
            Sut.Dependency.ShouldBeTheSame(_dependency);
        }

        [Observation]
        public void Should_inject_a_stub_object_for_the_dependency()
        {
            Sut.Dependency.ShouldNotBeNull();
        }
    }

    [Concern(typeof(InstanceContextSpecification<>))]
    public class When_a_mock_instance_is_resolved_with_the_The_accessor : InstanceContextSpecification<object>
    {
        private IDependency _dependency;

        protected override void Because()
        {
            _dependency = The<IDependency>();
        }

        [Observation]
        public void Should_always_the_same_mock_resolved()
        {
            _dependency.ShouldBeTheSame(The<IDependency>());
            _dependency.ShouldBeTheSame(The<IDependency>());
            _dependency.ShouldBeTheSame(The<IDependency>());
        }

    }
    [Concern(typeof(InstanceContextSpecification<>))]
    public class When_an_instance_with_two_dependencies_on_the_same_interface_type_is_resolved : InstanceContextSpecification<ClassWithTwoDependenciesOfTheSameType>
    {
        protected override void Because()
        {
        }

        [Observation]
        public void It_is_a_situation_which_is_not_properly_handled_currently_because_the_same_instance_is_injected_twice()
        {
            Sut.DependencyA.ShouldBeTheSame(Sut.DependencyB);
        }
    }

    #region Test classes

    public class DependencyConfiguration : IBehaviorConfig
    {
        public IDependencyAccessor Accessor;

        public void EstablishContext(IDependencyAccessor acessor)
        {
            Accessor = acessor;
        }

        public void PrepareSut(object sut)
        {
            
        }
    }

    public interface IDependency
    {
        object Invoke();
        object Invoke(IDependency transientDependency);
    }

    public class ClassWithSingleDependency
    {
        public readonly IDependency Dependency;

        public ClassWithSingleDependency(IDependency dependency)
        {
            Dependency = dependency;
        }
    }

    public class ClassWithTwoDependenciesOfTheSameType
    {
        public readonly IDependency DependencyA;
        public readonly IDependency DependencyB;

        public ClassWithTwoDependenciesOfTheSameType(IDependency dependencyA, IDependency dependencyB)
        {
            DependencyA = dependencyA;
            DependencyB = dependencyB;
        }
    }

    public class ClassWithGenericEnumerableDependency
    {
        public IEnumerable<IDependency> Dependencies;

        public ClassWithGenericEnumerableDependency(IEnumerable<IDependency> dependencies)
        {
            Dependencies = dependencies;
        }
    }

    public class ClassWithGenericEnumerableWhoseItemTypeIsNotAnInterface
    {
        public IEnumerable<string> Dependencies;

        public ClassWithGenericEnumerableWhoseItemTypeIsNotAnInterface(IEnumerable<string> dependencies)
        {
            Dependencies = dependencies;
        }
    }

    #endregion
}