using System;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class StringIdControl : GuerillaControl
    {
        public StringIdControl(Field field) : this()
        {
            Field = field;
        }
        private StringIdControl()
        {
            InitializeComponent();
        }
        protected override void OnFieldChanged(EventArgs e)
        {
            base.OnFieldChanged(e);
            stringTextBox.Text = Field?.Value.ToString() ?? string.Empty;
        }
        private void stringTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Field != null) Field.Value = stringTextBox.Text;
        }
    }
}
