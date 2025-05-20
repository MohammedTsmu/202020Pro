using System.Media;
using System.IO;
using System;
using _202020Pro.Forms;

namespace _202020Pro
{
    public static class AppUtilities
    {
        //public static void PlayReminderSound()
        //{
        //    if (!Models.AppConfig.SoundEnabled)
        //        return;

        //    string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", "alert.wav");

        //    if (File.Exists(soundPath))
        //    {
        //        try
        //        {
        //            using (SoundPlayer player = new SoundPlayer(soundPath))
        //            {
        //                player.Play();
        //            }
        //        }
        //        catch
        //        {
        //            SystemSounds.Exclamation.Play(); // fallback
        //        }
        //    }
        //    else
        //    {
        //        SystemSounds.Exclamation.Play(); // fallback
        //    }
        //}

        public static void PlayReminderSound()
        {
            if (!Models.AppConfig.SoundEnabled)
                return;

            try
            {
                SoundPlayer player = new SoundPlayer(Properties.Resources.alert);
                player.Play();
            }
            catch
            {
                SystemSounds.Exclamation.Play();
            }
        }
    }
}
