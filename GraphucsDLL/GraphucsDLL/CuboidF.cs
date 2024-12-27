using GraphucsDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsDLL
{
    public class CuboidF
    {
        public Vector3F corner;
        public float width, height, depth;

        public CuboidF(Vector3F corner, float width = 0, float height = 0, float depth = 0)
        {
            this.corner = corner ?? throw new ArgumentNullException(nameof(corner));
            this.width = width;
            this.height = height;
            this.depth = depth;
        }
    }
}
