namespace PaymentGatewayIntegration
{
    public interface IPaymentGateway
    {
        void ProcessPayment(decimal amount);
    }

    public class CreditCardPaymentGateway : IPaymentGateway
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing Credit card payment of amount: ${amount}");
        }
    }

    public class PaypalPaymentGateway : IPaymentGateway
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing Paypal payment of amount: ${amount}");
        }
    }

    public class BitcoinPaymentGateway : IPaymentGateway
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing Bitcoin payment of amount: ${amount}");
        }
    }

    public abstract class PaymentGatewayFactory
    {
        public abstract IPaymentGateway CreatePaymentGateway();
    }

    public class CreditCardPaymentGatewayFactory : PaymentGatewayFactory
    {
        public override IPaymentGateway CreatePaymentGateway()
        {
            return new CreditCardPaymentGateway();
        }
    }

    public class PaypalPaymentGatewayFactory : PaymentGatewayFactory
    {
        public override IPaymentGateway CreatePaymentGateway()
        {
            return new PaypalPaymentGateway();
        }
    }

    public class BitcoinPaymentGatewayFactory : PaymentGatewayFactory
    {
        public override IPaymentGateway CreatePaymentGateway()
        {
            return new BitcoinPaymentGateway();
        }
    }

    //Client Code
    public class ECommercePlatform
    {
        public void Checkout(PaymentGatewayFactory factory, decimal amount)
        {
            IPaymentGateway paymentGateway = factory.CreatePaymentGateway();
            paymentGateway.ProcessPayment(amount);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var platform = new ECommercePlatform();

            platform.Checkout(new CreditCardPaymentGatewayFactory(), 100.75M);

            platform.Checkout(new PaypalPaymentGatewayFactory(), 150.50M);

            platform.Checkout(new BitcoinPaymentGatewayFactory(), 50.30M);

            Console.ReadKey();
        }
    }
}
