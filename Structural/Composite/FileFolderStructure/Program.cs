namespace FileFolderStructure
{
    public class Result
    {

        /*
         * Complete the 'truckTour' function below.
         *
         * The function is expected to return an INTEGER.
         * The function accepts 2D_INTEGER_ARRAY petrolpumps as parameter.
         */

        public static int TruckTour(List<List<int>> petrolpumps)
        {
            int dist = 0, fuel = 0;
            for (int i = 0; i < petrolpumps.Count; i++)
            {
                fuel += petrolpumps[i].ToArray()[0] - petrolpumps[i].ToArray()[1];
                if (fuel < 0)
                {
                    dist = i + 1;
                    fuel = 0;
                }

            }
            return dist;
        }

    }

    public class Program
    {
        public static void Main(string[] args)
        {
            //TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            int n = Convert.ToInt32(Console.ReadLine().Trim());

            List<List<int>> petrolpumps = new List<List<int>>();

            for (int i = 0; i < n; i++)
            {
                petrolpumps.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(petrolpumpsTemp => Convert.ToInt32(petrolpumpsTemp)).ToList());
            }

            int result = Result.TruckTour(petrolpumps);

            Console.WriteLine(result);

            //textWriter.Flush();
            //textWriter.Close();
        }
    }
}
