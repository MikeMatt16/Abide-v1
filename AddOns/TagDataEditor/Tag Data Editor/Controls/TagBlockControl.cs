using System;
using System.Windows.Forms;

namespace Tag_Data_Editor.Controls
{
    public partial class TagBlockControl : MetaControl
    {
        public EventHandler<BlockOptionEventArgs> SelectedBlockChanged
        {
            get { return blockChanged; }
            set { blockChanged = value; }
        }
        public override string Type
        {
            get { return string.Empty; }
            set { }
        }
        public override string ControlName
        {
            get { return nameLabel.Text; }
            set { nameLabel.Text = value; }
        }
        public FlowLayoutPanel Contents
        {
            get { return nestedPanel; }
        }
        public BlockOption SelectedBlock
        {
            get { return indexSelectBox.SelectedItem as BlockOption; }
            set
            {
                if (indexSelectBox.Items.Contains(value))
                    indexSelectBox.SelectedItem = value;
            }
        }

        private EventHandler<BlockOptionEventArgs> blockChanged;

        public void Clear()
        {
            //Reset
            indexSelectBox.DataSource = null;
            indexSelectBox.DisplayMember = null;

            //Disable
            indexSelectBox.Enabled = false;
            nestedPanel.Enabled = false;
        }
        public void SetBlocks(BlockOption[] options)
        {
            //Check
            if (options.Length > 0)
            {
                //Create Data binding
                indexSelectBox.DataSource = options;
                indexSelectBox.DisplayMember = "Display";

                //Enable
                indexSelectBox.Enabled = true;
                nestedPanel.Enabled = true;
            }
            else Clear();
        }
        public TagBlockControl()
        {
            InitializeComponent();
        }

        private void indexSelectBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            blockChanged?.Invoke(this, new BlockOptionEventArgs(SelectedBlock));
            Label.Text = SelectedBlock.Label == null || string.IsNullOrEmpty(SelectedBlock.Label.Text) ? SelectedBlock.Name : SelectedBlock.Label.Text;
        }
    }

    public class BlockOptionEventArgs : EventArgs
    {
        public BlockOption Option
        {
            get { return blockOption; }
        }

        private readonly BlockOption blockOption;

        public BlockOptionEventArgs(BlockOption blockOption)
        {
            this.blockOption = blockOption;
        }
    }

    public class BlockOption : IComparable<BlockOption>, IEquatable<BlockOption>
    {
        public string Display
        {
            get { return ToString(); }
        }
        public LabelReference Label
        {
            get { return labelReference; }
            set { labelReference = value; }
        }
        public long Address
        {
            get { return pointer; }
        }
        public int BlockIndex
        {
            get { return index; }
        }
        public int BlockLength
        {
            get { return length; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private LabelReference labelReference;
        private readonly uint pointer;
        private readonly int index;
        private readonly int length;
        private string name;
        
        public BlockOption(int index)
        {
            this.index = index;
        }
        public BlockOption(uint pointer, int index, int length, string name)
        {
            this.pointer = pointer;
            this.index = index;
            this.length = length;
            this.name = name;
        }
        public BlockOption(uint pointer, int index, int length)
        {
            this.pointer = pointer;
            this.index = index;
            this.length = length;
        }
        public override bool Equals(object obj)
        {
            if (obj is BlockOption)
                return index.Equals(((BlockOption)obj).index);
            else return false;
        }
        public override int GetHashCode()
        {
            return index.GetHashCode();
        }
        public override string ToString()
        {
            if (labelReference != null) return $"{index}: {labelReference}";
            else return $"{index}: {name}";
        }
        
        public int CompareTo(BlockOption other)
        {
            return index.CompareTo(other.index);
        }
        public bool Equals(BlockOption other)
        {
            return index.Equals(other.index);
        }

        public static implicit operator BlockOption(int index)
        {
            return new BlockOption(index);
        }
        public static implicit operator int(BlockOption option)
        {
            return option.index;
        }
    }
}
