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
using DevExpress.XtraExport.Xls;


namespace _202020Pro.Forms
{
    public partial class BreakForm : Form
    {
        private Timer breakTimer;
        private int countdownSeconds = 20;
        
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

        #region تهيئة النموذج
            // إعدادات النموذج
            InitializeComponent();

            // إعدادات النافذة
            this.BackColor = ColorTranslator.FromHtml(AppConfig.BreakBackgroundColor);

            // تشغيل صوت التنبيه
            AppUtilities.PlayReminderSound();
            StartFocusLoop(); // بدء حلقة التركيز

            //// إعدادات الرسالة
            message.ForeColor = ColorTranslator.FromHtml(AppConfig.BreakTextColor);
            message.Font = new Font(AppConfig.BreakFontFamily, AppConfig.BreakFontSize, FontStyle.Bold);
            message.BackColor = Color.Transparent; // لجعل الخلفية شفافة

            
            //اعدادات العد التنازلي
            if (AppConfig.BreakCountdownEnabled)
            {
                countdownLabel.ForeColor = ColorTranslator.FromHtml(AppConfig.BreakTextColor);
                countdownLabel.Font = new Font(AppConfig.BreakFontFamily, AppConfig.BreakFontSize - 4, FontStyle.Regular);
                countdownLabel.AutoSize = true;
                countdownLabel.BackColor = Color.Transparent;
                

                countdownLabel.Text = $"⏳ {countdownSeconds} ثانية متبقية";
            }


            // إضافة زر الطوارئ
            btnEmergency.BackColor = Color.FromArgb(180, Color.Red); // شبه شفاف
            btnEmergency.ForeColor = Color.White;
            #endregion

        #region العد التنازلي والمؤقت
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
            }
            else
            {
                Console.WriteLine($"🕐 عد تنازلي (غير ظاهر): {countdownSeconds} ثانية");
            }

            countdownSeconds--;
        }
        #endregion

        #region زر الطوارئ
        private void btnEmergency_Click(object sender, EventArgs e)
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
        #endregion


        // نحدد المفاتيح المسموح بها فقط
        private bool IsAllowedKey(Keys key)
        {
            // نسمح فقط بـ Ctrl+E مثلاً أو مفتاح الطوارئ حسب ما تختار لاحقاً
            return false; // لا نسمح بأي مفتاح الآن
        }

        #region التحكم في الإغلاق
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
        #endregion

        #region حلقة التركيز
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
        #endregion

        private void BreakForm_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml(AppConfig.BreakBackgroundColor);

            message.Text = "👁️ خذ استراحة الآن! انظر بعيداً لمدة 20 ثانية";
            message.Font = new Font(AppConfig.BreakFontFamily, AppConfig.BreakFontSize, FontStyle.Bold);
            message.ForeColor = ColorTranslator.FromHtml(AppConfig.BreakTextColor);

            countdownLabel.Visible = AppConfig.BreakCountdownEnabled;
            if (AppConfig.BreakCountdownEnabled)
            {
                countdownLabel.Text = $"⏳ {countdownSeconds} ثانية متبقية";
                countdownLabel.Font = new Font(AppConfig.BreakFontFamily, AppConfig.BreakFontSize - 4);
                countdownLabel.ForeColor = ColorTranslator.FromHtml(AppConfig.BreakTextColor);
            }
            else
            {
                countdownLabel.Visible = false;
            }
        }
    }
}