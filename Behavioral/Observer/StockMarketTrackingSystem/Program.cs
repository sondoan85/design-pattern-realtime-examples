namespace StockMarketTrackingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Stock HSBLStock = new Stock("HSBL", 150.00);
            Trader Pranaya = new Trader("Pranaya");
            Trader Kumar = new Trader("Kumar");
            Trader Rout = new Trader("Rout");

            //Register Traders
            HSBLStock.RegisterTrader(Pranaya);
            HSBLStock.RegisterTrader(Kumar);
            HSBLStock.RegisterTrader(Rout);

            //UnRegister
            HSBLStock.UnregisterTrader(Kumar);

            // Simulate a price change.
            HSBLStock.Price = 152.50;
            HSBLStock.Price = 149.50;
            HSBLStock.Price = 155.50;

            Console.ReadKey();
        }

        public interface ITrader
        {
            void Update(Stock stock, double oldPrice);
        }

        // Subject Interface
        public interface IStockTicker
        {
            void RegisterTrader(ITrader trader);
            void UnregisterTrader(ITrader trader);
            void Notify(double oldPrice);
        }

        public class Trader : ITrader
        {
            public string Name { get; set; }

            public Trader(string name)
            {
                Name = name;
            }

            public void Update(Stock stock, double oldPrice)
            {
                Console.WriteLine($"Notifying {Name} of {stock.Symbol}'s price change from ${oldPrice} to ${stock.Price}");
            }
        }

        public class Stock : IStockTicker
        {
            private List<ITrader> _traders = new List<ITrader>();
            
            private double _price;

            public string Symbol { get; private set; }

            public double Price
            {
                get { return _price; }
                set
                {
                    if (_price != value)
                    {
                        var oldPrice = _price;
                        _price = value;
                        Notify(oldPrice);
                    }
                }
            }

            public Stock(string symbol, double price)
            {
                Symbol = symbol;
                Price = price;
            }

            public void RegisterTrader(ITrader trader)
            {
                _traders.Add(trader);
            }

            public void UnregisterTrader(ITrader trader)
            {
                _traders.Remove(trader);
            }

            public void Notify(double oldPrice)
            {
                foreach (var trader in _traders)
                {
                    trader.Update(this, oldPrice);
                }
            }
        }
    }
}
