using Timer = System.Windows.Forms.Timer;


namespace VoxelRender;

public partial class Form1 : Form
{
    private readonly VoxelRendering _rendering;

    public Form1(VoxelRendering render)
    {
        _rendering = render;
        KeyDown += (sender, args) => KeysHandler.OnPress(args);
        KeyUp += (sender, args) => KeysHandler.OnRelease(args);
        var tmr = new Timer();
        tmr.Interval = 10;
        tmr.Tick += (sender, args) => Invalidate();
        tmr.Start();
        InitializeComponent();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Size = new Size(Config.WindowWindth, Config.WindowHeight);

        DoubleBuffered = true;
        var g = e.Graphics;
        _rendering.Update();
        var myBrush = new SolidBrush(Color.White);
        var myFont = new Font("arial", 30);
        g.DrawImage(_rendering.screenImage.GetBmp(), 0, 0, Config.WindowWindth, Config.WindowHeight);
        g.DrawString(_rendering.Player.Pos.ToString(), myFont, myBrush, 20, 20);
        g.DrawString(_rendering.Player.Angles.ToString(), myFont, myBrush, 20, 70);
        g.DrawString(_rendering.elapsedTime, myFont, myBrush, 20, 100, new StringFormat());
    }
}