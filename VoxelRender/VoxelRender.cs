using System.Drawing.Imaging;

namespace VoxelRender;

public class VoxelRendering
{
    public BMPHandler screenImage;
    private int[,] heightMap;
    private (byte r, byte g, byte b)[,] textureMap;
    private Player Player;


    public VoxelRendering()
    {
        GetHeightMapInts();
        textureMap = new BMPHandler(ImageHandler.textureMap).matrix;
        Player = new Player();
        screenImage = new BMPHandler(new Bitmap(150, 200, PixelFormat.Format24bppRgb));
    }

    void GetHeightMapInts()
    {
        var heightHandler = new BMPHandler(ImageHandler.heightMap);
        var (height, width) = (heightHandler.matrix.GetLength(0), heightHandler.matrix.GetLength(1));
        heightMap = new int[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                heightMap[i, j] = heightHandler[i, j].r + heightHandler[i, j].g + heightHandler[i, j].b;
            }
        }
    }

    

    public void Update()
    {
        var y_buffer = new int[Config.WindowWidth];
        Array.Fill(y_buffer, Config.WindowHeight);
        
    
    
        var rayAngle = Player.Angles.oxy - Config.FOV;
        for (int ray = 0; ray < Config.WindowWidth; ray++)
        {
            var sinA = Math.Sin(rayAngle);
            var cosA = Math.Cos(rayAngle);
            for (double depth = 0; depth < Config.RayDistance; depth++)
            {
                var x = (int) (Player.Pos.x + depth * cosA);
                if (0 < x && x < Config.MapWidth)
                {
                    var y = (int) (Player.Pos.x + depth * sinA);
                    if (0 < y && x < Config.MapHeight)
                    {
                        depth *= Math.Cos(Player.Angles.oxy - rayAngle);
                        var heightOnSceen = (int) ((Player.Pos.z - heightMap[x,y]) / depth);
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
    
        screenImage.Update();
        
    }
}