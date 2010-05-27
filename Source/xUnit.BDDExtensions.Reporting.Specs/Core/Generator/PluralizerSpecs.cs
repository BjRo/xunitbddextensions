using Xunit.Reporting.Core.Generator;

namespace Xunit.Reporting.Specs.Core.Generator
{
    [Concern(typeof (Pluralizer))]
    public class When_deciding_whether_to_pluralize_a_specified_name_based_on_a_specified_amount_and_the_amount_is_equal_to_one : InstanceContextSpecification<Pluralizer>
    {
        private int amount;
        private string singular;
        private string pluralized;

        protected override void EstablishContext()
        {
            amount = 1;
            singular = "Car";
        }

        protected override void Because()
        {
            pluralized = Sut.Pluralize(singular, amount);
        }

        [Observation]
        public void Should_not_pluralize_the_name()
        {
            pluralized.ShouldBeEqualTo("Car");
        }
    }

    [Concern(typeof(Pluralizer))]
    public class When_deciding_whether_to_pluralize_a_specified_name_based_on_a_specified_amount_and_the_amount_is_less_than_one : InstanceContextSpecification<Pluralizer>
    {
        private int amount;
        private string singular;
        private string pluralized;

        protected override void EstablishContext()
        {
            amount = -1;
            singular = "Car";
        }

        protected override void Because()
        {
            pluralized = Sut.Pluralize(singular, amount);
        }

        [Observation]
        public void Should_not_pluralize_the_name()
        {
            pluralized.ShouldBeEqualTo("Car");
        }
    }

    [Concern(typeof(Pluralizer))]
    public class When_deciding_whether_to_pluralize_a_specified_name_based_on_a_specified_amount_and_the_amount_is_larger_than_one : InstanceContextSpecification<Pluralizer>
    {
        private int amount;
        private string singular;
        private string pluralized;

        protected override void EstablishContext()
        {
            amount = 2;
            singular = "Car";
        }

        protected override void Because()
        {
            pluralized = Sut.Pluralize(singular, amount);
        }

        [Observation]
        public void Should_pluralize_the_name()
        {
            pluralized.ShouldBeEqualTo("Cars");
        }
    }

}