namespace BankingSystem
{
    public class Program
    {
        //Command Interface
        public interface IBankCommand
        {
            void Execute();
        }
        //Receiver - Bank Account
        public class BankAccount
        {
            public decimal Balance { get; private set; }
            public void Deposit(decimal amount)
            {
                Balance += amount;
                Console.WriteLine($"Deposited ${amount}. New balance: ${Balance}");
            }
            public void Withdraw(decimal amount)
            {
                if (Balance >= amount)
                {
                    Balance -= amount;
                    Console.WriteLine($"Withdrew ${amount}. New balance: ${Balance}");
                }
                else
                {
                    Console.WriteLine("Insufficient funds.");
                }
            }
            public void Transfer(BankAccount toAccount, decimal amount)
            {
                if (Balance >= amount)
                {
                    Balance -= amount;
                    toAccount.Deposit(amount);
                    Console.WriteLine($"Transferred ${amount} to another account. New balance: ${Balance}");
                }
                else
                {
                    Console.WriteLine("Insufficient funds for transfer.");
                }
            }
        }
        //Concrete Commands
        public class DepositCommand : IBankCommand
        {
            private BankAccount _account;
            private decimal _amount;
            public DepositCommand(BankAccount account, decimal amount)
            {
                _account = account;
                _amount = amount;
            }
            public void Execute()
            {
                _account.Deposit(_amount);
            }
        }
        public class WithdrawCommand : IBankCommand
        {
            private BankAccount _account;
            private decimal _amount;
            public WithdrawCommand(BankAccount account, decimal amount)
            {
                _account = account;
                _amount = amount;
            }
            public void Execute()
            {
                _account.Withdraw(_amount);
            }
        }
        public class TransferCommand : IBankCommand
        {
            private BankAccount _fromAccount;
            private BankAccount _toAccount;
            private decimal _amount;
            public TransferCommand(BankAccount fromAccount, BankAccount toAccount, decimal amount)
            {
                _fromAccount = fromAccount;
                _toAccount = toAccount;
                _amount = amount;
            }
            public void Execute()
            {
                _fromAccount.Transfer(_toAccount, _amount);
            }
        }
        //Invoker - Bank App
        public class BankApp
        {
            public void PerformOperation(IBankCommand command)
            {
                command.Execute();
            }
        }

        public static void Main(string[] args)
        {
            BankAccount johnsAccount = new BankAccount();
            BankAccount janesAccount = new BankAccount();

            BankApp bankApp = new BankApp();
            bankApp.PerformOperation(new DepositCommand(johnsAccount, 500));
            bankApp.PerformOperation(new WithdrawCommand(johnsAccount, 200));
            bankApp.PerformOperation(new TransferCommand(johnsAccount, janesAccount, 150));

            Console.ReadKey();
        }
    }
}
