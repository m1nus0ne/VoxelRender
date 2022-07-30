using System.Drawing.Imaging;

namespace VoxelRender;

public class VoxelRendering
{
    public BMPHandler screenImage;
    private int[,] heightMap;
    private (byte r, byte g, byte b)[,] textureMap;
    public  Player Player;


    public VoxelRendering()
    {
        GetHeightMapInts();
        textureMap = new BMPHandler(ImageHandler.textureMap).matrix;
        Player = new Player();
        screenImage = new BMPHandler(new Bitmap(500,500,PixelFormat.Format24bppRgb));
    }

    void GetHeightMapInts()
    {
        var heightHandler = new BMPHandler(ImageHandler.heightMap);
        var (height, width) = (heightHandler.Height,heightHandler.Width);
        heightMap = new int[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                heightMap[i, j] = heightHandler[j, i].r + heightHandler[j, i].g + heightHandler[j, i].b;
            }
        }
    }

    

    public void Update()
    {
        var y_buffer = new int[Config.WindowWidth];
        Array.Fill(y_buffer, Config.WindowHeight);
        
        var rayAngle = Player.Angles.X - Config.FOV;
        for (int ray = 0; ray < Config.WindowWidth; ray++)
        {
            var sinA = Math.Sin(rayAngle);
            var cosA = Math.Cos(rayAngle);
            for (double depth = 0; depth < Config.RayDistance; depth+=1)
            {
                var x = (int) (Player.Pos.X + depth * cosA);
                if (0 < x && x < Config.MapWidth)
                {
                    var y = (int) (Player.Pos.X + depth * sinA);
                    if (0 < y && x < Config.MapHeight)
                    {
                        
                        var heightOnSceen = (int) ((Player.Pos.Z - heightMap[x,y]) * depth);
                        if (heightOnSceen < y_buffer[ray])
                        {
                            if (heightOnSceen < 0)
                                heightOnSceen = 0;
                            for (int screenY = heightOnSceen; screenY < y_buffer[ray]; screenY++)
                            {
                                screenImage[ray, screenY] =
                                    textureMap[x,y];
                                
                            }
                        }
                    }
                }
            }
    
            rayAngle += Config.deltaAngle;
        }
    
        screenImage.UpdateBytes();
        
    }
}