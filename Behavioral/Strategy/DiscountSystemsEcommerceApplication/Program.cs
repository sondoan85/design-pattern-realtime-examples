namespace DiscountSystemsEcommerceApplication
{
    //Strategy (Interface)
    public interface IDiscountStrategy
    {
        double ApplyDiscount(double originalPrice);
    }
    //Concrete Strategies
    public class SeasonalDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(double originalPrice)
        {
            // For this example, let's assume a 10% discount for seasonal promotions.
            return originalPrice * 0.9;
        }
    }
    public class LoyaltyDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(double originalPrice)
        {
            // For loyal customers, the platform offers a 15% discount.
            return originalPrice * 0.85;
        }
    }
    public class EventDiscount : IDiscountStrategy
    {
        public double ApplyDiscount(double originalPrice)
        {
            // For special events, there's a 20% discount.
            return originalPrice * 0.8;
        }
    }
    //Context
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        private IDiscountStrategy _discountStrategy;
        public Product(string name, double price, IDiscountStrategy discountStrategy)
        {
            Name = name;
            Price = price;
            _discountStrategy = discountStrategy;
        }
        public double GetDiscountedPrice()
        {
            return _discountStrategy.ApplyDiscount(Price);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Product without any discount
            Product normalProduct = new Product("Shirt", 100, new SeasonalDiscount());
            Console.WriteLine($"Price of {normalProduct.Name} after Seasonal Discount: ${normalProduct.GetDiscountedPrice()}");

            // Product with loyalty discount
            Product loyalProduct = new Product("Pants", 150, new LoyaltyDiscount());
            Console.WriteLine($"Price of {loyalProduct.Name} after Loyalty Discount: ${loyalProduct.GetDiscountedPrice()}");

            // Product with event discount
            Product eventProduct = new Product("Shoes", 200, new EventDiscount());
            Console.WriteLine($"Price of {eventProduct.Name} after Event Discount: ${eventProduct.GetDiscountedPrice()}");

            Console.ReadKey();
        }
    }
}
