using GraphucsDLL;

namespace GraphicsBase
{
    public partial class Form1 : Form
    {
        Graphics g;
        PointF p0;
        PointF p1;
        int grabbed = -1;
        public Form1()
        {
            InitializeComponent();
            p0 = new PointF(50, canvas.Height - 50);
            p1 = new PointF(canvas.Width - 50, 50);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            Pen pen = new Pen(Color.Black, 1f);
            Pen penA = new Pen(Color.LightGray, 1f);
            Pen penM = new Pen(Color.Red, 3f);
            //g.DrawLineWithMarker(Pens.Black, p0, p1 );
            //g.DrawLineDDA(Pens.Black, p0, p1 );
            //g.DrawLineDDA(Color.Yellow, Color.Blue, p0, p1 );

            //g.DrawLineMidpointV2(penM, p0.X, p0.Y, p1.X, p1.Y);
            //g.DrawLineMidpointV2(Color.Blue, Color.Yellow, p0.X, p0.Y, p1.X, p1.Y);
            g.DrawDashedLineMidpointV2(20, 10, Color.Blue, Color.Red, p0.X, p0.Y, p1.X, p1.Y);

            int x00 = 100;
            int y00 = 100;

            Pen[] ccc = new Pen[8];
            ccc[0] = new Pen(Color.Red, 3f);
            ccc[1] = new Pen(Color.Blue, 3f);
            ccc[2] = new Pen(Color.Yellow, 3f);
            ccc[3] = new Pen(Color.Green, 3f);
            ccc[4] = new Pen(Color.Magenta, 3f);
            ccc[5] = new Pen(Color.Cyan, 3f);
            ccc[6] = new Pen(Color.Brown    , 3f);
            ccc[7] = new Pen(Color.Orange, 3f);

            for (int i = 0; i <= 7; i++)
            {
                g.DrawLine(pen, x00 * i + 100, y00 + 100, x00 * i + 140, y00 + 80);
                g.DrawLine(pen, x00 * i + 100, y00 + 100, x00 * i + 120, y00 + 60);
                g.DrawLine(pen, x00 * i + 100, y00 + 100, x00 * i + 80, y00 + 60);
                g.DrawLine(pen, x00 * i + 100, y00 + 100, x00 * i + 60, y00 + 80);
                g.DrawLine(pen, x00 * i + 100, y00 + 100, x00 * i + 60, y00 + 120);
                g.DrawLine(pen, x00 * i + 100, y00 + 100, x00 * i + 80, y00 + 140);
                g.DrawLine(pen, x00 * i + 100, y00 + 100, x00 * i + 120, y00 + 140);
                g.DrawLine(pen, x00 * i + 100, y00 + 100, x00 * i + 140, y00 + 120);

                switch (i)
                {
                    case 0: { g.DrawLine(ccc[i], x00 * i + 100, y00 + 100, x00 * i + 140, y00 + 80); break; }
                    case 1: { g.DrawLine(ccc[i], x00 * i + 100, y00 + 100, x00 * i + 120, y00 + 60); break; }
                    case 2: { g.DrawLine(ccc[i], x00 * i + 100, y00 + 100, x00 * i + 80, y00 + 60); break; }
                    case 3: { g.DrawLine(ccc[i], x00 * i + 100, y00 + 100, x00 * i + 60, y00 + 80); break; }
                    case 4: { g.DrawLine(ccc[i], x00 * i + 100, y00 + 100, x00 * i + 60, y00 + 120); break; }
                    case 5: { g.DrawLine(ccc[i], x00 * i + 100, y00 + 100, x00 * i + 80, y00 + 140); break; }
                    case 6: { g.DrawLine(ccc[i], x00 * i + 100, y00 + 100, x00 * i + 120, y00 + 140); break; }
                    case 7: { g.DrawLine(ccc[i], x00 * i + 100, y00 + 100, x00 * i + 140, y00 + 120); break; }
                }
            }

            y00 += 200;
            for (int i = 0; i <= 7; i++)
            {
                g.DrawLine(penA, x00 * i + 100, y00 + 100, x00 * i + 140, y00 + 80);
                g.DrawLine(penA, x00 * i + 100, y00 + 100, x00 * i + 120, y00 + 60);
                g.DrawLine(penA, x00 * i + 100, y00 + 100, x00 * i + 80, y00 + 60);
                g.DrawLine(penA, x00 * i + 100, y00 + 100, x00 * i + 60, y00 + 80);
                g.DrawLine(penA, x00 * i + 100, y00 + 100, x00 * i + 60, y00 + 120);
                g.DrawLine(penA, x00 * i + 100, y00 + 100, x00 * i + 80, y00 + 140);
                g.DrawLine(penA, x00 * i + 100, y00 + 100, x00 * i + 120, y00 + 140);
                g.DrawLine(penA, x00 * i + 100, y00 + 100, x00 * i + 140, y00 + 120);

                switch (i)
                {
                    case 0: { g.DrawLineMidpointV2(ccc[i], x00 * i + 100, y00 + 100, x00 * i + 140, y00 + 80); break; }
                    case 1: { g.DrawLineMidpointV2(ccc[i], x00 * i + 100, y00 + 100, x00 * i + 120, y00 + 60); break; }
                    case 2: { g.DrawLineMidpointV2(ccc[i], x00 * i + 100, y00 + 100, x00 * i + 80, y00 + 60); break; }
                    case 3: { g.DrawLineMidpointV2(ccc[i], x00 * i + 100, y00 + 100, x00 * i + 60, y00 + 80); break; }
                    case 4: { g.DrawLineMidpointV2(ccc[i], x00 * i + 100, y00 + 100, x00 * i + 60, y00 + 120); break; }
                    case 5: { g.DrawLineMidpointV2(ccc[i], x00 * i + 100, y00 + 100, x00 * i + 80, y00 + 140); break; }
                    case 6: { g.DrawLineMidpointV2(ccc[i], x00 * i + 100, y00 + 100, x00 * i + 120, y00 + 140); break; }
                    case 7: { g.DrawLineMidpointV2(ccc[i], x00 * i + 100, y00 + 100, x00 * i + 140, y00 + 120); break; }
                }
            }
            //g.DrawAndFillRectangle(Pens.Black, Brushes.Yellow, 100,100,200,200);

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
