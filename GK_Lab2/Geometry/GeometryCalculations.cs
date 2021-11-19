using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Lab2.Geometry
{
    public static class GeometryCalculations
    {
        public const double Eps = 1e-8;
        public const double Infinity = 1 / Eps;
        public static double CheckDistance(Point x1, Point x2)
        {
            return Math.Sqrt((Math.Pow(x1.X - x2.X, 2) + Math.Pow(x1.Y - x2.Y, 2)));
        }

        public static double Slope(Point p1, Point p2)
        {
            return Math.Abs(p1.X - p2.X) < Eps ? Infinity : (p2.Y - p1.Y) / (p2.X - p1.X);
        }
    }
}
