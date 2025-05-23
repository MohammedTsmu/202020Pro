using System;
using System.Windows.Forms;
using _202020Pro.Models;

namespace _202020Pro.Forms
{
    public partial class EmergencyForm : Form
    {
        public bool IsAuthorized { get; private set; } = false;

        public EmergencyForm()
        {
            InitializeComponent();
            this.AcceptButton = btnConfirm; // ✅ لتفعيل زر Enter

            // ❌ لتفعيل زر Esc
            Button btnCancel = new Button
            {
                DialogResult = DialogResult.Cancel,
            };
            this.Controls.Add(btnCancel);

            this.CancelButton = btnCancel; // ❌ لتفعيل زر Esc
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == AppConfig.EmergencyPassword) // الرمز السري للطوارئ
            {
                IsAuthorized = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("رمز غير صحيح", "رفض", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }
    }
}