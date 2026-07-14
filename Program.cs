using System;

public class BankAccount
{
    public int AccountNumber { get; private set; }
    public string HolderName { get; private set; }
    public double Balance { get; private set; }

    // Read-only computed property (Case 18)
    public bool IsOverdrawn => Balance < 0;

    // Default constructor
    public BankAccount()
    {
        AccountNumber = 0;
        HolderName = "Unknown";
        Balance = 0;
    }

    // Parameterized constructor (used directly in Case 16 and in Program's static fields)
    public BankAccount(int accountNumber, string holderName, double balance)
    {
        AccountNumber = accountNumber;
        HolderName = holderName;
        Balance = balance;
    }

    public void Deposit(double amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Deposit failed: amount must be greater than zero.");
            return;
        }

        Balance += amount;
    }

    public void Withdraw(double amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Withdrawal failed: amount must be greater than zero.");
            return;
        }

        if (amount > Balance)
        {
            Console.WriteLine("Withdrawal failed: insufficient balance.");
            return;
        }

        Balance -= amount;
    }

    public void CheckBalance()
    {
        Console.WriteLine($"Account Number: {AccountNumber}");
        Console.WriteLine($"Holder Name:    {HolderName}");
        Console.WriteLine($"Balance:        {Balance:F3}");
    }
}

public class Student
{
    // Static counter (Case 17)
    private static int totalStudents = 0;

    public string Name { get; set; }
    public string Address { get; set; }
    public int Grade { get; set; }

    // Private field - can only be set through Register()
    private string email;

    // Write-only property (Case 19) - no getter, so it cannot be read back
    private int securityPin;
    public int SecurityPin
    {
        set { securityPin = value; }
    }

    public Student(string name, string address, int grade)
    {
        Name = name;
        Address = address;
        Grade = grade;
        email = string.Empty;
        securityPin = 0;

        totalStudents++;
    }

    public void Register(string email)
    {
        this.email = email;
    }

    public static int GetTotalStudents()
    {
        return totalStudents;
    }
}

public class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int StockQuantity { get; private set; }

    public Product(string name, double price, int stockQuantity)
    {
        Name = name;
        Price = price;
        StockQuantity = stockQuantity;
    }

    public double GetInventoryValue()
    {
        return Price * StockQuantity;
    }

    public void Restock(int quantity)
    {
        if (quantity <= 0)
        {
            Console.WriteLine("Restock failed: quantity must be greater than zero.");
            return;
        }

        StockQuantity += quantity;
    }

    public void Sell(int quantity)
    {
        if (quantity <= 0)
        {
            Console.WriteLine("Sale failed: quantity must be greater than zero.");
            return;
        }

        if (quantity > StockQuantity)
        {
            Console.WriteLine("Sale failed: not enough stock.");
            return;
        }

        StockQuantity -= quantity;
    }
}

public static class InputHelper
{
    public static BankAccount ChooseAccount(BankAccount acc1, BankAccount acc2)
    {
        while (true)
        {
            Console.WriteLine($"1. {acc1.HolderName} (Account #{acc1.AccountNumber})");
            Console.WriteLine($"2. {acc2.HolderName} (Account #{acc2.AccountNumber})");
            Console.Write("Choose an account (1 or 2): ");
            string input = Console.ReadLine()!;

            if (input == "1") return acc1;
            if (input == "2") return acc2;

            Console.WriteLine("Invalid choice. Please enter 1 or 2.");
        }
    }

    public static Student ChooseStudent(Student s1, Student s2)
    {
        while (true)
        {
            Console.WriteLine($"1. {s1.Name}");
            Console.WriteLine($"2. {s2.Name}");
            Console.Write("Choose a student (1 or 2): ");
            string input = Console.ReadLine()!;

            if (input == "1") return s1;
            if (input == "2") return s2;

            Console.WriteLine("Invalid choice. Please enter 1 or 2.");
        }
    }

