﻿using GraphicsDLL;
using System.Drawing;
using static GraphucsDLL.ExtensionPointF;

namespace GraphucsDLL
{
    public static class ExtensionGraphics
    {
        public static void DrawPixel(this Graphics g, Pen pen, float x, float y)
        {
            g.DrawRectangle(pen, x, y, 0.5f, 0.5f);
        }
        public static void DrawPixel(this Graphics g, Color color, float x, float y)
        {
            g.DrawRectangle(new Pen(color), x, y, 0.5f, 0.5f);
        }
        public static void DrawAndFillRectangle(this Graphics g, Pen pen, Brush brush, float x, float y, float width, float heigh)
        {
            g.FillRectangle(brush, x, y, width, heigh);
            g.DrawRectangle(pen, x, y, width, heigh);
        }

        public static void DrawLineWithMarker(this Graphics g, Pen pen, PointF p0, PointF p1)
        {
            g.DrawLine(pen, p0, p1);
            g.DrawRectangle(Pens.Black, p0.X - MathF.Abs((Settings.GRAB_DISTANTE / 2)),
                                        p0.Y - MathF.Abs((Settings.GRAB_DISTANTE / 2)),
                                        Settings.GRAB_DISTANTE, Settings.GRAB_DISTANTE);
            g.DrawRectangle(Pens.Black, p1.X - MathF.Abs((Settings.GRAB_DISTANTE / 2)),
                                        p1.Y - MathF.Abs((Settings.GRAB_DISTANTE / 2)),
                                        Settings.GRAB_DISTANTE, Settings.GRAB_DISTANTE);
        }

        public static void DrawLineDDA(this Graphics g, Pen pen, PointF p0, PointF p1)
        {
            float dx = p1.X - p0.X;
            float dy = p1.Y - p0.Y;
            float lenght = MathF.Abs(dx);
            if (MathF.Abs(dy) > lenght) lenght = MathF.Abs(dy);

            float x = p0.X;
            float y = p0.Y;

            float nx = dx / lenght;
            float ny = dy / lenght;
            g.DrawPixel(Pens.Black, x, y);
            for (int i = 0; i < lenght; i++)
            {
                x += nx;
                y += ny;
                g.DrawPixel(Pens.Black, x, y);
            }
        }
        public static void DrawLineDDA(this Graphics g, Color c0, Color c1, PointF p0, PointF p1)
        {
            float dx = p1.X - p0.X;
            float dy = p1.Y - p0.Y;
            float lenght = MathF.Abs(dx);
            if (MathF.Abs(dy) > lenght) lenght = MathF.Abs(dy);

            float R = c0.R;
            float G = c0.G;
            float B = c0.B;

            float dR = c1.R - c0.R;
            float dG = c1.G - c0.G;
            float dB = c1.B - c0.B;

            float nR = dR / lenght;
            float nG = dG / lenght;
            float nB = dB / lenght;

            float x = p0.X;
            float y = p0.Y;

            float nx = dx / lenght;
            float ny = dy / lenght;
            g.DrawPixel(Color.FromArgb((int)R, (int)G, (int)B), x, y);
            for (int i = 0; i < lenght; i++)
            {
                x += nx;
                y += ny;

                R += nR;
                G += nG;
                B += nB;
                g.DrawPixel(Color.FromArgb((int)R, (int)G, (int)B), x, y);
            }
        }
        public delegate float RtoR(float x);
        public static void DrawParametricCurve(this Graphics g, Pen pen, RtoR x, RtoR y, float a, float b, int n = 500)
        {
            float t = a;
            float h = (b - a) / n;
            PointF p0 = new PointF(x(t), y(t));
            PointF p1;
            while (t < b)
            {
                t += h;
                p1 = new PointF(x(t), y(t));
                g.DrawLine(pen, p0, p1);
                p0 = p1;
            }
        }
        public static void DrawParametricCurve(this Graphics g, Color c0, Color c1, RtoR x, RtoR y, float a, float b, int n = 500, int penWidth = 1)
        {
            float t = a;
            float h = (b - a) / n;

            float R = c0.R;
            float G = c0.G;
            float B = c0.B;

            float dR = c1.R - c0.R;
            float dG = c1.G - c0.G;
            float dB = c1.B - c0.B;

            float nR = dR / n;
            float nG = dG / n;
            float nB = dB / n;

            PointF p0 = new PointF(x(t), y(t));
            PointF p1;
            Pen pen;
            while (t < b)
            {
                t += h;
                p1 = new PointF(x(t), y(t));

                pen = new Pen(Color.FromArgb((int)R, (int)G, (int)B), penWidth);
                g.DrawLine(pen, p0, p1);

                R += nR;
                G += nG;
                B += nB;
                
                p0 = p1;
            }
        }
        public static void DrawHermiteArc(this Graphics g, Pen pen, Hermite arc)
        {
            g.DrawParametricCurve(pen,
                t => Hermite.H0(t) * arc.p0.x + Hermite.H1(t) * arc.p1.x +
                     Hermite.H2(t) * arc.t0.x + Hermite.H3(t) * arc.t1.x,
                t => Hermite.H0(t) * arc.p0.y + Hermite.H1(t) * arc.p1.y +
                     Hermite.H2(t) * arc.t0.y + Hermite.H3(t) * arc.t1.y,
                0f, 1f);
        }
        public static void DrawHermiteArc(this Graphics g, Color c0, Color c1, Hermite arc, int penWidth = 1)
        {
            g.DrawParametricCurve(c0, c1,
                t => Hermite.H0(t) * arc.p0.x + Hermite.H1(t) * arc.p1.x +
                     Hermite.H2(t) * arc.t0.x + Hermite.H3(t) * arc.t1.x,
                t => Hermite.H0(t) * arc.p0.y + Hermite.H1(t) * arc.p1.y +
                     Hermite.H2(t) * arc.t0.y + Hermite.H3(t) * arc.t1.y,
                0f, 1f, 500, penWidth);
        }
        public static void DrawBezier3(this Graphics g, Pen pen, Bezier3F curve, int penWidth = 1)
        {
            g.DrawParametricCurve(pen,
                t => Bezier3F.B0(t) * curve.p0.x + Bezier3F.B1(t) * curve.p1.x +
                     Bezier3F.B2(t) * curve.p2.x + Bezier3F.B3(t) * curve.p3.x,
                t => Bezier3F.B0(t) * curve.p0.y + Bezier3F.B1(t) * curve.p1.y +
                     Bezier3F.B2(t) * curve.p2.y + Bezier3F.B3(t) * curve.p3.y,
                0f, 1f);
        }
        public static void DrawBezier3(this Graphics g, Color c0, Color c1, Bezier3F curve, int penWidth = 1)
        {
            g.DrawParametricCurve(c0, c1,
                t => Bezier3F.B0(t) * curve.p0.x + Bezier3F.B1(t) * curve.p1.x +
                     Bezier3F.B2(t) * curve.p2.x + Bezier3F.B3(t) * curve.p3.x,
                t => Bezier3F.B0(t) * curve.p0.y + Bezier3F.B1(t) * curve.p1.y +
                     Bezier3F.B2(t) * curve.p2.y + Bezier3F.B3(t) * curve.p3.y,
                0f, 1f, 500, penWidth);
        }

