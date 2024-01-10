using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscapeFromTheWoods
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string path = @"C:\Users\LaurensW\Desktop\Specialisatie_Laurens_Walraeve\Refactor\Data";
            string connectionString = "mongodb://localhost:27017";
            DBwriter db = new DBwriter(connectionString, "monkeys");

            var woods = new List<Wood>
            {
                CreateAndPopulateWood(0, 500, 0, 500, 500, new[] { "Alice", "Janice", "Toby", "Mindy", "Jos" }, path, db),
                CreateAndPopulateWood(0, 200, 0, 400, 2500, new[] { "Tom", "Jerry", "Tiffany", "Mozes", "Jebus" }, path, db),
                CreateAndPopulateWood(0, 400, 0, 400, 2000, new[] { "Kelly", "Kenji", "Kobe", "Kendra" }, path, db)
            };

            foreach (var wood in woods)
            {
                await wood.EscapeAsync();
            }

            stopwatch.Stop();
            Console.WriteLine($"Time elapsed: {stopwatch.Elapsed}");
            Console.WriteLine("end");
        }

        private static Wood CreateAndPopulateWood(int minX, int maxX, int minY, int maxY, int treeCount, string[] monkeyNames, string path, DBwriter db)
        {
            Map map = new Map(minX, maxX, minY, maxY);
            Wood wood = WoodBuilder.GetWood(treeCount, map, path, db);
            foreach (var name in monkeyNames)
            {
                wood.PlaceMonkey(name, IDgenerator.GetMonkeyID());
            }
            return wood;
        }
    }
}
