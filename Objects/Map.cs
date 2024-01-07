using System;

namespace EscapeFromTheWoods
{
    public class Map
    {
        public int Xmin { get; private set; }
        public int Xmax { get; private set; }
        public int Ymin { get; private set; }
        public int Ymax { get; private set; }

        public Map(int xmin, int xmax, int ymin, int ymax)
        {
            ValidateCoordinates(xmin, xmax, ymin, ymax);
            Xmin = xmin;
            Xmax = xmax;
            Ymin = ymin;
            Ymax = ymax;
        }

        private void ValidateCoordinates(int xmin, int xmax, int ymin, int ymax)
        {
            if (xmin >= xmax || ymin >= ymax)
            {
                throw new ArgumentException("Invalid map coordinates");
            }
        }
    }
}
