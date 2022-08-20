using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.InteropServices;
using Timer = System.Windows.Forms.Timer;


namespace VoxelRender;

public partial class Form1 : Form
{
    private VoxelRendering vr;
    private BMPHandler bmp;
    private int l;

    public Form1(VoxelRendering render)
    {
        bmp = new BMPHandler(new Bitmap(500, 500, PixelFormat.Format32bppRgb));
        vr = render;
        var tmr = new Timer();
        tmr.Interval = 10;
        tmr.Tick += (sender, args) => Invalidate();
        tmr.Start();
        InitializeComponent();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Size = new Size(1000, 800);
        
        DoubleBuffered = true;
        var g = e.Graphics;
        vr.Update();
        g.DrawImage(vr.screenImage.GetBmp(),0,0,1000,800);
        g.DrawString(vr.Player.Pos.ToString(), new Font("arial", 30), new SolidBrush(Color.White), 20, 20);
        g.DrawString(vr.Player.Angles.ToString(), new Font("arial", 30), new SolidBrush(Color.White), 20, 70);
    }
}