using System;

namespace EscapeFromTheWoods.Database
{
    public class DBLogRecord
    {
       /* public int Id { get; set; } // For MongoDB, you would typically use ObjectId
        public int WoodID { get; set; }
        public int MonkeyID { get; set; }
        public string Message { get; private set; } // Make setter private if you're always setting via constructor

        // Assuming you have access to the Monkey and Tree objects when creating a log record
        public DBLogRecord(int woodID, int monkeyID, Monkey monkey, Tree tree)
        {
            WoodID = woodID;
            MonkeyID = monkeyID;
            Message = $"{monkey.Name} is now in tree {tree.TreeID} at location ({tree.Y}, {tree.X})";
        }

        // If you need to set the message later, you can use a method like this
        public void SetMessage(Monkey monkey, Tree tree)
        {
            Message = $"{monkey.Name} is now in tree {tree.TreeID} at location ({tree.X}, {tree.Y})";
        }*/



        public int MonkeyID { get; }
        public string MonkeyName { get; }
        public int WoodID { get; }
        public int SeqNr { get; }
        public int TreeID { get; }
        public int X { get; }
        public int Y { get; }
        public string Message { get; private set; }

        public DBLogRecord(int monkeyID, string monkeyName, int woodID, int seqNr, int treeID, int x, int y)
        {
            MonkeyID = monkeyID;
            MonkeyName = monkeyName ?? throw new ArgumentNullException(nameof(monkeyName));
            WoodID = woodID;
            SeqNr = seqNr;
            TreeID = treeID;
            X = x;
            Y = y;
            Message = $"{MonkeyName} is now in tree {TreeID} at location ({X}, {Y})";
        }

        public void SetMessage(Monkey monkey, Tree tree)
        {
            Message = $"{monkey.Name} is now in tree {tree.TreeID} at location ({tree.X}, {tree.Y})";
        }
    }
}
