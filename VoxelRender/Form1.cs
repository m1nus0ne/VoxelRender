using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Timer = System.Windows.Forms.Timer;


namespace VoxelRender;

public partial class Form1 : Form
{
    private Bitmap img;
    private VoxelRendering vr;

    public Form1()
    {
        vr = new VoxelRendering();
        InitializeComponent();
        
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;

        void LockUnlockBitsExample(PaintEventArgs e)
        {
            DoubleBuffered = true;
            // Create a new bitmap.
            Bitmap bmp = new Bitmap(1000, 1000, PixelFormat.Format24bppRgb);

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData =
                bmp.LockBits(rect, ImageLockMode.ReadWrite,
                    bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;
            var stride = (bmp.Width * 3) + ((bmp.Width * 3) % 4);
            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            Marshal.Copy(ptr, rgbValues, 0, bytes);

            var rnd = new Random();
            // Set every third value to 255. A 24bpp bitmap will look red.  

            rnd.NextBytes(rgbValues);
            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);

            // Draw the modified image.aww
            e.Graphics.DrawImage(bmp, 0, 0);
        }


        vr.Update();
        g.DrawImage(vr.screenImage.GetBmp(),Point.Empty);
    }
}