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

            this.AcceptButton = btnConfirm;

            // ❌ لتفعيل زر Esc
            Button btnCancel = new Button
            {
                Text = "إلغاء",
                DialogResult = DialogResult.Cancel,
                Width = 80,
                Height = 30,
                Anchor = AnchorStyles.Bottom,
                Left = 30,
                Top = btnConfirm.Bottom + 10 // حسب ترتيبك
            };
            this.Controls.Add(btnCancel);

            this.CancelButton = btnCancel; // ❌ لتفعيل زر Esc

            this.ActiveControl = txtPassword;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //if (txtPassword.Text == "gamer")
            if (txtPassword.Text == AppConfig.GamingPassword)
            {
                if (!GamingModeManager.CanEnableGamingMode())
                {
                    MessageBox.Show("لقد تجاوزت الحد المسموح لوضع الألعاب اليوم.", "مرفوض", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                GamingModeManager.EnableGamingMode();
                GamingLogger.Log("تم تفعيل وضع الألعاب");
                MessageBox.Show("تم تفعيل وضع الألعاب بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("رمز خاطئ", "رفض", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}