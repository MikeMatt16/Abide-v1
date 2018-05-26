namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class Vector3Control : GuerillaControl
    {
        public bool IsReadOnly
        {
            get { return iTextBox.Enabled || jTextBox.Enabled || kTextBox.Enabled; }
            set { iTextBox.Enabled = value; jTextBox.Enabled = value; kTextBox.Enabled = value; }
        }

        public Vector3Control()
        {
            InitializeComponent();
        }
    }
}
