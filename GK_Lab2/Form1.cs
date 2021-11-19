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
using GK_Lab2.FillingPolygon;
using GK_Lab2.Geometry;
using GK_Lab2.Options;

namespace GK_Lab2
{
    public partial class Form1 : Form
    {
        public List<List<Vector3>> TrianglesList;
        public DirectBitmap dirBitmap;
        public List<DirectBitmap> Texture = new List<DirectBitmap>();
        public ColoringOptions Options = new ColoringOptions();
        public int Radius;
        public Point ClickedPoint = new Point();
        public bool MovingVerticesFlag = false;
        public Vector3 MovingVertice;
        public float angle, radius;
        public float angleSpeed = 2;
        public float radialSpeed = 0.5f;

        public Form1()
        {
            InitializeComponent();
        }

        public void ColorSphere(List<List<Vector3>> TrianglesList, ColoringOptions opt)
        {
            for(int i = 0; i < dirBitmap.Width; i++)
            {
                for(int j = 0; j < dirBitmap.Height; j++)
                {
                    dirBitmap.SetPixel(i, j, SystemColors.Control);
                }
            }

            Parallel.For(0, TrianglesList.Count, i =>
              {
                  Fill.FillPolygon(TrianglesList[i], dirBitmap, opt);
              });

            pictureBox1.Image = dirBitmap.Bitmap;
        }

