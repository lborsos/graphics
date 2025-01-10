using GraphucsDLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GraphicsBase
{
    public partial class Form1 : Form
    {
        Graphics g;
        List<PointF> startPoligon = new List<PointF>()
        {
            new PointF(100, 100),
            new PointF(200, 200),
            new PointF(250, 150),
            new PointF(300, 250),
            new PointF(450, 300),
            new PointF(250, 400),
            new PointF(150, 250)
        };

        List<List<PointF>> poligonok = new List<List<PointF>>();
        const bool close_poligon = true;
        const int generation = 10;
        int pointSize = 5;

        public Form1()
        {
            InitializeComponent();
            GenerateGenerations();
        }

        static PointF GetInterpolatedPoint(PointF start, PointF end, float fraction)
        {
            float x = start.X + (end.X - start.X) * fraction;
            float y = start.Y + (end.Y - start.Y) * fraction;
            return new PointF(x, y);
        }

        void GenerateGenerations()
        {
            poligonok.Add(new List<PointF>(startPoligon));

            for (int gen = 0; gen < generation; gen++)
            {
                List<PointF> currentPoligon = poligonok[gen];
                List<PointF> nextGeneration = new List<PointF>();

                for (int i = 0; i < currentPoligon.Count - (close_poligon ? 0 : 1); i++)
                {
                    PointF start = currentPoligon[i];
                    PointF end = currentPoligon[(i + 1) % currentPoligon.Count];

                    PointF quarterPoint = GetInterpolatedPoint(start, end, 0.25f);
                    PointF threeQuarterPoint = GetInterpolatedPoint(start, end, 0.75f);

                    //nextGeneration.Add(start);
                    nextGeneration.Add(quarterPoint);
                    nextGeneration.Add(threeQuarterPoint);
                }

                poligonok.Add(nextGeneration);
            }
        }
        void DrawGen(List<PointF> gen, Color color, bool sq = false)
        {
            Pen pen = new Pen(color);

            for (int i = 0; i < gen.Count; i++)
            {
                if (sq)
                {
                    g.DrawPixel(color, gen[i].X-pointSize/2, gen[i].Y-pointSize/2, pointSize);
                }
                if (i < gen.Count - 1)
                {
                    g.DrawLine(pen, gen[i], gen[i + 1]);
                }
            }
            if (close_poligon)
            {
                g.DrawLine(pen, gen[0], gen[gen.Count - 1]);
            }
            ;
        }


        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            DrawGen(startPoligon, Color.Gray, true);
            DrawGen(poligonok[2], Color.Red, true);
        }


        private void canvas_MouseDown(object sender, MouseEventArgs e) { }
        private void canvas_MouseMove(object sender, MouseEventArgs e) { }
        private void canvas_MouseUp(object sender, MouseEventArgs e) { }
    }
}
