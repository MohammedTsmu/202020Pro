namespace _202020Pro.Forms
{
    partial class GamingModeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GamingModeForm));
            this.lbl = new DevExpress.XtraEditors.LabelControl();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl
            // 
            this.lbl.Appearance.Font = new System.Drawing.Font("LBC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.Appearance.Options.UseFont = true;
            this.lbl.Appearance.Options.UseTextOptions = true;
            this.lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl.Location = new System.Drawing.Point(0, 0);
            this.lbl.Margin = new System.Windows.Forms.Padding(4);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(332, 30);
            this.lbl.TabIndex = 0;
            this.lbl.Text = "أدخل رمز تفعيل الألعاب";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(36, 38);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("LBC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Properties.Appearance.Options.UseTextOptions = true;
            this.txtPassword.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtPassword.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(260, 32);
            this.txtPassword.TabIndex = 1;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Appearance.Font = new System.Drawing.Font("LBC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Appearance.Options.UseFont = true;
            this.btnConfirm.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnConfirm.ImageOptions.SvgImage")));
            this.btnConfirm.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btnConfirm.Location = new System.Drawing.Point(36, 78);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnConfirm.Size = new System.Drawing.Size(260, 43);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "تفعيل الوضع";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // GamingModeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 133);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GamingModeForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "وضع الألعاب";
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lbl;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
    }
}