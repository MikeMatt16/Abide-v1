namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class StringIdControl : GuerillaControl
    {
        public override object Value
        {
            get { return stringTextBox.Text; }
            set { stringTextBox.Text = value.ToString(); }
        }

        public StringIdControl()
        {
            InitializeComponent();
        }
    }
}
