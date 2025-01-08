using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphucsDLL.ExtensionPointF;

namespace GraphicsBase
{
    internal class Rect
    {
        const int GRAB_SIZE = 10;
        const int CREATE_SIZE = 20;
        private int x;
        private int y;
        private int width;
        private int height;
        private Color color;
        private static Random rnd = new Random();
        private int offsetX;
        private int offsetY;

        public Rect(int x, int y, int width, int height, Color color)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.color = color;
        }
        public Rect(PointF mou) : this((int)mou.X - CREATE_SIZE, (int)mou.Y -CREATE_SIZE, CREATE_SIZE, CREATE_SIZE, Color.FromArgb(rnd.Next(256),rnd.Next(256), rnd.Next(256)))
        {
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(color), x, y, width, height);
        }
        public void StartMove(PointF mousePos)
        {
            offsetX = (int)mousePos.X - x;
            offsetY = (int)mousePos.Y - y;
        }
        public void Move(PointF p)
        {
            this.x = (int)p.X - offsetX;
            this.y = (int)p.Y - offsetY;
        }
        public void Resize(PointF p)
        {
            this.width = (int)p.X - this.x;
            this.height = (int)p.Y - this.y;

            if (this.width < 0) this.width = 0;
            if (this.height < 0) this.height = 0;
        }
        public bool IsResize(PointF others)
        {
            return MathF.Abs(x + width - others.X) <= GRAB_SIZE &&
                MathF.Abs(y + height - others.Y) <= GRAB_SIZE;
        }
        public bool IsMove(PointF others)
        {
            return others.X >= x && others.X <= x + width &&
            others.Y >= y && others.Y <= y + height;
        }
    }
}
