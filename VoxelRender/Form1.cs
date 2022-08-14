using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.InteropServices;
using Timer = System.Windows.Forms.Timer;


namespace VoxelRender;

public partial class Form1 : Form
{
    private VoxelRendering vr;
    private int l;

    public Form1(VoxelRendering render)
    {
        vr = render;
        var tmr = new Timer();
        tmr.Interval = 10;
        tmr.Tick += (sender, args) => Invalidate();
        tmr.Start();
        InitializeComponent();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        DoubleBuffered = true;
        var g = e.Graphics;
        vr.screenImage.Clear();
        for (int i = 0; i < 500; i++)
        {
            for (int j = 0; j < 500; j++)
            {
                if (i == l)
                    vr.screenImage[i, j] = (100, 100, 100);
            }
        }

        l += 1;
        vr.Update();
        g.DrawImage(vr.screenImage.GetBmp(), Point.Empty);
        // g.DrawString(vr.Player.Pos.ToString(), new Font("arial", 30), new SolidBrush(Color.White), 20, 20);
        // g.DrawString(vr.Player.Angles.ToString(), new Font("arial", 30), new SolidBrush(Color.White), 20, 70);
    }
}