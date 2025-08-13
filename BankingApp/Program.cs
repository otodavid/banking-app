using BankingApp.Models;

namespace BankingApp;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter your name: ");
        string? name = Console.ReadLine();

        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("A valid name is needed to create an account");
            return;
        }

        var account = BankService.GetOrCreateAccount(name);

        while (true)
        {
            DisplayMenu();

            var choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter amount to deposit: ");
                    ProcessTransaction(account, TransactionType.Deposit);
                    break;

                case "2":
                    Console.WriteLine("Enter amount to withdraw: ");
                    ProcessTransaction(account, TransactionType.Withdraw);
                    break;

                case "3":
                    Console.WriteLine(account.GetBalance());
                    break;

                case "4":
                    Console.WriteLine("Thank you for banking with us");
                    return;

                default:
                    Console.WriteLine("Invalid Option");
                    break;
            }
        }

    }

    private static void DisplayMenu()
    {
        Console.WriteLine("\nChoose an action:");
        Console.WriteLine("1. Deposit:");
        Console.WriteLine("2. Withdraw:");
        Console.WriteLine("3. View Balance:");
        Console.WriteLine("4. Exit:");
        Console.Write("Enter your choice: ");
    }

    private static void ProcessTransaction(BankAccount account, TransactionType transactionType)
    {
        if (decimal.TryParse(Console.ReadLine(), out var amount))
        {
            try
            {
                if (transactionType == TransactionType.Deposit)
                {
                    account.Deposit(amount);
                }
                else
                {
                    account.Withdraw(amount);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid amount. Please enter a positive number");
        }
    }
}