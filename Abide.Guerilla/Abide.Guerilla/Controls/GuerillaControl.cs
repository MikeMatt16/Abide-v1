using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class GuerillaControl : UserControl
    {
        public virtual object Value { get; set; } = null;

        public Field Field { get; set; }
        protected ToolTip InformationTooltip
        {
            get { return informationToolTip; }
        }
        public string Title
        {
            get { return nameLabel.Text; }
            set { nameLabel.Text = value; }
        }

        public GuerillaControl()
        {
            InitializeComponent();
        }
    }
}
