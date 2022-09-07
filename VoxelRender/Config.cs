namespace VoxelRender;

public class Config
{
    public static int WindowWidth = 300;
    public static int WindowHeight = 300;
    public static int MapHeight = ImageHandler.heightMap.Height;
    public static int MapWidth = ImageHandler.heightMap.Width;
    
    public static int RayDistance = 2000; 
    public static double FOV = Math.PI/6;
    public static double HFOV = Math.PI/16;
    public static double deltaAngle = HFOV / WindowWidth;
}
