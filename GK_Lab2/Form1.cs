using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GK_Lab2.Triangles;

namespace GK_Lab2
{
    public partial class Form1 : Form
    {
        List<Triangle> Triangles = new List<Triangle>();
        public Form1()
        {
            InitializeComponent();
        }

        private void ParallelsCountTrackBar_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = "Parallels Count: " + ParallelsCountTrackBar.Value.ToString();
        }

        private void ParallelApproximationTrackBar_ValueChanged(object sender, EventArgs e)
        {
            label2.Text = "Parallel Approximation Vertices Count: " + ParallelApproximationTrackBar.Value.ToString();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen blackPen = new Pen(Color.Black);
            Triangle[] Triangles = SphereTriangulation.TriangulateSphere(ParallelsCountTrackBar.Value/4, ParallelApproximationTrackBar.Value/4, 200, new Point(pictureBox1.Width / 2, pictureBox1.Height / 2));

            foreach(var triangle in Triangles)
            {
                if(triangle != default)
                {
                    e.Graphics.DrawLine(blackPen, triangle.x1, triangle.x2);
                    e.Graphics.DrawLine(blackPen, triangle.x1, triangle.x3);
                    e.Graphics.DrawLine(blackPen, triangle.x2, triangle.x3);
                }
            }
        }
    }
}
