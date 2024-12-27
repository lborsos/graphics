using GraphucsDLL;

namespace GraphicsBase
{
    public partial class Form1 : Form
    {
        Graphics g;
        PointF p0 = new PointF(50,50);
        PointF p1;
        int grabbed = -1;
        public Form1()
        {
            InitializeComponent();
            p1 = new PointF(canvas.Width - 50, canvas.Height - 50);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            //g.DrawLineWithMarker(Pens.Black, p0, p1 );
            //g.DrawLineDDA(Pens.Black, p0, p1 );
            g.DrawLineDDA(Color.Yellow, Color.Blue, p0, p1 );

            g.DrawAndFillRectangle(Pens.Black, Brushes.Yellow, 100,100,200,200);

        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (p0.IsGrabbedBy(e.Location))
                grabbed = 0;
            else if (p1.IsGrabbedBy(e.Location))
                grabbed = 1;
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (grabbed != -1)
            {
                switch (grabbed)
                {
                    case 0: p0 = e.Location; break;
                    case 1: p1= e.Location; break;
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
