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
        private static readonly string filePath = @"C:\dev\accounts.json";
        private static readonly JsonSerializerOptions CachedJsonOptions = new JsonSerializerOptions { WriteIndented = true };

        public static List<BankAccount> LoadAccounts()
        {
            string? directory = Path.GetDirectoryName(filePath);

            if (!String.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
                return new List<BankAccount>();
            }

            var jsonString = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<List<BankAccount>>(jsonString) ?? new List<BankAccount>();
        }

        public static void SaveAccounts(List<BankAccount> accounts)
        {
            string? directory = Path.GetDirectoryName(filePath);

            if (!String.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var jsonData = JsonSerializer.Serialize(accounts, CachedJsonOptions);
            File.WriteAllText(filePath, jsonData);
        }

        public static BankAccount GetOrCreateAccount(string owner)
        {
            var accounts = LoadAccounts();

            var user = accounts.FirstOrDefault(a => a.Owner == owner);

            if (user == null)
            {
                var newAccount = new BankAccount(owner);
                accounts.Add(newAccount);
                SaveAccounts(accounts);

                return newAccount;
            }

            return user;
        }
    }
}
