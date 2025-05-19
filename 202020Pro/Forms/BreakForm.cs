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


namespace _202020Pro.Forms
{
    public partial class BreakForm : Form
    {
        private Timer breakTimer;
        private int countdownSeconds = 20;
        private Label countdownLabel = null;


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

            // تشغيل صوت التنبيه
            AppUtilities.PlayReminderSound();

            // إعدادات النموذج
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            //this.BackColor = Color.Black;
            this.BackColor = ColorTranslator.FromHtml(AppConfig.BreakBackgroundColor);           
            this.Opacity = 0.8;


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

            // إعدادات العد التنازلي
            //Label countdownLabel = new Label
            //{
            //    ForeColor = ColorTranslator.FromHtml(AppConfig.BreakTextColor),
            //    Font = new Font(AppConfig.BreakFontFamily, AppConfig.BreakFontSize - 4, FontStyle.Regular),
            //    AutoSize = true,
            //    BackColor = Color.Transparent
            //};




            //countdownLabel = new Label
            //{
            //    ForeColor = ColorTranslator.FromHtml(AppConfig.BreakTextColor),
            //    Font = new Font(AppConfig.BreakFontFamily, AppConfig.BreakFontSize - 4, FontStyle.Regular),
            //    AutoSize = true,
            //    BackColor = Color.Transparent
            //};

            //countdownLabel.Location = new Point(
            //    (this.ClientSize.Width - countdownLabel.PreferredWidth) / 2,
            //    message.Location.Y + message.Height + 20
            //);
            //Controls.Add(countdownLabel);
            //// إعدادات العد التنازلي
            ////countdownLabel.Text = $"⏳ {countdownSeconds} ثانية متبقية";
            ///

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

        //private void BreakTimer_Tick(object sender, EventArgs e)
        //{
        //    breakTimer.Stop();
        //    this.Close(); // إغلاق بعد انتهاء المدة
        //}

        //private void BreakTimer_Tick(object sender, EventArgs e)
        //{
        //    if (AppConfig.BreakCountdownEnabled && countdownLabel != null)
        //    {
        //        countdownLabel.Text = $"⏳ {countdownSeconds} ثانية متبقية";
        //        countdownLabel.Left = (this.ClientSize.Width - countdownLabel.PreferredWidth) / 2;
        //    }

        //    countdownSeconds--;

        //    countdownLabel.Text = $"⏳ {countdownSeconds} ثانية متبقية";
        //    countdownLabel.Left = (this.ClientSize.Width - countdownLabel.PreferredWidth) / 2;

        //    if (countdownSeconds <= 0)
        //    {
        //        breakTimer.Stop();
        //        this.Close();
        //    }
        //}
        private void BreakTimer_Tick(object sender, EventArgs e)
        {
            if (AppConfig.BreakCountdownEnabled && countdownLabel != null)
            {
                countdownLabel.Text = $"⏳ {countdownSeconds} ثانية متبقية";
                countdownLabel.Left = (this.ClientSize.Width - countdownLabel.PreferredWidth) / 2;
            }

            countdownSeconds--;

            if (countdownSeconds <= 0)
            {
                breakTimer.Stop();
                this.Close();
            }
        }




        private void BtnEmergency_Click(object sender, EventArgs e)
        {
            this.TopMost = false; // نوقف الواجهة فوق الكل مؤقتاً

            using (EmergencyForm emergencyForm = new EmergencyForm())
            {
                emergencyForm.ShowDialog();
                if (emergencyForm.IsAuthorized)
                {
                    breakTimer.Stop(); // إيقاف المؤقت
                    this.Close();     // إغلاق نافذة الاستراحة
                }
            }

            this.TopMost = true; // نعيدها فوق الكل
        }


    }
}