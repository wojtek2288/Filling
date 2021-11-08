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
        public static double CheckDistance(Point x1, Point x2)
        {
            return Math.Sqrt((Math.Pow(x1.X - x2.X, 2) + Math.Pow(x1.Y - x2.Y, 2)));
        }
    }
}