    public static Product ChooseProduct(Product p1, Product p2)
    {
        while (true)
        {
            Console.WriteLine($"1. {p1.Name}");
            Console.WriteLine($"2. {p2.Name}");
            Console.Write("Choose a product (1 or 2): ");
            string input = Console.ReadLine()!;

            if (input == "1") return p1;
            if (input == "2") return p2;

            Console.WriteLine("Invalid choice. Please enter 1 or 2.");
        }
    }

    public static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine()!;

            if (double.TryParse(input, out double value))
            {
                return value;
            }

            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }

    public static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine()!;

            if (int.TryParse(input, out int value))
            {
                return value;
            }

            Console.WriteLine("Invalid input. Please enter a valid whole number.");
        }
    }
}
// Case 1 - View Account Details
public static class Case01
{
    public static void Run(BankAccount acc1, BankAccount acc2)
    {
        Console.WriteLine("--- Case 1: View Account Details ---");
        BankAccount chosen = InputHelper.ChooseAccount(acc1, acc2);
        chosen.CheckBalance();
    }
}


// Case 2 - Update Student Address
public static class Case02
{
    public static void Run(Student s1, Student s2)
    {
        Console.WriteLine("--- Case 2: Update Student Address ---");
        Student chosen = InputHelper.ChooseStudent(s1, s2);
        Console.Write("Enter new address: ");
        string newAddress = Console.ReadLine()!;

        chosen.Address = newAddress;
        Console.WriteLine($"Address updated. {chosen.Name}'s new address is: {chosen.Address}");
    }
}


// Case 3 - Make a Deposit
public static class Case03
{
    public static void Run(BankAccount acc1, BankAccount acc2)
    {
        Console.WriteLine("--- Case 3: Make a Deposit ---");
        BankAccount chosen = InputHelper.ChooseAccount(acc1, acc2);
        double amount = InputHelper.ReadDouble("Enter deposit amount: ");

        chosen.Deposit(amount);
        Console.WriteLine($"{chosen.HolderName}'s updated balance: {chosen.Balance:F3}");
    }
}

// Case 4 - Make a Withdrawal
public static class Case04
{
    public static void Run(BankAccount acc1, BankAccount acc2)
    {
        Console.WriteLine("--- Case 4: Make a Withdrawal ---");
        BankAccount chosen = InputHelper.ChooseAccount(acc1, acc2);
        double amount = InputHelper.ReadDouble("Enter withdrawal amount: ");

        // Withdraw() itself already protects against overdrawing
        chosen.Withdraw(amount);
        Console.WriteLine($"Updated balance: {chosen.Balance:F3}");
    }
}


// Case 5 - View Product Details
public static class Case05
{
    public static void Run(Product p1, Product p2)
    {
        Console.WriteLine("--- Case 5: View Product Details ---");
        Product chosen = InputHelper.ChooseProduct(p1, p2);
        double value = chosen.GetInventoryValue();

        Console.WriteLine($"Total Inventory Value: {value:F3}");
    }
}

// Case 6 - Register a Student
public static class Case06
{
    public static void Run(Student s1, Student s2)
    {
        Console.WriteLine("--- Case 6: Register a Student ---");
        Student chosen = InputHelper.ChooseStudent(s1, s2);
        Console.Write("Enter email: ");
        string email = Console.ReadLine()!;

        // The private "email" field can ONLY be set through Register()
        chosen.Register(email);

        // Confirmation message never reveals the email
        Console.WriteLine($"{chosen.Name} has been registered successfully.");
    }
}


// Case 7 - Compare Two Account Balances
public static class Case07
{
    public static void Run(BankAccount acc1, BankAccount acc2)
    {
        Console.WriteLine("--- Case 7: Compare Two Account Balances ---");

        if (acc1.Balance > acc2.Balance)
        {
            Console.WriteLine($"{acc1.HolderName} holds more money ({acc1.Balance:F3} vs {acc2.Balance:F3}).");
        }
        else if (acc2.Balance > acc1.Balance)
        {
            Console.WriteLine($"{acc2.HolderName} holds more money ({acc2.Balance:F3} vs {acc1.Balance:F3}).");
        }
        else
        {
            Console.WriteLine("Both accounts have equal balances.");
        }
    }
}

