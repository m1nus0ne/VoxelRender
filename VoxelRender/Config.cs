using System.IO;

namespace VoxelRender;

public static class Config
{
    #region GraphicSettings

    public static int WindowWidth = 300;
    public static int WindowHeight = 300;

    public static int RayDistance = 800;
    public static double FOV = Math.PI / 6;
    public static double HFOV = Math.PI / 16;
    public static double deltaAngle = HFOV / WindowWidth;

    #endregion

    #region PlayerSettings

    public static float playerSpeed = 10f;

    #endregion

    public static Bitmap textureMap = new Bitmap("textureMap.bmp");
    public static Bitmap heightMap = new Bitmap("heightMap.bmp");

    //TODO: кидать exception при несовпадении размеров
    public static int MapHeight = ImageHandler.heightMap.Height;
    public static int MapWidth = ImageHandler.heightMap.Width;

}
