using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GK_Lab2.FastBitmap;

namespace GK_Lab2.FillingRectangle
{
    public static class Filling
    {
        public static void FillPolygon(List<Vector3> Polygon, DirectBitmap dirBitmap)
        {
            Polygon.Sort((x, y) => x.Y.CompareTo(y.Y));

            for(int y = Convert.ToInt32(Polygon[0].Y); y <= Convert.ToInt32(Polygon[Polygon.Count - 1].Y); y++)
            {

            }
        }
    }
}
