using GraphicsDLL;
using GraphucsDLL;

namespace GraphicsBase
{
    public partial class Form1 : Form
    {
        Graphics g;
        float s = 1.5f;
        int grabbed = -1; // -1 no grabbed, 0 move, 1, resize
        List<Rect> rects = new List<Rect>();
        int index = -1; // kivalasztott teglalap (Resize / Move hasznalja)
        Hermite arc;
        public Form1()
        {
            InitializeComponent();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            
            foreach (Rect rect in rects)
            {
                rect.Draw(e.Graphics); 
            }
            if (rects.Count > 1) 
            {
                for (int i = 0; i < rects.Count - 1; i++)
                {
                    // Hermite arc = new Hermite(p0, p1, s * (t0 - p0), s * (t1 - p1));
                    arc = new Hermite(rects[i].RightTop(), rects[i + 1].LeftTop(), s * (rects[i].Right2Top() - rects[i].RightTop()), s * (rects[i].Right2Top() - rects[i].LeftTop()));
                    g.DrawHermiteArc(rects[i].GetColor(), rects[i + 1].GetColor(), arc, 3);
                    arc = new Hermite(rects[i].RightBottom(), rects[i + 1].LeftBottom(), s * (rects[i].Right2Bottom() - rects[i].RightBottom()), s * (rects[i].Right2Bottom() - rects[i].LeftBottom()));
                    g.DrawHermiteArc(rects[i].GetColor(), rects[i + 1].GetColor(), arc, 3);
                }
            }
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            // jobb klick torli a teglalapot ha benne volt a kttintas!
            if (e.Button == MouseButtons.Right)
            {
                index = rects.FindIndex(x => x.IsMove(e.Location));
                if (index >= 0)
                {
                    rects.RemoveAt(index);
                    canvas.Invalidate(); 
                    return;
                }
            }
            if (grabbed == -1)
            {
                index = rects.FindIndex(x => x.IsResize(e.Location));
                if (index >= 0) 
                {
                    grabbed = 1;
                }
                else
                {
                    index = rects.FindIndex(x => x.IsMove(e.Location));
                    if (index >= 0)
                    {
                        grabbed = 0;
                        rects[index].StartMove(e.Location);
                    }
                    else
                    {
                        grabbed = 1;
                        rects.Add(new Rect(e.Location));
                        index = rects.Count - 1;
                    }
                }
                canvas.Invalidate();
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (grabbed == 0)
            {
                rects[index].Move(e.Location);
                canvas.Invalidate();
            }
            if (grabbed == 1)
            {
                rects[index].Resize(e.Location);
                canvas.Invalidate();
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            grabbed = -1;
        }
    }
}
