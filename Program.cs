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
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public BankAccount1() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    }
}
