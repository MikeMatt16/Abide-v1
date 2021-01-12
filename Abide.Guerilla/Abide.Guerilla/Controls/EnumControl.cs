using System;
using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class EnumControl : GuerillaControl
    {
        public EnumControl(Field field) : this()
        {
            Field = field;
            detailsLabel.Text = field.Details;
        }
        private EnumControl()
        {
            InitializeComponent();
        }
        protected override void OnFieldChanged(EventArgs e)
        {
            base.OnFieldChanged(e);
            enumComboBox.Items.Clear();
            int selectedValue = -1;
            switch (Field.Type)
            {
                case FieldType.FieldCharEnum:
                    foreach (var option in ((CharEnumField)Field).Options)
                        enumComboBox.Items.Add(new EnumValue(option.Index, option.Name));
                    selectedValue = (byte)Field.Value;
                    break;
                case FieldType.FieldEnum:
                    foreach(var option in ((EnumField)Field).Options)
                        enumComboBox.Items.Add(new EnumValue(option.Index, option.Name));
                    selectedValue = (short)Field.Value;
                    break;
                case FieldType.FieldLongEnum:
                    foreach (var option in ((LongEnumField)Field).Options)
                        enumComboBox.Items.Add(new EnumValue(option.Index, option.Name));
                    selectedValue = (int)Field.Value;
                    break;
            }

            //Find item
            foreach (EnumValue obj in enumComboBox.Items)
                if(selectedValue == obj.Value)
                {
                    enumComboBox.SelectedItem = obj;
                    return;
                }

            //Add
            enumComboBox.SelectedIndex = enumComboBox.Items.Add(new EnumValue(selectedValue, string.Empty));
        }
        private void enumComboBox_MouseHover(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Field.Information))
                informationToolTip.Show(Field.Information, (Control)sender);
        }
        private void enumComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Field.Type)
            {
                case FieldType.FieldCharEnum:
                    if (enumComboBox.SelectedItem is EnumValue charValue)
                        Field.Value = (byte)charValue.Value;
                    else Field.Value = (byte)enumComboBox.SelectedIndex;
                    break;
                case FieldType.FieldEnum:
                    if (enumComboBox.SelectedItem is EnumValue value)
                        Field.Value = (short)value.Value;
                    else Field.Value = (short)enumComboBox.SelectedIndex;
                    break;
                case FieldType.FieldLongEnum:
                    if (enumComboBox.SelectedItem is EnumValue intValue)
                        Field.Value = intValue.Value;
                    else Field.Value = enumComboBox.SelectedIndex;
                    break;
            }
        }

        private class EnumValue
        {
            public int Value { get; }
            public string Text { get; }

            public EnumValue(int value, string text)
            {
                Value = value;
                Text = text;
            }

            public override string ToString()
            {
                return Text ?? string.Empty;
            }
        }
    }
}
