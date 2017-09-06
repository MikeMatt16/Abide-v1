using System;
using System.Windows.Forms;

namespace Tag_Data_Editor.Controls
{
    public partial class BitflagsControl : MetaControl
    {
        public EventHandler<FlagsChangedEventArgs> FlagsChanged
        {
            get { return flagsChanged; }
            set { flagsChanged = value; }
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
        public uint Flags
        {
            get { return flags; }
            set { OnFlagsSet(value); }
        }

        private uint flags;
        private EventHandler<FlagsChangedEventArgs> flagsChanged;

        public void AddOption(int value, string name)
        {
            AddOption(new Option(value, name) { ShowValue = false });
        }
        public void AddOption(Option option)
        {
            //Add?
            if (!flagsBox.Items.Contains(option))
                flagsBox.Items.Add(option);
        }
        public BitflagsControl()
        {
            InitializeComponent();
        }

        protected virtual void OnFlagsSet(uint flags)
        {
            //Set
            this.flags = flags;

            //Set Checked
            Option flag = null;
            for (int i = 0; i < flagsBox.Items.Count; i++)
            {
                flag = (Option)flagsBox.Items[i];
                if ((flag.Value & flags) == flag.Value)
                    flagsBox.SetItemChecked(flagsBox.Items.IndexOf(flag), true);
                else flagsBox.SetItemChecked(flagsBox.Items.IndexOf(flag), false);
            }
        }
        private void flagsBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //Setup flags
            bool toggled = e.NewValue == CheckState.Checked;
            Option flag = (Option)flagsBox.Items[e.Index];
            uint flagValue = (uint)flag.Value;
            if (toggled) flags |= flagValue;
            else flags &= ~flagValue;

            //Setup Label
            Label.Text = $"0x{flags:x8}";

            //Throw?
            flagsChanged?.Invoke(this, new FlagsChangedEventArgs(flags));
        }
    }

    public class FlagsChangedEventArgs : EventArgs
    {
        public uint Flags
        {
            get { return flags; }
        }

        private readonly uint flags;

        public FlagsChangedEventArgs(uint flags)
        {
            this.flags = flags;
        }
    }

    public class Option : IComparable<Option>, IEquatable<Option>
    {
        public long Value
        {
            get { return value; }
        }
        public string Name
        {
            get { return name; }
        }
        public bool ShowValue
        {
            get { return showValue; }
            set { showValue = value; }
        }

        private readonly long value;
        private readonly string name;
        private bool showValue = true;

        public Option(long value)
        {
            this.value = value;
            name = string.Empty;
        }
        public Option(long value, string name)
        {
            this.value = value;
            this.name = name;
        }
        public bool Equals(Option other)
        {
            return value.Equals(other.value);
        }
        public int CompareTo(Option other)
        {
            return value.CompareTo(other.value);
        }
        public override string ToString()
        {
            return showValue ? $"{value}: {name}" : name;
        }
        public override bool Equals(object obj)
        {
            if (obj is Option)
                return value.Equals(((Option)obj).value);
            else return false;
        }
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public static implicit operator Option(long value)
        {
            return new Option(value);
        }
        public static implicit operator long(Option option)
        {
            return option.value;
        }
    }
}
