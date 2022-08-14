namespace VoxelRender;

public class Config
{
    public static int WindowWidth = 500;
    public static int WindowHeight = 500;
    public static int MapHeight = ImageHandler.heightMap.Height;
    public static int MapWidth = ImageHandler.heightMap.Width;
    
    public static int RayDistance = 200; 
    public static double FOV = Math.PI/6;
    public static double HFOV = Math.PI/12;
    public static double deltaAngle = FOV / WindowWidth;
}