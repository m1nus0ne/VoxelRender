namespace VoxelRender;

public class Config
{
    public static int WindowWidth = 350;
    public static int WindowHeight = 350;
    public static int MapHeight = ImageHandler.heightMap.Height;
    public static int MapWidth = ImageHandler.heightMap.Width;
    
    public static int RayDistance = 1000; 
    public static double FOV = Math.PI/6;
    public static double HFOV = Math.PI/12;
    public static double deltaAngle = HFOV / WindowWidth;
}
