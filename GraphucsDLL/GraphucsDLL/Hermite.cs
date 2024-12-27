using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsDLL
{
    public class Hermite
    {
            public static float H0(float t) { return 2 * t * t * t - 3 * t * t + 1; }
            public static float H1(float t) { return -2 * t * t * t + 3 * t * t; }
            public static float H2(float t) { return t * t * t - 2 * t * t + t; }
            public static float H3(float t) { return t * t * t - t * t; }

        public Vector2F p0, p1, t0, t1;

        public Hermite(Vector2F p0, Vector2F p1, Vector2F t0, Vector2F t1)
        {
            this.p0 = p0;
            this.p1 = p1;
            this.t0 = t0;
            this.t1 = t1;
        }
    }
}
