using GraphicsDLL;

namespace GraphicsBase
{
    public partial class Form1 : Form
    {
        Graphics g;
        float lambda = 3f;
        List<Vector2F> v = new List<Vector2F>();
        int grabbed = -1;

        public Form1()
        {
            InitializeComponent();

        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            for (int i = 0; i < v.Count; i++)
                g.DrawRectangle(new Pen(Color.Black, 3), v[i].x - 5, v[i].y - 5, 10, 10);
            for (int i = 0; i < v.Count - 1; i++)
                g.DrawLine(new Pen(Color.Black, 3), v[i], v[i + 1]);

            if (v.Count == 4)
            {
                g.DrawBezier3P(new Pen(Color.Blue, 7f),
                    new Bezier3P(v[0], v[1], v[2], v[3], lambda));
            }
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < v.Count; i++)
            {
                if (v[i].IsGrabbedBy(e.Location))
                {
                    grabbed = i;
                    break;
                }
            }
            if (grabbed == -1 && v.Count < 4)
            {
                v.Add(new Vector2F(e.X, e.Y));
                grabbed = v.Count - 1;
                canvas.Invalidate();
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (grabbed != -1)
            {
                v[grabbed].x = e.X;
                v[grabbed].y = e.Y;
                canvas.Invalidate();
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            grabbed = -1;
        }

        private void sbLambda_ValueChanged(object sender, EventArgs e)
        {
            lambda = sbLambda.Value / 100f;
            canvas.Invalidate();
        }

        private void cbIsClosed_CheckedChanged(object sender, EventArgs e)
        {
            canvas.Invalidate();
        }
    }
}
