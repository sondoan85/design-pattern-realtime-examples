namespace PaymentMethods
{
    //Strategy (Interface)
    public interface IPaymentStrategy
    {
        bool ProcessPayment(double amount);
    }

    //Concrete Strategies
    public class CreditCardPayment : IPaymentStrategy
    {
        private string _cardNumber;
        private string _expiryDate;
        private string _cvc;

        public CreditCardPayment(string cardNumber, string expiryDate, string cvc)
        {
            _cardNumber = cardNumber;
            _expiryDate = expiryDate;
            _cvc = cvc;
        }
        public bool ProcessPayment(double amount)
        {
            // Logic to process credit card payment
            Console.WriteLine($"Processed payment of ${amount} using Credit Card");
            return true;
        }
    }

    public class PayPalPayment : IPaymentStrategy
    {
        private string _email;

        public PayPalPayment(string email)
        {
            _email = email;
        }

        public bool ProcessPayment(double amount)
        {
            // Logic to process credit card payment
            Console.WriteLine($"Processed payment of ${amount} using PayPal");
            return true;
        }
    }

    public class CryptoPayment : IPaymentStrategy
    {
        private string _cryptoWalletAddress;

        public CryptoPayment(string walletAddress)
        {
            string _cryptoWalletAddress = walletAddress;
        }

        public bool ProcessPayment(double amount)
        {
            // Logic to process credit card payment
            Console.WriteLine($"Processed payment of ${amount} using Cryptocurrency");
            return true;
        }
    }

    //Context
    public class Checkout
    {
        private IPaymentStrategy _paymentMethod;

        public Checkout(IPaymentStrategy paymentMethod)
        {
            _paymentMethod = paymentMethod;
        }

        public bool CompletePurchase(double amount)
        {
            return _paymentMethod.ProcessPayment(amount);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            double purchaseAmount = 100.00;

            // Choose Credit Card payment
            IPaymentStrategy paymentMethod = new CreditCardPayment("1234567890123456", "12/25", "123");
            Checkout checkout = new Checkout(paymentMethod);
            checkout.CompletePurchase(purchaseAmount);

            // Choose PayPal payment
            purchaseAmount = 200.00;
            paymentMethod = new PayPalPayment("user@example.com");
            checkout = new Checkout(paymentMethod);
            checkout.CompletePurchase(purchaseAmount);

            // Choose Cryptocurrency payment
            purchaseAmount = 300.00;
            paymentMethod = new CryptoPayment("1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa");
            checkout = new Checkout(paymentMethod);
            checkout.CompletePurchase(purchaseAmount);

            Console.ReadKey();
        }
    }
}
