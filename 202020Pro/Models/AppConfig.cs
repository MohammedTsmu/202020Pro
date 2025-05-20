using System;

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

        /// Background Color for Break Message
        public static string BreakBackgroundColor
        {
            get => Properties.Settings.Default.BreakBackgroundColor;
            set { Properties.Settings.Default.BreakBackgroundColor = value; Properties.Settings.Default.Save(); }
        }

        /// Text Color for Break Message
        public static string BreakTextColor
        {
            get => Properties.Settings.Default.BreakTextColor;
            set { Properties.Settings.Default.BreakTextColor = value; Properties.Settings.Default.Save(); }
        }

        /// Font Family for Break Message
        public static string BreakFontFamily
        {
            get => Properties.Settings.Default.BreakFontFamily;
            set { Properties.Settings.Default.BreakFontFamily = value; Properties.Settings.Default.Save(); }
        }

        /// Font Size for Break Message
        public static int BreakFontSize
        {
            get => Properties.Settings.Default.BreakFontSize;
            set { Properties.Settings.Default.BreakFontSize = value; Properties.Settings.Default.Save(); }
        }

        /// Break Countdown Enabled
        public static bool BreakCountdownEnabled
        {
            get => Properties.Settings.Default.BreakCountdownEnabled;
            set
            {
                Properties.Settings.Default.BreakCountdownEnabled = value;
                Properties.Settings.Default.Save();
            }
        }

        //Gaming Mode Last Toggle Time
        public static DateTime LastGamingToggleTime
        {
            get => Properties.Settings.Default.LastGamingToggleTime;
            set
            {
                Properties.Settings.Default.LastGamingToggleTime = value;
                Properties.Settings.Default.Save();
            }
        }


        //Selected Sound Name
        public static string SelectedSoundName
        {
            get => Properties.Settings.Default.SelectedSoundName;
            set
            {
                Properties.Settings.Default.SelectedSoundName = value;
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