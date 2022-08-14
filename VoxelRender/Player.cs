using System.Numerics;

namespace VoxelRender;

public class Player
{
    public Vector3 Pos;
    public Vector2 Angles;

    public Player()
    {
        Pos = new Vector3(0, 0, 270);
        Angles = new Vector2(MathF.PI/2, MathF.PI/9);
    }

    public Player(Vector3 pos, Vector2 angles)
    {
        Pos = pos;
        Angles = angles;
    }
}