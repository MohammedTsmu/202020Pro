using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using _202020Pro.Models;

namespace _202020Pro
{
    public static class AppUtilities
    {
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


        // ✅ تستدعى في أي مكان لتشغيل الصوت الحالي المحدد
        public static void PlayReminderSound()
        {
            PlayReminderSound(AppConfig.SelectedSoundName);
        }


        // ✅ تستدعى لتجربة أي صوت قبل التحديد
        // ✅ محاولة تشغيل صوت احتياطي
        // 🔊 ابحث عن صوت احتياطي مختلف
        // 🔊 ابحث عن أي صوت متاح
        // 🔊 إذا لم يكن هناك صوت احتياطي، استخدم صوت النظام الافتراضي
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
                        return; // ✅ اشتغل بنجاح
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ فشل تشغيل الصوت المحدد: " + ex.Message);
                }
            }

            string backup = GetAvailableSoundNames().FirstOrDefault(s => s != soundName);
            if (!string.IsNullOrEmpty(backup))
            {
                try
                {
                    object backupObj = Properties.Resources.ResourceManager.GetObject(backup);
                    if (backupObj is System.IO.Stream backupStream)
                    {
                        using (System.Media.SoundPlayer fallbackPlayer = new System.Media.SoundPlayer(backupStream))
                        {
                            fallbackPlayer.Play();
                            Console.WriteLine("✅ تم تشغيل صوت احتياطي: " + backup);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("⚠️ فشل الصوت الاحتياطي: " + ex.Message);
                }
            }

            // 🔊 الخطة النهائية: صوت نظام من ويندوز
            try
            {
                System.Media.SystemSounds.Exclamation.Play();
                Console.WriteLine("✅ تم تشغيل صوت النظام كخطة نهائية.");
            }
            catch
            {
                MessageBox.Show("❌ تعذر تشغيل أي صوت. الرجاء التأكد من ملفات الموارد.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}