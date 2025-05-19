using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _202020Pro.Forms;
using _202020Pro.Models;
using Microsoft.VisualBasic;


namespace _202020Pro.Forms
{
    public partial class MainForm : Form
    {
        private System.Windows.Forms.Timer mainTimer;

        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private System.Windows.Forms.Timer gamingTooltipTimer;


        public MainForm()
        {
            InitializeComponent();

            mainTimer = new Timer();
            //mainTimer.Interval = 20 * 60 * 1000; // 20 دقيقة
            //mainTimer.Interval = 1 * 60 * 1000; // 20 دقيقة
            mainTimer.Interval = AppConfig.BreakMinutes * 60 * 1000; // 20 دقيقة
            mainTimer.Tick += MainTimer_Tick;
            mainTimer.Start();

            // مؤقت عرض الوقت المتبقي في Gaming Mode
            gamingTooltipTimer = new Timer();
            gamingTooltipTimer.Interval = 60 * 1000; // كل دقيقة
            gamingTooltipTimer.Tick += GamingTooltipTimer_Tick;
            gamingTooltipTimer.Start(); // يعمل دائمًا في الخلفية


            // إعدادات النموذج
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;

            // إعداد القائمة المنبثقة
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("تفعيل/تعطيل وضع الألعاب", null, ToggleGamingMode_Click);

            trayMenu.Items.Add("خروج", null, Exit_Click);

            trayMenu.Items.Add(new ToolStripSeparator()); // فاصل بين العناصر


            trayMenu.Items.Add("📄 عرض سجل وضع الألعاب", null, ShowGamingLog_Click);
            trayMenu.Items.Add(new ToolStripSeparator()); // فاصل بين العناصر


            // إضافة قائمة فرعية للإعدادات
            var settingsMenu = new ToolStripMenuItem("⚙️ الإعدادات");
            settingsMenu.DropDownItems.Add("🔐 تعديل كلمة مرور الطوارئ", null, ChangeEmergencyPassword_Click);
            settingsMenu.DropDownItems.Add("🕹️ تعديل كلمة مرور الألعاب", null, ChangeGamingPassword_Click);
            settingsMenu.DropDownItems.Add("⏱️ تعديل مدة الراحة", null, ChangeBreakInterval_Click);
            settingsMenu.DropDownItems.Add("⌛ تعديل مدة وضع الألعاب", null, ChangeGamingDuration_Click);
            settingsMenu.DropDownItems.Add("🔊 تشغيل/إيقاف الصوت", null, ToggleSound_Click);
            settingsMenu.DropDownItems.Add("🔁 إعادة الإعدادات الافتراضية", null, ResetSettings_Click);

            trayMenu.Items.Add(settingsMenu);

            // إنشاء الأيقونة
            trayIcon = new NotifyIcon
            {
                Text = "202020Pro - حماية العين",
                Icon = SystemIcons.Application,
                ContextMenuStrip = trayMenu,
                Visible = true
            };
        }

        
        private void MainTimer_Tick(object sender, EventArgs e)
        {
            if (this.IsDisposed || !this.IsHandleCreated)
            {
                mainTimer.Stop();
                return;
            }

            try
            {
                BreakForm breakForm = new BreakForm();
                breakForm.ShowDialog(); // نافذة لا يمكن تجاوزها بسهولة
            }
            catch (ObjectDisposedException)
            {
                mainTimer.Stop();
            }
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (trayIcon != null)
            {
                trayIcon.Visible = false;
                trayIcon.Dispose();
                trayIcon = null;
            }

            base.OnFormClosing(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            StartupHelper.AddToStartup();

            // اختصار سريع للتجربة
            this.KeyPreview = true;
            this.KeyDown += MainForm_KeyDown;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.G)
            {
                GamingModeForm gmForm = new GamingModeForm();
                gmForm.ShowDialog();
            }
        }

