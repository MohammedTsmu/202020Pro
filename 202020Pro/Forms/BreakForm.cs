using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _202020Pro.Models;
using _202020Pro.Forms;
using System.Runtime.InteropServices;


namespace _202020Pro.Forms
{
    public partial class BreakForm : Form
    {
        private Timer breakTimer;
        private int countdownSeconds = 20;
        private Label countdownLabel = null;

        private bool forceClose = false;

        // تعريف الرسائل الخاصة بلوحة المفاتيح والفأرة
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_MBUTTONDOWN = 0x0207;
        private const int WM_MOUSEMOVE = 0x0200;

        
        private Timer focusTimer;



        public BreakForm()
        {
            // إذا كان في وضع الألعاب، لا نعرض نافذة الاستراحة
            if (AppSettings.IsGamingMode)
            {
                // تحقق إذا انتهت المدة المسموحة
                if (GamingModeManager.TotalUsedToday >= GamingModeManager.AllowedPerDay)
                {
                    GamingModeManager.DisableGamingMode();
                    GamingLogger.Log("تم إيقاف وضع الألعاب تلقائيًا بعد انتهاء المدة");
                }
                else
                {
                    // تشغيل إشعار صوتي فقط
                    System.Media.SystemSounds.Exclamation.Play();
                    AppUtilities.PlayReminderSound(); // تشغيل صوت التنبيه

                    // إغلاق نافذة الاستراحة
                    this.Close();
                    return;
                }
            }



            // إعدادات النموذج
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.BackColor = ColorTranslator.FromHtml(AppConfig.BreakBackgroundColor);           
            this.Opacity = 0.8;

            // تشغيل صوت التنبيه
            AppUtilities.PlayReminderSound();
            StartFocusLoop(); // بدء حلقة التركيز

            // إعدادات الرسالة
            Label message = new Label
            {
                Text = "👁️ خذ استراحة الآن! انظر بعيداً لمدة 20 ثانية",
                //ForeColor = Color.White,
                //Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml(AppConfig.BreakTextColor),
                Font = new Font(AppConfig.BreakFontFamily, AppConfig.BreakFontSize, FontStyle.Bold),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            message.Location = new Point(
                (this.ClientSize.Width - message.PreferredWidth) / 2,
                (this.ClientSize.Height - message.PreferredHeight) / 2
            );
            message.Anchor = AnchorStyles.None;
            Controls.Add(message);


            //اعدادات العد التنازلي
            if (AppConfig.BreakCountdownEnabled)
            {
                countdownLabel = new Label
                {
                    ForeColor = ColorTranslator.FromHtml(AppConfig.BreakTextColor),
                    Font = new Font(AppConfig.BreakFontFamily, AppConfig.BreakFontSize - 4, FontStyle.Regular),
                    AutoSize = true,
                    BackColor = Color.Transparent
                };

                countdownLabel.Text = $"⏳ {countdownSeconds} ثانية متبقية";
                countdownLabel.Location = new Point(
                    (this.ClientSize.Width - countdownLabel.PreferredWidth) / 2,
                    message.Location.Y + message.Height + 20
                );

                // اعادة ضبط موضع العد التنازلي
                //countdownLabel.Anchor = AnchorStyles.None;
                Controls.Add(countdownLabel);
            }




            // إضافة زر الطوارئ
            Button btnEmergency = new Button
            {
                Text = "طوارئ؟",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(180, Color.Red), // شبه شفاف
                ForeColor = Color.White,
                Width = 90,
                Height = 30,
                FlatStyle = FlatStyle.Flat,
                Top = 10,
                Left = this.ClientSize.Width - 100,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnEmergency.FlatAppearance.BorderSize = 0;
            btnEmergency.Click += BtnEmergency_Click;
            Controls.Add(btnEmergency);


            breakTimer = new Timer();
            //breakTimer.Interval = 20000; // 20 ثانية
            breakTimer.Interval = 1000; // 1 ثانية
            breakTimer.Tick += BreakTimer_Tick;
            breakTimer.Start();
        }

        private void BreakTimer_Tick(object sender, EventArgs e)
        {
            if (countdownSeconds <= 0)
            {
                forceClose = true;
                this.Close();
                return; // ضروري جداً لمنع الاستمرار
            }

            if (AppConfig.BreakCountdownEnabled && countdownLabel != null)
            {
                countdownLabel.Text = $"⏳ {countdownSeconds} ثانية متبقية";
                countdownLabel.Left = (this.ClientSize.Width - countdownLabel.PreferredWidth) / 2;
            }
            else
            {
                Console.WriteLine($"🕐 عد تنازلي (غير ظاهر): {countdownSeconds} ثانية");
            }

            countdownSeconds--;
        }

        private void BtnEmergency_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            this.Enabled = false;

            focusTimer.Stop(); // ⛔️ إيقاف مؤقت التركيز حتى لا يسحب الفوكس

            using (EmergencyForm emergencyForm = new EmergencyForm())
            {
                emergencyForm.StartPosition = FormStartPosition.CenterScreen;
                emergencyForm.ShowDialog();

                if (emergencyForm.IsAuthorized)
                {
                    forceClose = true;
                    this.Close();
                    return; // لا نرجع لتشغيل المؤقت
                }
            }

            this.Enabled = true;
            this.TopMost = true;
            focusTimer.Start(); // ✅ إعادة تشغيل المؤقت فقط إذا لم يتم الإغلاق
        }



        // نحدد المفاتيح المسموح بها فقط
        private bool IsAllowedKey(Keys key)
        {
            // نسمح فقط بـ Ctrl+E مثلاً أو مفتاح الطوارئ حسب ما تختار لاحقاً
            return false; // لا نسمح بأي مفتاح الآن
        }

        protected override void WndProc(ref Message m)
        {
            // منع الكيبورد
            if (m.Msg == WM_KEYDOWN || m.Msg == WM_SYSKEYDOWN)
            {
                Keys key = (Keys)(int)m.WParam & Keys.KeyCode;
                if (!IsAllowedKey(key))
                {
                    return; // تجاهل المفتاح
                }
            }

            // منع الضغط على الماوس (يسار، يمين، أوسط)
            if (m.Msg == WM_LBUTTONDOWN || m.Msg == WM_RBUTTONDOWN || m.Msg == WM_MBUTTONDOWN)
            {
                return; // تجاهل الضغط
            }

            base.WndProc(ref m); // استدعاء السلوك الطبيعي لباقي الرسائل
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_SYSMENU = 0x80000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | 0x200; // Prevent Alt+F4
                cp.Style &= ~WS_SYSMENU; // Remove system menu (no close)
                return cp;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!forceClose)
            {
                e.Cancel = true; // لا نسمح بالإغلاق من Alt+F4
            }

            base.OnFormClosing(e);
        }

        private void StartFocusLoop()
        {
            focusTimer = new Timer();
            focusTimer.Interval = 500; // كل نصف ثانية
            focusTimer.Tick += (s, e) =>
            {
                if (!this.Focused)
                {
                    this.Activate();
                }
            };
            focusTimer.Start();
        }
    }
}