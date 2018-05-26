using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class ExplanationControl : UserControl
    {
        public string Title
        {
            get { return titleLabel.Text; }
            set { titleLabel.Text = value; }
        }
        public string Explanation
        {
            get { return explanationLabel.Text; }
            set
            {
                explanationLabel.Text = value ?? string.Empty;
                explanationLabel.Visible = !string.IsNullOrEmpty(value);
            }
        }

        public ExplanationControl()
        {
            InitializeComponent();
        }
    }
}
