namespace tasks_solutions
{
    internal class Program1;



    namespace BankingSystemApp
    {
        internal class Program1
        {
            // Shared data storage - declared at class level (static) so that
            // EVERY function below can read and modify the same three lists
            // without needing them passed in as parameters.
            static List<string> customerNames = new List<string>();
            static List<string> accountNumbers = new List<string>();
            static List<double> balances = new List<double>();

            static void Main1(string[] args)
            {
                bool exitApp = false;

                while (!exitApp)
                {
                    Console.WriteLine("\n--- Welcome to zy Bank ---");
                    Console.WriteLine("1. Add New Account");
                    Console.WriteLine("2. Deposit Money");
                    Console.WriteLine("3. Withdraw Money");
                    Console.WriteLine("4. Show Balance");
                    Console.WriteLine("5. Transfer Amount");
                    Console.WriteLine("6. List All Accounts");
                    Console.WriteLine("7. Find Richest Customer");
                    Console.WriteLine("8. Exit");
                    Console.Write("Choose an option: ");

                    int choice;
                    try
                    {
                        choice = int.Parse(Console.ReadLine()!);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid input. plz enter a num from 1 to 8.");
                        continue; // skip the rest of this loop pass, show the menu again
                    }

                    switch (choice)
                    {
                        case 1:
                            AddAccount();
                            break;
                        case 2:
                            DepositMoney();
                            break;
                        case 3:
                            WithdrawMoney();
                            break;
                        case 4:
                            ShowBalance();
                            break;
                        case 5:
                            TransferAmount();
                            break;
                        case 6:
                            ListAllAccounts();
                            break;
                        case 7:
                            FindRichestCustomer();
                            break;
                        case 8:
                            exitApp = true;
                            Console.WriteLine("Thank you for banking with zy Bank. Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Invalid option, please choose between 1 and 8.");
                            break;
                    }
                }

                // ===================== SERVICE FUNCTIONS =====================
                // Each function owns ONE service end-to-end: it asks the user for
                // whatever it needs, validates it, updates the shared lists, and
                // prints the outcome. Main never reads input or prints results
                // for these services - it only shows the menu and calls them.

                static void AddAccount()
                {
                    Console.Write("Enter customer name: ");
                    string name = Console.ReadLine()!;

                    Console.Write("Enter a new account number: ");
                    string accNum = Console.ReadLine()!;

                    // an account number can't be reused, so check before we go any further
                    if (accountNumbers.Contains(accNum))
                    {
                        Console.WriteLine("That account number is already taken, Try a different one");
                        return;
                    }

                    double initialDeposit;
                    try
                    {
                        Console.Write("Enter the initial deposit amount: ");
                        initialDeposit = double.Parse(Console.ReadLine()!);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("That doesn't look like a valid amount, Account not created.");
                        return;
                    }

                    if (initialDeposit < 0)
                    {
                        Console.WriteLine("Initial deposit can't be negative, Account not created.");
                        return;
                    }

                    // keep the three lists moving together - same index for the same account
                    customerNames.Add(name);
                    accountNumbers.Add(accNum);
                    balances.Add(initialDeposit);

                    Console.WriteLine("\nAccount created successfully!");
                    Console.WriteLine("Customer: " + name);
                    Console.WriteLine("Account Number: " + accNum);
                    Console.WriteLine("Starting Balance: " + initialDeposit.ToString("F2"));
                }

                static void DepositMoney()
                {
                    Console.Write("Enter account number: ");
                    string accNum = Console.ReadLine()!;

                    int index = accountNumbers.IndexOf(accNum);
                    if (index == -1)
                    {
                        Console.WriteLine("No account found with that number");
                        return;
                    }

                    double amount;
                    try
                    {
                        Console.Write("Enter deposit amount: ");
                        amount = double.Parse(Console.ReadLine()!);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("That doesn't look like a valid amount. Deposit cancelled");
                        return;
                    }

                    if (amount <= 0)
                    {
                        Console.WriteLine("Deposit amount must be positive");
                        return;
                    }

                    balances[index] += amount;
                    Console.WriteLine("Deposit successful, New balance for " + customerNames[index] +
                        " is " + balances[index].ToString("F2"));
                }

                static void WithdrawMoney()
                {
                    Console.Write("Enter account number: ");
                    string accNum = Console.ReadLine()!;

                    int index = accountNumbers.IndexOf(accNum);
                    if (index == -1)
                    {
                        Console.WriteLine("No account found with that number.");
                        return;
                    }

                    double amount;
                    try
                    {
                        Console.Write("Enter withdrawal amount: ");
                        amount = double.Parse(Console.ReadLine()!);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("That doesn't look like a valid amount, Withdrawal cancelled");
                        return;
                    }

                    if (amount <= 0)
                    {
                        Console.WriteLine("Withdrawal amount must be positive");
                        return;
                    }

                    if (amount > balances[index])
                    {
                        Console.WriteLine("Insufficient balance. Current balance is " + balances[index].ToString("F2"));
                        return;
                    }

                    balances[index] -= amount;
                    Console.WriteLine("Withdrawal successful. New balance for " + customerNames[index] +
                        " is " + balances[index].ToString("F2"));
                }

                static void ShowBalance()
                {
                    Console.Write("Enter account number: ");
                    string accNum = Console.ReadLine()!;

                    int index = accountNumbers.IndexOf(accNum);
                    if (index == -1)
                    {
                        Console.WriteLine("No account found with that number.");
                        return;
                    }

                    Console.WriteLine("\nCustomer: " + customerNames[index]);
                    Console.WriteLine("Account Number: " + accountNumbers[index]);
                    Console.WriteLine("Current Balance: " + balances[index].ToString("F2"));
                }

                static void TransferAmount()
                {
                    Console.Write("Enter sender's account number: ");
                    string senderAcc = Console.ReadLine()!;

                    Console.Write("Enter receiver's account number: ");
                    string receiverAcc = Console.ReadLine()!;

                    int senderIndex = accountNumbers.IndexOf(senderAcc);
                    int receiverIndex = accountNumbers.IndexOf(receiverAcc);

                    if (senderIndex == -1 || receiverIndex == -1)
                    {
                        Console.WriteLine("One or both account numbers were not found, Transfer cancelled");
                        return;
                    }

                    double amount;
                    try
                    {
                        Console.Write("Enter amount to transfer: ");
                        amount = double.Parse(Console.ReadLine()!);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("That doesn't look like a valid amount, Transfer cancelled");
                        return;
                    }

                    if (amount <= 0)
                    {
                        Console.WriteLine("Transfer amount must be positive");
                        return;
                    }

                    if (amount > balances[senderIndex])
                    {
                        Console.WriteLine("Sender doesn't have enough balance for this transfer");
                        return;
                    }

                    balances[senderIndex] -= amount;
                    balances[receiverIndex] += amount;

                    Console.WriteLine("\nTransfer complete");
                    Console.WriteLine(customerNames[senderIndex] + "'s new balance: " + balances[senderIndex].ToString("F2"));
                    Console.WriteLine(customerNames[receiverIndex] + "'s new balance: " + balances[receiverIndex].ToString("F2"));
                }

                // ---- Custom Service 1: List All Accounts ----
                // Just prints every account currently stored in the system, so the
                // user can see everything at a glance instead of looking one up at a time.
                static void ListAllAccounts()
                {
                    if (customerNames.Count == 0)
                    {
                        Console.WriteLine("There are no accounts in the system yet.");
                        return;
                    }

                    Console.WriteLine("\n----- All Accounts -----");
                    for (int i = 0; i < customerNames.Count; i++)
                    {
                        Console.WriteLine((i + 1) + ". " + customerNames[i] + " | Acc#: " + accountNumbers[i] +
                            " | Balance: " + balances[i].ToString("F2"));
                    }
                }

                // ---- Custom Service 2: Find Richest Customer ----
                // Loops through balances to find the highest one and reports who owns it.
                static void FindRichestCustomer()
                {
                    if (balances.Count == 0)
                    {
                        Console.WriteLine("No accounts to check yet");
                        return;
                    }

                    int richestIndex = 0;
                    for (int i = 1; i < balances.Count; i++)
                    {
                        if (balances[i] > balances[richestIndex])
                        {
                            richestIndex = i;
                        }
                    }

                    Console.WriteLine("\nThe richest customer right now is " + customerNames[richestIndex] +
                        " with a balance of " + balances[richestIndex].ToString("F2") +
                        " (Account: " + accountNumbers[richestIndex] + ")");
                }
            }
        }

    }
}

