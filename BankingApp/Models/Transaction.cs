using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Models
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceAfter { get; set; }
        public TransactionType Type { get; set; }

        public override string ToString()
        {
            return $"{Date:yyyy-MM-dd HH:mm}\t{Type} \t\t {Amount:C} \t\t {BalanceAfter:C}";
        }
    }
}
