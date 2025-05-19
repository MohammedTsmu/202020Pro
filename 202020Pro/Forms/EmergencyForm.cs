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
    public partial class EmergencyForm : Form
    {
        public bool IsAuthorized { get; private set; } = false;

        public EmergencyForm()
        {
            InitializeComponent();
            this.Text = "تأكيد الطوارئ";
            this.Width = 300;
            this.Height = 150;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lbl = new Label
            {
                Text = "أدخل رمز الطوارئ:",
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
                Text = "تأكيد",
                Dock = DockStyle.Top
            };

            btnConfirm.Click += (s, e) =>
            {
                if (txtPassword.Text == "911") // الرمز السري للطوارئ
                {
                    IsAuthorized = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("رمز غير صحيح", "رفض", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            Controls.Add(btnConfirm);
            Controls.Add(txtPassword);
            Controls.Add(lbl);
        }
    }
}
