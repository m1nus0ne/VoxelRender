namespace VoxelRender;

static class Program
{
  
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        var vxlRender = new VoxelRendering();
        var mainForm = new Form1(vxlRender);
        Application.Run(mainForm);
    }
}