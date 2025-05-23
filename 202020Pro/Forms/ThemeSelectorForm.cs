using System;
using System.Windows.Forms;


namespace _202020Pro.Forms
{

    public partial class ThemeSelectorForm : Form
    {
        public event Action<string> OnThemeSelected;

        public ThemeSelectorForm()
        {
            InitializeComponent();
        }


        private void ThemeButton_Click(object sender, EventArgs e)
        {
            // نحاول صعود التسلسل حتى نجد أول عنصر فيه Tag
            Control current = sender as Control;

            while (current != null && string.IsNullOrEmpty(current.Tag?.ToString()))
            {
                current = current.Parent;
            }

            if (current != null && !string.IsNullOrEmpty(current.Tag?.ToString()))
            {
                string themeCode = current.Tag.ToString();
                OnThemeSelected?.Invoke(themeCode); // إبلاغ MainForm
                this.Close();
            }
            else
            {
                MessageBox.Show("⚠️ لم يتم العثور على كود الثيم.");
            }
        }

    }
}