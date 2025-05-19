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

namespace _202020Pro.Forms
{
    public partial class BreakForm : Form
    {
        private Timer breakTimer;


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
            this.BackColor = Color.Black;
            this.Opacity = 0.8;

            // إعدادات الرسالة
            Label message = new Label
            {
                Text = "👁️ خذ استراحة الآن! انظر بعيداً لمدة 20 ثانية",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            message.Location = new Point(
                (this.ClientSize.Width - message.PreferredWidth) / 2,
                (this.ClientSize.Height - message.PreferredHeight) / 2
            );
            message.Anchor = AnchorStyles.None;
            Controls.Add(message);



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
            breakTimer.Interval = 20000; // 20 ثانية
            breakTimer.Tick += BreakTimer_Tick;
            breakTimer.Start();
        }

        private void BreakTimer_Tick(object sender, EventArgs e)
        {
            breakTimer.Stop();
            this.Close(); // إغلاق بعد انتهاء المدة
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