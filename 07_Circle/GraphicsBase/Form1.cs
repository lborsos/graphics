using GraphucsDLL;

namespace GraphicsBase
{
    public partial class Form1 : Form
    {
        Graphics g;
        public Form1()
        {
            InitializeComponent();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            Pen pen = new Pen(Brushes.Blue, 1);
<<<<<<< HEAD
            g.DrawMidPointCircle(pen, 300, 300, 250);
            //g.DrawMidPointCycleNotCompleted(Color.White, Color.Yellow, 300, 300, 250);
=======
            //g.DrawMidPointCircle(pen, 300, 300, 250);
           
            g.DrawMidPointCycleNotCompleted(Color.Yellow, Color.Blue, 300 + 200, 300, 250);
            g.DrawMidPointCycle(Color.Yellow, Color.Blue, 300, 300, 250);
>>>>>>> acea041b5b0691c41a1a4187430d4445e68a0378

            g.DrawLineMidpointV2(Color.Blue, Color.Yellow, 400, 50, 700, 550);
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
