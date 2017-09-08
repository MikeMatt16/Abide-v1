using System;

namespace HUD_Editor.Controls
{
    public partial class ValueControl : MetaControl
    {
        public EventHandler<ValueChangedEventArgs> ValueChanged
        {
            get { return valueChanged; }
            set { valueChanged = value; }
        }
        public override string Type
        {
            get { return typeLabel.Text; }
            set { typeLabel.Text = value; }
        }
        public override string ControlName
        {
            get { return nameLabel.Text; }
            set { nameLabel.Text = value; }
        }
        public string Value
        {
            get { return valueBox.Text; }
            set { valueBox.Text = value; }
        }

        private EventHandler<ValueChangedEventArgs> valueChanged;

        public ValueControl()
        {
            InitializeComponent();
        }

        private void valueBox_TextChanged(object sender, EventArgs e)
        {
            valueChanged?.Invoke(this, new ValueChangedEventArgs(valueBox.Text));
            Label.Text = valueBox.Text;
        }
    }

    public class ValueChangedEventArgs : EventArgs
    {
        public string Value
        {
            get { return valueString; }
        }

        private readonly string valueString;

        public ValueChangedEventArgs(string valueString)
        {
            this.valueString = valueString;
        }
    }
}