// Case 8 - Restock Product & Stock Level Check
public static class Case08
{
    public static void Run(Product p1, Product p2)
    {
        Console.WriteLine("--- Case 8: Restock Product & Stock Level Check ---");
        Product chosen = InputHelper.ChooseProduct(p1, p2);
        int qty = InputHelper.ReadInt("Enter quantity to restock: ");

        chosen.Restock(qty);

        if (chosen.StockQuantity < 10)
        {
            Console.WriteLine($"Stock level: Low ({chosen.StockQuantity} units).");
        }
        else if (chosen.StockQuantity <= 49)
        {
            Console.WriteLine($"Stock level: Moderate ({chosen.StockQuantity} units).");
        }
        else
        {
            Console.WriteLine($"Stock level: Well Stocked ({chosen.StockQuantity} units).");
        }
    }
}

// Case 9 - Transfer Between Accounts
public static class Case09
{
    public static void Run(BankAccount acc1, BankAccount acc2)
    {
        Console.WriteLine("--- Case 9: Transfer Between Accounts ---");

        Console.WriteLine("Select the SOURCE account:");
        BankAccount source = InputHelper.ChooseAccount(acc1, acc2);

        Console.WriteLine("Select the DESTINATION account:");
        BankAccount destination = InputHelper.ChooseAccount(acc1, acc2);

        if (source == destination)
        {
            Console.WriteLine("Transfer failed: source and destination cannot be the same account.");
            return;
        }

        double amount = InputHelper.ReadDouble("Enter amount to transfer: ");

        // Check BEFORE changing anything
        if (source.Balance >= amount)
        {
            source.Withdraw(amount);
            destination.Deposit(amount);
            Console.WriteLine($"Transfer successful. {source.HolderName}'s new balance: {source.Balance:F3} | {destination.HolderName}'s new balance: {destination.Balance:F3}");
        }
        else
        {
            Console.WriteLine("Transfer failed: source account has insufficient balance. No accounts were changed.");
        }
    }
}

// Case 10 - Update Student Grade (Validated)
public static class Case10
{
    public static void Run(Student s1, Student s2)
    {
        Console.WriteLine("--- Case 10: Update Student Grade (Validated) ---");
        Student chosen = InputHelper.ChooseStudent(s1, s2);

        Console.Write("Enter new grade: ");
        string input = Console.ReadLine()!;

        int grade;
        if (!int.TryParse(input, out grade))
        {
            Console.WriteLine("Update rejected: the value entered is not a valid number. No change was made.");
            return;
        }

        if (grade < 0 || grade > 100)
        {
            Console.WriteLine("Update rejected: grade must be between 0 and 100. No change was made.");
            return;
        }

        chosen.Grade = grade;
        Console.WriteLine($"{chosen.Name}'s grade was updated to {chosen.Grade}.");
    }
}

// Case 11 - Student Report Card
public static class Case11
{
    public static void Run(Student s1, Student s2)
    {
        Console.WriteLine("--- Case 11: Student Report Card ---");
        Student chosen = InputHelper.ChooseStudent(s1, s2);

        string status = chosen.Grade >= 60 ? "Pass" : "Fail";

        Console.WriteLine("========= Report Card =========");
        Console.WriteLine($"Name:    {chosen.Name}");
        Console.WriteLine($"Address: {chosen.Address}");
        Console.WriteLine($"Grade:   {chosen.Grade}");
        Console.WriteLine($"Status:  {status}");
        Console.WriteLine("================================");
    }
}

// Case 12 - Account Health Status
public static class Case12
{
    public static void Run(BankAccount acc1, BankAccount acc2)
    {
        Console.WriteLine("--- Case 12: Account Health Status ---");
        BankAccount chosen = InputHelper.ChooseAccount(acc1, acc2);

        if (chosen.Balance < 50)
        {
            Console.WriteLine("Status: Low Balance");
        }
        else if (chosen.Balance <= 1000)
        {
            Console.WriteLine("Status: Healthy");
        }
        else
        {
            Console.WriteLine("Status: Premium");
        }
    }
}

