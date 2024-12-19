namespace SendMessage
{
    public class Result
    {
        public static void minimumBribes(List<int> q)
        {
            var count = q.Count();
            var bribes = 0;
            var tooCha = false;

            for (var i = count - 1; i > 0; i--)
            {
                var expected = i + 1;

                if (q[i] == expected)
                {
                    continue;
                }
                else if (i - 1 >= 0 && q[i - 1] == expected)
                {
                    bribes++;
                    // reorder array
                    q[i - 1] = q[i];
                    q[i] = expected;
                    continue;
                }
                else if (i - 2 >= 0 && q[i - 2] == expected)
                {
                    bribes += 2;
                    // reorder array               
                    q[i - 2] = q[i - 1];
                    q[i - 1] = q[i];
                    q[i] = expected;
                    continue;
                }

                // bribed more than twice - break loop      
                tooCha = true;
                break;
            }

            if (tooCha)
            {
                Console.WriteLine("Too chaotic");
            }
            else
            {
                Console.WriteLine(bribes.ToString());
            }
        }

    }

    public class Program
    {
        public static void Main(string[] args)
        {
            int t = Convert.ToInt32(Console.ReadLine().Trim());

            for (int tItr = 0; tItr < t; tItr++)
            {
                int n = Convert.ToInt32(Console.ReadLine().Trim());

                List<int> q = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(qTemp => Convert.ToInt32(qTemp)).ToList();

                Result.minimumBribes(q);
            }
        }
    }
}
