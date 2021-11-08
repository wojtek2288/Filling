using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GK_Lab2.SphereTriangulation;
using GK_Lab2.FastBitmap;
using GK_Lab2.FillingRectangle;
using GK_Lab2.Geometry;

namespace GK_Lab2
{
    public partial class Form1 : Form
    {
        public List<List<Vector3>> TrianglesList;
        public DirectBitmap dirBitmap;
        public Point ClickedPoint = new Point();
        public bool MovingVerticesFlag = false;
        public Vector3 MovingVertice;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TrianglesList = Triangulation.TriangulateSphere(Convert.ToInt32(pictureBox1.Width / 3.2), Convert.ToInt32(pictureBox1.Width / 2), Convert.ToInt32(pictureBox1.Height / 2), ParallelsCountTrackBar.Value);
            dirBitmap = new DirectBitmap(Convert.ToInt32(pictureBox1.Width), Convert.ToInt32(pictureBox1.Height));

            for (int i = 0; i < dirBitmap.Width; i++)
            {
                for (int j = 0; j < dirBitmap.Height; j++)
                {
                    dirBitmap.SetPixel(i, j, Color.AliceBlue);
                }
            }

            pictureBox1.Image = dirBitmap.Bitmap;
        }

        private void ParallelsCountTrackBar_ValueChanged(object sender, EventArgs e)
        {
            TrianglesList.Clear();
            TrianglesList = Triangulation.TriangulateSphere(Convert.ToInt32(pictureBox1.Width / 3.2), Convert.ToInt32(pictureBox1.Width / 2), Convert.ToInt32(pictureBox1.Height / 2), ParallelsCountTrackBar.Value);
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen blackPen = new Pen(Color.Black);

            foreach (var triangle in TrianglesList)
            {
                e.Graphics.DrawLine(blackPen, new Point(Convert.ToInt32(triangle[0].X), Convert.ToInt32(triangle[0].Y)), 
                    new Point(Convert.ToInt32(triangle[1].X), Convert.ToInt32(triangle[1].Y)));
                e.Graphics.DrawLine(blackPen, new Point(Convert.ToInt32(triangle[0].X), Convert.ToInt32(triangle[0].Y)),
                    new Point(Convert.ToInt32(triangle[2].X), Convert.ToInt32(triangle[2].Y)));
                e.Graphics.DrawLine(blackPen, new Point(Convert.ToInt32(triangle[1].X), Convert.ToInt32(triangle[1].Y)),
                    new Point(Convert.ToInt32(triangle[2].X), Convert.ToInt32(triangle[2].Y)));
            }

            Filling.FillPolygon(TrianglesList[0], dirBitmap);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                colorDialog1.ShowDialog();
                pictureBox2.BackColor = colorDialog1.Color;

                for (int i = 0; i < dirBitmap.Width; i++)
                {
                    for (int j = 0; j < dirBitmap.Height; j++)
                    {
                        dirBitmap.SetPixel(i, j, colorDialog1.Color);
                    }
                }

                pictureBox1.Image = dirBitmap.Bitmap;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Point ClickedPosition = pictureBox1.PointToClient(Cursor.Position);

            if(!MovingVerticesFlag)
            {
                foreach (var triangle in TrianglesList)
                {
                    foreach (var point in triangle)
                    {
                        if (GeometryCalculations.CheckDistance(ClickedPosition, new Point(Convert.ToInt32(point.X), Convert.ToInt32(point.Y))) < 10)
                        {
                            MovingVerticesFlag = true;
                            MovingVertice = point;
                            ClickedPoint = new Point(Convert.ToInt32(point.X), Convert.ToInt32(point.Y));
                        }
                    }
                }
            }
            else
            {
                MovingVerticesFlag = false;
                ClickedPoint = new Point();
                MovingVertice = new Vector3();
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (MovingVerticesFlag)
            {
                Point cursorPosition = pictureBox1.PointToClient(Cursor.Position);
                Vector2 Vector = new Vector2(ClickedPoint.X - cursorPosition.X,
                    ClickedPoint.Y - cursorPosition.Y);

                for(int i = 0; i < TrianglesList.Count; i++)
                {
                    for(int j = 0; j < TrianglesList[i].Count; j++)
                    {
                        Vector3 point = TrianglesList[i][j];
                        if(point.X == ClickedPoint.X && point.Y == ClickedPoint.Y)
                        {
                            point.X = point.X - Vector.X;
                            point.Y = point.Y - Vector.Y;

                            TrianglesList[i][j] = point;
                        }
                    }
                }
                pictureBox1.Refresh();
                ClickedPoint = new Point(cursorPosition.X, cursorPosition.Y);
            }
        }
    }
}