        public void LoadTexture(int idx)
        {
            DirectBitmap dBmt = new DirectBitmap(pictureBox1.Width, pictureBox1.Height);

            switch(idx)
            {
                case 0:
                    {
                        using (var bmt = new Bitmap(System.IO.Path.Combine(Application.StartupPath, "..\\..\\..\\Img\\metal.jpg")))
                        {
                            for (int i = 0; i < dirBitmap.Width; i++)
                            {
                                for (int j = 0; j < dirBitmap.Height; j++)
                                {
                                    dBmt.SetPixel(i, j, bmt.GetPixel(i % bmt.Width, j % bmt.Height));
                                }
                            }
                        }
                        Texture.Add(dBmt);
                        break;
                    }
                case 1:
                    {
                        using (var bmt = new Bitmap(System.IO.Path.Combine(Application.StartupPath, "..\\..\\..\\Img\\bricks.png")))
                        {
                            for (int i = 0; i < dirBitmap.Width; i++)
                            {
                                for (int j = 0; j < dirBitmap.Height; j++)
                                {
                                    dBmt.SetPixel(i, j, bmt.GetPixel(i % bmt.Width, j % bmt.Height));
                                }
                            }
                        }
                        Texture.Add(dBmt);
                        break;
                    }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Radius = Convert.ToInt32(pictureBox1.Width / 3.2);
            TrianglesList = Triangulation.TriangulateSphere(Radius, Convert.ToInt32(pictureBox1.Width / 2), Convert.ToInt32(pictureBox1.Height / 2), ParallelsCountTrackBar.Value);
            dirBitmap = new DirectBitmap(Convert.ToInt32(pictureBox1.Width), Convert.ToInt32(pictureBox1.Height));
            LoadTexture(0);
            colorDialog1.Color = Color.White;
            colorDialog2.Color = Color.White;
            colorDialog3.Color = Color.White;
            pictureBox3.BackColor = Color.White;
            pictureBox4.BackColor = Color.White;
            comboBox1.SelectedIndex = 0;

            Options = new ColoringOptions();
            Options.k = 0.5;
            Options.kd = 0.5;
            Options.ks = 0.5;
            Options.m = 50;
            Options.LightColor = colorDialog2.Color;
            Options.LightPos = new Vector3(pictureBox1.Width / 2, pictureBox1.Height / 2, Radius + 150);
            Options.SphereCenter = new Vector3(pictureBox1.Width / 2, pictureBox1.Height / 2, 0);
            Options.NormalMap = Texture[0];
            Options.ObjColor = ObjectColor.Presise;
            Options.ObjSurface = ObjectSurface.Texture;

            angle = 0;
            radius = Radius;
            timer1.Tick += new EventHandler(TimerEventProcessor);
            timer1.Interval = 10;
            ColorSphere(TrianglesList, Options);
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            angle += (float)0.1;
            radius -= 1;

            if (radius < 0)
            {
                Options.LightPos = new Vector3(pictureBox1.Width / 2, pictureBox1.Height / 2, Radius + trackBar1.Value);
                radius = Radius;
            }


            Options.LightPos = new Vector3(radius*(float)Math.Cos(angle) + pictureBox1.Width/2, radius*(float)Math.Sin(angle) + pictureBox1.Height/2, Options.LightPos.Z);

            ColorSphere(TrianglesList, Options);
            pictureBox1.Refresh();
        }

        private void ParallelsCountTrackBar_ValueChanged(object sender, EventArgs e)
        {
            TrianglesList.Clear();
            TrianglesList = Triangulation.TriangulateSphere(Radius, Convert.ToInt32(pictureBox1.Width / 2), Convert.ToInt32(pictureBox1.Height / 2), ParallelsCountTrackBar.Value);
            ColorSphere(TrianglesList, Options);
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen blackPen = new Pen(Color.Black);

            if (checkBox1.Checked)
            {
                foreach (var triangle in TrianglesList)
                {
                    e.Graphics.DrawLine(blackPen, new Point(Convert.ToInt32(triangle[0].X), Convert.ToInt32(triangle[0].Y)),
                        new Point(Convert.ToInt32(triangle[1].X), Convert.ToInt32(triangle[1].Y)));
                    e.Graphics.DrawLine(blackPen, new Point(Convert.ToInt32(triangle[0].X), Convert.ToInt32(triangle[0].Y)),
                        new Point(Convert.ToInt32(triangle[2].X), Convert.ToInt32(triangle[2].Y)));
                    e.Graphics.DrawLine(blackPen, new Point(Convert.ToInt32(triangle[1].X), Convert.ToInt32(triangle[1].Y)),
                        new Point(Convert.ToInt32(triangle[2].X), Convert.ToInt32(triangle[2].Y)));
                }

                e.Graphics.DrawEllipse(new Pen(Color.Red), Options.LightPos.X - 6, Options.LightPos.Y - 6,
                    6 + 6, 6 + 6);

                e.Graphics.FillEllipse(new SolidBrush(Color.Red), Options.LightPos.X - 6, Options.LightPos.Y - 6,
                    6 + 6, 6 + 6);
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
                timer1.Stop();
                radioButton6.Checked = true;
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
                ColorSphere(TrianglesList, Options);
                ClickedPoint = new Point(cursorPosition.X, cursorPosition.Y);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            colorDialog2.ShowDialog();
            pictureBox3.BackColor = colorDialog2.Color;
            Options.LightColor = colorDialog2.Color;
            ColorSphere(TrianglesList, Options);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label2.Text = "Pixels above sphere: " + trackBar1.Value.ToString();
            Options.LightPos = new Vector3(Options.LightPos.X, Options.LightPos.Y, Radius + trackBar1.Value);
            ColorSphere(TrianglesList, Options);
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            label_k.Text = "k: " + (double)trackBar2.Value / 100;
            Options.k = (double)trackBar2.Value / 100;
            ColorSphere(TrianglesList, Options);
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            label_kd.Text = "kd: " + (double)trackBar3.Value / 100;
            Options.kd = (double)trackBar3.Value / 100;
            ColorSphere(TrianglesList, Options);
        }

        private void trackBar4_ValueChanged(object sender, EventArgs e)
        {
            label_ks.Text = "ks: " + (double)trackBar4.Value / 100;
            Options.ks = (double)trackBar4.Value / 100;
            ColorSphere(TrianglesList, Options);
        }

        private void trackBar5_ValueChanged(object sender, EventArgs e)
        {
            label_m.Text = "m: " + trackBar5.Value;
            Options.m = trackBar5.Value;
            ColorSphere(TrianglesList, Options);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }
        private void textureRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(textureRadioButton.Checked)
            {
                Options.ObjSurface = ObjectSurface.Texture;
                Options.NormalMap = Texture[comboBox1.SelectedIndex];
                ColorSphere(TrianglesList, Options);
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (textureRadioButton.Checked)
            {
                if (Texture.Count == comboBox1.SelectedIndex)
                    LoadTexture(comboBox1.SelectedIndex);

                Options.NormalMap = Texture[comboBox1.SelectedIndex];
                ColorSphere(TrianglesList, Options);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if(checkBox2.Checked)
            {
                colorDialog3.ShowDialog();
                Options.SolidCol = colorDialog3.Color;
                Options.ObjSurface = ObjectSurface.Solid;
                ColorSphere(TrianglesList, Options);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked)
            {
                Options.SolidCol = colorDialog3.Color;
                Options.ObjSurface = ObjectSurface.Solid;
                ColorSphere(TrianglesList, Options);
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton5.Checked)
                timer1.Start();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
                timer1.Stop();
        }
    }
}
