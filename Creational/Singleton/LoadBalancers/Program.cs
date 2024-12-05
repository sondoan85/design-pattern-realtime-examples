namespace LoadBalancers
{
    public class LoadBalancer
    {
        // Static instance for Singleton
        private static readonly Lazy<LoadBalancer> _instance = new Lazy<LoadBalancer>(() => new LoadBalancer());
        private List<string> servers = new List<string>();
        private Random random = new Random();
        // Private constructor to prevent external instantiation and to initialize server list
        private LoadBalancer()
        {
            // Add some server addresses to the list for this example
            servers.Add("Server1");
            servers.Add("Server2");
            servers.Add("Server3");
            servers.Add("Server4");
            servers.Add("Server5");
        }
        // Public method to get next server
        public string GetNextServer()
        {
            int index = random.Next(servers.Count);
            return servers[index];
        }
        // Public property to access the Singleton instance
        public static LoadBalancer Instance => _instance.Value;
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Usage:
            // Get the next server to handle a client request
            for (int i = 1; i < 6; i++)
            {
                string server = LoadBalancer.Instance.GetNextServer();
                Console.WriteLine($"Redirecting client to: {server}");
            }

            Console.ReadKey();
        }
    }
}
