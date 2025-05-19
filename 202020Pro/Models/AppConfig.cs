//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace _202020Pro.Models
//{
//    internal class AppConfig
//    {
//    }
//}


//using System;

//namespace _202020Pro.Models
//{
//    public static class AppConfig
//    {
//        public static string EmergencyPassword { get; set; } = "911";
//        public static string GamingPassword { get; set; } = "gamer";
//        public static int BreakMinutes { get; set; } = 20;
//        public static int GamingModeMinutes { get; set; } = 180;
//        public static bool SoundEnabled { get; set; } = true;

//        public static void ResetToDefaults()
//        {
//            EmergencyPassword = "911";
//            GamingPassword = "gamer";
//            BreakMinutes = 20;
//            GamingModeMinutes = 180;
//            SoundEnabled = true;
//        }
//    }
//}




namespace _202020Pro.Models
{
    public static class AppConfig
    {
        public static string EmergencyPassword
        {
            get => Properties.Settings.Default.EmergencyPassword;
            set
            {
                Properties.Settings.Default.EmergencyPassword = value;
                Properties.Settings.Default.Save();
            }
        }

        public static string GamingPassword
        {
            get => Properties.Settings.Default.GamingPassword;
            set
            {
                Properties.Settings.Default.GamingPassword = value;
                Properties.Settings.Default.Save();
            }
        }

        public static int BreakMinutes
        {
            get => Properties.Settings.Default.BreakMinutes;
            set
            {
                Properties.Settings.Default.BreakMinutes = value;
                Properties.Settings.Default.Save();
            }
        }

        public static int GamingModeMinutes
        {
            get => Properties.Settings.Default.GamingModeMinutes;
            set
            {
                Properties.Settings.Default.GamingModeMinutes = value;
                Properties.Settings.Default.Save();
            }
        }

        public static bool SoundEnabled
        {
            get => Properties.Settings.Default.SoundEnabled;
            set
            {
                Properties.Settings.Default.SoundEnabled = value;
                Properties.Settings.Default.Save();
            }
        }

        public static void ResetToDefaults()
        {
            EmergencyPassword = "911";
            GamingPassword = "gamer";
            BreakMinutes = 20;
            GamingModeMinutes = 180;
            SoundEnabled = true;
        }
    }
}