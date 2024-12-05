namespace DataSynchronization
{
    //Abstract Class(Template)
    public abstract class DataSynchronizer
    {
        public void Synchronize()
        {
            OpenSource();
            OpenDestination();
            TransferData();
            CloseSource();
            CloseDestination();
        }
        protected abstract void OpenSource();
        protected abstract void OpenDestination();
        protected abstract void TransferData();
        protected abstract void CloseSource();
        protected abstract void CloseDestination();
    }

    //Concrete Implementations:
    //For Database to File synchronization
    public class DatabaseToFileSynchronizer : DataSynchronizer
    {
        protected override void OpenSource()
        {
            Console.WriteLine("Opening database connection...");
        }
        protected override void OpenDestination()
        {
            Console.WriteLine("Opening file for writing...");
        }
        protected override void TransferData()
        {
            Console.WriteLine("Reading data from database and writing to file...");
        }
        protected override void CloseSource()
        {
            Console.WriteLine("Closing database connection...");
        }
        protected override void CloseDestination()
        {
            Console.WriteLine("Closing file...");
        }
    }

    //For API to Database synchronization
    public class ApiToDatabaseSynchronizer : DataSynchronizer
    {
        protected override void OpenSource()
        {
            Console.WriteLine("Connecting to API...");
        }
        protected override void OpenDestination()
        {
            Console.WriteLine("Opening database connection for writing...");
        }
        protected override void TransferData()
        {
            Console.WriteLine("Fetching data from API and inserting into database...");
        }
        protected override void CloseSource()
        {
            Console.WriteLine("Closing API connection...");
        }
        protected override void CloseDestination()
        {
            Console.WriteLine("Closing database connection...");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            DataSynchronizer dbToFileSync = new DatabaseToFileSynchronizer();
            dbToFileSync.Synchronize();

            Console.WriteLine();

            DataSynchronizer apiToDbSync = new ApiToDatabaseSynchronizer();
            apiToDbSync.Synchronize();

            Console.ReadKey();
        }
    }
}
