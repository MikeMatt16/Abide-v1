using System;

namespace Tag_Data_Editor.Controls
{
    public partial class EnumControl : MetaControl
    {
        public EventHandler<OptionSelectEventArgs> OptionSelected
        {
            get { return optionSelected; }
            set { optionSelected = value; }
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
        public Option SelectedOption
        {
            get { return dropDownBox.SelectedItem as Option; }
            set
            {
                //Add?
                if (!dropDownBox.Items.Contains(value))
                    AddOption(value);
                dropDownBox.SelectedItem = value;
            }
        }

        private EventHandler<OptionSelectEventArgs> optionSelected;

        public void AddOption(int value, string name)
        {
            AddOption(new Option(value, name));
        }
        public void AddOption(Option option)
        {
            //Add?
            if (!dropDownBox.Items.Contains(option))
                dropDownBox.Items.Add(option);
        }
        public EnumControl()
        {
            InitializeComponent();
        }

        private void dropDownBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Prepare
            var args = new OptionSelectEventArgs((Option)dropDownBox.SelectedItem);
            Label.Text = args.SelectedOption.Name;

            //Invoke
            optionSelected?.Invoke(this, args);
        }
    }

    public class OptionSelectEventArgs : EventArgs
    {
        public Option SelectedOption
        {
            get { return selectedOption; }
        }

        private readonly Option selectedOption;

        public OptionSelectEventArgs(Option selectedOption)
        {
            this.selectedOption = selectedOption;
        }
    }
}
