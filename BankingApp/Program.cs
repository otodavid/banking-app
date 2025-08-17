using BankingApp.Models;

namespace BankingApp;

class Program
{
    static void Main(string[] args)
    {
        var filePath = Path.Combine(AppContext.BaseDirectory, "accounts.json");
        var repository = new AccountsRepository(filePath);
        var bankService = new BankService(repository);

        Console.WriteLine("Choose an action");
        Console.WriteLine("1. New Customer");
        Console.WriteLine("2. Existing Customer");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("\nEnter owner name: ");
                string? owner = Console.ReadLine();
                if (string.IsNullOrEmpty(owner))
                {
                    Console.WriteLine("A valid owner name is required.");
                    return;
                }

                Console.Write("Enter initial deposit: ");
                if (!decimal.TryParse(Console.ReadLine(), out var deposit))
                {
                    Console.WriteLine("A valid owner name is required.");
                    return;
                }

                var newAccount = bankService.CreateAccount(owner, deposit);

                Console.WriteLine("\n✅ Your account has been successfully created");
                Console.WriteLine($"Your account number is {newAccount.AccountNumber}");

                MainBankFlow(repository, bankService);
                break;

            case "2":
                MainBankFlow(repository, bankService);
                break;
        }
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("\nMenu");
        Console.WriteLine("1. Deposit:");
        Console.WriteLine("2. Withdraw:");
        Console.WriteLine("3. View Balance:");
        Console.WriteLine("4. Print Statement:");
        Console.WriteLine("5. Exit:");
        Console.Write("Choose an action: ");
    }

    private static void MainBankFlow(AccountsRepository repository, BankService bankService)
    {
        Console.WriteLine("\nEnter Account Number: ");
        var accountNumber = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(accountNumber))
        {
            Console.WriteLine("A valid account number is needed to continue");
            return;
        }

        var account = repository.GetAccountByAccountNumber(accountNumber);

        if (account is null)
        {
            Console.WriteLine("Account not Found");
            return;
        }

        while (true)
        {
            DisplayMenu();

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\nEnter amount to deposit: ");
                    if (decimal.TryParse(Console.ReadLine(), out var depositAmount))
                    {
                        try
                        {
                            bankService.Deposit(depositAmount, account);
                            repository.AddOrUpdateAccount(account);
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

                    break;

                case "2":
                    Console.WriteLine("\nEnter amount to withdraw: ");
                    if (decimal.TryParse(Console.ReadLine(), out var withdrawAmount))
                    {
                        try
                        {
                            bankService.Withdraw(withdrawAmount, account);
                            repository.AddOrUpdateAccount(account);
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

                    break;

                case "3":
                    Console.WriteLine("\n");
                    account.PrintBalance();
                    break;

                case "4":
                    Console.WriteLine("\n");
                    account.PrintStatement();
                    break;

                case "5":
                    Console.WriteLine("\nThank you for banking with us");
                    return;

                default:
                    Console.WriteLine("Invalid Option");
                    break;
            }
        }
    }
}