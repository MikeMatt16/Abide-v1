namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class TwoTupleControl : GuerillaControl
    {
        public string[] TupleValue
        {
            get { return new string[] { aTextBox.Text, bTextBox.Text }; }
            set { aTextBox.Text = value[0]; bTextBox.Text = value[1]; }
        }
        public string LabelA
        {
            get { return aLabel.Text; }
            set { aLabel.Text = value; }
        }
        public string LabelB
        {
            get { return bLabel.Text; }
            set { bLabel.Text = value; }
        }
        public bool IsReadOnly
        {
            get { return aTextBox.Enabled || bTextBox.Enabled; }
            set { aTextBox.Enabled = value; bTextBox.Enabled = value; }
        }

        public TwoTupleControl()
        {
            InitializeComponent();
        }
    }
}
