using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace StagePlayout.App;

public partial class App : Application
{
    [DllImport("winmm.dll")] private static extern uint timeBeginPeriod(uint uMilliseconds);
    [DllImport("winmm.dll")] private static extern uint timeEndPeriod(uint uMilliseconds);
    [DllImport("dwmapi.dll")] private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int val, int size);
    [DllImport("dwmapi.dll")] private static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

    private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
    private const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
    private const int DWMWA_BORDER_COLOR = 34;

    public struct MARGINS { public int left, right, top, bottom; }

    protected override void OnStartup(StartupEventArgs e)
    {
        timeBeginPeriod(1);
        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        timeEndPeriod(1);
        base.OnExit(e);
    }

    public static void ApplyDarkMode(Window window)
    {
        window.Loaded += (_, _) =>
        {
            var hwnd = new WindowInteropHelper(window).EnsureHandle();
            int useDark = 1;
            DwmSetWindowAttribute(hwnd, 19, ref useDark, sizeof(int));
            DwmSetWindowAttribute(hwnd, 20, ref useDark, sizeof(int));
        };
    }
}
