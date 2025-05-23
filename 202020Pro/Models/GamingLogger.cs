using System;
using System.IO;

namespace _202020Pro.Models
{
    public static class GamingLogger
    {
        private static string logPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "202020Pro", "gaming_log.txt"
        );

        public static void Log(string message)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(logPath));
                File.AppendAllText(logPath,
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}");
            }
            catch
            {
                // نتجاهل الخطأ بصمت - لا كراش
            }
        }
    }
}