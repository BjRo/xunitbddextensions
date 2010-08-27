using System;
using System.Collections.Generic;

namespace Xunit.Specs
{
    public class Given_that_a_context_specification_uses_the_FactAttribute_instead_of_the_ObservationAttribute_when_beeing_run : StaticContextSpecification
    {
        private List<string> _calls = new List<string>();

        protected override void EstablishContext()
        {
            _calls.Add("EstablishContext");
        }

        protected override void Because()
        {
            _calls.Add("Because");
        }

        [Fact]
        public void Should_run_the_specification_as_normal()
        {
            _calls.ShouldOnlyContainInOrder("EstablishContext", "Because");
        }
    }
}