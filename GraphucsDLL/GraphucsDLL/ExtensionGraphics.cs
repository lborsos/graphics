﻿using GraphicsDLL;
using System.Drawing;
using static GraphucsDLL.ExtensionPointF;

namespace GraphucsDLL
{
    public static class ExtensionGraphics
    {
        public static void DrawPixel(this Graphics g, Pen pen, float x, float y, float size = 1f)
        {
            g.DrawRectangle(pen, x, y, size == 1 ? 0.5f : size, size == 1 ? 0.5f : size);
        }
        public static void DrawPixel(this Graphics g, Color color, float x, float y, float size = 1f)
        {
            g.DrawRectangle(new Pen(color), x, y, size == 1 ? 0.5f : size, size == 1 ? 0.5f : size);
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

        public static void DrawBezier3P(this Graphics g, Pen pen, Bezier3P b, int n = 500)
        {
            g.DrawParametricCurve(pen,
                t => Bezier3P.Basis.B0(t, b.p) * b.p0.x +
                     Bezier3P.Basis.B1(t, b.p) * b.p1.x +
                     Bezier3P.Basis.B2(t, b.p) * b.p2.x +
                     Bezier3P.Basis.B3(t, b.p) * b.p3.x,
                t => Bezier3P.Basis.B0(t, b.p) * b.p0.y +
                     Bezier3P.Basis.B1(t, b.p) * b.p1.y +
                     Bezier3P.Basis.B2(t, b.p) * b.p2.y +
                     Bezier3P.Basis.B3(t, b.p) * b.p3.y,
                0f, 1f, n);
        }
       


        public static void DrawLineMidpointV1(this Graphics g, Pen pen, float x0, float y0, float x1, float y1)
        {
            float d, x, y;
            float dx = x1 - x0;
            float dy = y1 - y0;
            d = 2 * dy - dx;
            x = x0;
            y = y0;
            for (int i = 1; i <= dx; i++)
            {
                g.DrawPixel(pen.Color, x, y);
                if (d > 0)
                {
                    y++;
                    d = d + 2 * (dy - dx);
                }
                else
                {
                    d = d + 2 * dy;
                }
                x++;
            }
        }

        // Home work
        public static void DrawLineMidpointV2(this Graphics g, Pen pen, float x0, float y0, float x1, float y1)
        {
            float dx = MathF.Abs(x1 - x0);
            float dy = MathF.Abs(y1 - y0);
            float sx = x0 < x1 ? 1 : -1;
            float sy = y0 < y1 ? 1 : -1;
            bool steep = dy > dx;

            if (steep) // ha meredek
            {
                (x0, y0) = (y0, x0); // tuple... C# 7.0 tol jelent meg (ket valtozo ertekenek felcserelese)
                (x1, y1) = (y1, x1);

                (dx, dy) = (dy, dx);
                (sx, sy) = (sy, sx);
            }

            float d = 2 * dy - dx;
            float x = x0;
            float y = y0;

            for (int i = 0; i <= dx; i++)
            {
                if (steep)
                {
                    g.DrawPixel(pen.Color, y, x);
                }
                else
                {
                    g.DrawPixel(pen.Color, x, y);
                }

                if (d > 0)
                {
                    y += sy;
                    d -= 2 * dx;
                }
                d += 2 * dy;
                x += sx;
            }
        }


        // Home work
        public static void DrawLineMidpointV2(this Graphics g, Color c0, Color c1, float x0, float y0, float x1, float y1)
        {
            float dx = MathF.Abs(x1 - x0);
            float dy = MathF.Abs(y1 - y0);
            float sx = x0 < x1 ? 1 : -1;
            float sy = y0 < y1 ? 1 : -1;
            bool steep = dy > dx;


            if (steep) // ha meredek
            {
                (x0, y0) = (y0, x0); // tuple... C# 7.0 tol jelent meg (ket valtozo ertekenek felcserelese)
                (x1, y1) = (y1, x1);

                (dx, dy) = (dy, dx);
                (sx, sy) = (sy, sx);
            }

            float R = c0.R;
            float G = c0.G;
            float B = c0.B;

            float dR = c1.R - c0.R;
            float dG = c1.G - c0.G;
            float dB = c1.B - c0.B;

            float nR = dR / dx;
            float nG = dG / dx;
            float nB = dB / dx;

            float d = 2 * dy - dx;
            float x = x0;
            float y = y0;

            Pen pen;
            for (int i = 0; i <= dx; i++)
            {
                pen = new Pen(Color.FromArgb((int)R, (int)G, (int)B));

                if (steep)
                {
                    g.DrawPixel(pen, y, x);
                }
                else
                {
                    g.DrawPixel(pen, x, y);
                }

                if (d > 0)
                {
                    y += sy;
                    d -= 2 * dx;
                }
                d += 2 * dy;
                x += sx;

                R += nR;
                G += nG;
                B += nB;
            }
        }
        // Project
        public static void DrawDashedLineMidpointV2(this Graphics g, int dashLenght, int spaceLenght, Color c0, Color c1, float x0, float y0, float x1, float y1)
        {
            float dx = MathF.Abs(x1 - x0);
            float dy = MathF.Abs(y1 - y0);
            float sx = x0 < x1 ? 1 : -1;
            float sy = y0 < y1 ? 1 : -1;
            bool steep = dy > dx;


            if (steep) // ha meredek
            {
                (x0, y0) = (y0, x0); // tuple... C# 7.0 tol jelent meg (ket valtozo ertekenek felcserelese)
                (x1, y1) = (y1, x1);

                (dx, dy) = (dy, dx);
                (sx, sy) = (sy, sx);
            }

            float R = c0.R;
            float G = c0.G;
            float B = c0.B;

            float dR = c1.R - c0.R;
            float dG = c1.G - c0.G;
            float dB = c1.B - c0.B;

            float nR = dR / dx;
            float nG = dG / dx;
            float nB = dB / dx;

            float d = 2 * dy - dx;
            float x = x0;
            float y = y0;

            Pen pen;

            int dashLeft = dashLenght;
            Dash dash = Dash.dash;
            for (int i = 0; i <= dx; i++)
            {
                pen = new Pen(Color.FromArgb((int)R, (int)G, (int)B));

                if (dash == Dash.dash)
                {
                    if (steep)
                    {
                        g.DrawPixel(pen, y, x);
                    }
                    else
                    {
                        g.DrawPixel(pen, x, y);
                    }
                }
                if (d > 0)
                {
                    y += sy;
                    d -= 2 * dx;
                }
                d += 2 * dy;
                x += sx;

                R += nR;
                G += nG;
                B += nB;
                dashLeft--;
                if (dashLeft == 0)
                {
                    switch (dash)
                    {
                        case Dash.dash:  { dash = Dash.space; dashLeft = spaceLenght; break; }
                        case Dash.space: { dash = Dash.dash; dashLeft = dashLenght; break; }
                    }
                }
            }
        }
        // Home work
        public static void DrawMidPointCircle(this Graphics g, Pen pen, float cx, float cy, float r)
        {
            float x = 0;
            float y = r;
            float h = 1 - r;

            DrawCirclePoints(g, pen, cx, cy, x, y);

            while (y > x)
            {
                if (h < 0)
                {
                    h += 2 * x + 3;
                }
                else
                {
                    h += 2 * (x - y) + 5;
                    y--;
                }
                x++;
                DrawCirclePoints(g, pen, cx, cy, x, y); 
            }
        }

        private static void DrawCirclePoints(Graphics g, Pen pen, float cx, float cy, float x, float y)
        {
            g.DrawPixel(pen, cx + x, cy + y);
            g.DrawPixel(pen, cx - x, cy + y);
            g.DrawPixel(pen, cx + x, cy - y);
            g.DrawPixel(pen, cx - x, cy - y);
            g.DrawPixel(pen, cx + y, cy + x);
            g.DrawPixel(pen, cx - y, cy + x);
            g.DrawPixel(pen, cx + y, cy - x);
            g.DrawPixel(pen, cx - y, cy - x);
        }

        // Project
        // a szinatmenet felulrol lefele kell legyen mindket oldalan a kornek (tukrozve)

        public static void DrawMidPointCycleNotCompleted(this Graphics g, Color c0, Color c1, float cx, float cy, float r)
        {
            float x = 0;
            float y = r;
            float h = 1 - r;
            float R = c0.R;
            float G = c0.G;
            float B = c0.B;
            // temp variables for calc
            float tR = c0.R;
            float tG = c0.G;
            float tB = c0.B;
            float dR = c1.R - c0.R;
            float dG = c1.G - c0.G;
            float dB = c1.B - c0.B;
            float distance = (float)Math.PI * r;
            float nR = dR / (distance * 4);
            float nG = dG / (distance * 4);
            float nB = dB / (distance * 4);
            // sektoronkenti tavolsag
            float sR = dR / 4;
            float sG = dG / 4;
            float sB = dB / 4;
            int index = 0;
            Pen pen = new Pen(Color.FromArgb((int)R, (int)G, (int)B));
            g.DrawPixel(pen, cx + x, cy + y); // 1
            g.DrawPixel(pen, cx - x, cy + y); // 2
            g.DrawPixel(pen, cx + x, cy - y); // 3
            g.DrawPixel(pen, cx - x, cy - y); // 4
            g.DrawPixel(pen, cx + y, cy + x); // 5
            g.DrawPixel(pen, cx - y, cy + x); // 6
            g.DrawPixel(pen, cx + y, cy - x); // 7
            g.DrawPixel(pen, cx - y, cy - x); // 8
            while (y > x)
            {
                if (h < 0)
                {
                    h += 2 * x + 3;
                }
                else
                {
                    h += 2 * (x - y) + 5;
                    y--;
                }
                x++;
                //pen = new Pen(Color.FromArgb((int)R, (int)G, (int)B));
                calcColor(index, 3, true);
                g.DrawPixel(pen, cx + x, cy + y); // 1
                g.DrawPixel(pen, cx - x, cy + y); // 2
                calcColor(index, 0, false);
                g.DrawPixel(pen, cx + x, cy - y); // 3
                g.DrawPixel(pen, cx - x, cy - y); // 4
                calcColor(index, 2, false);
                g.DrawPixel(pen, cx + y, cy + x); // 5
                g.DrawPixel(pen, cx - y, cy + x); // 6
                calcColor(index, 1, true);
                g.DrawPixel(pen, cx + y, cy - x); // 7
                g.DrawPixel(pen, cx - y, cy - x); // 8
                //R += nR;
                //G += nG;
                //B += nB;
                index++;
            }
            void calcColor(float index, int sector, bool reverse)
            {
//                tR = MathF.Min(255, (MathF.Max(0, sR * sector + (reverse ? (sR - index) * nR : index * nR))));
//                tG = MathF.Min(255, (MathF.Max(0, sG * sector + (reverse ? (sG - index) * nG : index * nG))));
//                tB = MathF.Min(255, (MathF.Max(0, sB * sector + (reverse ? (sB - index) * nB : index * nB))));
                tR = R + sR * sector + (reverse ? (sR - index) * nR : index * nR);
                tG = G + sG * sector + (reverse ? (sG - index) * nG : index * nG);
                tB = B + sB * sector + (reverse ? (sB - index) * nB : index * nB);



                pen = new Pen(Color.FromArgb((int)tR, (int)tG, (int)tB));
            }
        }
        // Project
        // a szinatmenet felulrol lefele kell legyen mindket oldalan a kornek (tukrozve)

        public static void DrawMidPointCycle(this Graphics g, Color c0, Color c1, float cx, float cy, float r)
        {
            float x = 0;
            float y = r;
            float h = 1 - r;

            float totalDistance = 2 * r;

            float dR = c1.R - c0.R;
            float dG = c1.G - c0.G;
            float dB = c1.B - c0.B;

            Pen pen;
            void calculateColor(float currentY)
            {
                float progress = (currentY + r) / totalDistance;
                progress = Math.Max(0, Math.Min(1, progress)); 

                int R = (int)(c0.R + (dR * progress));
                int G = (int)(c0.G + (dG * progress));
                int B = (int)(c0.B + (dB * progress));

                pen = new Pen(Color.FromArgb(R, G, B));
            }

            calculateColor(y);

            // Kezdő pixelek rajzolása
            g.DrawPixel(pen, cx + x, cy + y);
            g.DrawPixel(pen, cx - x, cy + y);
            calculateColor(-y);
            g.DrawPixel(pen, cx + x, cy - y);
            g.DrawPixel(pen, cx - x, cy - y);
            calculateColor(x);
            g.DrawPixel(pen, cx + y, cy + x);
            g.DrawPixel(pen, cx - y, cy + x);
            calculateColor(-x);
            g.DrawPixel(pen, cx + y, cy - x);
            g.DrawPixel(pen, cx - y, cy - x);

            while (y > x)
            {
                if (h < 0)
                {
                    h += 2 * x + 3;
                }
                else
                {
                    h += 2 * (x - y) + 5;
                    y--;
                }
                x++;

                calculateColor(y);
                g.DrawPixel(pen, cx + x, cy + y);
                g.DrawPixel(pen, cx - x, cy + y);

                calculateColor(-y);
                g.DrawPixel(pen, cx + x, cy - y);
                g.DrawPixel(pen, cx - x, cy - y);

                calculateColor(x);
                g.DrawPixel(pen, cx + y, cy + x);
                g.DrawPixel(pen, cx - y, cy + x);

                calculateColor(-x);
                g.DrawPixel(pen, cx + y, cy - x);
                g.DrawPixel(pen, cx - y, cy - x);
            }
        }


        //Projekt
        public static void DrawCircleColorSpectrum(this Graphics g, float cx, float cy, float r)
        {
            float x = 0;
            float y = r;
            float h = 1 - r;

            void DrawColorPixel(float px, float py)
            {
                // Szög számítás és konvertálás HSV-be (0-360)
                float angle = (float)(Math.Atan2(py - cy, px - cx) * 180 / Math.PI + 180);

                // HSV-ből közvetlen RGB konverzió
                float sector = angle / 60;
                int i = (int)Math.Floor(sector);
                float f = sector - i;

                byte v = 255;
                byte p = 0;
                byte q = (byte)(255 * (1 - f));
                byte t = (byte)(255 * f);

                Color color = i switch
                {
                    0 => Color.FromArgb(v, t, p),
                    1 => Color.FromArgb(q, v, p),
                    2 => Color.FromArgb(p, v, t),
                    3 => Color.FromArgb(p, q, v),
                    4 => Color.FromArgb(t, p, v),
                    _ => Color.FromArgb(v, p, q)
                };

                g.DrawLineDDA(Color.White, color, new PointF(cx, cy), new PointF(px, py));
            }

            // A kör rajzolása változatlan marad
            DrawColorPixel(cx + x, cy + y);
            DrawColorPixel(cx - x, cy + y);
            DrawColorPixel(cx + x, cy - y);
            DrawColorPixel(cx - x, cy - y);
            DrawColorPixel(cx + y, cy + x);
            DrawColorPixel(cx - y, cy + x);
            DrawColorPixel(cx + y, cy - x);
            DrawColorPixel(cx - y, cy - x);

            while (y > x)
            {
                if (h < 0)
                    h += 2 * x + 3;
                else
                {
                    h += 2 * (x - y) + 5;
                    y--;
                }
                x++;
                DrawColorPixel(cx + x, cy + y);
                DrawColorPixel(cx - x, cy + y);
                DrawColorPixel(cx + x, cy - y);
                DrawColorPixel(cx - x, cy - y);
                DrawColorPixel(cx + y, cy + x);
                DrawColorPixel(cx - y, cy + x);
                DrawColorPixel(cx + y, cy - x);
                DrawColorPixel(cx - y, cy - x);
            }
        }

        public static void DrawCircleColorSpectrum2(this Graphics g, float cx, float cy, float r)
        {
            float x = 0;
            float y = r;
            float h = 1 - r;

            void DrawColorPixel(float px, float py)
            {
                // Számoljuk ki a szöget az adott pixelhez (most 0 foktól kezdve)
                float angle = (float)Math.Atan2(py - cy, px - cx);
                // Konvertáljuk 0-360 fokra, de most 90 fokkal eltoljuk, hogy a megfelelő helyen kezdődjön
                float degrees = (float)(angle * 180 / Math.PI + 90);
                if (degrees < 0) degrees += 360;

                int R, G, B;

                // 6 egyenlő részre osztjuk a 360 fokot (minden szín 60 fok)
                degrees = degrees % 360;
                float normalized = degrees / 60f;
                int section = (int)normalized;
                float remainder = normalized - section;

                switch (section)
                {
                    case 0: // Ciánkék -> Zöld
                        R = 0;
                        G = 255;
                        B = (int)(255 * (1 - remainder));
                        break;
                    case 1: // Zöld -> Sárga
                        R = (int)(255 * remainder);
                        G = 255;
                        B = 0;
                        break;
                    case 2: // Sárga -> Piros
                        R = 255;
                        G = (int)(255 * (1 - remainder));
                        B = 0;
                        break;
                    case 3: // Piros -> Lila
                        R = 255;
                        G = 0;
                        B = (int)(255 * remainder);
                        break;
                    case 4: // Lila -> Kék
                        R = (int)(255 * (1 - remainder));
                        G = 0;
                        B = 255;
                        break;
                    default: // Kék -> Ciánkék
                        R = 0;
                        G = (int)(255 * remainder);
                        B = 255;
                        break;
                }

                g.DrawLineDDA(Color.White, Color.FromArgb(R, G, B), new PointF(cx, cy), new PointF(px, py));
            }

            DrawColorPixel(cx + x, cy + y);
            DrawColorPixel(cx - x, cy + y);
            DrawColorPixel(cx + x, cy - y);
            DrawColorPixel(cx - x, cy - y);
            DrawColorPixel(cx + y, cy + x);
            DrawColorPixel(cx - y, cy + x);
            DrawColorPixel(cx + y, cy - x);
            DrawColorPixel(cx - y, cy - x);

            while (y > x)
            {
                if (h < 0)
                {
                    h += 2 * x + 3;
                }
                else
                {
                    h += 2 * (x - y) + 5;
                    y--;
                }
                x++;

                DrawColorPixel(cx + x, cy + y);
                DrawColorPixel(cx - x, cy + y);
                DrawColorPixel(cx + x, cy - y);
                DrawColorPixel(cx - x, cy - y);
                DrawColorPixel(cx + y, cy + x);
                DrawColorPixel(cx - y, cy + x);
                DrawColorPixel(cx + y, cy - x);
                DrawColorPixel(cx - y, cy - x);
            }
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

        // 3D
        #region 3D graphics
        private static Matrix4F projection  = Matrix4F.Projection.Parallel(0, 0, -1);
        private static Matrix4F transformation;
        public static Vector4F centerOfCanvas = new Vector4F(0, 0, 0);
        private static Vector4F O = new Vector4F(0, 0, 0);
        private static Vector4F E1 = new Vector4F(1, 0, 0);
        private static Vector4F E2 = new Vector4F(0, 1, 0);
        private static Vector4F E3 = new Vector4F(0, 0, 1);
        private static Vector4F camera = new Vector4F(0, 0, 0);

        public static Vector4F o { get { return Projection.Project(Transformation.Transform(O)); } }
        public static Vector4F e1 { get { return Projection.Project(Transformation.Transform(E1)); } }
        public static Vector4F e2 { get { return Projection.Project(Transformation.Transform(E2)); } }
        public static Vector4F e3 { get { return Projection.Project(Transformation.Transform(E3)); } }

        static ExtensionGraphics()
        {
            transformation = new Matrix4F();
            transformation.LoadIdentity();
        }
        public static void SetProiection(Matrix4F p)
        {
            ExtensionGraphics.projection = p;
        }

        public static void LoadIdentity()
        {
            ExtensionGraphics.transformation.LoadIdentity();
        }
        public static void PushTransformation(Matrix4F p)
        {
            ExtensionGraphics.transformation = p * ExtensionGraphics.transformation;
        }
        public static class Projection
        {
            public static void SetParallel(float vx, float vy, float vz)
            {
                projection = Matrix4F.Projection.Parallel(vx, vy, vz);
                camera = -1000000 * new Vector4F(vx, vy, vz);
            }
            public static void SetCentral(float s)
            {
                projection = Matrix4F.Projection.Central(s);
                camera = new Vector4F(0, 0, s);
            }
            public static Vector4F Project(Vector4F v)
            {
                Vector4F res = projection * v;
                res.ToCartesian();
                res += centerOfCanvas;
                return res;
            }
        }

        public static class Transformation
        {
            public static void Push(Matrix4F trans)
            {
                transformation = trans * transformation;
            }
            public static void LoadIdentity()
            {
                transformation.LoadIdentity();
            }
            public static Vector4F Transform(Vector4F v)
            {
                return transformation * v;
            }
        }

        public static void DrawParametricCurve3D(this Graphics g, Pen pen,
            Func<float, float> x, Func<float, float> y, Func<float, float> z,
            float a, float b, int n = 500)
        {
            if (b <= a)
                throw new Exception("Wrong interval!");

            float h = (b - a) / n;
            float t = a;
            Vector4F v0 = new Vector4F(x(t), y(t), z(t));
            Vector4F pv0 = Projection.Project(Transformation.Transform(v0));
            while (t < b)
            {
                t += h;
                Vector4F v1 = new Vector4F(x(t), y(t), z(t));
                Vector4F pv1 = Projection.Project(Transformation.Transform(v1));
                g.DrawLine(pen, pv0, pv1);
                pv0 = pv1;
            }
        }


        //Proiect
        public static void DrawHermiteCurve3D(this Graphics g, Pen pen, List<Vector4F> v, int n = 500)
        {

        }


        public delegate float RRtoR(float x, float y);
        public static void DrawParametricSurface(this Graphics g, Pen pen,
            RRtoR x, RRtoR y, RRtoR z,
            float a, float b,
            float c, float d,
            int du = 15, int dv = 15,
            int nu = 500, int nv = 500)
        {
            float v = c;
            float hv = (d - c) / dv;
            while (v < d)
            {
                g.DrawParametricCurve3D(pen,
                    _u => x(_u, v),
                    _u => y(_u, v),
                    _u => z(_u, v),
                    a, b, nu);
                v += hv;
            }

            float u = a;
            float hu = (b - a) / du;
            while (u < b)
            {
                g.DrawParametricCurve3D(pen,
                    _v => x(u, _v),
                    _v => y(u, _v),
                    _v => z(u, _v),
                    a, b, nu);
                u += hu;
            }
        }
        public static void DisplayBRep(this Graphics g, Pen pen, ModelBRep model, bool backfaceCulling = true)
        {
            foreach (Triangle triangle in model.triangles.OrderBy(t => t.WeightZ))
            {
                Vector4F a = Transformation.Transform(triangle.a);
                Vector4F b = Transformation.Transform(triangle.b);
                Vector4F c = Transformation.Transform(triangle.c);

                Triangle tri = new Triangle(a, b, c);
                if (!backfaceCulling || tri.IsVisible(camera))
                {
                    a = Projection.Project(a);
                    b = Projection.Project(b);
                    c = Projection.Project(c);

                    g.DrawLine(pen, a, b);
                    g.DrawLine(pen, c, b);
                    g.DrawLine(pen, a, c);
                }
            }
        }
        public static void FlatShadingBRep(this Graphics g, Color color, ModelBRep model)
        {
            List<Triangle> transformed = new List<Triangle>();
            foreach (Triangle triangle in model.triangles)
            {
                Vector4F a = Transformation.Transform(triangle.a);
                Vector4F b = Transformation.Transform(triangle.b);
                Vector4F c = Transformation.Transform(triangle.c);
                transformed.Add(new Triangle(a, b, c));
            }

            foreach (Triangle triangle in transformed
                                            .Where(t => t.IsVisible(camera))
                                            .OrderBy(t => t.WeightZ))
            {
                Vector4F a = Projection.Project(triangle.a);
                Vector4F b = Projection.Project(triangle.b);
                Vector4F c = Projection.Project(triangle.c);

                float visibilityValue = triangle.VisibilityValueA(camera);
                Color col = Color.FromArgb((int)(visibilityValue * color.R),
                                           (int)(visibilityValue * color.G),
                                           (int)(visibilityValue * color.B));
                g.FillPolygon(new SolidBrush(col), new PointF[] { a, b, c });
            }
        }
        #endregion

    }
}
