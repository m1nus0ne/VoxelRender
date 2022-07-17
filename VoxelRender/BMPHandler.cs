namespace VoxelRender;

public class BMPHandler
{
    private Bitmap BMP;
    private int Stride;
    private int Height;
    private int Width;
    private int BPP;
    public (byte r, byte g, byte b)[,] matrix;

    public BMPHandler(Bitmap bmp)
    {
        //TODO: Стоковое 32bpp привести к 24bpp
        BMP = bmp;
        Height = bmp.Height;
        Width = bmp.Width;
        BPP = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
        Stride = (Width * BPP) + ((Width * BPP) % 4);
        SetMatrix();
    }

    public Bitmap GetBmp()
    {
        return BMP;
    }

    public (byte r, byte g, byte b) this[int x, int y]
    {
        set { matrix[x, y] = value; }
        get { return matrix[x, y]; }
    }

    private void SetMatrix()
    {
        var bytes = BMP.GetBitmapBytes()
            .Chunk(Stride)
            .ToArray();

        matrix = new (byte red, byte green, byte blue)[Height, Width];

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] = (bytes[i][j * BPP], bytes[i][j * BPP + 1], bytes[i][j * BPP + 2]);
            }
        }
    }

    public void Update()
    {
        var bytesM = new byte[Height, Stride];
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                bytesM[i, j * BPP] = matrix[i, j].r;
                bytesM[i, j * BPP + 1] = matrix[i, j].g;
                bytesM[i, j * BPP + 2] = matrix[i, j].b;
            }
        }

        var pos = 0;
        var bytesArr = new byte[Stride * Height];
        foreach (var b in bytesM)
        {
            bytesArr[pos++] = b;
        }

        BMP.SetBitmapBytes(bytesArr);
    }
}