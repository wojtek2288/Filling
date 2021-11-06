using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_Lab2.Triangles
{
    public class Triangle
    {
        public Triangle() { }
        public Triangle(Point x1, Point x2, Point x3)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.x3 = x3;
        }
        public Point x1 { get; set; }
        public Point x2 { get; set; }
        public Point x3 { get; set; }
    }
}
