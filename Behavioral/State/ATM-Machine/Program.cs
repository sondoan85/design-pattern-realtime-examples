using System.Reflection.Metadata;

namespace ATM_Machine
{
    // State Interface
    public interface IATMState
    {
        void InsertCard();
        void EjectCard();
        void EnterPin();
        void WithdrawMoney();
    }

    // Concrete State Classes
    public class NoCard : IATMState
    {
        public void InsertCard()
        {
            Console.WriteLine("Card Inserted. Please Enter the PIN.");
        }
        public void EjectCard()
        {
            Console.WriteLine("No Card to Eject.");
        }
        public void EnterPin()
        {
            Console.WriteLine("Please Insert Card First.");
        }
        public void WithdrawMoney()
        {
            Console.WriteLine("Insert and Verify Card to Withdraw Money.");
        }
    }

    public class CardInserted : IATMState
    {
        public void InsertCard()
        {
            Console.WriteLine("Card is Already Inserted.");
        }
        public void EjectCard()
        {
            Console.WriteLine("Card Ejected.");
        }
        public void EnterPin()
        {
            Console.WriteLine("PIN Accepted. You can now Withdraw Money.");
        }
        public void WithdrawMoney()
        {
            Console.WriteLine("Please Enter the PIN First.");
        }
    }

    public class PinVerified : IATMState
    {
        public void InsertCard()
        {
            Console.WriteLine("Card is Already Inserted.");
        }
        public void EjectCard()
        {
            Console.WriteLine("Card Ejected.");
        }
        public void EnterPin()
        {
            Console.WriteLine("PIN Already Verified.");
        }
        public void WithdrawMoney()
        {
            Console.WriteLine("Money Withdrawn. Card will be Ejected.");
        }
    }

    // Context
    public class ATM
    {
        public IATMState _currentState;

        public ATM()
        {
            // Default state.
            _currentState = new NoCard();
        }

        public void SetState(IATMState state)
        {
            _currentState = state;
        }

        public void InsertCard()
        {
            _currentState.InsertCard();
        }

        public void EjectCard()
        {
            _currentState.EjectCard();
        }

        public void EnterPin()
        {
            _currentState.EnterPin();
        }

        public void WithdrawMoney()
        {
            _currentState.WithdrawMoney();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            ATM atm = new ATM();

            atm.InsertCard();  // Output: Card Inserted. Please Enter the PIN.

            atm.SetState(new CardInserted());
            atm.EnterPin();    // Output: PIN accepted. You can now Withdraw Money.

            atm.SetState(new PinVerified());
            atm.WithdrawMoney();  // Output: Money Withdrawn. Card will be Ejected.

            atm.EjectCard();   // Output: Card Ejected.

            Console.ReadKey();
        }
    }
}