// Case 13 - Bulk Sale With Revenue Calculation
public static class Case13
{
    public static void Run(Product p1, Product p2)
    {
        Console.WriteLine("--- Case 13: Bulk Sale With Revenue Calculation ---");
        Product chosen = InputHelper.ChooseProduct(p1, p2);
        int qty = InputHelper.ReadInt("Enter quantity to sell: ");

        if (qty > chosen.StockQuantity)
        {
            int shortage = qty - chosen.StockQuantity;
            Console.WriteLine($"Not enough stock. You need {shortage} more unit(s) to fulfill this order. Nothing was sold.");
            return;
        }

        chosen.Sell(qty);
        double revenue = qty * chosen.Price;
        Console.WriteLine($"Sale completed. Total revenue: {revenue:F3}");
    }
}

// Case 14 - Scholarship Eligibility Check
public static class Case14
{
    public static void Run(Student s1, Student s2, BankAccount acc1, BankAccount acc2)
    {
        Console.WriteLine("--- Case 14: Scholarship Eligibility Check ---");
        Student student = InputHelper.ChooseStudent(s1, s2);
        BankAccount account = InputHelper.ChooseAccount(acc1, acc2);

        bool gradeOk = student.Grade >= 80;
        bool balanceOk = account.Balance >= 100;

        if (gradeOk && balanceOk)
        {
            Console.WriteLine("Eligible");
        }
        else
        {
            Console.WriteLine("Not Eligible. Reason(s):");
            if (!gradeOk)
            {
                Console.WriteLine($" - Grade ({student.Grade}) is below the required 80.");
            }
            if (!balanceOk)
            {
                Console.WriteLine($" - Balance ({account.Balance:F3}) is below the required 100.");
            }
        }
    }
}

// Case 15 - Full Balance Top-Up Flow
public static class Case15
{
    public static void Run(BankAccount acc1, BankAccount acc2)
    {
        Console.WriteLine("--- Case 15: Full Balance Top-Up Flow ---");
        BankAccount chosen = InputHelper.ChooseAccount(acc1, acc2);
        double before = chosen.Balance;

        if (before < 50)
        {
            double topUp = 100 - before;
            chosen.Deposit(topUp);
            Console.WriteLine($"Balance before: {before:F3} | Topped up by: {topUp:F3} | Balance after: {chosen.Balance:F3}");
        }
        else
        {
            Console.WriteLine("No top-up needed. Balance is already 50 or above.");
        }
    }
}


// Case 16 - Quick Account Opening [Parameterized Constructor]
public static class Case16
{
    public static BankAccount Run()
    {
        Console.WriteLine("--- Case 16: Quick Account Opening (Parameterized Constructor) ---");
        int accNum = InputHelper.ReadInt("Enter new account number: ");
        Console.Write("Enter holder name: ");
        string name = Console.ReadLine()!;
        double startBalance = InputHelper.ReadDouble("Enter starting balance: ");

        // Created using ONLY the parameterized constructor - no separate
        // property assignments afterward.
        BankAccount account = new BankAccount(accNum, name, startBalance);

        Console.WriteLine("New account created successfully:");
        account.CheckBalance();

        return account;
    }
}

// Case 17 - Total Students Counter [Static Fields & Methods]
public static class Case17
{
    public static void Run()
    {
        Console.WriteLine("--- Case 17: Total Students Counter (Static) ---");

        // Called through the class name, not through an object
        int total = Student.GetTotalStudents();

        Console.WriteLine($"Total Student objects created so far: {total}");
    }
}

// Case 18 - Overdrawn Account Check [Read-Only Property]
public static class Case18
{
    public static void Run(BankAccount acc1, BankAccount acc2)
    {
        Console.WriteLine("--- Case 18: Overdrawn Account Check (Read-Only Property) ---");
        BankAccount chosen = InputHelper.ChooseAccount(acc1, acc2);

        if (chosen.IsOverdrawn)
        {
            Console.WriteLine($"{chosen.HolderName}'s account is currently OVERDRAWN.");
        }
        else
        {
            Console.WriteLine($"{chosen.HolderName}'s account is not overdrawn.");
        }
    }
}

