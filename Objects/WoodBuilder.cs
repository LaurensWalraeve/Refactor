using System;
using System.Collections.Generic;

namespace EscapeFromTheWoods
{
    public static class WoodBuilder
    {
        private static readonly Random RandomGenerator = new Random(100);

        public static Wood GetWood(int size, Map map, string path, DBwriter db)
        {
            List<Tree> trees = GenerateRandomTrees(size, map);
            int woodID = IDgenerator.GetWoodID();

            List<DBWoodRecord> woodRecords = new List<DBWoodRecord>();


            foreach (Tree tree in trees)
            {
                DBWoodRecord woodRecord = new DBWoodRecord(woodID, tree.TreeID, tree.X, tree.Y);
                woodRecords.Add(woodRecord);
            }

            db.WriteWoodRecordsAsync(woodRecords);

            return new Wood(woodID, trees, map, path, db);
        }

        private static List<Tree> GenerateRandomTrees(int size, Map map)
        {
            var trees = new HashSet<Tree>();
            while (trees.Count < size)
            {
                int x = RandomGenerator.Next(map.Xmin, map.Xmax);
                int y = RandomGenerator.Next(map.Ymin, map.Ymax);
                var tree = new Tree(IDgenerator.GetTreeID(), x, y);
                trees.Add(tree);
            }
            return new List<Tree>(trees);
        }
    }
}
