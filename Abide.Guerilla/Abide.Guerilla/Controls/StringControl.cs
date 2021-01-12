using Abide.Tag.Guerilla;
using System;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class StringControl : GuerillaControl
    {
        public StringControl(Field field) : this()
        {
            Field = field;
        }
        private StringControl()
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
            if (Field != null)
                switch (Field.Type)
                {
                    case FieldType.FieldString:
                        Field.Value = new String32() { String = stringTextBox.Text };
                        break;
                    case FieldType.FieldLongString:
                        Field.Value = new String256() { String = stringTextBox.Text };
                        break;
                }
        }
    }
}
