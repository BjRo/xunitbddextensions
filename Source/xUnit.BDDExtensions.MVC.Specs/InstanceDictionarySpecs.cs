using System;
using Xunit.Internal;

namespace Xunit.Specs
{
    [Concern(typeof (InstanceDictionary))]
    public class When_setting_values_for_an_instance : InstanceContextSpecification<InstanceDictionary>
    {
        private object instance1;
        private object instance2;

        protected override void EstablishContext()
        {
            base.EstablishContext();
            instance1 = new object();
            instance2 = new object();
        }

        protected override void Because()
        {
            Sut.Set(instance1, "Key", "wert1");
            Sut.Set(instance2, "Key", "wert2");
            Sut.Set(instance1, "Key2", "value1");
            Sut.Set(instance2, "Key2", "value2");
        }

        [Observation]
        public void Should_key_of_instance1_has_wert1()
        {
            Sut.Get<string>(instance1, "Key").ShouldBeEqualTo("wert1");
        }

        [Observation]
        public void Should_key_of_instance2_has_wert2()
        {
            Sut.Get<string>(instance2, "Key").ShouldBeEqualTo("wert2");
        }

        [Observation]
        public void Should_key2_of_instance1_has_value1()
        {
            Sut.Get<string>(instance1, "Key2").ShouldBeEqualTo("value1");
        }

        [Observation]
        public void Should_key2_of_instance2_has_value2()
        {
            Sut.Get<string>(instance2, "Key2").ShouldBeEqualTo("value2");
        }
    }
}