using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphucsDLL.ExtensionPointF;

namespace GraphucsDLL
{
    internal class Vector2F
    {
    }
}
public class Vector2F
{
    public float x;

    public float y;

    public Vector2F(float x = 0f, float y = 0f)
    {
        this.x = x;
        this.y = y;
    }

    public static Vector2F operator +(Vector2F a, Vector2F b)
    {
        return new Vector2F(a.x + b.x, a.y + b.y);
    }

    public static Vector2F operator -(Vector2F a, Vector2F b)
    {
        return new Vector2F(a.x - b.x, a.y - b.y);
    }

    public static Vector2F operator *(Vector2F a, float lambda)
    {
        return new Vector2F(a.x * lambda, a.y * lambda);
    }

    public static Vector2F operator *(float lambda, Vector2F a)
    {
        return a * lambda;
    }

    public static implicit operator PointF(Vector2F v0)
    {
        return new PointF(v0.x, v0.y);
    }

    public bool IsGrabbedBy(PointF p)
    {
        return MathF.Abs(x - p.X) <= (float)Settings.POINT_SIZE && MathF.Abs(y - p.Y) <= (float)Settings.POINT_SIZE;
    }
}