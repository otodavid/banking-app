using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Models
{
    public class BankAccount(string owner)
    {
        public string Owner { get; set; } = owner;
        public decimal Balance { get; set; } = 0;

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit must be greater than 0");
            }

            Balance += amount;
            Console.WriteLine($"{amount:C} deposited successfully");
        }

        public void Withdraw(decimal amount)
        {
            if (amount < 0) throw new ArgumentException("Withdraw amount must be positive");
            if (amount > Balance)
            {
                throw new InvalidOperationException("Insufficient Funds");
            }

            Balance -= amount;

            Console.WriteLine($"{amount:C} withdrawn successfully");

        }

        public string GetBalance()
        {
            return $"Balance: {Balance:C}";
        }
    }
}
