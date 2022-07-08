namespace VoxelRender;

public class BMPHandler
{
    private Bitmap BMP;
    private int Stride;
    private int Height;
    private int Width;
    private (byte r, byte g, byte b)[,] matrix;

    public BMPHandler(Bitmap bmp)
    {
        BMP = bmp;
        Height = bmp.Height;
        Width = bmp.Width;
        Stride = (Width * 3) + ((Width * 3) % 4);
        SetMatrix();
    }

    public Bitmap GetBMP()
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
        var bytes = BMP.GetBitmapBytes().Chunk(Stride).ToArray();
        matrix = new (byte red, byte green, byte blue)[Width, Height];
        var delta = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] = (bytes[i][delta++], bytes[i][delta++], bytes[i][delta++]);
            }

            delta = 0;
        }
    }

    public void Update()
    {
        var bytesM = new byte[Stride, Height];
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width;)
            {
                bytesM[i, j++] = matrix[i, j].r;
                bytesM[i, j++] = matrix[i, j].g;
                bytesM[i, j++] = matrix[i, j].b;
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