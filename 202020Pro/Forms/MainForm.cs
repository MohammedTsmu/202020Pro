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
using System.Media;



namespace _202020Pro.Forms
{
    public partial class MainForm : Form
    {
        private System.Windows.Forms.Timer mainTimer;

        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private System.Windows.Forms.Timer gamingTooltipTimer;

        
        private ToolStripMenuItem settingsMenu;


        public MainForm()
        {
            InitializeComponent();
            
            mainTimer = new Timer();
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
            trayMenu = new ContextMenuStrip
            {
                // إعدادات القائمة المنبثقة
                RightToLeft = RightToLeft.Yes // مهم جداً
            };

            trayMenu.Items.Add("🎮 تفعيل/تعطيل وضع الألعاب", null, ToggleGamingMode_Click);
            trayMenu.Items.Add("📄 عرض سجل وضع الألعاب", null, ShowGamingLog_Click);
            trayMenu.Items.Add("🔊 تشغيل صوت تجريبي", null, TestSound_Click);

            trayMenu.Items.Add(new ToolStripSeparator());

            BuildSettingsMenu(); // قائمة الإعدادات بطريقة منظمة
            trayMenu.Items.Add(settingsMenu);

            trayMenu.Items.Add(new ToolStripSeparator());
            trayMenu.Items.Add("🚪 خروج", null, Exit_Click);


            // إنشاء الأيقونة
            trayIcon = new NotifyIcon
            {
                Text = "202020Pro - حماية العين",
                //Icon = SystemIcons.Application,
                Icon = Properties.Resources.ChatGPT_Eye,
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

            if (AppConfig.NightModeEnabled)
            {
                int nowHour = DateTime.Now.Hour;
                int start = AppConfig.NightModeStartHour;
                int end = AppConfig.NightModeEndHour;

                // التحقق من كون الوقت الحالي داخل النطاق
                bool isNightNow = (start < end && nowHour >= start && nowHour < end) ||
                                  (start > end && (nowHour >= start || nowHour < end)); // تغطية النطاق العكسي مثل 22–3

                if (isNightNow)
                {
                    // تخطي الاستراحة أثناء الوضع الليلي
                    return;
                }
            }


            // إذا كان وضع الألعاب مفعلًا، تحقق من الوقت المستهلك
            if (AppSettings.IsGamingMode)
            {
                if (GamingModeManager.TotalUsedToday >= GamingModeManager.AllowedPerDay)
                {
                    // إيقاف وضع الألعاب تلقائيًا بعد انتهاء المدة
                    GamingModeManager.DisableGamingMode();
                    GamingLogger.Log("تم إيقاف وضع الألعاب تلقائيًا بعد انتهاء المدة");
                    AppUtilities.PlayReminderSound();
                }
                else
                {
                    AppUtilities.PlayReminderSound(); // تشغيل صوت التنبيه
                }

                return; // لا تعرض نافذة الاستراحة
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

            // تعيين الصوت الافتراضي إذا لم يكن محددًا مسبقًا
            if (string.IsNullOrEmpty(AppConfig.SelectedSoundName))
            {
                var availableSounds = AppUtilities.GetAvailableSoundNames();
                if (availableSounds.Count > 0)
                {
                    string defaultSound = availableSounds[0];
                    AppConfig.SelectedSoundName = defaultSound;

                    // تشغيل الصوت فورًا + عرض رسالة فقط في أول مرة
                    AppUtilities.PlayReminderSound();

                    if (!Properties.Settings.Default.DefaultSoundAssignedBefore)
                    {
                        MessageBox.Show("تم اختيار صوت افتراضي: " + defaultSound, "الصوت الحالي", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Properties.Settings.Default.DefaultSoundAssignedBefore = true;
                        Properties.Settings.Default.Save();
                    }
                }
            }
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
            TimeSpan sinceLastToggle = DateTime.Now - AppConfig.LastGamingToggleTime;
            if (sinceLastToggle.TotalSeconds < 30)
            {
                MessageBox.Show("لا يمكنك تبديل وضع الألعاب أكثر من مرة كل 30 ثانية.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            AppConfig.LastGamingToggleTime = DateTime.Now; // تحديث الوقت الأخير لتبديل وضع الألعاب 

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
                    AppUtilities.PlayReminderSound(); // تشغيل صوت التنبيه
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
            string input = Microsoft.VisualBasic.Interaction.InputBox("أدخل عدد الدقائق بين كل استراحة (10 - 60):", "مدة الراحة", AppConfig.BreakMinutes.ToString());
            if (int.TryParse(input, out int value) && value >= 10 && value <= 60)
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

        // سجل حالات وضع الألعاب
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

        // 🔘 تفعيل / تعطيل الوضع الليلي
        private void ToggleNightMode_Click(object sender, EventArgs e)
        {
            AppConfig.NightModeEnabled = !AppConfig.NightModeEnabled;
            MessageBox.Show("تم " + (AppConfig.NightModeEnabled ? "تفعيل" : "تعطيل") + " الوضع الليلي.", "الإعدادات");
        }

        // ⏱️ تعديل وقت الوضع الليلي
        private void EditNightModeHours_Click(object sender, EventArgs e)
        {
            string inputStart = Interaction.InputBox("أدخل ساعة البدء (0 إلى 23):", "بداية الوضع الليلي", AppConfig.NightModeStartHour.ToString());
            string inputEnd = Interaction.InputBox("أدخل ساعة الانتهاء (0 إلى 23):", "نهاية الوضع الليلي", AppConfig.NightModeEndHour.ToString());

            if (int.TryParse(inputStart, out int startHour) && int.TryParse(inputEnd, out int endHour))
            {
                if (IsNightHour(startHour) && IsNightHour(endHour))
                {
                    AppConfig.NightModeStartHour = startHour;
                    AppConfig.NightModeEndHour = endHour;
                    MessageBox.Show($"تم تعديل وقت الوضع الليلي: من {startHour}:00 إلى {endHour}:00", "نجاح");
                }
                else
                {
                    MessageBox.Show("الرجاء اختيار ساعات ليلية فقط (مثلاً من 0 إلى 7 أو من 22 إلى 6)", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("تنسيق غير صحيح.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsNightHour(int hour)
        {
            return (hour >= 0 && hour <= 7) || (hour >= 22 && hour <= 23);
        }

        private void TestSound_Click(object sender, EventArgs e)
        {
            AppUtilities.PlayReminderSound();
        }


        // 🎨 تخصيص واجهة الاستراحة
        private void CustomizeBreakScreen_Click(object sender, EventArgs e)
        {
            try
            {
                // 🎨 اختيار لون الخلفية
                ColorDialog bgDialog = new ColorDialog();
                bgDialog.Color = ColorTranslator.FromHtml(AppConfig.BreakBackgroundColor);
                if (bgDialog.ShowDialog() == DialogResult.OK)
                {
                    AppConfig.BreakBackgroundColor = ColorTranslator.ToHtml(bgDialog.Color);
                }

                // 🎨 اختيار لون النص
                ColorDialog fgDialog = new ColorDialog();
                fgDialog.Color = ColorTranslator.FromHtml(AppConfig.BreakTextColor);
                if (fgDialog.ShowDialog() == DialogResult.OK)
                {
                    AppConfig.BreakTextColor = ColorTranslator.ToHtml(fgDialog.Color);
                }

                // 🔤 اختيار الخط
                FontDialog fontDialog = new FontDialog();
                fontDialog.Font = new Font(AppConfig.BreakFontFamily, AppConfig.BreakFontSize);
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    AppConfig.BreakFontFamily = fontDialog.Font.FontFamily.Name;
                    AppConfig.BreakFontSize = (int)fontDialog.Font.Size;
                }

                // 👀 عرض معاينة مباشرة
                ShowBreakPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء التخصيص: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🖼️ معاينة بسيطة للاستراحة
        private void ShowBreakPreview()
        {
            Form preview = new Form();
            preview.Text = "معاينة الاستراحة";
            preview.Size = new Size(700, 400);
            preview.BackColor = ColorTranslator.FromHtml(AppConfig.BreakBackgroundColor);
            preview.StartPosition = FormStartPosition.CenterScreen;

            Label lbl = new Label();
            lbl.Text = "👁️ خذ استراحة الآن! انظر بعيداً لمدة 20 ثانية";
            lbl.ForeColor = ColorTranslator.FromHtml(AppConfig.BreakTextColor);
            lbl.Font = new Font(AppConfig.BreakFontFamily, AppConfig.BreakFontSize, FontStyle.Bold);
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;

            preview.Controls.Add(lbl);
            preview.ShowDialog();
        }

        
        private void ResetBreakScreenDefaults_Click(object sender, EventArgs e)
        {
            // تأكيد إعادة الإعدادات الافتراضية
            if (MessageBox.Show("هل أنت متأكد من أنك تريد إعادة واجهة الاستراحة للوضع الافتراضي؟",
                                "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AppConfig.BreakBackgroundColor = "#000000";
                AppConfig.BreakTextColor = "#FFFFFF";
                AppConfig.BreakFontFamily = "Segoe UI";
                AppConfig.BreakFontSize = 24;

                MessageBox.Show("تمت إعادة واجهة الاستراحة إلى الإعدادات الافتراضية.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("لم يتم إجراء أي تغييرات.", "إلغاء", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // 🧪 عرض واجهة الاستراحة الآن لاغراض المعاينة المباشرة
        private void ShowBreakScreenNow_Click(object sender, EventArgs e)
        {
            try
            {
                BreakForm breakForm = new BreakForm();
                breakForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء عرض واجهة الاستراحة: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ⏳ تشغيل/إيقاف العداد العكسي
        private void ToggleBreakCountdown_Click(object sender, EventArgs e)
        {
            AppConfig.BreakCountdownEnabled = !AppConfig.BreakCountdownEnabled;
            MessageBox.Show("تم " + (AppConfig.BreakCountdownEnabled ? "تفعيل" : "إيقاف") + " العداد العكسي.", "الإعدادات");
        }

        // 🎭 اختيار ثيم جاهز
        private void ApplyTheme(string themeName)
        {
            switch (themeName)
            {
                // 🌙 Midnight Dark
                case "Midnight":
                    AppConfig.BreakBackgroundColor = "#121212"; // رمادي غامق جدًا
                    AppConfig.BreakTextColor = "#F5F5F5";
                    AppConfig.BreakFontFamily = "Segoe UI";
                    AppConfig.BreakFontSize = 24;
                    break;

                // ☀️ Sunny Light
                case "Sunny":
                    AppConfig.BreakBackgroundColor = "#FFFDE7"; // أصفر فاتح دافئ
                    AppConfig.BreakTextColor = "#333333";
                    AppConfig.BreakFontFamily = "Calibri";
                    AppConfig.BreakFontSize = 22;
                    break;

                // 🌊 Ocean Blue
                case "Ocean":
                    AppConfig.BreakBackgroundColor = "#01579B"; // أزرق عميق
                    AppConfig.BreakTextColor = "#E1F5FE";
                    AppConfig.BreakFontFamily = "Verdana";
                    AppConfig.BreakFontSize = 26;
                    break;

                // 🌿 Forest Nature
                case "Forest":
                    AppConfig.BreakBackgroundColor = "#2E7D32"; // أخضر داكن طبيعي
                    AppConfig.BreakTextColor = "#E8F5E9";
                    AppConfig.BreakFontFamily = "Tahoma";
                    AppConfig.BreakFontSize = 24;
                    break;

                // 🎨 Modern Lavender
                case "Lavender":
                    AppConfig.BreakBackgroundColor = "#B39DDB"; // بنفسجي ناعم
                    AppConfig.BreakTextColor = "#212121";
                    AppConfig.BreakFontFamily = "Century Gothic";
                    AppConfig.BreakFontSize = 23;
                    break;

                // 🍬 Candy Pink
                case "Candy":
                    AppConfig.BreakBackgroundColor = "#F8BBD0"; // وردي لطيف
                    AppConfig.BreakTextColor = "#4A148C"; // بنفسجي غامق
                    AppConfig.BreakFontFamily = "Comic Sans MS";
                    AppConfig.BreakFontSize = 22;
                    break;

                case "Sunset":
                    AppConfig.BreakBackgroundColor = "#FF6F61";
                    AppConfig.BreakTextColor = "#FFECD2";
                    AppConfig.BreakFontFamily = "LBC";
                    AppConfig.BreakFontSize = 24;
                    break;

                case "AquaMint":
                    AppConfig.BreakBackgroundColor = "#00BFA5";
                    AppConfig.BreakTextColor = "#E0F2F1";
                    AppConfig.BreakFontFamily = "Verdana";
                    AppConfig.BreakFontSize = 24;
                    break;

                case "RoyalGold":
                    AppConfig.BreakBackgroundColor = "#FFD700";
                    AppConfig.BreakTextColor = "#3E2723";
                    AppConfig.BreakFontFamily = "LBC";
                    AppConfig.BreakFontSize = 25;
                    break;

                case "Galaxy":
                    AppConfig.BreakBackgroundColor = "#1A1A2E";
                    AppConfig.BreakTextColor = "#EDEDED";
                    AppConfig.BreakFontFamily = "Consolas";
                    AppConfig.BreakFontSize = 23;
                    break;

                case "MidnightBlue":
                    AppConfig.BreakBackgroundColor = "#0F1C2E";
                    AppConfig.BreakTextColor = "#D0E8F2";
                    AppConfig.BreakFontFamily = "Segoe UI";
                    AppConfig.BreakFontSize = 23;
                    break;

                case "NebulaPurple":
                    AppConfig.BreakBackgroundColor = "#2C003E";
                    AppConfig.BreakTextColor = "#E0D7F3";
                    AppConfig.BreakFontFamily = "Trebuchet MS";
                    AppConfig.BreakFontSize = 24;
                    break;

            }

            MessageBox.Show("تم تطبيق الثيم: " + themeName, "نجاح");
        }

        // 🎭 اختيار ثيم جاهز
        private void ShowThemeSelector()
        {
            ThemeSelectorForm selector = new ThemeSelectorForm();

            // ✅ هذا هو الربط مع الحدث
            selector.OnThemeSelected += (selectedThemeCode) =>
            {
                ApplyTheme(selectedThemeCode); // هذا موجود عندك من قبل
            };

            selector.ShowDialog();
        }

        private void BuildSoundMenu(ToolStripMenuItem settingsMenu)
        {
            // حذف العناصر القديمة أولاً إذا تم استدعاء الدالة أكثر من مرة
            var existingSoundMenu = settingsMenu.DropDownItems
                .OfType<ToolStripMenuItem>()
                .FirstOrDefault(i => i.Text.Contains("🔈 اختيار صوت التنبيه"));
            if (existingSoundMenu != null)
                settingsMenu.DropDownItems.Remove(existingSoundMenu);

            // قائمة جديدة لاختيار الصوت
            ToolStripMenuItem soundSelectionItem = new ToolStripMenuItem("🔈 اختيار صوت التنبيه");

            foreach (var name in AppUtilities.GetAvailableSoundNames())
            {
                // عنصر رئيسي لكل صوت
                var parentItem = new ToolStripMenuItem(name);
                parentItem.Checked = (name == AppConfig.SelectedSoundName);

                // خيار التحديد
                var selectItem = new ToolStripMenuItem("🟢 اختيار هذا الصوت", null, (s, e) =>
                {
                    AppConfig.SelectedSoundName = name;
                    MessageBox.Show("✅ تم اختيار الصوت: " + name);
                    BuildSoundMenu(settingsMenu); // 🔁 تحديث القائمة لتحديث علامة الصح
                });

                // خيار المعاينة
                var testItem = new ToolStripMenuItem("🔊 تجربة", null, (s, e) =>
                {
                    AppUtilities.PlayReminderSound(name); // نمرر الاسم لاختباره فقط
                });

                parentItem.DropDownItems.Add(selectItem);
                parentItem.DropDownItems.Add(testItem);
                soundSelectionItem.DropDownItems.Add(parentItem);
            }


            settingsMenu.DropDownItems.Add(soundSelectionItem);

            // ✅ إضافة خيار لتجربة الصوت
            settingsMenu.DropDownItems.Add("🔊 تجربة الصوت الحالي", null, (s, e) =>
            {
                AppUtilities.PlayReminderSound();
            });
        }

        private void BuildSettingsMenu()
        {
            settingsMenu = new ToolStripMenuItem("⚙️ الإعدادات")
            {
                RightToLeft = RightToLeft.Yes
            };

            // 🔐 كلمات المرور
            settingsMenu.DropDownItems.Add("🔐 تعديل كلمة مرور الطوارئ", null, ChangeEmergencyPassword_Click);
            settingsMenu.DropDownItems.Add("🕹️ تعديل كلمة مرور الألعاب", null, ChangeGamingPassword_Click);

            settingsMenu.DropDownItems.Add(new ToolStripSeparator());

            // ⏱️ مدد التوقيت
            settingsMenu.DropDownItems.Add("⏱️ تعديل مدة الراحة", null, ChangeBreakInterval_Click);
            settingsMenu.DropDownItems.Add("⌛ تعديل مدة وضع الألعاب", null, ChangeGamingDuration_Click);

            settingsMenu.DropDownItems.Add(new ToolStripSeparator());

            // 🔊 الصوت
            settingsMenu.DropDownItems.Add("🔊 تشغيل/إيقاف الصوت", null, ToggleSound_Click);
            settingsMenu.DropDownItems.Add("🔁 إعادة اعدادات وقت الراحة الافتراضية", null, ResetSettings_Click);

            settingsMenu.DropDownItems.Add(new ToolStripSeparator());

            // 🌙 الوضع الليلي
            settingsMenu.DropDownItems.Add("🌙 تفعيل/تعطيل الوضع الليلي", null, ToggleNightMode_Click);
            settingsMenu.DropDownItems.Add("🕓 تعديل وقت الوضع الليلي", null, EditNightModeHours_Click);

            settingsMenu.DropDownItems.Add(new ToolStripSeparator());

            // 🎨 تصميم واجهة الاستراحة
            settingsMenu.DropDownItems.Add("🎭 اختيار ثيم جاهز", null, (s, e) => ShowThemeSelector());
            settingsMenu.DropDownItems.Add("🎨 تخصيص واجهة الاستراحة", null, CustomizeBreakScreen_Click);
            settingsMenu.DropDownItems.Add("🧹 إعادة واجهة الاستراحة للوضع الافتراضي", null, ResetBreakScreenDefaults_Click);
            settingsMenu.DropDownItems.Add("🧪 عرض واجهة الاستراحة الآن للمعاينة", null, ShowBreakScreenNow_Click);

            settingsMenu.DropDownItems.Add(new ToolStripSeparator());

            // ⏳ العداد العكسي
            settingsMenu.DropDownItems.Add("⏳ تشغيل/إيقاف العداد العكسي", null, ToggleBreakCountdown_Click);

            settingsMenu.DropDownItems.Add(new ToolStripSeparator());

            // 🔈 قائمة الأصوات
            BuildSoundMenu(settingsMenu);

            settingsMenu.DropDownItems.Add(new ToolStripSeparator());
            // حول البرنامج
            settingsMenu.DropDownItems.Add("ℹ️ حول البرنامج", null, ShowAboutForm_Click);
        }

        private void ShowAboutForm_Click(object sender, EventArgs e)
        {
            About aboutForm = new About();
            aboutForm.ShowDialog();
        }

    }

}