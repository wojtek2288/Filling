using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GK_Lab2.FastBitmap;
using GK_Lab2.Options;

namespace GK_Lab2.FillingPolygon
{
    public class AETItem
    {
        public Point start;
        public Point end;
        public double x;
        public double reciprocalM;

        public AETItem(Point s, Point e, double x, double rM)
        {
            start = s;
            end = e;
            this.x = x;
            reciprocalM = rM;
        }
    }
    public static class Fill
    {
        public static float CalculateZ(int x, int y, Vector3 P1, Vector3 P2, Vector3 P3)
        {
            float z = (P3.Z*(x - P1.X)*(y - P2.Y) + P1.Z*(x - P2.X)*(y - P3.Y) + P2.Z*(x - P3.X)*(y - P1.Y) - P2.Z*(x - P1.X)*(y - P3.Y) - P3.Z*(x - P2.X)*(y - P1.Y) - P1.Z*(x - P3.X)*(y - P2.Y))
                    / ((x - P1.X)*(y - P2.Y) + (x - P2.X)*(y - P3.Y) + (x - P3.X)*(y - P1.Y) - (x - P1.X)*(y - P3.Y) - (x - P2.X)*(y - P1.Y) - (x - P3.X)*(y - P2.Y));

            return z;
        }

        public static Vector3 CalculateNormalVector(Vector3 PointPos, Vector3 SphereCenter)
        {
            Vector3 Vec = new Vector3(PointPos.X - SphereCenter.X, PointPos.Y - SphereCenter.Y, PointPos.Z - SphereCenter.Z);

            Vec = Vector3.Normalize(Vec);

            return Vec;
        }
        public static void FillPolygon(List<Vector3> Polygon, DirectBitmap dirBitmap, ColoringOptions Options)
        {
            Color[] color = new Color[3];
            Vector3[] normalVec = new Vector3[3];
            Vector3[] textureVec = new Vector3[3];
            float triangleDenominator = 1;

            if (Options.ObjColor == ObjectColor.Interpolated)
            {
                float x1 = Polygon[0].X, y1 = Polygon[0].Y;
                float x2 = Polygon[1].X, y2 = Polygon[1].Y;
                float x3 = Polygon[2].X, y3 = Polygon[2].Y;

                triangleDenominator = (y2 - y3) * (x1 - x3) + (x3 - x2) * (y1 - y3);

                if (triangleDenominator == 0)
                    triangleDenominator = 1;

                for(int i = 0; i < 3; i++)
                {
                    color[i] = Options.ObjSurface == ObjectSurface.Solid ? Options.SolidCol
                    : (Options.NormalMap.GetPixel((int)Polygon[i].X, (int)Polygon[i].Y));

                    normalVec[i] = CalculateNormalVector(Polygon[i], Options.SphereCenter);

                    textureVec[i] = PixelColoring.GetVectorFromTexture((int)Polygon[i].X, (int)Polygon[i].Y, Options.NormalMap);
                }
            }

            var sortedVertices = Polygon.ConvertAll(v => v);
            sortedVertices.Sort((a, b) =>
            {
                if (a.Y < b.Y)
                    return -1;
                if (a.Y == b.Y)
                    return 0;
                return 1;
            });

            var ind = sortedVertices.ConvertAll(v => Polygon.IndexOf(v));
            List<Point> P = new List<Point>();
            foreach(var vert in Polygon)
            {
                P.Add(new Point((int)vert.X, (int)vert.Y));
            }

            double ymin = P[ind[0]].Y;
            double ymax = P[ind[ind.Count - 1]].Y;
            int k = 0; // current vertex index;
            List<AETItem> AET = new List<AETItem>();

            // for each scanline
            for (double y = ymin + 1; y <= ymax + 1; y++)
            {
                while (k < ind.Count && y - 1 == P[ind[k]].Y)
                {
                    // previous vertex
                    int pVertInd = ind[k] - 1 >= 0 ? ind[k] - 1 : P.Count - 1;
                    if (P[pVertInd].Y >= P[ind[k]].Y)
                    {
                        // add to AET
                        double dx = (double)(P[pVertInd].X - P[ind[k]].X);
                        double dy = (double)(P[pVertInd].Y - P[ind[k]].Y);
                        if (dy != 0)
                            AET.Add(new AETItem(P[pVertInd], P[ind[k]], P[ind[k]].X, dx / dy));
                    }
                    else
                    {
                        // remove from AET
                        AET.RemoveAll(i => i.start == P[pVertInd] && i.end == P[ind[k]]);
                    }

                    // next vertex
                    int nVertInd = ind[k] + 1 < P.Count ? ind[k] + 1 : 0;
                    if (P[nVertInd].Y >= P[ind[k]].Y)
                    {
                        // add to AET
                        double dx = (double)(P[ind[k]].X - P[nVertInd].X);
                        double dy = (double)(P[ind[k]].Y - P[nVertInd].Y);
                        if (dy != 0)
                            AET.Add(new AETItem(P[ind[k]], P[nVertInd], P[ind[k]].X, dx / dy));
                    }
                    else
                    {
                        // remove from AET
                        AET.RemoveAll(i => i.start == P[ind[k]] && i.end == P[nVertInd]);
                    }

                    k++;
                }

                // sort by x
                AET.Sort((a, b) =>
                {
                    if (a.x < b.x)
                        return -1;
                    if (a.x == b.x)
                        return 0;
                    return 1;
                });

                // set pixels
                for (int i = 0; i < AET.Count - 1; i += 2)
                {
                    for (double x = AET[i].x; x <= AET[i + 1].x; x++)
                    {
                        Vector3 PointPos = new Vector3((float)x, (float)y - 1, CalculateZ((int)x, (int)y - 1, Polygon[0], Polygon[1], Polygon[2]));
                        if (Options.ObjColor == ObjectColor.Presise)
                        {

                            Vector3 NormalVector = CalculateNormalVector(PointPos, Options.SphereCenter);
                            dirBitmap.SetPixel((int)x, (int)y - 1, PixelColoring.ColorPixel(Options, PointPos, NormalVector));
                        }
                        else if(Options.ObjColor == ObjectColor.Interpolated)
                        {
                            dirBitmap.SetPixel((int)x, (int)y - 1,
                                PixelColoring.ColorInterpolatedPixel(Options, PointPos, normalVec, textureVec, 
                                color.ToList(), Polygon.ToArray(), triangleDenominator));
                        }
                    }
                }

                // update x
                AET.ForEach(v => v.x += v.reciprocalM);
            }
        }
    }
}
