using System;
using Xunit.Internal;

namespace Xunit.Specs
{
    [Concern(typeof(AutoMockingContainer<>))]
    public class When_trying_to_build_a_class_under_test_that_has_a_ctor_dependency_on_an_abstract_class : StaticContextSpecification
    {
        private TestClassWithAbstractClassDependency _instance;
        private AutoMockingContainer<TestClassWithAbstractClassDependency> _autoMocker;

        protected override void EstablishContext()
        {
            _autoMocker =  new AutoMockingContainer<TestClassWithAbstractClassDependency>(new RhinoMocksFactory());
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
            _instance.Dependency.ShouldNotBeNull();
        }
    }

    public class TestClassWithAbstractClassDependency
    {
        public Dependency Dependency;

        public TestClassWithAbstractClassDependency(Dependency dependency)
        {
            Dependency = dependency;
        }
    }

    public abstract class Dependency
    {
    }
}