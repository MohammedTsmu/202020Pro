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
    public partial class GamingModeForm : Form
    {
        public GamingModeForm()
        {
            InitializeComponent();
            this.Text = "وضع الألعاب";
            this.Width = 300;
            this.Height = 150;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lbl = new Label
            {
                Text = "أدخل رمز تفعيل الألعاب:",
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter
            };
            TextBox txtPassword = new TextBox
            {
                Dock = DockStyle.Top,
                PasswordChar = '*',
                TextAlign = HorizontalAlignment.Center
            };
            Button btnConfirm = new Button
            {
                Text = "تفعيل الوضع",
                Dock = DockStyle.Top
            };


            btnConfirm.Click += (s, e) =>
            {
                if (txtPassword.Text == "gamer")
                {
                    if (!GamingModeManager.CanEnableGamingMode())
                    {
                        MessageBox.Show("لقد تجاوزت الحد المسموح لوضع الألعاب اليوم.", "مرفوض", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Close();
                        return;
                    }

                    GamingModeManager.EnableGamingMode();
                    MessageBox.Show("تم تفعيل وضع الألعاب بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("رمز خاطئ", "رفض", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };


            Controls.Add(btnConfirm);
            Controls.Add(txtPassword);
            Controls.Add(lbl);
        }
    }
}