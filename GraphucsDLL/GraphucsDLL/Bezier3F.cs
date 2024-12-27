using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsDLL
{
    public class Bezier3F
    {
        public static float B0(float t) { return (1.0f - t) * (1.0f - t) * (1.0f - t); }
        public static float B1(float t) { return 3.0f * (1.0f - t) * (1.0f - t) * t; }
        public static float B2(float t) { return 3.0f * (1.0f - t) * t * t; }
        public static float B3(float t) { return t * t * t; }


        public Vector2F p0, p1, p2, p3;

        public Bezier3F(Vector2F p0, Vector2F p1, Vector2F p2, Vector2F p3)
        {
            this.p0 = p0;
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }
    }
}
