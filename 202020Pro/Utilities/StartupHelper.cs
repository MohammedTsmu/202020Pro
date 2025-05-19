//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace _202020Pro.Utilities
//{
//    internal class StartupHelper
//    {
//    }
//}

// ***C:\EyeCareApps\202020Pro\Utilities\StartupHelper.cs***
using Microsoft.Win32;
using System.Windows.Forms;


public static class StartupHelper
{
    public static void AddToStartup()
    {
        string exePath = Application.ExecutablePath;
        RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
        rk.SetValue("202020Pro", exePath);
    }
}
