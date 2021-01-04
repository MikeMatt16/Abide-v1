using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using System;
using System.Windows.Forms;

namespace Abide.Tag.Ui.Guerilla.Controls
{
    public partial class ValueControl : GuerillaControl
    {
        public ValueControl(Field field): this()
        {
            Field = field;
            detailsLabel.Text = field.Details;
        }
        private ValueControl()
        {
            InitializeComponent();
        }
        protected override void OnFieldChanged(EventArgs e)
        {
            base.OnFieldChanged(e);
            valueTextBox.Text = Field?.Value?.ToString() ?? string.Empty;
        }
        private void valueTextBox_TextChanged(object sender, EventArgs e)
        {
            switch (Field.Type)
            {
                case Definition.FieldType.FieldCharInteger:
                    byte value8 = 0;
                    try { value8 = byte.Parse(valueTextBox.Text); } catch { }
                    Field.Value = value8;
                    break;
                case Definition.FieldType.FieldShortInteger:
                    short value16 = 0;
                    try { value16 = short.Parse(valueTextBox.Text); } catch { }
                    Field.Value = value16;
                    break;
                case Definition.FieldType.FieldLongInteger:
                    int value32 = 0;
                    try { value32 = int.Parse(valueTextBox.Text); } catch { }
                    Field.Value = value32;
                    break;
                case Definition.FieldType.FieldAngle:
                    float angle = 0;
                    try { angle = float.Parse(valueTextBox.Text); } catch { }
                    Field.Value = angle;
                    break;
                case Definition.FieldType.FieldTag:
                    TagFourCc tag = "\0\0\0\0";
                    try { tag = valueTextBox.Text; } catch { }
                    Field.Value = tag;
                    break;
                case Definition.FieldType.FieldReal:
                case Definition.FieldType.FieldRealFraction:
                    float real = 0;
                    try { real = float.Parse(valueTextBox.Text); } catch { }
                    Field.Value = real;

                    break;
            }
        }
        private void valueTextBox_MouseHover(object sender, EventArgs e)
        {
            //Check
            if (!string.IsNullOrEmpty(Field.Information))
                informationToolTip.Show(Field.Information, (Control)sender);
        }
    }
}
