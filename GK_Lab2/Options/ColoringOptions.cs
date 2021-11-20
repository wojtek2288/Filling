using GK_Lab2.FastBitmap;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GK_Lab2.Options
{
    public class ColoringOptions
    {
        public double kd { get; set; }
        public double ks { get; set; }
        public double k { get; set; }
        public int m { get; set; }
        public Color LightColor { get; set; }
        public Vector3 LightPos { get; set; }
        public Vector3 SphereCenter { get; set; }
        public Color SolidCol { get; set; }
        public DirectBitmap NormalMap { get; set; }
        public ObjectColor ObjColor { get; set; }
        public ObjectSurface ObjSurface { get; set; }

        public ColoringOptions() { }
    }

    public enum ObjectColor
    {
        Presise,
        Interpolated
    }

    public enum ObjectSurface
    {
        Solid,
        Texture
    }
}
