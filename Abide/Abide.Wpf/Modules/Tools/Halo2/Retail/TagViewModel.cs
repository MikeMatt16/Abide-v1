using Abide.HaloLibrary;
using Abide.Tag;
using Abide.Tag.Cache;
using Abide.HaloLibrary.Halo2.Retail;
using Abide.Wpf.Modules.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail
{
    public sealed class TagGroupViewModel : BaseViewModel
    {
        private HaloMapFile map = null;
        private Group tagGroup = null;
        private TagFourCc groupTag = new TagFourCc();
        private string name = string.Empty;
        private int count = 0;

        public ObservableCollection<TagBlockViewModel> Blocks { get; } = new ObservableCollection<TagBlockViewModel>();
        public HaloMapFile Map
        {
            get => map;
            set
            {
                if (map != value)
                {
                    map = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public Group TagGroup
        {
            get => tagGroup;
            set
            {
                if (tagGroup != value)
                {
                    tagGroup = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int Count
        {
            get => count;
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
            get => name;
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
            get => groupTag;
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
        protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(TagGroup):
                    Blocks.Clear();
                    if (tagGroup != null)
                    {
                        foreach (var tagBlock in tagGroup.TagBlocks)
                        {
                            var model = new TagBlockViewModel(null) { Map = map };
                            model.TagBlock = tagBlock;
                            Blocks.Add(model);
                        }

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
                    break;

                case nameof(Map):
                    foreach (var block in Blocks)
                    {
                        block.Map = map;
                    }

                    break;
            }

            base.OnNotifyPropertyChanged(e);
        }
    }

    public sealed class TagBlockViewModel : BaseViewModel
    {
        private HaloMapFile map = null;
        private Block tagBlock = null;
        private string name, displayName = string.Empty;
        private int count = 0;

        public TagBlockViewModel Owner { get; }
        public ObservableCollection<TagFieldViewModel> Fields { get; } = new ObservableCollection<TagFieldViewModel>();
        public HaloMapFile Map
        {
            get => map;
            set
            {
                if (map != value)
                {
                    map = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public Block TagBlock
        {
            get => tagBlock;
            set
            {
                if (tagBlock != value)
                {
                    tagBlock = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int Count
        {
            get => count;
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
            get => name;
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
            get => displayName;
            private set
            {
                if (displayName != value)
                {
                    displayName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public TagBlockViewModel(TagBlockViewModel owner)
        {
            Owner = owner;
        }
        protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(TagBlock):
                    Fields.Clear();
                    if (tagBlock != null)
                    {
                        foreach (var tagField in tagBlock)
                        {
                            Fields.Add(new TagFieldViewModel(Owner) { Map = map, TagField = tagField });
                        }

                        foreach (var tagField in tagBlock)
                        {
                            if (tagField is CharBlockIndexField)
                            {

                            }
                        }

                        Count = tagBlock.FieldCount;
                        Name = tagBlock.BlockName;
                        DisplayName = GetName(tagBlock);
                    }
                    else
                    {
                        Count = 0;
                        Name = string.Empty;
                        DisplayName = string.Empty;
                    }
                    break;

                case nameof(Map):
                    foreach (var field in Fields)
                    {
                        field.Map = map;
                    }

                    if (Fields.Any(f => f.IsBlockName))
                    {
                        DisplayName = GetName(tagBlock);
                    }
                    break;
            }

            base.OnNotifyPropertyChanged(e);
        }
        private string GetName(Block tagBlock)
        {
            if (Fields.Any(f => f.IsBlockName))
            {
                return string.Join(", ", Fields.Where(f => f.IsBlockName).Select(f => GetName(f)));
            }

            return tagBlock.ToString();
        }
        private string GetName(TagFieldViewModel field)
        {
            HaloTag tag;
            switch (field.Type)
            {
                case FieldType.FieldString:
                case FieldType.FieldLongString:
                case FieldType.FieldTag:
                    return field?.Value.ToString();

                case FieldType.FieldStringId:
                case FieldType.FieldOldStringId:
                    return Map?.GetStringById((StringId)field.Value);

                case FieldType.FieldTagReference:
                    tag = Map?.GetTagById(((TagReference)field.Value).Id);
                    if (tag == null)
                    {
                        return "null";
                    }

                    return $"{tag.TagName}.{tag.GroupTag}";

                case FieldType.FieldTagIndex:
                    tag = Map?.GetTagById((TagId)field.Value);
                    if (tag == null)
                    {
                        return "null";
                    }

                    return $"{tag.TagName}.{tag.GroupTag}";

                default:
                    return field.Value?.ToString() ?? "null";
            }
        }
    }

    public sealed class TagFieldViewModel : BaseViewModel
    {
        private HaloMapFile map = null;
        private TagBlockViewModel structure = null;
        private FieldType type = FieldType.FieldString;
        private string name = string.Empty;
        private string information = string.Empty;
        private string details = string.Empty;
        private string explanation = string.Empty;
        private bool isReadOnly = false;
        private bool isBlockName = false;
        private bool isExpanded = false;
        private int size = 0;
        private object value = null;
        private ITagField tagField = null;

        public TagBlockViewModel Owner { get; }
        public ObservableCollection<TagBlockViewModel> BlockList { get; } = new ObservableCollection<TagBlockViewModel>();
        public ObservableCollection<TagOptionViewModel> OptionList { get; } = new ObservableCollection<TagOptionViewModel>();
        public TagBlockViewModel Structure
        {
            get => structure;
            set
            {
                if (structure != value)
                {
                    structure = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public HaloMapFile Map
        {
            get => map;
            set
            {
                if (map != value)
                {
                    map = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public ITagField TagField
        {
            get => tagField;
            set
            {
                if (tagField != value)
                {
                    tagField = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public FieldType Type
        {
            get => type;
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
            get => name;
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
            get => information;
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
            get => details;
            private set
            {
                if (details != value)
                {
                    details = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Explanation
        {
            get => explanation;
            private set
            {
                if (explanation != value)
                {
                    explanation = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool IsReadOnly
        {
            get => isReadOnly;
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
            get => isBlockName;
            private set
            {
                if (isBlockName != value)
                {
                    isBlockName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                if (isExpanded != value)
                {
                    isExpanded = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int Size
        {
            get => size;
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
            get => value;
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public short Point2dX
        {
            get
            {
                if (Type != FieldType.FieldPoint2D)
                {
                    throw new InvalidOperationException();
                }

                return ((Point2)Value).X;
            }
            set
            {
                if (Type != FieldType.FieldPoint2D)
                {
                    throw new InvalidOperationException();
                }

                Point2 point = (Point2)Value;

                if (point.X != value)
                {
                    point.X = value;
                    Value = point;

                    NotifyPropertyChanged();
                }
            }
        }
        public short Point2dY
        {
            get
            {
                if (Type != FieldType.FieldPoint2D)
                {
                    throw new InvalidOperationException();
                }

                return ((Point2)Value).Y;
            }
            set
            {
                if (Type != FieldType.FieldPoint2D)
                {
                    throw new InvalidOperationException();
                }

                Point2 point = (Point2)Value;

                if (point.Y != value)
                {
                    point.Y = value;
                    NotifyPropertyChanged();

                    Value = point;
                }
            }
        }

        public TagFieldViewModel(TagBlockViewModel owner)
        {
            Owner = owner;
        }
        protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(TagField):
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
                        IsExpanded = false;

                        switch (tagField)
                        {
                            case BlockField blockField:
                                foreach (var tagBlock in blockField.BlockList)
                                {
                                    TagBlockViewModel viewModel = new TagBlockViewModel(Owner) { Map = map, TagBlock = tagBlock };
                                    BlockList.Add(viewModel);
                                }
                                IsExpanded = BlockList.Count > 0;
                                if (BlockList.Count > 0)
                                {
                                    Structure = BlockList[0];
                                }
                                break;

                            case StructField structField:
                                TagBlockViewModel structure = new TagBlockViewModel(Owner) { Map = map, TagBlock = (Block)structField.Value };
                                Structure = structure;
                                IsExpanded = true;
                                break;

                            case OptionField optionField:
                                foreach (var option in optionField.Options)
                                {
                                    OptionList.Add(new TagOptionViewModel(this) { Name = option.Name, Index = option.Index });
                                }
                                break;

                            case ExplanationField explanationField:
                                Explanation = explanationField.Explanation;
                                break;
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
                    break;

                default:
                    break;
            }
            base.OnNotifyPropertyChanged(e);
        }
        public override string ToString()
        {
            return tagField?.Name ?? base.ToString();
        }
    }

    public sealed class TagOptionViewModel : BaseViewModel
    {
        private bool hasFlag = false;
        private bool isSelected = false;
        private int index = 0;

        public TagFieldViewModel Field { get; }
        public string Name { get; set; }
        public int Index
        {
            get => index;
            set
            {
                if (index != value)
                {
                    index = value;
                    NotifyPropertyChanged();

                    int flag = 1 << index;
                    int flags = GetFlagsValue();
                    HasFlag = (flags & flag) == flag;
                }
            }
        }
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool HasFlag
        {
            get => hasFlag;
            set
            {
                if (hasFlag != value)
                {
                    hasFlag = ToggleFlags(1 << index, !hasFlag);
                    if (hasFlag == value)
                    {
                        NotifyPropertyChanged();
                    }
                }
            }
        }

        public TagOptionViewModel(TagFieldViewModel field)
        {
            Field = field ?? throw new ArgumentNullException(nameof(field));
        }
        private int GetFlagsValue()
        {
            if (Enum.GetName(typeof(FieldType), Field.Type).Contains("Flags"))
            {
                switch (Field.Type)
                {
                    case FieldType.FieldLongFlags:
                    case FieldType.FieldLongBlockFlags: return (int)Field.Value;
                    case FieldType.FieldWordFlags:
                    case FieldType.FieldWordBlockFlags: return (short)Field.Value;
                    case FieldType.FieldByteFlags:
                    case FieldType.FieldByteBlockFlags: return (byte)Field.Value;
                    default: return 0;
                }
            }

            return 0;
        }
        private bool ToggleFlags(int flags, bool direction)
        {
            int targetFlags = direction ? GetFlagsValue() | flags : GetFlagsValue() & ~flags;

            switch (Field.Type)
            {
                case FieldType.FieldLongFlags:
                case FieldType.FieldLongBlockFlags:
                    Field.Value = targetFlags;
                    break;
                case FieldType.FieldWordFlags:
                case FieldType.FieldWordBlockFlags:
                    Field.Value = (short)(targetFlags & ushort.MaxValue);
                    break;
                case FieldType.FieldByteFlags:
                case FieldType.FieldByteBlockFlags:
                    Field.Value = (byte)(targetFlags & byte.MaxValue);
                    break;
            }

            return (GetFlagsValue() & flags) == flags;
        }
    }
}
