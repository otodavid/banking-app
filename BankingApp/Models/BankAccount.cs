using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Models
{
    public class BankAccount()
    {
        public string OwnerName { get; set; } = "";
        public string AccountNumber { get; set; } = "";
        public decimal Balance { get; set; }
        public List<Transaction> TransactionHistory { get; set; } = [];

        public void PrintBalance()
        {
            Console.WriteLine($"Balance: {Balance:C}");
        }

        public void PrintStatement()
        {
            Console.WriteLine("Date\t\t\tTransaction\t\t Amount\t\t\t Balance");
            foreach (Transaction transaction in TransactionHistory)
            {
                Console.WriteLine(transaction.ToString());
            }
        }
        public override string ToString()
        {
            return $"Account name: {OwnerName}.\nAccount Number: {AccountNumber}.\nAccount Balance: {Balance}";
        }
    }
}
