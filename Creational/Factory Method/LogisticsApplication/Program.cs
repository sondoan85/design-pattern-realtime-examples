namespace LogisticsApplication
{
    public interface ITransport
    {
        double GetDeliveryCost(int distance);    
    }

    public class Truck : ITransport
    {
        public double GetDeliveryCost(int distance)
        {
            // Cost per mile for the truck might be $1.00
            return 1.00 * distance;
        }
    }

    public class Ship : ITransport
    {
        public double GetDeliveryCost(int distance)
        {
            // Cost per mile for the ship might be $0.50
            return 0.50 * distance;
        }
    }

    public abstract class TransportFactory
    {
        public abstract ITransport CreateTransport();
    }

    public class TruckFactory : TransportFactory
    {
        public override ITransport CreateTransport()
        {
            return new Truck();
        }
    }

    public class ShipFactory : TransportFactory
    {
        public override ITransport CreateTransport()
        {
            return new Ship();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            TransportFactory factory;
            ITransport transport;

            factory = new TruckFactory();
            transport = factory.CreateTransport();
            Console.WriteLine($"Truck delivery cost: ${transport.GetDeliveryCost(100)} for 100 miles.");

            factory = new ShipFactory();
            transport = factory.CreateTransport();
            Console.WriteLine($"Ship delivery cost: ${transport.GetDeliveryCost(100)} for 100 miles.");

            Console.ReadKey();
        }
    }
}
