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
