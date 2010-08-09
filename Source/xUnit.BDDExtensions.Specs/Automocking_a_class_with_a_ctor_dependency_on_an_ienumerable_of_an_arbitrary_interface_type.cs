using System.Collections.Generic;
using System.Linq;
using Xunit.Internal;

namespace Xunit.Specs
{
    [Concern(typeof(AutoMockingContainer<>))]
    public class When_trying_to_build_a_class_under_test_that_has_a_ctor_dependency_on_a_generic_collections_of_interface_types
        : StaticContextSpecification
    {
        private AutoMockingContainer<TestClassWithGenericCollectionOfInterfaceTypes> _autoMocker;
        private TestClassWithGenericCollectionOfInterfaceTypes _instance;

        protected override void EstablishContext()
        {
            _autoMocker = new AutoMockingContainer<TestClassWithGenericCollectionOfInterfaceTypes>();
        }

        protected override void Because()
        {
            _instance = _autoMocker.ClassUnderTest;
        }

        [Observation]
        public void Should_be_able_to_resolve_the_class_under_test()
        {
            _instance.ShouldNotBeNull();
        }

        [Observation]
        public void Should_be_able_to_resolve_the_dependency()
        {
            _instance.Dependencies.ShouldNotBeNull();   
        }

        [Observation(
            Skip = "Broken because of changed IEnumerable<T> handling in StructureMap ...")
        ]
        public void Should_inject_3_interface_stubs_into_the_collection()
        {
            _instance.Dependencies.Count().ShouldBeEqualTo(3);
        }
    }

    public class TestClassWithGenericCollectionOfInterfaceTypes
    {
        public IEnumerable<ITestDependency> Dependencies;

        public TestClassWithGenericCollectionOfInterfaceTypes(IEnumerable<ITestDependency> dependencies)
        {
            Dependencies = dependencies;
        }
    }

    public interface ITestDependency
    {
    }
}