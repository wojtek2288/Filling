using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GK_Lab2.FillingRectangle
{
    public class ScanLine
    {
        private readonly List<AETPointer> AET;
        private readonly List<Vector3> Polygon;

        public ScanLine(List<Vector3> Polygon)
        {
            AET = new List<AETPointer>();
            Polygon.Sort((x, y) => x.Y.CompareTo(y.Y));
            this.Polygon = Polygon;
        }
    }
    class AETPointer : IComparable<AETPointer>
    {
        public int yMax;
        public double x;
        public double _m;

        public AETPointer(double yMax, double x, double m)
        {
            this.yMax = (int)yMax;
            this.x = x;
            _m = 1.0 / m;
        }

        public void UpdateX()
        {
            x += _m;
        }

        public int CompareTo(AETPointer other)
        {
            var xCmp = x.CompareTo(other.x);
            return xCmp == 0 ? yMax.CompareTo(other.yMax) : xCmp;
        }
    }
}
