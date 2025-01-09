using GraphicsDLL;
using GraphucsDLL;

namespace GraphicsBase
{
    public partial class Form1 : Form
    {
        Graphics g;
        Vector2F p0 = new Vector2F(100, 400),
                 t0 = new Vector2F(400, 30),
                 p1 = new Vector2F(600, 100),
                 t1 = new Vector2F(800, 500);

        int grabbed = -1;

        Pen penArc = new Pen(Brushes.Blue, 3f);

        public Form1()
        {
            InitializeComponent();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.DrawRectangle(Pens.Black, p0.x - 5, p0.y - 5, 10, 10);
            g.DrawRectangle(Pens.Black, p1.x - 5, p1.y - 5, 10, 10);
            g.DrawLine(Pens.Black, p0, t0);
            g.DrawLine(Pens.Black, p1, t1);
            float s = 1.5f;
            Hermite arc = new Hermite(p0, p1, s * (t0 - p0), s* (t1 - p1));
            //g.DrawHermiteArc(penArc, arc);
            g.DrawHermiteArc(Color.Red, Color.Yellow, arc, 3);
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (p0.IsGrabbedBy(e.Location))
                grabbed = 0;
            else if (p1.IsGrabbedBy(e.Location))
                grabbed = 1;
            else if (t0.IsGrabbedBy(e.Location))
                grabbed = 2;
            else if (t1.IsGrabbedBy(e.Location))
                grabbed = 3;
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (grabbed != -1)
            {
                switch (grabbed)
                {
                    case 0: p0 = new Vector2F(e.X, e.Y); break;
                    case 1: p1 = new Vector2F(e.X, e.Y); break;
                    case 2: t0 = new Vector2F(e.X, e.Y); break;
                    case 3: t1 = new Vector2F(e.X, e.Y); break;
                    default: break;
                }
                canvas.Invalidate();
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            grabbed = -1;
        }
    }
}
