using System;
using Xunit.Examples.Banking;

namespace Xunit.Examples.BankingSpecs
{
	[Concern(typeof(Account), "Funds transfer")]
	public class When_transferring_between_two_accounts : AccountSpecs
	{
		protected override void Because()
		{
			_fromAccount.Transfer(1m, _toAccount);
		}

		[Fact]
		public void Should_credit_the_to_account_by_the_amount_transferred()
		{
			_toAccount.Balance.ShouldBeEqualTo(2m);
		}

		[Fact]
		public void Should_debit_the_from_account_by_the_amount_transferred()
		{
			_fromAccount.Balance.ShouldBeEqualTo(0m);
		}
	}

	[Concern(typeof(Account), "Funds transfer")]
	public class When_transferring_an_amount_larger_than_the_balance_of_the_from_account : AccountSpecs
	{
		private static Exception _exception;

		protected override void Because()
		{
			_exception = Catch.Exception<ArgumentException>(() => _fromAccount.Transfer(2m, _toAccount));
		}

		[Fact]
		public void Should_not_allow_the_transfer()
		{
			_exception.ShouldNotBeNull();
		}
	}

	public abstract class AccountSpecs : StaticContextSpecification
	{
		protected static Account _fromAccount;
		protected static Account _toAccount;

		protected override void EstablishContext()
		{
			_fromAccount = new Account { Balance = 1m };
			_toAccount = new Account { Balance = 1m };
		}
	}
}