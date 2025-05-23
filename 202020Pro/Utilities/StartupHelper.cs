using System.Windows.Forms;
using Microsoft.Win32;


public static class StartupHelper
{
    public static void AddToStartup()
    {
        string exePath = Application.ExecutablePath;
        RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
        rk.SetValue("202020Pro", exePath);
    }
}