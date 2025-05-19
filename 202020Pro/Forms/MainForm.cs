using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _202020Pro.Forms;
using _202020Pro.Models;

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
            //this.WindowState = FormWindowState.Minimized;
            //this.ShowInTaskbar = false;

            mainTimer = new Timer();
            //mainTimer.Interval = 20 * 60 * 1000; // 20 دقيقة
            mainTimer.Interval = 1 * 60 * 1000; // 20 دقيقة
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

            // إنشاء الأيقونة
            trayIcon = new NotifyIcon
            {
                Text = "202020Pro - حماية العين",
                Icon = SystemIcons.Application,
                ContextMenuStrip = trayMenu,
                Visible = true
            };
        }

        //private void MainTimer_Tick(object sender, EventArgs e)
        //{
        //    BreakForm breakForm = new BreakForm();
        //    breakForm.ShowDialog(); // نافذة لا يمكن تجاوزها بسهولة
        //}
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
            }
            else
            {
                GamingModeForm gmForm = new GamingModeForm();
                gmForm.ShowDialog();
            }
        }


        //private void Exit_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (trayIcon != null)
        //        {
        //            trayIcon.Visible = false;
        //            trayIcon.Dispose();
        //            trayIcon = null;
        //        }

        //        Application.ExitThread(); // الأفضل من Exit()
        //    }
        //    catch (Exception ex)
        //    {
        //        // تسجيل أو عرض الخطأ (اختياري)
        //        MessageBox.Show("خطأ أثناء الإغلاق: " + ex.Message);
        //    }
        //}

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


    }

}