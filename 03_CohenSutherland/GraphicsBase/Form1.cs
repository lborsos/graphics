using GraphucsDLL;

namespace GraphicsBase
{
    public partial class Form1 : Form
    {
        Graphics g;
        Random rnd = new Random();
        RectangleF win = new RectangleF(100, 60, 700, 460);
        Pen penWin = new Pen(Color.Black, 3f);
        Pen penLineFull = new Pen(Color.Gray, 1f);
        Pen penLineIn = new Pen(Color.Red, 2f);
        List<PointF> p = new List<PointF>();    

        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 200; i++) 
            {
                p.Add(new PointF(rnd.Next(canvas.Width), rnd.Next(canvas.Height)));
            }
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            for (int i = 0; i < p.Count -1 ; i+=2) 
            {
                g.DrawLine(penLineFull, p[i], p[i + 1]);
                g.DrawLineCS(penLineIn, p[i], p[i + 1], win);
            }
            g.DrawRectangle(penWin, win);
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
