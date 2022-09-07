using System.Numerics;

namespace VoxelRender;

public static class KeysHandler
{
    private static Vector2 _direction;
    private static Vector2 _deltaAnlge;


    public static void OnPress(KeyEventArgs e)
    {
        // TODO: Забахать направления и высоту
        switch (e.KeyValue)
        {
            case (int) Keys.A:
                _direction.X = 1;
                break;
            case (int) Keys.D:
                _direction.X = -1;
                break;
            case (int) Keys.S:
                _direction.Y = -1;
                break;
            case (int) Keys.W:
                _direction.Y = 1;
                break;
            // case (int) Keys.Q:
            //     _direction.Z = 1;
            //     break;
            // case (int) Keys.E:
            //     _direction.Z = -1;
            //     break;
            case (int) Keys.Left:
                _deltaAnlge.X = 1;
                break;
            case (int) Keys.Right:
                _deltaAnlge.X = -1;
                break;
            case (int) Keys.Up:
                _deltaAnlge.Y = 60;
                break;
            case (int) Keys.Down:
                _deltaAnlge.Y = -60;
                break;
        }
    }

    public static void OnRelease(KeyEventArgs e)
    {
        switch (e.KeyValue)
        {
            case (int) Keys.A:
                _direction.X = 0;
                break;
            case (int) Keys.D:
                _direction.X = 0;
                break;
            case (int) Keys.S:
                _direction.Y = 0;
                break;
            case (int) Keys.W:
                _direction.Y = 0;
                break;
            // case (int) Keys.Q:
            //     _direction.Z = 0;
            //     break;
            // case (int) Keys.E:
            //     _direction.Z = 0;
            //     break;
            case (int) Keys.Left:
                _deltaAnlge.X = 0;
                break;
            case (int) Keys.Right:
                _deltaAnlge.X = 0;
                break;
            case (int) Keys.Up:
                _deltaAnlge.Y = 0;
                break;
            case (int) Keys.Down:
                _deltaAnlge.Y = 0;
                break;
        }
    }

    public static (Vector2, Vector2) GetState() => (_direction, _deltaAnlge);
}