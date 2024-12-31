using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsDLL
{
    public class Bezier3P
    {
        public static class Basis
        {
            public static float B0(float t, float p) { return (1.0f - t) * (1.0f - t) * (1.0f + (2.0f - p) * t); }
            public static float B1(float t, float p) { return p * t * (1.0f - t) * (1.0f - t); }
            public static float B2(float t, float p) { return p * t * t * (1.0f - t); }
            public static float B3(float t, float p) { return t * t * (3 - p + (p - 2.0f) * t); }
        }

        public Vector2F p0, p1, p2, p3;
        public float p;

        public Bezier3P(Vector2F p0, Vector2F p1, Vector2F p2, Vector2F p3, float p = 3f)
        {
            this.p0 = p0;
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            this.p = p;
        }
    }
}
