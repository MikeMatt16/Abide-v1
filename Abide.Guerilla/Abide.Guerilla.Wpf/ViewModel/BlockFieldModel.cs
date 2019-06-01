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
        private readonly static ITagBlock Null = new GNullBlock();

        /// <summary>
        /// Gets and returns a string for the expand/collapse button.
        /// </summary>
        public string ExpandButtonText
        {
            get
            {
                return expanded ? "-" : "+";
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
                    if (value >= 0 && value < blockList.Count) SelectedTagBlockModel.TagBlock = blockList[value];
                    else SelectedTagBlockModel.TagBlock = Null;
                }
            }
        }
        /// <summary>
        /// Gets and returns the block field container's block list.
        /// </summary>
        public ObservableCollection<ITagBlock> BlockList
        {
            get { return blockList; }
            private set
            {
                bool changed = blockList != value;
                blockList = value;
                if (changed) NotifyPropertyChanged();
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
        public TagBlockModel SelectedTagBlockModel { get; }
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

        private ObservableCollection<ITagBlock> blockList = new ObservableCollection<ITagBlock>();
        private int selectedBlockIndex = -1;
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
            InsertCommand = new RelayCommand(p => InsertBlock(), p => CanOperateOnCurrentBlock());
            DuplicateCommand = new RelayCommand(p => DuplicateBlock(), p => { return false; });
            DeleteCommand = new RelayCommand(p => DeleteBlock(), p => CanDeleteBlock());
            DeleteAllCommand = new RelayCommand(p => DeleteAll(), p => CanDeleteBlock());
            ExpandCommand = new RelayCommand(p => ToggleExpand(), p => CanExpand());
        }
        /// <summary>
        /// Occurs when the tag field has been changed.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnTagFieldChanged(EventArgs e)
        {
            //Clear
            if (TagField != null) BlockList = new ObservableCollection<ITagBlock>(TagField.BlockList);
            else BlockList.Clear();

            //Check
            if (BlockList.Count > 0) SelectedBlockIndex = 0;
            else SelectedBlockIndex = -1;

            //Notify
            expanded = BlockList.Count > 0;
            NotifyPropertyChanged(nameof(HasBlocks));
            NotifyPropertyChanged(nameof(ExpandButtonText));

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
        private bool CanExpand()
        {
            //Return
            return HasBlocks;
        }

        private void AddBlock()
        {
            //Count
            int count = blockList.Count;

            //Add
            ITagBlock tagBlock = TagField.Add(out bool success);
            if (success)
            {
                //Add
                BlockList.Add(tagBlock);

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
            BlockList.Insert(selectedBlockIndex, tagBlock);

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
        private void ToggleExpand()
        {
            //Check
            if(SelectedTagBlockModel != null)
            {
                //Toggle
                Visibility blockVisibility = SelectedTagBlockModel.Visiblity;
                switch (blockVisibility)
                {
                    case Visibility.Visible:
                        SelectedTagBlockModel.Visiblity = Visibility.Collapsed;
                        expanded = false;
                        break;
                    case Visibility.Hidden:
                    case Visibility.Collapsed:
                        SelectedTagBlockModel.Visiblity = Visibility.Visible;
                        expanded = true;
                        break;
                }

                //Notify
                NotifyPropertyChanged(nameof(ExpandButtonText));
            }
        }
    }
}
