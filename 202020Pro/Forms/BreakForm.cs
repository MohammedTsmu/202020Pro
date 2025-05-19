using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _202020Pro.Forms
{
    public partial class BreakForm : Form
    {
        private Timer breakTimer;

        public BreakForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.BackColor = Color.Black;
            this.Opacity = 0.8;

            Label message = new Label
            {
                Text = "👁️ خذ استراحة الآن! انظر بعيداً لمدة 20 ثانية",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(message);

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
    }
}