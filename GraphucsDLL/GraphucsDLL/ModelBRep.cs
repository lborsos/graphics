using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GraphicsDLL
{
    public class ModelBRep : Object3D
    {
        public List<Vector4F> vertices = new List<Vector4F>();
        public List<Triangle> triangles = new List<Triangle>();
    }
}
