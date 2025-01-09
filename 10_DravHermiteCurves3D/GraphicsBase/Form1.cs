using GraphicsDLL;
using GraphucsDLL;

namespace GraphicsBase
{
    public partial class Form1 : Form
    {
        Graphics g;
        Pen pen = new Pen(Color.Violet, 3);
        float r = 200;
        float m = 50;
        public Form1()
        {
            InitializeComponent();
            ExtensionGraphics.centerOfCanvas = new Vector4F(canvas.Width / 2, canvas.Height / 2, 0);
            ExtensionGraphics.SetProiection(Matrix4F.Projection.Parallel(0.4f, 0.4f, -0.6f));

        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //g.DrawLine(new Pen(Color.Red, 3), ExtensionGraphics.o, ExtensionGraphics.e1);
            //g.DrawLine(new Pen(Color.Green, 3), ExtensionGraphics.o, ExtensionGraphics.e2);
            //g.DrawLine(new Pen(Color.Blue, 3), ExtensionGraphics.o, ExtensionGraphics.e3);

            ExtensionGraphics.LoadIdentity();
            ExtensionGraphics.PushTransformation(Matrix4F.Transformations.RotX(alpha));
            ExtensionGraphics.PushTransformation(Matrix4F.Transformations.RotY(beta));
            ExtensionGraphics.PushTransformation(Matrix4F.Transformations.RotZ(gamma));

            g.DrawParametricCurve3D(pen,
                t => r * MathF.Cos(t),
                t => r * MathF.Sin(t),
                t => m * t / (2 * MathF.PI),
                -10, 10 * MathF.PI);
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {

        }
        float alpha = 0;
        float beta = 0;
        float gamma = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            alpha += 0.01f;
            beta += 0.02f;
            gamma += 0.03f;
            canvas.Invalidate();
        }
    }
}
