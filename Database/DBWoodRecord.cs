using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeFromTheWoods
{
    public class DBWoodRecord
    {
        public int WoodID { get; }
        public int TreeID { get; }
        public int X { get; }
        public int Y { get; }

        public DBWoodRecord(int woodID, int treeID, int x, int y)
        {
            WoodID = woodID;
            TreeID = treeID;
            X = x;
            Y = y;
        }
    }
}
