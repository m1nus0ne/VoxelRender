using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.InteropServices;
using Timer = System.Windows.Forms.Timer;


namespace VoxelRender;

public partial class Form1 : Form
{
    private VoxelRendering vr;

    public Form1(VoxelRendering render)
    {
        vr = render;
        var tmr = new Timer();
        tmr.Interval = 500;
        tmr.Tick += (sender, args) => Invalidate();
        tmr.Start();
        InitializeComponent();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        DoubleBuffered = true;
        var g = e.Graphics;
        vr.Update();
        g.DrawImage(vr.screenImage.GetBmp(), Point.Empty);
        g.DrawString(vr.Player.Pos.ToString(), new Font("arial", 30), new SolidBrush(Color.White), 20, 20);
    }
}