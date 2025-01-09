using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsDLL
{
    public class Vector4F
    {
        public float x, y, z, w;

        public Vector4F() : this(0, 0, 0)
        {
        }
        public Vector4F(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 1.0f;
        }
        public Vector4F(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public void ToCartesian()
        {
            this.x /= this.w;
            this.y /= this.w;
            this.z /= this.w;
            this.w = 1.0f;
        }
        public float Length { get { return MathF.Sqrt(x * x + y * y + z * z); } }

        public static Vector4F operator +(Vector4F a, Vector4F b)
        {
            return new Vector4F(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static Vector4F operator +(Vector4F a, Vector2F b)
        {
            return new Vector4F(a.x + b.x, a.y + b.y, 0.0f);
        }
        public static Vector4F operator -(Vector4F a, Vector4F b)
        {
            return new Vector4F(a.x - b.x, a.y - b.y, a.z - b.z);
        }
        public static Vector4F operator *(float a, Vector4F v)
        {
            return new Vector4F(a * v.x, a * v.y, a * v.z);
        }
        public static Vector4F operator *(Vector4F v, float a)
        {
            return new Vector4F(a * v.x, a * v.y, a * v.z);
        }
        public static implicit operator PointF(Vector4F v0)
        {
            return new PointF(v0.x, v0.y);
        }
        public static float operator *(Vector4F a, Vector4F b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }
        public static Vector4F operator ^(Vector4F a, Vector4F b)
        {
            return new Vector4F(a.y * b.z - a.z * b.y,
                                a.z * b.x - a.x * b.z,
                                a.x * b.y - a.y * b.x);

        }
    }
}
