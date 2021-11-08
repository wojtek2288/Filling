using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GK_Lab2.SphereTriangulation
{
    public static class Triangulation
    {
        public static List<List<Vector3>> TriangulateSphere(int radius, int centerX, int centerY, int quantity)
        {
            Vector3[] points = new Vector3[quantity * quantity * 4 + 1];
            (int, int, int)[] tri = new (int, int, int)[(quantity * quantity * 2 - quantity) * 4];
            List<List<Vector3>> TriangluatedSphere = new List<List<Vector3>>();

            int vind = 0, tind = 0;
            for (var level = 0; level < quantity; level++)
            {
                for (var around = 0; around < quantity * 4; around++)
                {
                    var lon = (around + level * 0.5f) * 2 * Math.PI / (quantity * 4);
                    var lat = level * 2*Math.PI / (quantity * 4);

                    var px = Math.Cos(lat) * Math.Cos(lon) * radius + centerX;
                    var py = Math.Cos(lat) * Math.Sin(lon) * radius + centerY;
                    var pz = Math.Sin(lat) * radius;

                    var newPoint = new Point((int)px, (int)py);
                    points[vind++] = new Vector3(newPoint.X, newPoint.Y, (float)pz);

                    if (level < quantity - 1)
                    {
                        tri[tind++] = (around + level * quantity * 4, (around + 1) % (quantity * 4) + level * quantity * 4,
                            around + (level + 1) * quantity * 4);
                        tri[tind++] = ((around + 1) % (quantity * 4) + level * quantity * 4,
                            (around + 1) % (quantity * 4) + (level + 1) * quantity * 4,
                            around + (level + 1) * quantity * 4);
                    }
                }
            }

            var zeroPoint = new Point(centerX, centerY);
            points[quantity * quantity * 4] = new Vector3(zeroPoint.X, zeroPoint.Y, 0);

            for (int around = 0; around < quantity * 4; around++)
            {
                tri[tind++] = (around + (quantity - 1) * quantity * 4, (around + 1) % (quantity * 4) 
                    + (quantity - 1) * quantity * 4, quantity * quantity * 4);
            }

            for (var i = 0; i < tri.Length; i++)
            {
                TriangluatedSphere.Add(new List<Vector3>
                {
                    points[tri[i].Item1],
                    points[tri[i].Item2],
                    points[tri[i].Item3]
                });
            }

            return TriangluatedSphere;
        }
    }
}
