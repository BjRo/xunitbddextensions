using Xunit.Internal;

namespace Xunit.Specs
{
    [Concern(typeof (MockedRequestContextValueStore))]
    public class When_setting_a_value_for_an_instance_in_the_dictionary : StaticContextSpecification
    {
        private IMockedRequestContext _instance;
       
        protected override void EstablishContext()
        {
            _instance = An<IMockedRequestContext>();
        }

        protected override void Because()
        {
            _instance.SetValue("Key", "wert1");
        }

        [Observation]
        public void Should_be_able_to_retrieve_the_value_with_the_key_and_the_related_instance()
        {
            _instance.GetValue<string>("Key").ShouldBeEqualTo("wert1");
        }

        [Observation]
        public void Should_not_be_able_to_retrieve_a_value_with_the_key_and_and_an_unrelated_instance()
        {
            var otherInstance = An<IMockedRequestContext>();
            string value;
            otherInstance.TryGetValue("Key",out value).ShouldBeFalse();
            value.ShouldBeNull();
        }
    }
}