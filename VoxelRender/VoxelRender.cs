using System.Drawing.Imaging;

namespace VoxelRender;

public class VoxelRendering
{
    public BMPHandler screenImage;
    private int[,] heightMap;
    private (byte r, byte g, byte b)[,] _textureMap;
    public  Player Player;
    private int[] _yBuffer;

    public VoxelRendering()
    {
        GetHeightMapInts();
        _textureMap = new BMPHandler(ImageHandler.textureMap).matrix;
        Player = new Player();
        screenImage = new BMPHandler(new Bitmap(Config.WindowWidth,Config.WindowHeight,PixelFormat.Format32bppRgb));
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
                heightMap[i, j] = heightHandler[j, i].r;
            }
        }
    }

    

    public void Update()
    {
        var y_buffer = new int[Config.WindowWidth];
        Array.Fill(y_buffer, Config.WindowHeight);
        screenImage.Clear();
        
        var rayAngle = Player.Angles.X - Config.HFOV;
        for (int ray = 0; ray < Config.WindowWidth; ray++)
        {
            var firstContact = false;
            var sinA = Math.Sin(rayAngle);
            var cosA = Math.Cos(rayAngle);
            for (int depth = 1; depth < Config.RayDistance; depth++)
            {
                var x = (int) (Player.Pos.X + depth * cosA);
                if (0 < x && x < Config.MapWidth)
                {
                    var y = (int) (Player.Pos.Y + depth * sinA);
                    if (0 < y && x < Config.MapHeight)
                    {
                        
                        var heightOnSceen = (int) ((Player.Pos.Z - heightMap[x,y]) / depth*620+Player.Angles.Y);
                        if (!firstContact)
                        {
                            y_buffer[ray] = Math.Min(heightOnSceen, Config.WindowHeight);
                            firstContact = true;
                        }
                        if (heightOnSceen < 0)
                            heightOnSceen = 0;
                        if (heightOnSceen < y_buffer[ray])
                        {
                            
                            for (int screenY = heightOnSceen; screenY < y_buffer[ray]; screenY++)
                            {
                                screenImage[ray, screenY] =
                                    _textureMap[x,y];
                                
                            }

                            y_buffer[ray] = heightOnSceen;
                        }
                    }
                }
            }
    
            rayAngle += Config.deltaAngle;
        }
    
        screenImage.UpdateBytes();
        
    }
}