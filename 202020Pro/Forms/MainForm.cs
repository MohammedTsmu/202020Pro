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

namespace _202020Pro.Forms
{
    public partial class MainForm : Form
    {
        private System.Windows.Forms.Timer mainTimer;

        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            mainTimer = new Timer();
            //mainTimer.Interval = 20 * 60 * 1000; // 20 دقيقة
            mainTimer.Interval = 1 * 60 * 1000; // 20 دقيقة
            mainTimer.Tick += MainTimer_Tick;
            mainTimer.Start();
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            BreakForm breakForm = new BreakForm();
            breakForm.ShowDialog(); // نافذة لا يمكن تجاوزها بسهولة
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            StartupHelper.AddToStartup();
        }

    }

}