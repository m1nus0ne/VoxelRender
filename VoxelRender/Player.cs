namespace VoxelRender;

public class Player
{
    public (float x, float y, float z) Pos;
    public (double oxy, double oyz) Angles;

    public Player()
    {
        Pos = (10f, 10f, 10f);
        Angles = (0, 0);
    }

}