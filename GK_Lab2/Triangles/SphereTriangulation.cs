using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GK_Lab2.Triangles
{
    public static class SphereTriangulation
    {
        public static Triangle[] TriangulateSphere(int ParallesCount, int ParallesAccuracy, int Radius, Point Center)
        {
            int m = ParallesCount;
            int n = ParallesAccuracy;

            Vector3[] vertices = new Vector3[m * n + 2];
            Triangle[] Triangles = new Triangle[2 * m * n];

            vertices[0] = new Vector3(0, Radius, 0);
            vertices[m * n + 1] = new Vector3(0, -Radius, 0);

            for(int i = 1; i <= m; i++)
            {
                for(int j = 1; j <= n; j++)
                {
                    if(i*n + j < m*n+2)
                        vertices[i * n + j] = new Vector3(
                            (float)(Radius * Math.Cos(2 * Math.PI * (j - 1) / n)*Math.Sin(Math.PI*i/(m+1))),
                            (float)(Radius * Math.Cos(Math.PI * j / (m + 1))),
                            (float)(Radius * Math.Sin(2*Math.PI * (j - 1) / n) * Math.Sin(Math.PI * i / (m+1))));
                }
            }

            Vector2 movePoints = new Vector2(Center.X, Center.Y);

            for(int i = 0; i < m*n + 2; i++)
            {
                vertices[i].X = vertices[i].X + movePoints.X;
                vertices[i].Y = vertices[i].Y + movePoints.Y;
            }

            for(int i = 0; i <= n-1; i++)
            {
                if(i == n-1)
                {
                    if (vertices[0].Z >= 0 && vertices[1].Z >= 0 && vertices[n].Z >= 0)
                        Triangles[i] = new Triangle(
                            new Point(Convert.ToInt32(vertices[0].X), Convert.ToInt32(vertices[0].Y)),
                            new Point(Convert.ToInt32(vertices[1].X), Convert.ToInt32(vertices[1].Y)),
                            new Point(Convert.ToInt32(vertices[n].X), Convert.ToInt32(vertices[n].Y)));
                    else
                        Triangles[i] = new Triangle();

                    if (vertices[m * n + 1].Z >= 0 && vertices[m * n].Z >= 0 && vertices[(m - 1) * n + 1].Z >= 0)
                        Triangles[2 * (m - 1) * n + i] = new Triangle(
                            new Point(Convert.ToInt32(vertices[m*n + 1].X), Convert.ToInt32(vertices[m*n+1].Y)),
                            new Point(Convert.ToInt32(vertices[m*n].X), Convert.ToInt32(vertices[m*n].Y)),
                            new Point(Convert.ToInt32(vertices[(m - 1) * n + 1].X), Convert.ToInt32(vertices[(m - 1) * n + 1].Y)));
                    else
                        Triangles[2 * (m - 1) * n + i] = new Triangle();
                }
                else
                {
                    if (vertices[0].Z >= 0 && vertices[i + 2].Z >= 0 && vertices[i + 1].Z >= 0)
                        Triangles[i] = new Triangle(
                            new Point(Convert.ToInt32(vertices[0].X), Convert.ToInt32(vertices[0].Y)),
                            new Point(Convert.ToInt32(vertices[i + 2].X), Convert.ToInt32(vertices[i + 2].Y)),
                            new Point(Convert.ToInt32(vertices[i + 1].X), Convert.ToInt32(vertices[i + 1].Y)));
                    else
                        Triangles[i] = new Triangle();

                    if (vertices[m * n + 1].Z >= 0 && vertices[(m-1)*n + i + 1].Z >= 0 && vertices[(m-1)*n + i + 2].Z >= 0)
                        Triangles[2 * (m - 1) * n + i] = new Triangle(
                            new Point(Convert.ToInt32(vertices[m * n + 1].X), Convert.ToInt32(vertices[m * n + 1].Y)),
                            new Point(Convert.ToInt32(vertices[(m - 1) * n + i + 1].X), Convert.ToInt32(vertices[(m - 1) * n + i + 1].Y)),
                            new Point(Convert.ToInt32(vertices[(m - 1) * n + i + 2].X), Convert.ToInt32(vertices[(m - 1) * n + i + 2].Y)));
                }
            }
            for (int i = 0; i <= m-2; i++)
            {
                for(int j = 1; j <= n; j++)
                {
                    if(j == n)
                    {
                        if (vertices[(i + 1) * n].Z >= 0 && vertices[i * n + 1].Z >= 0 && vertices[(i + 1) * n + 1].Z >= 0)
                            Triangles[(2 * i + 1) * n + j - 1] = new Triangle(
                            new Point(Convert.ToInt32(vertices[(i + 1) * n].X), Convert.ToInt32(vertices[(i + 1) * n].Y)),
                            new Point(Convert.ToInt32(vertices[i * n + 1].X), Convert.ToInt32(vertices[i * n + 1].Y)),
                            new Point(Convert.ToInt32(vertices[(i + 1) * n + 1].X), Convert.ToInt32(vertices[(i + 1) * n + 1].Y)));
                        else
                            Triangles[(2 * i + 1) * n + j - 1] = new Triangle();

                        if (vertices[(i + 1) * n].Z >= 0 && vertices[(i + 1) * n + 1].Z >= 0 && vertices[(i + 2) * n].Z >= 0)
                            Triangles[(2 * i + 2) * n + j - 1] = new Triangle(
                            new Point(Convert.ToInt32(vertices[(i + 1) * n].X), Convert.ToInt32(vertices[(i + 1) * n].Y)),
                            new Point(Convert.ToInt32(vertices[(i + 1) * n + 1].X), Convert.ToInt32(vertices[(i + 1) * n + 1].Y)),
                            new Point(Convert.ToInt32(vertices[(i + 2) * n].X), Convert.ToInt32(vertices[(i + 2) * n].Y)));
                        else
                            Triangles[(2 * i + 2) * n + j - 1] = new Triangle();
                    }
                    else
                    {
                        if (vertices[i * n + j].Z >= 0 && vertices[i * n + j + 1].Z >= 0 && vertices[(i + 1) * n + j + 1].Z >= 0)
                            Triangles[(2 * i + 1) * n + j - 1] = new Triangle(
                            new Point(Convert.ToInt32(vertices[i * n + j].X), Convert.ToInt32(vertices[i * n + j].Y)),
                            new Point(Convert.ToInt32(vertices[i * n + j + 1].X), Convert.ToInt32(vertices[i * n + j + 1].Y)),
                            new Point(Convert.ToInt32(vertices[(i + 1) * n + j + 1].X), Convert.ToInt32(vertices[(i + 1) * n + j + 1].Y)));
                        else
                            Triangles[(2 * i + 1) * n + j - 1] = new Triangle();

                        if (vertices[i * n + j].Z >= 0 && vertices[(i + 1)*n + j + 1].Z >= 0 && vertices[(i + 1)*n + j].Z >= 0)
                            Triangles[(2*i + 2)*n + j - 1] = new Triangle(
                            new Point(Convert.ToInt32(vertices[i * n + j].X), Convert.ToInt32(vertices[i * n + j].Y)),
                            new Point(Convert.ToInt32(vertices[(i + 1) * n + j + 1].X), Convert.ToInt32(vertices[(i + 1) * n + j + 1].Y)),
                            new Point(Convert.ToInt32(vertices[(i + 1) * n + j].X), Convert.ToInt32(vertices[(i + 1) * n + j].Y)));
                        else
                            Triangles[(2 * i + 2) * n + j - 1] = new Triangle();
                    }
                }
            }
            return Triangles;
        }
    }
}
