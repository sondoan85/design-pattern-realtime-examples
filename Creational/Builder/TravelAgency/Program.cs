namespace TravelAgency
{
    public class HolidayPackage
    {
        public string Flight { get; set; }
        public string Hotel { get; set; }
        public string CarRental { get; set; }
        public List<string> Excursions { get; private set; } = new List<string>();

        public void DisplayPackageDetails()
        {
            Console.WriteLine($"Flight: {Flight ?? "Not selected"}");
            Console.WriteLine($"Hotel: {Hotel ?? "Not selected"}");
            Console.WriteLine($"Car Rental: {CarRental ?? "Not selected"}");
            Console.WriteLine("Excursions: " + (Excursions.Any() ? string.Join(", ", Excursions) : "No excursions selected"));
        }
    }

    //Builder (Abstract Builder)
    public abstract class HolidayPackageBuilder
    {
        protected HolidayPackage Package { get; private set; } = new HolidayPackage();

        public abstract void BookFlight(string flightDetails);
        public abstract void BookHotel(string hotelName);
        public abstract void RentCar(string carDetails);
        public abstract void AddExcursion(string excursion);

        public HolidayPackage GetPackage() => Package;
    }

    //Concrete Builder: CustomHolidayPackageBuilder
    public class CustomHolidayPackageBuilder : HolidayPackageBuilder
    {
        public override void BookFlight(string flightDetails)
        {
            Package.Flight = flightDetails;
        }

        public override void BookHotel(string hotelName)
        {
            Package.Hotel = hotelName;
        }

        public override void RentCar(string carDetails)
        {
            Package.CarRental = carDetails;
        }

        public override void AddExcursion(string excursion)
        {
            Package.Excursions.Add(excursion);
        }
    }

    //Concrete Builder: SummerHolidayPackageBuilder
    public class SummerHolidayPackageBuilder : HolidayPackageBuilder
    {
        public override void BookFlight(string flightDetails)
        {
            Package.Flight = flightDetails;
        }

        public override void BookHotel(string hotelName)
        {
            Package.Hotel = hotelName;
        }

        public override void RentCar(string carDetails)
        {
            Package.CarRental = carDetails;
        }

        public override void AddExcursion(string excursion)
        {
            Package.Excursions.Add(excursion);
        }
    }

    //Director: TravelAgent
    public class TravelAgent
    {
        public void CreatePackage
            (HolidayPackageBuilder builder, bool wantsFlight, bool wantsHotel, bool wantsCar, IEnumerable<string> excursions)
        {
            if (wantsFlight) builder.BookFlight("Flight details...");
            if (wantsHotel) builder.BookHotel("Fancy Hotel");
            if (wantsCar) builder.RentCar("SUV Model XYZ");
            foreach (var excursion in excursions)
            {
                builder.AddExcursion(excursion);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var travelAgent = new TravelAgent();
            HolidayPackageBuilder packageBuilder = new CustomHolidayPackageBuilder();
            travelAgent.CreatePackage(packageBuilder, true, true, false, new[] { "Beach trip", "Mountain hiking" });
            var holidayPackage = packageBuilder.GetPackage();
            holidayPackage.DisplayPackageDetails();

            packageBuilder = new SummerHolidayPackageBuilder();
            travelAgent.CreatePackage(packageBuilder, true, true, true, new[] { "Cherry Blossom festival", "River trip" });
            holidayPackage = packageBuilder.GetPackage();
            holidayPackage.DisplayPackageDetails();

            Console.ReadKey();
        }
    }
}
