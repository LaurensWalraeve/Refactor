using System;

namespace EscapeFromTheWoods.Database
{
    public class DBLogRecord
    {
        public int Id { get; set; } // For MongoDB, you would typically use ObjectId
        public int WoodID { get; set; }
        public int MonkeyID { get; set; }
        public string Message { get; private set; } // Make setter private if you're always setting via constructor

        // Assuming you have access to the Monkey and Tree objects when creating a log record
        public DBLogRecord(int id, int woodID, int monkeyID, Monkey monkey, Tree tree)
        {
            Id = id;
            WoodID = woodID;
            MonkeyID = monkeyID;
            Message = $"{monkey.Name} is now in tree {tree.TreeID} at location ({tree.Y}, {tree.X})";
        }

        // If you need to set the message later, you can use a method like this
        public void SetMessage(Monkey monkey, Tree tree)
        {
            Message = $"{monkey.Name} is now in tree {tree.TreeID} at location ({tree.X}, {tree.Y})";
        }
    }
}
