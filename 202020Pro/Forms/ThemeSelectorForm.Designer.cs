namespace _202020Pro.Forms
{
    partial class ThemeSelectorForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelDark = new System.Windows.Forms.Panel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.panelLight = new System.Windows.Forms.Panel();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelNature = new System.Windows.Forms.Panel();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.panelCalmBlue = new System.Windows.Forms.Panel();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelDark.SuspendLayout();
            this.panelLight.SuspendLayout();
            this.panelNature.SuspendLayout();
            this.panelCalmBlue.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panelCalmBlue, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelNature, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelLight, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelDark, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1062, 673);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelDark
            // 
            this.panelDark.BackColor = System.Drawing.Color.Black;
            this.panelDark.Controls.Add(this.simpleButton1);
            this.panelDark.Controls.Add(this.labelControl1);
            this.panelDark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDark.Location = new System.Drawing.Point(6, 6);
            this.panelDark.Name = "panelDark";
            this.panelDark.Size = new System.Drawing.Size(520, 326);
            this.panelDark.TabIndex = 0;
            this.panelDark.Tag = "Dark";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("LBC", 12F);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(205, 86);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(42, 26);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "غامق";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("LBC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.simpleButton1.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.simpleButton1.Location = new System.Drawing.Point(99, 118);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.simpleButton1.Size = new System.Drawing.Size(252, 45);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Tag = "";
            this.simpleButton1.Text = "اختيار الثيم";
            this.simpleButton1.Click += new System.EventHandler(this.ThemeButton_Click);
            // 
            // panelLight
            // 
            this.panelLight.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelLight.Controls.Add(this.simpleButton2);
            this.panelLight.Controls.Add(this.labelControl2);
            this.panelLight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLight.Location = new System.Drawing.Point(535, 6);
            this.panelLight.Name = "panelLight";
            this.panelLight.Size = new System.Drawing.Size(521, 326);
            this.panelLight.TabIndex = 1;
            this.panelLight.Tag = "Light";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("LBC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.simpleButton2.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.simpleButton2.Location = new System.Drawing.Point(99, 118);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.simpleButton2.Size = new System.Drawing.Size(252, 45);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "اختيار الثيم";
            this.simpleButton2.Click += new System.EventHandler(this.ThemeButton_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("LBC", 12F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(205, 86);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(35, 26);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "فاتح";
            // 
            // panelNature
            // 
            this.panelNature.BackColor = System.Drawing.Color.Lime;
            this.panelNature.Controls.Add(this.simpleButton3);
            this.panelNature.Controls.Add(this.labelControl3);
            this.panelNature.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNature.Location = new System.Drawing.Point(6, 341);
            this.panelNature.Name = "panelNature";
            this.panelNature.Size = new System.Drawing.Size(520, 326);
            this.panelNature.TabIndex = 2;
            this.panelNature.Tag = "Nature";
            // 
            // simpleButton3
            // 
            this.simpleButton3.Appearance.Font = new System.Drawing.Font("LBC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton3.Appearance.Options.UseFont = true;
            this.simpleButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.simpleButton3.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.simpleButton3.Location = new System.Drawing.Point(99, 118);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.simpleButton3.Size = new System.Drawing.Size(252, 45);
            this.simpleButton3.TabIndex = 1;
            this.simpleButton3.Text = "اختيار الثيم";
            this.simpleButton3.Click += new System.EventHandler(this.ThemeButton_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("LBC", 12F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(205, 86);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(58, 26);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "طبيعي";
            // 
            // panelCalmBlue
            // 
            this.panelCalmBlue.BackColor = System.Drawing.Color.SteelBlue;
            this.panelCalmBlue.Controls.Add(this.simpleButton4);
            this.panelCalmBlue.Controls.Add(this.labelControl4);
            this.panelCalmBlue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCalmBlue.Location = new System.Drawing.Point(535, 341);
            this.panelCalmBlue.Name = "panelCalmBlue";
            this.panelCalmBlue.Size = new System.Drawing.Size(521, 326);
            this.panelCalmBlue.TabIndex = 3;
            this.panelCalmBlue.Tag = "Calm Blue";
            // 
            // simpleButton4
            // 
            this.simpleButton4.Appearance.Font = new System.Drawing.Font("LBC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton4.Appearance.Options.UseFont = true;
            this.simpleButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.simpleButton4.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.simpleButton4.Location = new System.Drawing.Point(99, 118);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.simpleButton4.Size = new System.Drawing.Size(252, 45);
            this.simpleButton4.TabIndex = 1;
            this.simpleButton4.Text = "اختيار الثيم";
            this.simpleButton4.Click += new System.EventHandler(this.ThemeButton_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("LBC", 12F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(205, 86);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(29, 26);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "ازرق";
            // 
            // ThemeSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 673);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("LBC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ThemeSelectorForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "اختيار ثيم";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelDark.ResumeLayout(false);
            this.panelDark.PerformLayout();
            this.panelLight.ResumeLayout(false);
            this.panelLight.PerformLayout();
            this.panelNature.ResumeLayout(false);
            this.panelNature.PerformLayout();
            this.panelCalmBlue.ResumeLayout(false);
            this.panelCalmBlue.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelDark;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.Panel panelLight;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Panel panelCalmBlue;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.Panel panelNature;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}