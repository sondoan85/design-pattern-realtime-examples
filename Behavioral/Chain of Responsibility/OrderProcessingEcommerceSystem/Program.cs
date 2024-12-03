namespace OrderProcessingEcommerceSystem
{
    public class Order
    {
        public bool IsValid { get; set; } = true;

        public void ApplyDiscount()
        {
            Console.WriteLine("Discount Applied...");
            //Apply discount logic here
        }

        public bool ProcessPayment()
        {
            //Payment logic here 
            Console.WriteLine("Payment Processed...");
            return true;
        }

        public void Ship()
        {
            // Shipping logic here
            Console.WriteLine("Order Shipped...");
        }
    }

    //Abstract Handler
    public abstract class OrderHandler
    {
        protected OrderHandler nextHandler;

        public void SetNext(OrderHandler handler)
        {
            nextHandler = handler;
        }

        public abstract void Process(Order order);
    }

    public class ValidationHandler : OrderHandler
    {
        public override void Process(Order order)
        {
            if (order.IsValid)
            {
                Console.WriteLine("Order validation passed.");
                if (nextHandler != null) nextHandler.Process(order);
            }
            else
            {
                Console.WriteLine("Order validation failed. Halting process.");
            }
        }
    }

    public class DiscountHandler : OrderHandler
    {
        public override void Process(Order order)
        {
            order.ApplyDiscount();
            Console.WriteLine("Discount applied to order if any.");
            if (nextHandler != null) nextHandler.Process(order);
        }
    }

    public class PaymentHandler : OrderHandler
    {
        public override void Process(Order order)
        {
            if (order.ProcessPayment())
            {
                Console.WriteLine("Payment processed successfully.");
                if (nextHandler != null) nextHandler.Process(order);
            }
            else
            {
                Console.WriteLine("Payment processing failed. Halting process.");
            }
        }
    }

    public class ShippingHandler : OrderHandler
    {
        public override void Process(Order order)
        {
            order.Ship();
            Console.WriteLine("Order shipped to customer.");
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            // Setup order processing chain
            var validation = new ValidationHandler();
            var discount = new DiscountHandler();
            var payment = new PaymentHandler();
            var shipping = new ShippingHandler();

            validation.SetNext(discount);
            discount.SetNext(payment);
            payment.SetNext(shipping);

            var order = new Order();
            validation.Process(order);

            Console.ReadLine();
        }
    }
}
