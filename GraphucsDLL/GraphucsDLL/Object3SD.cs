using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsDLL
{
    public class Object3D
    {
        public Object3D()
        {
            transformation = new Matrix4F();
            transformation.LoadIdentity();
        }

        public Matrix4F transformation;
    }
}
