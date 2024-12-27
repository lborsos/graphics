using GraphucsDLL;

namespace GraphicsBase
{
    public partial class Form1 : Form
    {
        Graphics g;
        Pen pen = new Pen(Color.Red, 2);
        public Form1()
        {
            InitializeComponent();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            //g.DrawParametricCurve(pen,
            //    t => 100 * MathF.Cos(2 * t) + 200,
            //    t => 100 * MathF.Sin(3 * t) + 200,
            //    0, MathF.PI * 2, 7);
            g.DrawParametricCurve(Color.Blue, Color.Yellow,
                t => 100 * MathF.Cos(t) + 200,
                t => 100 * MathF.Sin(t) + 200,
                0, MathF.PI * 2, 100);
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
    }
}
