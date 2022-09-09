using System.Diagnostics;
using System.Drawing.Imaging;

namespace VoxelRender;

public class VoxelRendering
{
    public string elapsedTime;
    public BMPHandler screenImage;
    private int[,] heightMap;
    private (byte r, byte g, byte b)[,] _textureMap;
    public Player Player;
    private int[] y_buffer = new int[Config.ImageWidth];

    public VoxelRendering()
    {
        GetHeightMapInts();
        _textureMap = new BMPHandler(Config.textureMap).Matrix;
        Player = new Player();
        screenImage = new BMPHandler(new Bitmap(Config.ImageWidth, Config.ImageHeight, PixelFormat.Format32bppRgb));
    }

    void GetHeightMapInts()
    {
        var heightHandler = new BMPHandler(Config.heightMap);
        var (height, width) = (heightHandler.Height, heightHandler.Width);
        heightMap = new int[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                heightMap[i, j] = heightHandler[j, i].r;
            }
        }
    }

    private void CalculatePixels(int ray)
    {
        var rayAngle = Player.Angles.X - Config.HFOV + Config.deltaAngle * ray;

        var firstContact = false;
        var sinA = Math.Sin(rayAngle);
        var cosA = Math.Cos(rayAngle);
        int heightOnSceen;


        for (int depth = 1; depth < Config.RayDistance; depth++)
        {
            var x = (int) (Player.Pos.X + depth * cosA);
            if (0 < x && x < Config.MapWidth)
            {
                var y = (int) (Player.Pos.Y + depth * sinA);
                if (0 < y && x < Config.MapHeight)
                {
                    try
                    {
                        heightOnSceen =
                            (int) ((Player.Height - heightMap[x, y]) / depth * 900 + Player.Angles.Y);
                    }
                    catch (Exception e)
                    {
                        break;
                    }

                    if (!firstContact)
                    {
                        y_buffer[ray] = Math.Min(heightOnSceen, Config.ImageHeight);
                        firstContact = true;
                    }

                    if (heightOnSceen < 0)
                        heightOnSceen = 0;
                    if (heightOnSceen < y_buffer[ray])
                    {
                        for (int screenY = heightOnSceen; screenY < y_buffer[ray]; screenY++)
                        {
                            screenImage[ray, screenY] =
                                _textureMap[x, y];
                        }

                        y_buffer[ray] = heightOnSceen;
                    }
                }
            }
        }
    }


    public void Update()
    {
        Player.Update(KeysHandler.GetState());
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        Array.Fill(y_buffer, Config.ImageHeight);
        screenImage.Clear();
        Parallel.For(0, Config.ImageWidth, CalculatePixels);
        screenImage.UpdateBytes();
        stopWatch.Stop();
        TimeSpan ts = stopWatch.Elapsed;
        elapsedTime = ts.Milliseconds.ToString();
    }
}