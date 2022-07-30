namespace VoxelRender;

public class Config
{
    public static int WindowWidth = 500;
    public static int WindowHeight = 500;
    public static int MapHeight = ImageHandler.heightMap.Height;
    public static int MapWidth = ImageHandler.heightMap.Width;
    
    public static int RayDistance = 100; 
    public static double FOV = Math.PI/4;
    public static double deltaAngle = FOV / WindowWidth;
}