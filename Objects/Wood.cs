using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace EscapeFromTheWoods
{
    public class Wood
    {
        private const int DrawingFactor = 8;
        private readonly string _path;
        private readonly DBwriter _db;
        private readonly Random _random = new Random();
        public int WoodID { get; }
        public List<Tree> Trees { get; }
        public List<Monkey> Monkeys { get; }

        private readonly Map _map;

        public Wood(int woodID, List<Tree> trees, Map map, string path, DBwriter db)
        {
            WoodID = woodID;
            Trees = trees ?? throw new ArgumentNullException(nameof(trees));
            Monkeys = new List<Monkey>();
            _map = map ?? throw new ArgumentNullException(nameof(map));
            _path = path ?? throw new ArgumentNullException(nameof(path));
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void PlaceMonkey(string monkeyName, int monkeyID)
        {
            var availableTrees = Trees.Where(t => !t.HasMonkey).ToList();
            if (!availableTrees.Any())
                throw new InvalidOperationException("No available trees to place the monkey.");

            var tree = availableTrees[_random.Next(availableTrees.Count)];
            var monkey = new Monkey(monkeyID, monkeyName, tree);
            Monkeys.Add(monkey);
            tree.HasMonkey = true;
        }

        public async Task EscapeAsync()
        {
            var routes = Monkeys.Select(EscapeMonkey).ToList();
            //await WriteEscaperoutesToBitmapAsync(routes);
            await WriteRoutesToDbAsync(routes);
        }

        private async Task WriteRoutesToDbAsync(IEnumerable<List<Tree>> routes)
        {
            foreach (var route in routes)
            {
                var monkey = Monkeys.First(m => m.CurrentTree == route.First());
                var records = route.Select((t, index) => new DBMonkeyRecord(monkey.MonkeyID, monkey.Name, WoodID, index, t.TreeID, t.X, t.Y)).ToList();
                await _db.WriteMonkeyRecordsAsync(records);
            }
        }

        //private async Task WriteEscaperoutesToBitmapAsync(IEnumerable<List<Tree>> routes)
        //{
        //    using var bitmap = new Bitmap((_map.Xmax - _map.Xmin) * DrawingFactor, (_map.Ymax - _map.Ymin) * DrawingFactor);
        //    using var graphics = Graphics.FromImage(bitmap);
        //    DrawTrees(graphics);
        //    DrawRoutes(routes, graphics);
        //    bitmap.Save(Path.Combine(_path, $"{WoodID}_escapeRoutes.jpg"), ImageFormat.Jpeg);
        //}

        private void DrawTrees(Graphics graphics)
        {
            using var pen = new Pen(Color.Green, 1);
            foreach (var tree in Trees)
            {
                graphics.DrawEllipse(pen, tree.X * DrawingFactor, tree.Y * DrawingFactor, DrawingFactor, DrawingFactor);
            }
        }

        private void DrawRoutes(IEnumerable<List<Tree>> routes, Graphics graphics)
        {
            var colors = new[] { Color.Red, Color.Yellow, Color.Blue, Color.Cyan, Color.GreenYellow };
            int colorIndex = 0;

            foreach (var route in routes)
            {
                DrawRoute(route, graphics, colors[colorIndex % colors.Length]);
                colorIndex++;
            }
        }

        private void DrawRoute(List<Tree> route, Graphics graphics, Color color)
        {
            using var pen = new Pen(color, 1);
            var (prevX, prevY) = (route.First().X * DrawingFactor + DrawingFactor / 2, route.First().Y * DrawingFactor + DrawingFactor / 2);
            foreach (var tree in route.Skip(1))
            {
                var (nextX, nextY) = (tree.X * DrawingFactor + DrawingFactor / 2, tree.Y * DrawingFactor + DrawingFactor / 2);
                graphics.DrawLine(pen, prevX, prevY, nextX, nextY);
                prevX = nextX;
                prevY = nextY;
            }
        }

        private List<Tree> EscapeMonkey(Monkey monkey)
        {
            var route = new List<Tree>();
            Tree currentTree = monkey.CurrentTree;
            route.Add(currentTree);

            while (true) // Loop until the monkey escapes
            {
                // Mark the current tree as visited
                currentTree.HasMonkey = true;

                // Find the nearest tree that hasn't been visited
                Tree nearestTree = Trees
                    .Where(t => !t.HasMonkey)
                    .OrderBy(t => Distance(currentTree, t))
                    .FirstOrDefault();

                // Check if monkey can escape (if nearestTree is null or if nearest tree is farther than the edge)
                if (nearestTree == null || IsCloserToEdge(currentTree))
                {
                    break; // Monkey escapes the woods
                }

                // Move the monkey to the nearest tree
                route.Add(nearestTree);
                currentTree = nearestTree;
                currentTree.HasMonkey = true; // Mark new tree as visited
            }

            return route; // Return the route the monkey took to escape
        }

        // Helper method to calculate the distance between two trees
        private double Distance(Tree a, Tree b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        // Helper method to determine if the current tree is closer to the edge of the woods than to any other tree
        private bool IsCloserToEdge(Tree tree)
        {
            double distanceToNearestEdge = Math.Min(
                Math.Min(tree.X - _map.Xmin, _map.Xmax - tree.X),
                Math.Min(tree.Y - _map.Ymin, _map.Ymax - tree.Y)
            );

            return Trees
                .Where(t => !t.HasMonkey)
                .All(t => Distance(tree, t) > distanceToNearestEdge);
        }

    }
}
