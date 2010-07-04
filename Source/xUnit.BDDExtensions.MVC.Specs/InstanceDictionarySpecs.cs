using Xunit.Internal;

namespace Xunit.Specs
{
    [Concern(typeof (InstanceDictionary))]
    public class When_setting_a_value_for_an_instance_in_the_dictionary : InstanceContextSpecification<InstanceDictionary>
    {
        private object _instance;
       
        protected override void EstablishContext()
        {
            _instance = new object();
        }

        protected override void Because()
        {
            Sut.SetValue(_instance, "Key", "wert1");
        }

        [Observation]
        public void Should_be_able_to_retrieve_the_value_with_the_key_and_the_related_instance()
        {
            Sut.GetValue<string>(_instance, "Key").ShouldBeEqualTo("wert1");
        }

        [Observation]
        public void Should_not_be_able_to_retrieve_a_value_with_the_key_and_and_an_unrelated_instance()
        {
            var otherInstance = new object();
            string value;
            Sut.TryGetValue(otherInstance, "Key",out value).ShouldBeFalse();
            value.ShouldBeNull();
        }
    }
}