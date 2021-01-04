using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2.Retail.Tag;
using Abide.Wpf.Modules.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail
{
    public sealed class TagGroupViewModel : BaseViewModel
    {
        private Group tagGroup = null;
        private TagFourCc groupTag = new TagFourCc();
        private string name = string.Empty;
        private int count = 0;

        public ObservableCollection<TagBlockViewModel> Blocks { get; } = new ObservableCollection<TagBlockViewModel>();
        public Group TagGroup
        {
            get { return tagGroup; }
            set
            {
                if (tagGroup != value)
                {
                    tagGroup = value;
                    NotifyPropertyChanged();
                    Blocks.Clear();
                    if (tagGroup != null)
                    {
                        foreach (var tagBlock in tagGroup.TagBlocks)
                            Blocks.Add(new TagBlockViewModel() { TagBlock = tagBlock });
                        Count = tagGroup.TagBlockCount;
                        Name = tagGroup.GroupName;
                        GroupTag = tagGroup.GroupTag;
                    }
                    else
                    {
                        Count = 0;
                        Name = string.Empty;
                        GroupTag = new TagFourCc();
                    }
                }
            }
        }
        public int Count
        {
            get { return count; }
            private set
            {
                if (count != value)
                {
                    count = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Name
        {
            get { return name; }
            private set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public TagFourCc GroupTag
        {
            get { return groupTag; }
            private set
            {
                if (groupTag != value)
                {
                    groupTag = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public TagGroupViewModel() { }
    }

    public sealed class TagBlockViewModel : BaseViewModel
    {
        private Block tagBlock = null;
        private string name, displayName = string.Empty;
        private int count = 0;

        public ObservableCollection<TagFieldViewModel> Fields { get; } = new ObservableCollection<TagFieldViewModel>();
        public Block TagBlock
        {
            get { return tagBlock; }
            set
            {
                if (tagBlock != value)
                {
                    tagBlock = value;
                    NotifyPropertyChanged();

                    Fields.Clear();
                    if (tagBlock != null)
                    {
                        foreach (var tagField in tagBlock)
                            Fields.Add(new TagFieldViewModel() { TagField = tagField });
                        Count = tagBlock.FieldCount;
                        Name = tagBlock.BlockName;
                        DisplayName = tagBlock.ToString();
                    }
                    else
                    {
                        Count = 0;
                        Name = string.Empty;
                        DisplayName = string.Empty;
                    }
                }
            }
        }
        public int Count
        {
            get { return count; }
            private set
            {
                if (count != value)
                {
                    count = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Name
        {
            get { return name; }
            private set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string DisplayName
        {
            get { return displayName; }
            private set
            {
                if (displayName != value)
                {
                    displayName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public TagBlockViewModel() { }
    }

    public sealed class TagFieldViewModel : BaseViewModel
    {
        private TagBlockViewModel structure = null;
        private FieldType type = FieldType.FieldString;
        private string name = string.Empty;
        private string information = string.Empty;
        private string details = string.Empty;
        private bool isReadOnly = false;
        private bool isBlockName = false;
        private int size = 0;
        private object value = null;
        private ITagField tagField = null;

        public ObservableCollection<TagBlockViewModel> BlockList { get; } = new ObservableCollection<TagBlockViewModel>();
        public ObservableCollection<TagOptionViewModel> OptionList { get; } = new ObservableCollection<TagOptionViewModel>();
        public TagBlockViewModel Structure
        {
            get { return structure; }
            set
            {
                if (structure != value)
                {
                    structure = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ITagField TagField
        {
            get { return tagField; }
            set
            {
                if (tagField != value)
                {
                    tagField = value;
                    NotifyPropertyChanged();

                    //Setup other properties
                    BlockList.Clear();
                    OptionList.Clear();
                    Structure = null;
                    if (tagField != null)
                    {
                        Type = tagField.Type;
                        Name = tagField.Name;
                        Information = tagField.Information;
                        Details = tagField.Details;
                        IsReadOnly = tagField.IsReadOnly;
                        IsBlockName = tagField.IsBlockName;
                        Size = tagField.Size;
                        Value = tagField.Value;

                        if (tagField is BlockField blockField)
                        {
                            foreach (var tagBlock in blockField.BlockList)
                            {
                                BlockList.Add(new TagBlockViewModel() { TagBlock = tagBlock });
                            }

                            if (BlockList.Count > 0)
                            {
                                Structure = BlockList[0];
                            }
                        }
                        else if (tagField is StructField structField)
                        {
                            Structure = new TagBlockViewModel() { TagBlock = (Block)structField.Value };
                        }
                        if (tagField is OptionField optionField)
                        {
                            foreach (var option in optionField.Options)
                            {
                                OptionList.Add(new TagOptionViewModel() { Name = option.Name, Index = option.Index });
                            }
                        }
                    }
                    else
                    {
                        Type = FieldType.FieldString;
                        Name = Information = Details = string.Empty;
                        IsReadOnly = false;
                        IsBlockName = false;
                        Size = 0;
                        Value = null;
                    }
                }
            }
        }
        public FieldType Type
        {
            get { return type; }
            private set
            {
                if (type != value)
                {
                    type = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Name
        {
            get { return name; }
            private set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Information
        {
            get { return information; }
            private set
            {
                if (information != value)
                {
                    information = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Details
        {
            get { return details; }
            private set
            {
                if (details != value)
                {
                    details = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool IsReadOnly
        {
            get { return isReadOnly; }
            private set
            {
                if (isReadOnly != value)
                {
                    isReadOnly = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool IsBlockName
        {
            get { return isBlockName; }
            private set
            {
                if (isBlockName != value)
                {
                    isBlockName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int Size
        {
            get { return size; }
            private set
            {
                if (size != value)
                {
                    size = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public object Value
        {
            get { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public TagFieldViewModel() { }
        public override string ToString()
        {
            return tagField?.Name ?? base.ToString();
        }
    }

    public sealed class TagOptionViewModel : BaseViewModel
    {
        private bool isSelected = false;

        public string Name { get; set; }
        public int Index { get; set; }
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