// Case 19 - Set Student Security PIN [Write-Only Property]
public static class Case19
{
    public static void Run(Student s1, Student s2)
    {
        Console.WriteLine("--- Case 19: Set Student Security PIN (Write-Only Property) ---");
        Student chosen = InputHelper.ChooseStudent(s1, s2);

        int pin;
        while (true)
        {
            Console.Write("Enter a 4-digit PIN: ");
            string input = Console.ReadLine()!;
            if (input != null && input.Length == 4 && int.TryParse(input, out pin))
            {
                break;
            }
            Console.WriteLine("Invalid PIN. It must be exactly 4 digits.");
        }

        // The property cannot be read back - only written to
        chosen.SecurityPin = pin;

        Console.WriteLine($"PIN set successfully for {chosen.Name}.");
    }
}


public class Program
{
    // ---- The six required objects (individual, NOT in a collection) ----
    static BankAccount acc1 = new BankAccount(1163, "karim", 120);
    static BankAccount acc2 = new BankAccount(15203, "Ali", 63);

    static Student stu1 = new Student("Ali", "Muscat", 65);
    static Student stu2 = new Student("Ahmed", "Muscat", 70);

    static Product prod1 = new Product("Wireless Mouse", 5.500, 50);
    static Product prod2 = new Product("Mechanical Keyboard", 15.750, 20);

    // Holds the extra account created in Case 16, once the user creates one
    static BankAccount newAccount = null;

    public static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            PrintMenu();
            string choice = Console.ReadLine()!;
            Console.WriteLine();

            switch (choice)
            {
                case "1": Case01.Run(acc1, acc2); break;
                case "2": Case02.Run(stu1, stu2); break;
                case "3": Case03.Run(acc1, acc2); break;
                case "4": Case04.Run(acc1, acc2); break;
                case "5": Case05.Run(prod1, prod2); break;
                case "6": Case06.Run(stu1, stu2); break;
                case "7": Case07.Run(acc1, acc2); break;
                case "8": Case08.Run(prod1, prod2); break;
                case "9": Case09.Run(acc1, acc2); break;
                case "10": Case10.Run(stu1, stu2); break;
                case "11": Case11.Run(stu1, stu2); break;
                case "12": Case12.Run(acc1, acc2); break;
                case "13": Case13.Run(prod1, prod2); break;
                case "14": Case14.Run(stu1, stu2, acc1, acc2); break;
                case "15": Case15.Run(acc1, acc2); break;
                case "16": newAccount = Case16.Run(); break;
                case "17": Case17.Run(); break;
                case "18": Case18.Run(acc1, acc2); break;
                case "19": Case19.Run(stu1, stu2); break;
                case "20":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please pick a number from 1 to 20.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void PrintMenu()
    {
        Console.WriteLine("========== Bank & Student Management ==========");
        Console.WriteLine("1.  View Account Details");
        Console.WriteLine("2.  Update Student Address");
        Console.WriteLine("3.  Make a Deposit");
        Console.WriteLine("4.  Make a Withdrawal");
        Console.WriteLine("5.  View Product Details");
        Console.WriteLine("6.  Register a Student");
        Console.WriteLine("7.  Compare Two Account Balances");
        Console.WriteLine("8.  Restock Product & Stock Level Check");
        Console.WriteLine("9.  Transfer Between Accounts");
        Console.WriteLine("10. Update Student Grade (Validated)");
        Console.WriteLine("11. Student Report Card");
        Console.WriteLine("12. Account Health Status");
        Console.WriteLine("13. Bulk Sale With Revenue Calculation");
        Console.WriteLine("14. Scholarship Eligibility Check");
        Console.WriteLine("15. Full Balance Top-Up Flow");
        Console.WriteLine("16. Quick Account Opening (Parameterized Constructor)");
        Console.WriteLine("17. Total Students Counter (Static)");
        Console.WriteLine("18. Overdrawn Account Check (Read-Only Property)");
        Console.WriteLine("19. Set Student Security PIN (Write-Only Property)");
        Console.WriteLine("20. Exit");
        Console.Write("Enter your choice: ");
    }
}