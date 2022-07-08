using System.Drawing.Imaging;

namespace VoxelRender;

public class VoxelRendering
{
    public Bitmap screenImage;
    private int[,] heightMap;
    private (byte r, byte g, byte b)[,] textureMap;
    private Player Player;


    public VoxelRendering()
    {
        GetHeightMapInts();
        GetTextureMatrixBytes();
        Player = new Player();
        screenImage = new Bitmap(600, 400, PixelFormat.Format24bppRgb);
    }

    void GetHeightMapInts()
    {
        var heightMapImg = ImageHandler.heightMap;
        heightMap = new int[heightMapImg.Height, heightMapImg.Width];
        var bytes = heightMapImg.GetBitmapBytes();
        var delta = -1;
        for (int i = 0; i < heightMap.GetLength(0); i++)
        {
            for (int j = 0; j < heightMap.GetLength(1); j++)
            {
                var (red, green, blue) = (bytes[++delta], bytes[++delta], bytes[++delta]);
                heightMap[i, j] = (int) (0.2126 * red + 0.7152 * green + 0.0722 * blue);
            }
        }
    }

    void GetTextureMatrixBytes()
    {
        var bytes = ImageHandler.textureMap.GetBitmapBytes();
        var delta = -0;
        for (int i = 0; i < textureMap.GetLength(0); i++)
        {
            for (int j = 0; j <textureMap.GetLength(1); j++)
            {
                textureMap[i, j] = (bytes[delta++], bytes[delta++], bytes[delta++]);
            }
        }
    }

    // public void Update()
    // {
    //     var y_buffer = new int[Config.WindowWidth];
    //     Array.Fill(y_buffer, Config.WindowHeight);
    //     var imgBytes = screenImage.GetBitmapBytes();
    //
    //
    //     var rayAngle = Player.Angles.oxy - Config.FOV;
    //     for (int ray = 0; ray < Config.WindowWidth; ray++)
    //     {
    //         var sinA = Math.Sin(rayAngle);
    //         var cosA = Math.Cos(rayAngle);
    //         for (double depth = 0; depth < Config.RayDistance; depth++)
    //         {
    //             var x = (int) (Player.Pos.x + depth * cosA);
    //             if (0 < x && x < Config.MapWidth)
    //             {
    //                 var y = (int) (Player.Pos.x + depth * sinA);
    //                 if (0 < y && x < Config.MapHeight)
    //                 {
    //                     depth *= Math.Cos(Player.Angles.oxy - rayAngle);
    //                     var heightOnSceen = (int) ((Player.Pos.z - heightMap[x,y]) / depth);
    //                     if (heightOnSceen < y_buffer[ray])
    //                     {
    //                         if (heightOnSceen < 0)
    //                             heightOnSceen = 0;
    //                         for (int screenY = heightOnSceen; screenY < y_buffer[ray]; screenY++)
    //                         {
    //                             imgBytes[ray * Config.WindowWidth + screenY] =
    //                                 textureMap[x,y];
    //                         }
    //                     }
    //                 }
    //             }
    //         }
    //
    //         rayAngle += Config.deltaAngle;
    //     }
    //
    //     screenImage.SetBitmapBytes(imgBytes);
    // }
}