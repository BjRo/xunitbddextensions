using System;

namespace Xunit.Examples.Banking
{
	public class Account
	{
		public decimal Balance { get; set; }

		public void Transfer(decimal amount, Account toAccount)
		{
			if (amount > Balance)
			{
				throw new ArgumentException(String.Format("Cannot transfer ${0}. The available balance is ${1}.", amount, Balance));
			}

			Balance -= amount;
			toAccount.Balance += amount;
		}
	}
}