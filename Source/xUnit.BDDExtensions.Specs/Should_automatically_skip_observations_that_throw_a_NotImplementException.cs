using System;

namespace Xunit.Specs
{
    public class Given_one_or_more_classes_throw_a_SpecNotImplementedException : StaticContextSpecification
    {
        protected override void Because()
        {
        }

        [Observation]
        public void Should_be_automatically_skipped()
        {
            throw new ObservationNotImplementedException();
        }

        [Observation]
        public void Should_be_automatically_skipped_too()
        {
            throw new ObservationNotImplementedException();
        }
    }
}