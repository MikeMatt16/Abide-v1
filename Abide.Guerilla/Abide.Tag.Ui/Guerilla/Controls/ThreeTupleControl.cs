namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class ThreeTupleControl : GuerillaControl
    {
        public string[] TupleValue
        {
            get { return new string[] { aTextBox.Text, bTextBox.Text, cTextBox.Text }; }
            set { aTextBox.Text = value[0]; bTextBox.Text = value[1]; cTextBox.Text = value[2]; }
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
        public string LabelC
        {
            get { return cLabel.Text; }
            set { cLabel.Text = value; }
        }
        public bool IsReadOnly
        {
            get { return aTextBox.Enabled || bTextBox.Enabled || cTextBox.Enabled; }
            set { aTextBox.Enabled = value; bTextBox.Enabled = value; cTextBox.Enabled = value; }
        }

        public ThreeTupleControl()
        {
            InitializeComponent();
        }
    }
}
