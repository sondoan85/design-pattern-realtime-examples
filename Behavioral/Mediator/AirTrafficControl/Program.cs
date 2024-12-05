
namespace AirTrafficControl
{
    //Mediator Interface
    public interface IControlTower
    {
        void RegisterRunway(Runway runway);
        bool RequestLandingPermission(Airplane airplane);
        void ReleaseRunway(Runway runway);
    }

    //Concrete Mediator (Control Tower)
    public class ControlTower : IControlTower
    {
        private List<Runway> _availableRunways = new List<Runway>();

        public void RegisterRunway(Runway runway)
        {
            _availableRunways.Add(runway);
        }

        public bool RequestLandingPermission(Airplane airplane)
        {
            if (_availableRunways.Any())
            {
                var assignedRunway = _availableRunways.First();
                _availableRunways.Remove(assignedRunway);
                airplane.AssignRunway(assignedRunway);
                return true;
            }

            return false;
        }

        public void ReleaseRunway(Runway runway)
        {
            _availableRunways.Add(runway);
        }
    }

    public class Airplane
    {
        private readonly IControlTower _controlTower;
        public string FlightNumber { get; }
        public Runway AssignedRunway { get; private set; }

        public Airplane(string flightNumber, IControlTower controlTower)
        {
            FlightNumber = flightNumber;
            _controlTower = controlTower;
        }

        public void RequestLanding()
        {
            if (_controlTower.RequestLandingPermission(this))
            {
                Console.WriteLine($"Airplane {FlightNumber} is landing.");
            }
            else
            {
                Console.WriteLine($"Airplane {FlightNumber} is waiting for an available runway.");
            }
        }

        public void AssignRunway(Runway runway)
        {
            AssignedRunway = runway;
            Console.WriteLine($"Airplane {FlightNumber} assigned to runway {AssignedRunway.Id}.");
        }

        public void NotifyLanded()
        {
            Console.WriteLine($"Airplane {FlightNumber} has already landed. {AssignedRunway.Id} is released.");
            _controlTower.ReleaseRunway(AssignedRunway);
        }
    }

    public class Runway
    {
        public string Id { get; }
        public Runway(string id)
        {
            Id = id; 
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            IControlTower controlTower = new ControlTower();

            // Register two runways
            controlTower.RegisterRunway(new Runway("R1"));
            controlTower.RegisterRunway(new Runway("R2"));

            var airplane1 = new Airplane("FL111", controlTower);
            var airplane2 = new Airplane("FL222", controlTower);

            airplane1.RequestLanding();
            airplane2.RequestLanding();

            airplane1.NotifyLanded();

            // Only one plane should land as we only have one runway. 
            // The other should wait for an available runway.
            var airplane3 = new Airplane("FL333", controlTower);
            var airplane4 = new Airplane("FL444", controlTower);

            airplane3.RequestLanding();
            airplane4.RequestLanding();

            airplane2.NotifyLanded();
            airplane4.RequestLanding();

            var airplane5 = new Airplane("FL555", controlTower);
            var airplane6 = new Airplane("FL666", controlTower);

            airplane5.RequestLanding();
            airplane6.RequestLanding();

            Console.ReadKey();
        }
    }
}
