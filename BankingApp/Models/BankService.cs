using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankingApp.Models
{
    public class BankService
    {
        private readonly AccountsRepository _repository;
        private readonly Random _ang = new Random();
        public BankService(AccountsRepository repository)
        {
            _repository = repository;
        }

        public BankAccount CreateAccount(string ownerName, decimal initialDeposit)
        {
            if (string.IsNullOrWhiteSpace(ownerName))
            {
                throw new ArgumentException("Owner name is required");
            }

            if (initialDeposit < 0)
            {
                throw new ArgumentException("Initial deposit must be greater than 0");
            }

            string accountNumber;

            do
            {
                accountNumber = _ang.Next(100000, 999999).ToString();
            } while (
                _repository.GetAccountByAccountNumber(accountNumber) != null
            );

            var account = new BankAccount
            {
                AccountNumber = accountNumber,
                Balance = initialDeposit,
                OwnerName = ownerName,
                TransactionHistory = new List<Transaction>
                {
                    new Transaction
                    {
                        Amount = initialDeposit,
                        Date = DateTime.Now,
                        Type = TransactionType.Deposit,
                        BalanceAfter = initialDeposit
                    }
                }
            };

            _repository.AddOrUpdateAccount(account);
            return account;
        }

        public void Deposit(decimal amount, BankAccount account)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit must be greater than 0");
            }

            account.Balance += amount;
            account.TransactionHistory.Add(
                new Transaction
                {
                    Amount = amount,
                    Date = DateTime.Now,
                    Type = TransactionType.Deposit,
                    BalanceAfter = account.Balance
                }
             );
            Console.WriteLine($"{amount:C} deposited successfully");
        }

        public void Withdraw(decimal amount, BankAccount account)
        {
            if (amount < 0) throw new ArgumentException("Withdraw amount must be positive");
            if (amount > account.Balance)
            {
                throw new InvalidOperationException("Insufficient Funds");
            }

            account.Balance -= amount;
            account.TransactionHistory.Add(
                new Transaction 
                { 
                    Amount = amount, 
                    Date = DateTime.Now, 
                    Type = TransactionType.Withdraw, 
                    BalanceAfter = account.Balance });

            Console.WriteLine($"{amount:C} withdrawn successfully");
        }
    }
}
