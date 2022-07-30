using System.Drawing.Imaging;
using Timer = System.Windows.Forms.Timer;

namespace VoxelRender;

static class Program
{
  
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        var vxlRender = new VoxelRendering();
        var mainForm = new Form1(vxlRender);
        mainForm.KeyDown += (sender, args) => KeysHandler.OnPress(args);
        mainForm.KeyUp += (sender, args) => KeysHandler.OnRelease(args);
        var tmr = new Timer();
        tmr.Interval = 20;
        tmr.Tick += (sender, args) =>
        {
            KeysHandler.Update();
            mainForm.Invalidate();
        };
        KeysHandler.Player = vxlRender.Player;
        tmr.Start();
        Application.Run(mainForm);
    }
}