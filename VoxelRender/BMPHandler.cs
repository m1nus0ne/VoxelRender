namespace VoxelRender;

public class BMPHandler
{
    private readonly Bitmap _image;
    private readonly int _stride;
    public readonly int Height;
    public readonly int Width;
    private readonly int _bpp;
    private byte[] _bmpBytes;
    public (byte r, byte g, byte b)[,] Matrix;
    private byte[,] _bytesM;

    public BMPHandler(Bitmap image)
    {
        _image = image;
        Height = image.Height;
        Width = image.Width;
        _bpp = System.Drawing.Image.GetPixelFormatSize(image.PixelFormat) / 8;
        _stride = (Width * _bpp) + ((Width * _bpp) % 4);
        SetMatrix();
    }
    
    public Bitmap GetBmp()
    {
        return _image;
    }

    public (byte r, byte g, byte b) this[int x, int y]
    {
        set { Matrix[y, x] = value; }
        get { return Matrix[y, x]; }
    }

    private void SetMatrix()
    {
        var bytes = _image.GetBitmapBytes()
            .Chunk(_stride)
            .ToArray();

        Matrix = new (byte red, byte green, byte blue)[Height, Width];

        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                Matrix[i, j] = (bytes[i][j * _bpp], bytes[i][j * _bpp + 1], bytes[i][j * _bpp + 2]);
            }
        }
    }

    public void UpdateBytes()
    {
        _bytesM = new byte[Height, _stride];
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                _bytesM[i, j * _bpp] = Matrix[i, j].r;
                _bytesM[i, j * _bpp + 1] = Matrix[i, j].g;
                _bytesM[i, j * _bpp + 2] = Matrix[i, j].b;
            }
        }

        
        
        _image.SetBitmapBytes(_bytesM.Cast<byte>().ToArray());
    }

    public void Clear()
    {
        Matrix = new (byte red, byte green, byte blue)[Height, Width];
    }
}