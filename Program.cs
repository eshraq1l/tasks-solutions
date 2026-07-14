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