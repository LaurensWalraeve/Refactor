using System;

namespace EscapeFromTheWoods
{
    public class Tree
    {
        public int TreeID { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool HasMonkey { get; set; }

        public Tree(int treeID, int x, int y)
        {
            TreeID = treeID;
            X = x;
            Y = y;
            HasMonkey = false;
        }

        public override bool Equals(object obj)
        {
            if (obj is Tree otherTree)
            {
                return TreeID == otherTree.TreeID;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TreeID);
        }

        public override string ToString()
        {
            return $"TreeID: {TreeID}, Location: ({X}, {Y})";
        }
    }
}
