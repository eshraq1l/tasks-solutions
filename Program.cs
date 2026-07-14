using System;

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
        Console.WriteLine("Address updated. {chosen.Name}'s new address is: {chosen.Address}");
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
        Console.WriteLine("{chosen.HolderName}'s updated balance: {chosen.Balance:F3}");
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
        Console.WriteLine("Updated balance: {chosen.Balance:F3}");
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

        Console.WriteLine("Total Inventory Value: {value:F3}");
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
        Console.WriteLine("{chosen.Name} has been registered successfully.");
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
            Console.WriteLine("{acc1.HolderName} holds more money ({acc1.Balance:F3} vs {acc2.Balance:F3}).");
        }
        else if (acc2.Balance > acc1.Balance)
        {
            Console.WriteLine("{acc2.HolderName} holds more money ({acc2.Balance:F3} vs {acc1.Balance:F3}).");
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
            Console.WriteLine("Stock level: Low ({chosen.StockQuantity} units).");
        }
        else if (chosen.StockQuantity <= 49)
        {
            Console.WriteLine("Stock level: Moderate ({chosen.StockQuantity} units).");
        }
        else
        {
            Console.WriteLine("Stock level: Well Stocked ({chosen.StockQuantity} units).");
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
            Console.WriteLine("Transfer successful. {source.HolderName}'s new balance: {source.Balance:F3} | {destination.HolderName}'s new balance: {destination.Balance:F3}");
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
using System;

// Case 11 - Student Report Card
public static class Case11
{
    public static void Run(Student s1, Student s2)
    {
        Console.WriteLine("--- Case 11: Student Report Card ---");
        Student chosen = InputHelper.ChooseStudent(s1, s2);

        string status = chosen.Grade >= 60 ? "Pass" : "Fail";

        Console.WriteLine("========= Report Card =========");
        Console.WriteLine("Name:    {chosen.Name}");
        Console.WriteLine("Address: {chosen.Address}");
        Console.WriteLine("Grade:   {chosen.Grade}");
        Console.WriteLine("Status:  {status}");
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
            Console.WriteLine("Balance before: {before:F3} | Topped up by: {topUp:F3} | Balance after: {chosen.Balance:F3}");
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
            Console.WriteLine("{chosen.HolderName}'s account is currently OVERDRAWN.");
        }
        else
        {
            Console.WriteLine("{chosen.HolderName}'s account is not overdrawn.");
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

        Console.WriteLine("PIN set successfully for {chosen.Name}.");
    }
}