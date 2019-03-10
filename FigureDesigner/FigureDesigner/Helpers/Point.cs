using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigureDesigner
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public static double GetDistance(Point x, Point y)
        {
            var distance = Math.Sqrt((x.X - y.X) * (x.X - y.X) + (x.Y - y.Y) * (x.Y - y.Y));
            return distance;
        }
    }
}
