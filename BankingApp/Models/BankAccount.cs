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
        public List<Transaction> TransactionHistory { get; set; } = [];

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit must be greater than 0");
            }

            Balance += amount;
            TransactionHistory.Add(new Transaction { Amount = amount, Date = DateTime.Now, Type = TransactionType.Deposit, BalanceAfter = Balance });
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
            TransactionHistory.Add(new Transaction { Amount = amount, Date = DateTime.Now, Type = TransactionType.Withdraw, BalanceAfter = Balance });

            Console.WriteLine($"{amount:C} withdrawn successfully");
        }

        public string GetBalance()
        {
            return $"Balance: {Balance:C}";
        }

        public void PrintStatement()
        {
            foreach(Transaction transaction in TransactionHistory)
            {
                Console.WriteLine(transaction.ToString());
            }
        }

        public override string ToString()
        {
            return $"Account name: {Owner}.\nAccount Balance: {Balance}";
        }
    }
}