        private void ToggleGamingMode_Click(object sender, EventArgs e)
        {
            if (AppSettings.IsGamingMode)
            {
                AppSettings.IsGamingMode = false;
                MessageBox.Show("تم تعطيل وضع الألعاب", "202020Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GamingLogger.Log("تم تعطيل وضع الألعاب يدويًا");
            }
            else
            {
                GamingModeForm gmForm = new GamingModeForm();
                gmForm.ShowDialog();
            }
        }


        private void Exit_Click(object sender, EventArgs e)
        {
            try
            {
                if (mainTimer != null)
                {
                    mainTimer.Stop();
                    mainTimer.Dispose();
                    mainTimer = null;
                }

                if (trayIcon != null)
                {
                    trayIcon.Visible = false;
                    trayIcon.Dispose();
                    trayIcon = null;
                }

                Application.ExitThread(); // الأفضل من Exit()
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء الإغلاق: " + ex.Message);
            }
        }

        private void GamingTooltipTimer_Tick(object sender, EventArgs e)
        {
            if (AppSettings.IsGamingMode && GamingModeManager.GamingStartTime != null)
            {
                var timeUsed = GamingModeManager.TotalUsedToday;
                var timeLeft = GamingModeManager.AllowedPerDay - timeUsed;

                if (timeLeft.TotalMinutes <= 0)
                {
                    GamingModeManager.DisableGamingMode();
                    trayIcon.Text = "📴 تم إيقاف وضع الألعاب تلقائيًا";
                    GamingLogger.Log("تم تعطيل وضع الألعاب تلقائيًا");
                    return;
                }

                string tooltip = $"🎮 وضع الألعاب مفعل\n⏳ الوقت المتبقي: {FormatTimeSpan(timeLeft)}";
                trayIcon.Text = tooltip;
            }
            else
            {
                trayIcon.Text = "202020Pro - حماية العين";
            }
        }

        private string FormatTimeSpan(TimeSpan ts)
        {
            if (ts.TotalHours >= 1)
                return $"{(int)ts.TotalHours} ساعة و {ts.Minutes} دقيقة";
            else
                return $"{ts.Minutes} دقيقة";
        }

        // 🟦 تعديل كلمة مرور الطوارئ
        // 🟦 كلمة مرور الطوارئ 
        private void ChangeEmergencyPassword_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("أدخل كلمة المرور الجديدة للطوارئ:", "تعديل كلمة مرور الطوارئ", AppConfig.EmergencyPassword);
            if (!string.IsNullOrWhiteSpace(input) && input.Length >= 3)
            {
                AppConfig.EmergencyPassword = input;
                MessageBox.Show("تم تحديث كلمة المرور للطوارئ.", "نجاح");
            }
            else
            {
                MessageBox.Show("كلمة المرور يجب أن تكون 3 أحرف أو أكثر.", "خطأ");
            }
        }

        // 🟦 كلمة مرور الألعاب
        private void ChangeGamingPassword_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("أدخل كلمة المرور الجديدة لوضع الألعاب:", "تعديل كلمة المرور", AppConfig.GamingPassword);
            if (!string.IsNullOrWhiteSpace(input) && input.Length >= 3)
            {
                AppConfig.GamingPassword = input;
                MessageBox.Show("تم تحديث كلمة المرور لوضع الألعاب.", "نجاح");
            }
            else
            {
                MessageBox.Show("كلمة المرور يجب أن تكون 3 أحرف أو أكثر.", "خطأ");
            }
        }

        // 🟦 مدة الراحة
        private void ChangeBreakInterval_Click(object sender, EventArgs e)
        {
            //string input = Microsoft.VisualBasic.Interaction.InputBox("أدخل عدد الدقائق بين كل استراحة (10 - 60):", "مدة الراحة", AppConfig.BreakMinutes.ToString());
            string input = Microsoft.VisualBasic.Interaction.InputBox("أدخل عدد الدقائق بين كل استراحة (1 - 60):", "مدة الراحة", AppConfig.BreakMinutes.ToString());
            //if (int.TryParse(input, out int value) && value >= 10 && value <= 60)
            if (int.TryParse(input, out int value) && value >= 1 && value <= 60)//For testing delete later 1 minute timer
            {
                AppConfig.BreakMinutes = value;
                mainTimer.Interval = value * 60 * 1000;
                MessageBox.Show($"تم تعيين مدة الراحة إلى {value} دقيقة.", "نجاح");
            }
            else
            {
                MessageBox.Show("القيمة غير صالحة. الرجاء اختيار رقم بين 10 و 60.", "خطأ");
            }
        }

        // 🟦 مدة وضع الألعاب
        private void ChangeGamingDuration_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("أدخل عدد الدقائق المسموحة لوضع الألعاب (30 - 240):", "مدة وضع الألعاب", AppConfig.GamingModeMinutes.ToString());
            if (int.TryParse(input, out int value) && value >= 30 && value <= 240)
            {
                AppConfig.GamingModeMinutes = value;
                MessageBox.Show($"تم تعيين مدة وضع الألعاب إلى {value} دقيقة.", "نجاح");
            }
            else
            {
                MessageBox.Show("القيمة غير صالحة. الرجاء اختيار رقم بين 30 و 240.", "خطأ");
            }
        }


        // 🟦 تشغيل/إيقاف الصوت
        private void ToggleSound_Click(object sender, EventArgs e)
        {
            AppConfig.SoundEnabled = !AppConfig.SoundEnabled;
            MessageBox.Show("تم " + (AppConfig.SoundEnabled ? "تفعيل" : "إيقاف") + " الصوت.", "الإعدادات");
        }

        // 🟦 إعادة الإعدادات الافتراضية
        private void ResetSettings_Click(object sender, EventArgs e)
        {
            AppConfig.ResetToDefaults();
            mainTimer.Interval = AppConfig.BreakMinutes * 60 * 1000;
            MessageBox.Show("تمت إعادة الإعدادات إلى الوضع الافتراضي.", "نجاح");
        }

        private void ShowGamingLog_Click(object sender, EventArgs e)
        {
            try
            {
                string logPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "202020Pro", "gaming_log.txt"
                );

                if (File.Exists(logPath))
                    System.Diagnostics.Process.Start("notepad.exe", logPath);
                else
                    MessageBox.Show("لا يوجد سجل حتى الآن.", "سجل فارغ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("تعذر فتح السجل.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }

}