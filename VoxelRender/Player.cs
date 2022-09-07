using System.Numerics;

namespace VoxelRender;

public class Player
{
    public Vector2 Pos;
    public Vector2 Angles;
    public float Height;

    public Player()
    {
        Pos = new Vector2(0, 0);
        Angles = new Vector2(MathF.PI / 2, MathF.PI / 9);
        Height = 270;
    }

    

    public void Update((Vector2 deltaPos, Vector2 deltaAngle) data)
    {
        Angles += data.deltaAngle * (float) Math.PI / 36;
        Pos += data.deltaPos.RotateRadians(Angles.X) ;
        // Height += data.deltaPos.Z;
        
    }
}

public static class VectorExt
{
    private const double DegToRad = Math.PI / 180;

    public static Vector2 Rotate(this Vector2 v, double degrees)
    {
        return v.RotateRadians(degrees * DegToRad);
    }

    public static Vector2 RotateRadians(this Vector2 v, double radians)
    {
        var ca = (float) Math.Cos(radians);
        var sa = (float) Math.Sin(radians);
        return new Vector2(ca * v.X - sa * v.Y, sa * v.X + ca * v.Y);
    }
}