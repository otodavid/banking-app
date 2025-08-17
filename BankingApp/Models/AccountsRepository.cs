using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankingApp.Models
{
    public class AccountsRepository
    {
        private readonly string _filePath;
        private static readonly JsonSerializerOptions _cachedJsonOptions = new JsonSerializerOptions { WriteIndented = true };

        public AccountsRepository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
            {
                SaveAccounts(new List<BankAccount>());
            }
        }
        public List<BankAccount> LoadAccounts()
        {
            var jsonString = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<BankAccount>>(jsonString) ?? new List<BankAccount>();
        }

        public void SaveAccounts(List<BankAccount> accounts)
        {
            var jsonData = JsonSerializer.Serialize(accounts, _cachedJsonOptions);
            File.WriteAllText(_filePath, jsonData);
        }

        public void AddOrUpdateAccount(BankAccount account)
        {
            var accounts = LoadAccounts();

            var existingUser = accounts.FirstOrDefault(a => a.AccountNumber == account.AccountNumber);

            if (existingUser != null)
            {
                accounts.Remove(existingUser);
            }

            accounts.Add(account);
            SaveAccounts(accounts);
        }

        public BankAccount? GetAccountByAccountNumber(string accountNumber)
        {
            var accounts = LoadAccounts();

            return accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

        }
    }
}