        // Home work
        public static void DrawLineMidpointV2(this Graphics g, Pen pen, float x0, float y0, float x1, float y1)
        {

        }
        // Home work
        public static void DrawLineMidpointV2(this Graphics g, Color c0, Color c1, float x0, float y0, float x1, float y1)
        {

        }
        // Project
        public static void DrawDashedLineMidpointV2(this Graphics g, int dashLenght, int spaceLenght, Color c0, Color c1, float x0, float y0, float x1, float y1)
        {

        }
        // Home work
        public static void DrawMidPointCycle(this Graphics g, Pen pen, float x, float y, float r)
        {

        }
        // Project
        // a szinatmenet felulrol lefele kell legyen mindket oldalan a kornek (tukrozve)
        public static void DrawMidPointCycle(this Graphics g, Color c0, Color c1, float x, float y, float r)
        {

        }

        // this kulcsszo:
        // osztalyon belul onmagamra hivatkozok
        // konstruktort hivok vele a sajat osztalyban
        // indexelot irok
        // kiterjeszto metodust irok

        //public const byte INSIDE = 0;
        //public const byte TOP = 1;
        //public const byte RIGHT = 2;
        //public const byte BOTTOM = 4;
        //public const byte LEFT = 8;

        public static void DrawLineCS(this Graphics g, Pen pen, PointF p0, PointF p1, RectangleF win)
        {
            byte code0 = p0.OutCode(win);
            byte code1 = p1.OutCode(win);
            bool accept = false;
            while (true)
            {
                if ((code0 | code1) == INSIDE)
                {
                    accept = true;
                    break;
                }
                else if ((code0 & code1) != INSIDE)
                {
                    break;
                }
                else
                {
                    byte code = code0 != INSIDE ? code0 : code1;

                    float x = 0, y = 0;
                    if ((code & LEFT) != 0)
                    {
                        x = win.Left;
                        y = p0.Y + (p1.Y - p0.Y) * (win.Left - p0.X) / (p1.X - p0.X);
                    }
                    else if ((code & RIGHT) != 0)
                    {
                        x = win.Right;
                        y = p0.Y + (p1.Y - p0.Y) * (win.Right - p0.X) / (p1.X - p0.X);
                    }
                    else if ((code & TOP) != 0)
                    {
                        x = p0.X + (p1.X - p0.X) * (win.Top - p0.Y) / (p1.Y - p0.Y);
                        y = win.Top;
                    }
                    else if ((code & BOTTOM) != 0)
                    {
                        x = p0.X + (p1.X - p0.X) * (win.Bottom - p0.Y) / (p1.Y - p0.Y);
                        y = win.Bottom;
                    }
                    else break;

                    if (code == code0)
                    {
                        p0.X = x;
                        p0.Y = y;
                        code0 = p0.OutCode(win);
                    }
                    else
                    {
                        p1.X = x;
                        p1.Y = y;
                        code1 = p1.OutCode(win);
                    }
                }
            }
            if (accept)
                g.DrawLine(pen, p0, p1);
        }

        // Project
        public static void DrawLineCS3(this Graphics g, Pen pen, Vector3F p0, Vector3F p1, CuboidF win)
        {

        }

        // Project
        public static void DrawLIneByClipToConvexPolygone(this Graphics g, Pen pen, Point p0, Point p1, PointF[] win)
        {

        }
        // Home work
        public static void DrawLIneByClipToConcavePolygone(this Graphics g, Pen pen, Point p0, Point p1, PointF[] win)
        {

        }
        // Project
        public static void DrawLIneByClipToConcavePolygone(this Graphics g, Pen pen, Point p0, Point p1, PointF[][] win)
        {

        }
        // Home work
        public static void DrawHermiteCurve(this Graphics g, Pen pen, List<Point> p, List<Point> t, bool closed = false)
        {

        }

        //public static void DrawBezier3FP(this Graphics g, Pen pen, Bezier3FP curve)
        //{
        //    throw new ProjectException();
        //}

        public static void DrawBezierFN(this Graphics g, Pen pen, BezierFN curve)
        {
           // throw new ProjectException();
        }

        public static void DrawBSpline(this Graphics g, Pen pen, BSpline curve)
        {
            //throw new ProjectException();
        }

    }
}