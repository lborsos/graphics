using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsBase
{
    internal class Rect
    {
        private int x;
        private int y;
        private int width;
        private int height;
        private Color color;

        public Rect(int x, int y, int width, int height, Color color)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.color = color;
        }
        public Rect(int x, int y): this(x, y, 10, 10, Color.Aqua) 
        {
        }
    }
}
