namespace VoxelRender;

public class BMPHandler
{
    private Bitmap Image;
    private readonly int _stride;
    public int Height;
    public int Width;
    private int BPP;
    private byte[] _bmpBytes;
    public (byte r, byte g, byte b)[,] matrix;

    public BMPHandler(Bitmap image)
    {
        Image = image;
        Height = image.Height;
        Width = image.Width;
        BPP = System.Drawing.Image.GetPixelFormatSize(image.PixelFormat) / 8;
        _stride = (Width * BPP) + ((Width * BPP) % 4);
        SetMatrix();
    }
    
    public Bitmap GetBmp()
    {
        return Image;
    }

    public (byte r, byte g, byte b) this[int x, int y]
    {
        set { matrix[y, x] = value; }
        get { return matrix[y, x]; }
    }

    private void SetMatrix()
    {
        var bytes = Image.GetBitmapBytes()
            .Chunk(_stride)
            .ToArray();

        matrix = new (byte red, byte green, byte blue)[Height, Width];

        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                matrix[i, j] = (bytes[i][j * BPP], bytes[i][j * BPP + 1], bytes[i][j * BPP + 2]);
            }
        }
    }

    public void UpdateBytes()
    {
        var bytesM = new byte[Height, _stride];
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                bytesM[i, j * BPP] = matrix[i, j].r;
                bytesM[i, j * BPP + 1] = matrix[i, j].g;
                bytesM[i, j * BPP + 2] = matrix[i, j].b;
            }
        }

        
        
        Image.SetBitmapBytes(bytesM.Cast<byte>().ToArray());
    }

    public void Clear()
    {
        Image.SetBitmapBytes(new byte[Height * _stride]);
    }
}