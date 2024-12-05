namespace StockExchange
{
    // Mediator
    public interface IStockExchange
    {
        void PlaceOrder(Trader trader, string stockSymbol, int quantity, OrderType orderType);
    }

    public enum OrderType 
    { 
        Buy, 
        Sell 
    }

    public class StockExchange : IStockExchange
    {
        // Simplified order-matching logic for illustration purposes
        private Dictionary<string, List<Order>> _buyOrders = new Dictionary<string, List<Order>>();
        private Dictionary<string, List<Order>> _sellOrders = new Dictionary<string, List<Order>>();

        public void PlaceOrder(Trader trader, string stockSymbol, int quantity, OrderType orderType)
        {
            var order = new Order(trader, stockSymbol, quantity, orderType);

            if (orderType == OrderType.Buy && _sellOrders.ContainsKey(stockSymbol) && _sellOrders[stockSymbol].Any())
            {
                var matchingOrder = _sellOrders[stockSymbol].First();
                ExecuteTrade(order, matchingOrder);
                _sellOrders[stockSymbol].Remove(matchingOrder);
            }
            else if (orderType == OrderType.Sell && _buyOrders.ContainsKey(stockSymbol) && _buyOrders[stockSymbol].Any())
            {
                var matchingOrder = _buyOrders[stockSymbol].First();
                ExecuteTrade(order, matchingOrder);
                _buyOrders[stockSymbol].Remove(matchingOrder);
            }
            else
            {
                if (orderType == OrderType.Buy)
                {
                    if (!_buyOrders.ContainsKey(stockSymbol)) _buyOrders[stockSymbol] = new List<Order>();
                    _buyOrders[stockSymbol].Add(order);
                }
                else
                {
                    if (!_sellOrders.ContainsKey(stockSymbol)) _sellOrders[stockSymbol] = new List<Order>();
                    _sellOrders[stockSymbol].Add(order);
                }
            }
        }

        private void ExecuteTrade(Order buyOrder, Order sellOrder)
        {
            Console.WriteLine($"Trade executed: {buyOrder.StockSymbol} - {buyOrder.Quantity} shares @ market price");
        }
    }

    public class Order
    {
        public Trader Trader { get; }
        public string StockSymbol { get; }
        public int Quantity { get; }
        public OrderType OrderType { get; }

        public Order(Trader trader, string stockSymbol, int quantity, OrderType orderType)
        {
            Trader = trader;
            StockSymbol = stockSymbol;
            Quantity = quantity;
            OrderType = orderType;
        }
    }

    public class Trader
    {
        private IStockExchange _mediator;

        public string Name { get; }

        public Trader(string name, IStockExchange mediator)
        {
            Name = name;
            _mediator = mediator;
        }

        public void PlaceOrder(string stockSymbol, int quantity, OrderType orderType)
        {
            _mediator.PlaceOrder(this, stockSymbol, quantity, orderType);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var stockExchange = new StockExchange();

            var traderBob = new Trader("Bob", stockExchange);
            var traderAlice = new Trader("Alice", stockExchange);

            traderBob.PlaceOrder("AAPL", 100, OrderType.Buy);
            traderAlice.PlaceOrder("AAPL", 100, OrderType.Sell);

            Console.ReadKey();
        }
    }
}
