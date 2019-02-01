using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class ExplanationControl : UserControl
    {
        public ExplanationControl(ExplanationField field)
        {
            //Initialize
            InitializeComponent();

            //Setup
            titleLabel.Text = field.Name;
            explanationLabel.Text = field.Explanation;
            explanationLabel.Visible = !string.IsNullOrEmpty(field.Explanation);
        }
    }
}
