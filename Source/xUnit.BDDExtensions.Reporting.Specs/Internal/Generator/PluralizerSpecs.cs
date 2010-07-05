using Xunit.Reporting.Internal.Generator;

namespace Xunit.Reporting.Specs.Internal.Generator
{
    [Concern(typeof (Pluralizer))]
    public class When_deciding_whether_to_pluralize_a_specified_name_based_on_a_specified_amount_and_the_amount_is_equal_to_one : InstanceContextSpecification<Pluralizer>
    {
        private int _amount;
        private string _singular;
        private string _pluralized;

        protected override void EstablishContext()
        {
            _amount = 1;
            _singular = "Car";
        }

        protected override void Because()
        {
            _pluralized = Sut.Pluralize(_singular, _amount);
        }

        [Observation]
        public void Should_not_pluralize_the_name()
        {
            _pluralized.ShouldBeEqualTo("Car");
        }
    }

    [Concern(typeof(Pluralizer))]
    public class When_deciding_whether_to_pluralize_a_specified_name_based_on_a_specified_amount_and_the_amount_is_less_than_one : InstanceContextSpecification<Pluralizer>
    {
        private int _amount;
        private string _singular;
        private string _pluralized;

        protected override void EstablishContext()
        {
            _amount = -1;
            _singular = "Car";
        }

        protected override void Because()
        {
            _pluralized = Sut.Pluralize(_singular, _amount);
        }

        [Observation]
        public void Should_not_pluralize_the_name()
        {
            _pluralized.ShouldBeEqualTo("Car");
        }
    }

    [Concern(typeof(Pluralizer))]
    public class When_deciding_whether_to_pluralize_a_specified_name_based_on_a_specified_amount_and_the_amount_is_larger_than_one : InstanceContextSpecification<Pluralizer>
    {
        private int _amount;
        private string _singular;
        private string _pluralized;

        protected override void EstablishContext()
        {
            _amount = 2;
            _singular = "Car";
        }

        protected override void Because()
        {
            _pluralized = Sut.Pluralize(_singular, _amount);
        }

        [Observation]
        public void Should_pluralize_the_name()
        {
            _pluralized.ShouldBeEqualTo("Cars");
        }
    }

}