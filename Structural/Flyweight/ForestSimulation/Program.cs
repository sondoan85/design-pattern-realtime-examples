using System.IO;

namespace ForestSimulation
{
    public class Result
    {

        /*
         * Complete the 'findLowestPrice' function below.
         *
         * The function is expected to return an INTEGER.
         * The function accepts following parameters:
         *  1. 2D_STRING_ARRAY products
         *  2. 2D_STRING_ARRAY discounts
         */

        public static int findLowestPrice(List<List<string>> products, List<List<string>> discounts)
        {
            int lowestPrice = 0;
            foreach (var product in products)
            {
                int? firstPrice = null;
                int? secondPrice = null;
                int? thirdPrice = null;

                for (int i = product.Count - 1; i > 0; i--)
                {
                    foreach (var discount in discounts)
                    {
                        if (product[i] != "EMPTY" && product[i] == discount[0])
                        {
                            switch (discount[1])
                            {
                                case "0":
                                    firstPrice = Convert.ToInt32(discount[2]);
                                    break;
                                case "1":
                                    secondPrice = Convert.ToInt32(Convert.ToDouble(product[0]) - (Convert.ToDouble(discount[2])/100 * Convert.ToDouble(product[0])));
                                    break;
                                case "2":
                                    thirdPrice = Convert.ToInt32(product[0]) - Convert.ToInt32(discount[2]);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                lowestPrice += Math.Min
                                (firstPrice.HasValue ? firstPrice.Value : Int32.MaxValue, Math.Min
                                (secondPrice.HasValue ? secondPrice.Value : Int32.MaxValue,
                                thirdPrice.HasValue ? thirdPrice.Value : Int32.MaxValue));
            }

            return lowestPrice;
        }

        public static int FindLowestPrice(List<List<string>> products, List<List<string>> discounts)
        {
            int totalCost = 0;
            foreach (var product in products)
            {
                int minPrice = Convert.ToInt32(product[0]);

                for (int i = 1; i < product.Count; i++)
                {
                    foreach (var discount in discounts)
                    {
                        if (product[i] != "EMPTY" && product[i] == discount[0])
                        {
                            switch (discount[1])
                            {
                                case "0":
                                    minPrice = Math.Min(minPrice, Convert.ToInt32(discount[2]));
                                    break;
                                case "1":
                                    minPrice = Math.Min(minPrice, Convert.ToInt32(Convert.ToDouble(product[0]) - (Convert.ToDouble(discount[2]) / 100 * Convert.ToDouble(product[0]))));
                                    break;
                                case "2":
                                    minPrice = Math.Min(minPrice, Convert.ToInt32(product[0]) - Convert.ToInt32(discount[2]));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                totalCost += minPrice;
            }

            return totalCost;
        }

        public static int FindLowestPrice2(List<List<string>> products, List<List<string>> discounts)
        {
            // Create a dictionary to efficiently map product IDs to their discounts
            var discountMap = discounts.GroupBy(d => d[0]) // Group discounts by product ID
                                        .ToDictionary(g => g.Key, g => g.ToList());

            int totalCost = 0;

            foreach (var product in products)
            {
                int minPrice = int.Parse(product[0]); // Parse price directly

                for (int i = 1; i < product.Count; i++)
                {
                    if (discountMap.TryGetValue(product[i], out var productDiscounts))
                    {
                        foreach (var discount in productDiscounts)
                        {
                            switch (discount[1])
                            {
                                case "0":
                                    minPrice = Math.Min(minPrice, int.Parse(discount[2]));
                                    break;
                                case "1":
                                    minPrice = Math.Min(minPrice, (int)(double.Parse(product[0]) * (1 - (double.Parse(discount[2]) / 100))));
                                    break;
                                case "2":
                                    minPrice = Math.Min(minPrice, int.Parse(product[0]) - int.Parse(discount[2]));
                                    break;
                            }
                        }
                    }
                }

                totalCost += minPrice;
            }

            return totalCost;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            int productsRows = Convert.ToInt32(Console.ReadLine().Trim());
            int productsColumns = Convert.ToInt32(Console.ReadLine().Trim());

            List<List<string>> products = new List<List<string>>();

            for (int i = 0; i < productsRows; i++)
            {
                products.Add(Console.ReadLine().TrimEnd().Split(' ').ToList());
            }

            int discountsRows = Convert.ToInt32(Console.ReadLine().Trim());
            int discountsColumns = Convert.ToInt32(Console.ReadLine().Trim());

            List<List<string>> discounts = new List<List<string>>();

            for (int i = 0; i < discountsRows; i++)
            {
                discounts.Add(Console.ReadLine().TrimEnd().Split(' ').ToList());
            }

            //int result1 = Result.findLowestPrice(products, discounts);
            int result2 = Result.FindLowestPrice2(products, discounts);

            //Console.WriteLine(result1);
            Console.WriteLine(result2);
        }
    }
}
