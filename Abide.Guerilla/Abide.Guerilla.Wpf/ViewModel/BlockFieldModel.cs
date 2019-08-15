using Abide.Tag;
using Abide.Tag.Guerilla.Generated;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents a block field container.
    /// </summary>
    public class BlockFieldModel : FieldModel
    {
        private readonly static Block Null = new GNullBlock();
        
        /// <summary>
        /// Gets and returns the expand or collapse button tooltip.
        /// </summary>
        public string ExpandTooltip
        {
            get { return expanded ? "Collapse" : "Expand"; }
        }
        /// <summary>
        /// Gets or sets the expanded status of the block.
        /// </summary>
        public bool Expanded
        {
            get { return expanded; }
            set
            {
                if (expanded != value)
                {
                    expanded = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(ExpandTooltip));
                }
            }
        }
        /// <summary>
        /// Gets and returns a value that determines if the block field has any elements.
        /// </summary>
        public bool HasBlocks
        {
            get { return BlockList.Count > 0; }
        }
        /// <summary>
        /// Gets or sets the selected zero-based block index.
        /// </summary>
        public int SelectedBlockIndex
        {
            get { return selectedBlockIndex; }
            set
            {
                bool changed = selectedBlockIndex != value;
                selectedBlockIndex = value;
                if (changed)
                {
                    //Notify
                    NotifyPropertyChanged();

                    //Check block index
                    if (value >= 0 && value < BlockList.Count) SelectedTagBlockModel = BlockList[value];
                    else SelectedTagBlockModel.TagBlock = Null;
                }
            }
        }
        /// <summary>
        /// Gets or sets the block tag field that the model wraps.
        /// </summary>
        public new BlockField TagField
        {
            get { return (BlockField)base.TagField; }
            set { base.TagField = value; }
        }
        /// <summary>
        /// Gets or sets the currently selected tag block modal.
        /// </summary>
        public TagBlockModel SelectedTagBlockModel
        {
            get { return selectedTagBlockModel; }
            set
            {
                if (selectedTagBlockModel != value)
                {
                    selectedTagBlockModel = value;
                    selectedTagBlockModel.Visiblity = expanded ? Visibility.Visible : Visibility.Collapsed;
                    NotifyPropertyChanged();
                    SelectedBlockIndex = BlockList.IndexOf(value);
                }
            }
        }
        /// <summary>
        /// Gets and returns the block field container's block list.
        /// </summary>
        public ObservableCollection<TagBlockModel> BlockList { get; } = new ObservableCollection<TagBlockModel>();
        /// <summary>
        /// Gets and returns the command to add a block to tag block field.
        /// </summary>
        public ICommand AddCommand { get; }
        /// <summary>
        /// Gets and returns the command to insert a block to tag block field.
        /// </summary>
        public ICommand InsertCommand { get; }
        /// <summary>
        /// Gets and returns the command to insert a block to tag block field.
        /// </summary>
        public ICommand DuplicateCommand { get; }
        /// <summary>
        /// Gets and returns the command to insert a block to tag block field.
        /// </summary>
        public ICommand DeleteCommand { get; }
        /// <summary>
        /// Gets and returns the command to insert a block to tag block field.
        /// </summary>
        public ICommand DeleteAllCommand { get; }
        /// <summary>
        /// Gets and returns the command to expand or collapse the child view.
        /// </summary>
        public ICommand ExpandCommand { get; }

        private int selectedBlockIndex = -1;
        private TagBlockModel selectedTagBlockModel = null;
        private bool expanded = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockFieldModel"/> class.
        /// </summary>
        public BlockFieldModel()
        {
            //Setup modal
            SelectedTagBlockModel = new TagBlockModel();

            //Setup commands
            AddCommand = new RelayCommand(p => AddBlock(), p => CanAddBlock());
            InsertCommand = new RelayCommand(p => InsertBlock(), p => CanOperateOnCurrentBlock() && CanAddBlock());
            DuplicateCommand = new RelayCommand(p => DuplicateBlock(), p => false);
            DeleteCommand = new RelayCommand(p => DeleteBlock(), p => CanDeleteBlock());
            DeleteAllCommand = new RelayCommand(p => DeleteAll(), p => CanDeleteBlock());
            ExpandCommand = new RelayCommand(p => ToggleExpansion(), p => HasBlocks);
        }
        /// <summary>
        /// Occurs when the tag field has been changed.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnTagFieldChanged(EventArgs e)
        {
            //Reinitialize List
            BlockList.Clear();
            foreach (ITagBlock tagBlock in TagField.BlockList)
                BlockList.Add(new TagBlockModel() { Owner = Owner, TagBlock = tagBlock});

            //Check
            if (BlockList.Count > 0) SelectedBlockIndex = 0;
            else SelectedBlockIndex = -1;

            //Notify
            Expanded = BlockList.Count > 0;
            NotifyPropertyChanged(nameof(HasBlocks));

            //Base procedures
            base.OnTagFieldChanged(e);
        }
        /// <summary>
        /// Occurs when the owner has been changed.
        /// </summary>
        protected override void NotifyOwnerChanged()
        {
            //Set owner of model
            SelectedTagBlockModel.Owner = Owner;
        }

        private bool CanAddBlock()
        {
            if (TagField == null) return false;
            return TagField.BlockList.Count < TagField.BlockList.MaximumCount;
        }
        private bool CanDeleteBlock()
        {
            if (TagField == null) return false;
            return TagField.BlockList.Count > 0;
        }
        private bool CanOperateOnCurrentBlock()
        {
            if (TagField == null) return false;
            return selectedBlockIndex >= 0;
        }

        private void AddBlock()
        {
            //Count
            int count = BlockList.Count;

            //Add
            ITagBlock tagBlock = TagField.Add(out bool success);
            if (success)
            {
                //Add
                BlockList.Add(new TagBlockModel() { Owner = Owner, TagBlock = tagBlock });

                //Notify Changes
                if (count == 0) NotifyPropertyChanged(nameof(HasBlocks));

                //Set
                SelectedBlockIndex = TagField.BlockList.IndexOf(tagBlock);
                NotifyValueChanged();
            }
        }
        private void InsertBlock()
        {
            //Create
            ITagBlock tagBlock = TagField.Create();
            TagField.BlockList.Insert(selectedBlockIndex, tagBlock);
            BlockList.Insert(selectedBlockIndex, new TagBlockModel() { Owner = Owner, TagBlock = tagBlock });

            //Notify Changes
            NotifyPropertyChanged(nameof(HasBlocks));

            //Set
            SelectedBlockIndex = TagField.BlockList.IndexOf(tagBlock);
            NotifyValueChanged();
        }
        private void DuplicateBlock()
        {
            //TODO: Implement
        }
        private void DeleteBlock()
        {
            //Remove
            TagField.BlockList.RemoveAt(selectedBlockIndex);
            BlockList.RemoveAt(selectedBlockIndex);

            //Notify Changes
            NotifyPropertyChanged(nameof(HasBlocks));

            //Set
            SelectedBlockIndex = selectedBlockIndex - 1;
            NotifyValueChanged();
        }
        private void DeleteAll()
        {
            //Remove
            TagField.BlockList.Clear();
            BlockList.Clear();

            //Notify Changes
            NotifyPropertyChanged(nameof(HasBlocks));

            //Set
            SelectedBlockIndex = -1;
            NotifyValueChanged();
        }
        private void ToggleExpansion()
        {
            //Toggle
            Expanded = !expanded;
        }
    }
}
