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

        /// Night Mode Settings
        public static bool NightModeEnabled
        {
            get => Properties.Settings.Default.NightModeEnabled;
            set
            {
                Properties.Settings.Default.NightModeEnabled = value;
                Properties.Settings.Default.Save();
            }
        }

        /// Night Mode Start and End Hours
        public static int NightModeStartHour
        {
            get => Properties.Settings.Default.NightModeStartHour;
            set
            {
                Properties.Settings.Default.NightModeStartHour = value;
                Properties.Settings.Default.Save();
            }
        }

        /// Night Mode Start and End Hours
        public static int NightModeEndHour
        {
            get => Properties.Settings.Default.NightModeEndHour;
            set
            {
                Properties.Settings.Default.NightModeEndHour = value;
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