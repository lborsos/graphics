namespace GraphicsBase
{
    public partial class Form1 : Form
    {
        Graphics g;
        
        int grabbed = -1; // -1 no grabbed, 0 move, 1, resize
        int grabbed_object_index = -1;
        List<Rect> rects = new List<Rect>();
        int index = -1; // elore definialom, hogy legyen lefoglalva a valtozo helye, azert, hogy ne az esemenykezelo foglajja es szabaditsa fel a helyet
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
                    grabbed_object_index = index;
                }
                else
                {
                    index = rects.FindIndex(x => x.IsMove(e.Location));
                    if (index >= 0)
                    {
                        grabbed = 0;
                        grabbed_object_index = index;
                        rects[index].StartMove(e.Location);
                    }
                    else
                    {
                        grabbed = 1;
                        rects.Add(new Rect(e.Location));
                        index = rects.Count - 1;
                        grabbed_object_index = index;
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
