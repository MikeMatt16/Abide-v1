namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class FourTupleControl : GuerillaControl
    {
        public string[] TupleValue
        {
            get { return new string[] { aTextBox.Text, bTextBox.Text, cTextBox.Text, dTextBox.Text }; }
            set { aTextBox.Text = value[0]; bTextBox.Text = value[1]; cTextBox.Text = value[2]; dTextBox.Text = value[3]; }
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
        public string LabelD
        {
            get { return dLabel.Text; }
            set { dLabel.Text = value; }
        }

        public FourTupleControl()
        {
            InitializeComponent();
        }
    }
}
