using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace VoxelRender;

public static class BitmapExtension
{
    public static byte[] GetBitmapBytes(this Bitmap bmp)
    {
        var destRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
        var bmpData = bmp.LockBits(destRect, ImageLockMode.ReadOnly, bmp.PixelFormat);
        int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
        byte[] bmpBytes = new byte[bytes];
        Marshal.Copy(bmpData.Scan0,bmpBytes,0,bytes);
        bmp.UnlockBits(bmpData);
        return bmpBytes;
    }

    public static void SetBitmapBytes(this Bitmap bmp, byte[] bytes)
    {
        var destRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
        var bmpData = new BitmapData();
        Marshal.Copy(bytes,0,bmpData.Scan0,bytes.Length);
        bmp.UnlockBits(bmpData);
    }

    
}