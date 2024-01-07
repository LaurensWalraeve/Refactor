using System;

namespace EscapeFromTheWoods
{
    public class Monkey
    {
        public int MonkeyID { get; private set; }
        public string Name { get; private set; }
        public Tree CurrentTree { get; private set; }

        public Monkey(int monkeyID, string name, Tree tree)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Monkey name cannot be null or whitespace.");
            }

            MonkeyID = monkeyID;
            Name = name;
            MoveToTree(tree);
        }

        public void MoveToTree(Tree newTree)
        {
            CurrentTree = newTree ?? throw new ArgumentNullException(nameof(newTree), "New tree cannot be null.");
        }
    }
}
