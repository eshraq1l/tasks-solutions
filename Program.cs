namespace tasks_solutions
{
    internal class Program1;

    class BankAccount1
    {
        // ---- Public properties ----
        public int AccountNumber;
        public string HolderName;
        public double Balance;

        // Default (parameterless) constructor - needed because we also add
        // a parameterized one for Case 16.

 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        // ---- Case 16: Parameterized constructor ----
        public BankAccount1(int accountNumber, string holderName, double balance)
        {
            AccountNumber = accountNumber;
            HolderName = holderName;
            Balance = balance;
        }

        public void Deposit(double amount)
        {
            Balance += amount;
            SendEmail();
        }

        public void Withdraw(double amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
            }
            else
            {
                Console.WriteLine("Withdraw failed: insufficient balance.");
            }
            SendEmail();
        }

        public double CheckBalance()
        {
            PrintInformation();
            return Balance;
        }

        private void PrintInformation()
        {
            Console.WriteLine($"Holder: {HolderName} | Balance: {Balance}");
        }

        private void SendEmail()
        {
            Console.WriteLine("[Email] Notification sent to account holder.");
        }

    }
}
