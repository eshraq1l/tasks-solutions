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
        Console.WriteLine($"Address updated. {chosen.Name}'s new address is: {chosen.Address}");
    }
}
using System;

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