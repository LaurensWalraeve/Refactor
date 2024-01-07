using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EscapeFromTheWoods
{
    public class DBMonkeyRecord
    {
        public int MonkeyID { get; }
        public string MonkeyName { get; }
        public int WoodID { get; }
        public int SeqNr { get; }
        public int TreeID { get; }
        public int X { get; }
        public int Y { get; }

        public DBMonkeyRecord(int monkeyID, string monkeyName, int woodID, int seqNr, int treeID, int x, int y)
        {
            MonkeyID = monkeyID;
            MonkeyName = monkeyName ?? throw new ArgumentNullException(nameof(monkeyName));
            WoodID = woodID;
            SeqNr = seqNr;
            TreeID = treeID;
            X = x;
            Y = y;
        }
    }
}
