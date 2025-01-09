using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsDLL
{
    public class Triangle
    {
        public Vector4F a;
        public Vector4F b;
        public Vector4F c;

        public Triangle(Vector4F a, Vector4F b, Vector4F c)
        {
            this.a = a ?? throw new ArgumentNullException(nameof(a));
            this.b = b ?? throw new ArgumentNullException(nameof(b));
            this.c = c ?? throw new ArgumentNullException(nameof(c));
        }

        public Vector4F NormalA
        {
            get
            {
                return (b - a) ^ (c - a);
            }
        }
        public float VisibilityValueA(Vector4F camera)
        {

            Vector4F c = camera - a;
            float dotProduct = NormalA * c;
            dotProduct /= NormalA.Length;
            dotProduct /= c.Length;
            return dotProduct;

        }
        public bool IsVisible(Vector4F camera)
        {
            return VisibilityValueA(camera) > 0;
        }
        public float WeightZ
        {
            get
            {
                return (a.z + b.z + c.z) / 3.0f;
            }
        }
    }
}
