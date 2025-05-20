using System.Media;
using System.IO;
using System;
using _202020Pro.Forms;
using _202020Pro.Models;
using System.Collections.Generic;

using System.Linq;
using System.Resources;
using System.Windows.Forms;

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

        //public static void PlayReminderSound()
        //{
        //    if (!Models.AppConfig.SoundEnabled)
        //        return;

        //    try
        //    {
        //        SoundPlayer player = new SoundPlayer(Properties.Resources.alert);
        //        player.Play();
        //    }
        //    catch
        //    {
        //        SystemSounds.Exclamation.Play();
        //    }
        //}


        //public static void PlayReminderSound()
        //{
        //    try
        //    {
        //        if (!AppConfig.SoundEnabled)
        //            return;

        //        string customPath = AppConfig.CustomSoundPath;

        //        if (!string.IsNullOrEmpty(customPath) && File.Exists(customPath))
        //        {
        //            using (SoundPlayer player = new SoundPlayer(customPath))
        //            {
        //                player.Play();
        //            }
        //        }
        //        else
        //        {
        //            // إذا ماكو صوت مخصص أو مفقود، نرجع على الصوت الافتراضي من الموارد
        //            using (SoundPlayer player = new SoundPlayer(Properties.Resources.alert))
        //            {
        //                player.Play();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("خطأ في تشغيل الصوت: " + ex.Message);
        //    }
        //}

        public static List<string> GetAvailableSoundNames()
        {
            var soundNames = new List<string>();
            var resourceSet = Properties.Resources.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, true);

            foreach (System.Collections.DictionaryEntry entry in resourceSet)
            {
                if (entry.Value is System.IO.UnmanagedMemoryStream) // صوت
                {
                    soundNames.Add(entry.Key.ToString());
                }
            }

            return soundNames;
        }


        //public static void PlayReminderSound()
        //{
        //    if (!AppConfig.SoundEnabled || string.IsNullOrEmpty(AppConfig.SelectedSoundName)) return;

        //    object soundObj = Properties.Resources.ResourceManager.GetObject(AppConfig.SelectedSoundName);

        //    if (soundObj is System.IO.Stream stream)
        //    {
        //        try
        //        {
        //            System.Media.SoundPlayer player = new System.Media.SoundPlayer(stream);
        //            player.Play();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("🔊 خطأ أثناء تشغيل الصوت: " + ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine($"⚠️ الصوت المطلوب '{AppConfig.SelectedSoundName}' غير موجود في الموارد أو غير صالح.");
        //    }
        //}

        // ✅ تستدعى في أي مكان لتشغيل الصوت الحالي المحدد
        public static void PlayReminderSound()
        {
            PlayReminderSound(AppConfig.SelectedSoundName);
        }

        // ✅ تستدعى لتجربة أي صوت قبل التحديد
        public static void PlayReminderSound(string soundName)
        {
            if (string.IsNullOrEmpty(soundName)) return;

            object soundObj = Properties.Resources.ResourceManager.GetObject(soundName);
            if (soundObj is System.IO.Stream stream)
            {
                try
                {
                    using (System.Media.SoundPlayer player = new System.Media.SoundPlayer(stream))
                    {
                        player.Play();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("تعذر تشغيل صوت التنبيه.\n" + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }





    }
}
