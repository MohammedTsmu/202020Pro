namespace _202020Pro.Forms
{
    partial class BreakForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BreakForm));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.message = new DevExpress.XtraEditors.LabelControl();
            this.countdownLabel = new DevExpress.XtraEditors.LabelControl();
            this.btnEmergency = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // message
            // 
            this.message.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.message.Appearance.Font = new System.Drawing.Font("LBC", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.message.Appearance.ForeColor = System.Drawing.Color.White;
            this.message.Appearance.Options.UseBackColor = true;
            this.message.Appearance.Options.UseFont = true;
            this.message.Appearance.Options.UseForeColor = true;
            this.message.Appearance.Options.UseTextOptions = true;
            this.message.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.message.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom;
            this.message.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.message.Dock = System.Windows.Forms.DockStyle.Top;
            this.message.Location = new System.Drawing.Point(0, 0);
            this.message.Name = "message";
            this.message.Padding = new System.Windows.Forms.Padding(0, 20, 0, 20);
            this.message.Size = new System.Drawing.Size(1200, 372);
            this.message.TabIndex = 0;
            this.message.Text = "👁️ خذ استراحة الآن! انظر بعيداً لمدة 20 ثانية";
            // 
            // countdownLabel
            // 
            this.countdownLabel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.countdownLabel.Appearance.Font = new System.Drawing.Font("LBC", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countdownLabel.Appearance.ForeColor = System.Drawing.Color.White;
            this.countdownLabel.Appearance.Options.UseBackColor = true;
            this.countdownLabel.Appearance.Options.UseFont = true;
            this.countdownLabel.Appearance.Options.UseForeColor = true;
            this.countdownLabel.Appearance.Options.UseTextOptions = true;
            this.countdownLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.countdownLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.countdownLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.countdownLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.countdownLabel.Location = new System.Drawing.Point(0, 372);
            this.countdownLabel.Name = "countdownLabel";
            this.countdownLabel.Padding = new System.Windows.Forms.Padding(0, 20, 0, 20);
            this.countdownLabel.Size = new System.Drawing.Size(1200, 668);
            this.countdownLabel.TabIndex = 1;
            this.countdownLabel.Text = "مؤقت العد التنازلي";
            this.countdownLabel.Visible = false;
            // 
            // btnEmergency
            // 
            this.btnEmergency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEmergency.Appearance.BackColor = System.Drawing.Color.Red;
            this.btnEmergency.Appearance.Font = new System.Drawing.Font("LBC", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmergency.Appearance.Options.UseBackColor = true;
            this.btnEmergency.Appearance.Options.UseFont = true;
            this.btnEmergency.Appearance.Options.UseTextOptions = true;
            this.btnEmergency.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.btnEmergency.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.btnEmergency.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnEmergency.ImageOptions.SvgImage")));
            this.btnEmergency.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnEmergency.Location = new System.Drawing.Point(1088, 12);
            this.btnEmergency.Name = "btnEmergency";
            this.btnEmergency.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnEmergency.Size = new System.Drawing.Size(100, 40);
            toolTipItem1.Appearance.Font = new System.Drawing.Font("LBC", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            toolTipItem1.Appearance.Options.UseFont = true;
            toolTipItem1.Appearance.Options.UseTextOptions = true;
            toolTipItem1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            toolTipItem1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            toolTipItem1.Text = "الغاء شاشة الراحة لحالات الطوارئ فقط!";
            superToolTip1.Items.Add(toolTipItem1);
            this.btnEmergency.SuperTip = superToolTip1;
            this.btnEmergency.TabIndex = 2;
            this.btnEmergency.Text = "طوارئ؟";
            this.btnEmergency.Click += new System.EventHandler(this.btnEmergency_Click);
            // 
            // BreakForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 731);
            this.Controls.Add(this.btnEmergency);
            this.Controls.Add(this.countdownLabel);
            this.Controls.Add(this.message);
            this.Font = new System.Drawing.Font("LBC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "BreakForm";
            this.Opacity = 0.9D;
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BreakForm";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BreakForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl message;
        private DevExpress.XtraEditors.LabelControl countdownLabel;
        private DevExpress.XtraEditors.SimpleButton btnEmergency;
    }
}