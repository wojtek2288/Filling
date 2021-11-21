using GK_Lab2.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GK_Lab2.FastBitmap;
using System.Drawing.Drawing2D;

namespace GK_Lab2.FillingPolygon
{
    public static class PixelColoring
    {

        public enum Component
        {
            R,
            G,
            B,
        }
        public static Color ColorPixel(ColoringOptions Options, Vector3 PointPos, Vector3 NormalVector)
        {
            Vector3[] matrix = new Vector3[3];
            Vector3 B = Vector3.Normalize(Vector3.Cross(NormalVector, new Vector3(0, 0, 1)));
            Vector3 T = Vector3.Normalize(Vector3.Cross(B, NormalVector));

            if (NormalVector.X == 0 && NormalVector.Y == 0 && NormalVector.Z == 1)
                B = Vector3.Normalize(new Vector3(0, 1, 0));

            matrix[0] = T;
            matrix[1] = B;
            matrix[2] = NormalVector;

            Vector3 N = (float)Options.k * NormalVector + (float)(1 - Options.k) * CalculateMultiplication(matrix, GetVectorFromTexture((int)PointPos.X, (int)PointPos.Y, Options.NormalMap));
            N = Vector3.Normalize(N);

            Vector3 L = Vector3.Normalize(new Vector3(Options.LightPos.X - PointPos.X, Options.LightPos.Y - PointPos.Y, Options.LightPos.Z - PointPos.Z));

            Vector3 R = 2 * Vector3.Dot(N, L) * N - L;

            double ObjectR = (Options.ObjSurface == ObjectSurface.Solid ? Map_0_255_to_0_1(Options.SolidCol.R)
                : Map_0_255_to_0_1(Options.NormalMap.GetPixel((int)PointPos.X, (int)PointPos.Y).R));

            double ObjectG = (Options.ObjSurface == ObjectSurface.Solid ? Map_0_255_to_0_1(Options.SolidCol.G)
                : Map_0_255_to_0_1(Options.NormalMap.GetPixel((int)PointPos.X, (int)PointPos.Y).G));

            double ObjectB = (Options.ObjSurface == ObjectSurface.Solid ? Map_0_255_to_0_1(Options.SolidCol.B)
                : Map_0_255_to_0_1(Options.NormalMap.GetPixel((int)PointPos.X, (int)PointPos.Y).B));

            double RVal = Options.kd * Map_0_255_to_0_1(Options.LightColor.R) * ObjectR
                * CalcCos(N, L)
                + Options.ks * Map_0_255_to_0_1(Options.LightColor.R) * ObjectR
                * Math.Pow(CalcCos(new Vector3(0,0,1), R), Options.m);

            double GVal = Options.kd * Map_0_255_to_0_1(Options.LightColor.G) * ObjectG
                * CalcCos(N, L)
                + Options.ks * Map_0_255_to_0_1(Options.LightColor.G) * ObjectG
                * Math.Pow(CalcCos(new Vector3(0, 0, 1), R), Options.m);

            double BVal = Options.kd * Map_0_255_to_0_1(Options.LightColor.B) * ObjectB
                * CalcCos(N, L)
                + Options.ks * Map_0_255_to_0_1(Options.LightColor.B) * ObjectB
                * Math.Pow(CalcCos(new Vector3(0, 0, 1), R), Options.m);

            return Color.FromArgb(Map_0_1_to_0_255(RVal), Map_0_1_to_0_255(GVal), Map_0_1_to_0_255(BVal));
        }

        public static Color ColorInterpolatedPixel(ColoringOptions Options, Vector3 PointPos, List<Color> Colors, Vector3[] Triangle, float triangleDenominator)
        {
            if(Options.ObjColor == ObjectColor.Interpolated)
            {
                float x = PointPos.X, y = PointPos.Y;
                float x1 = Triangle[0].X, y1 = Triangle[0].Y;
                float x2 = Triangle[1].X, y2 = Triangle[1].Y;
                float x3 = Triangle[2].X, y3 = Triangle[2].Y;

                float W1 = ((y2 - y3) * (x - x3) + (x3 - x2) * (y - y3)) / triangleDenominator;
                float W2 = ((y3 - y1) * (x - x3) + (x1 - x3) * (y - y3)) / triangleDenominator;
                float W3 = 1 - W1 - W2;

                double ColorR = Map_0_255_to_0_1((int)(W1 * Colors[0].R + W2 * Colors[1].R + W3 * Colors[2].R));
                double ColorG = Map_0_255_to_0_1((int)(W1 * Colors[0].G + W2 * Colors[1].G + W3 * Colors[2].G));
                double ColorB = Map_0_255_to_0_1((int)(W1 * Colors[0].B + W2 * Colors[1].B + W3 * Colors[2].B));

                return Color.FromArgb(Map_0_1_to_0_255(ColorR), Map_0_1_to_0_255(ColorG), Map_0_1_to_0_255(ColorB));
            }
            return Color.White;
        }

        public static Vector3 CalculateMultiplication(Vector3[] matrix, Vector3 vec)
        {
            float sum;
            Vector3 res = new Vector3();

            for(int i = 0; i < 3; i++)
            {
                sum = 0;
                for(int j = 0; j < 3; j++)
                {
                    if(j == 0)
                        sum += matrix[i].X * vec.X;
                    else if(j == 1)
                        sum += matrix[i].Y * vec.Y;
                    else if(j == 2)
                        sum += matrix[i].Z + vec.Z;
                }
                if (i == 0)
                    res.X = sum;
                else if (i == 1)
                    res.Y = sum;
                else if (i == 2)
                    res.Z = sum;
            }

            return Vector3.Normalize(res);
        }

        public static Vector3 GetVectorFromTexture(int x, int y, DirectBitmap bitmap)
        {
            var vector = bitmap.GetPixel(x, y);

            return Vector3.Normalize(new Vector3((float)(vector.R/127.5 - 1.0), (float)(vector.G/127.5 - 1.0), (float)(vector.B/127.5 - 1.0)));
        }

        public static double CalcCos(Vector3 N, Vector3 L)
        {
            double res = N.X * L.X + N.Y * L.Y + N.Z * L.Z;
            return res > 0 ? res : 0;
        }

        public static double Map_0_255_to_0_1(int RGB)
        {
            double ret = (double)RGB / 255;
            if (ret > 1) return 1;
            if (ret < 0) return 0;
            return ret;
        }

        public static int Map_0_1_to_0_255(double val)
        {
            int ret = (int)(val * 255);
            if (ret > 255) return 255;
            if (ret < 0) return 0;
            return ret;
        }
    }
}
