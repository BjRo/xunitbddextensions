
using System;

namespace Xunit.Examples
{
	public class When_a_customer_first_views_the_account_summary_page : 
		StaticContextSpecification
	{
		protected override void EstablishContext()
		{
		}

		protected override void Because()
		{	
		}

		[Fact]
		public void Should_display_all_account_transactions_for_the_past_thirty_days()
		{
			throw new ObservationNotImplementedException();
		}
	}
}