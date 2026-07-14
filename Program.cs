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