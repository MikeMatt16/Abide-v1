using Abide.Tag;
using Abide.Wpf.Modules.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Abide.Wpf.Modules.Tag
{
    public abstract class AbideTagField : AbideTagObject
    {
        private readonly Field baseField;

        protected object FieldValue
        {
            get => baseField.Value;
            set
            {
                if (!baseField.Value.Equals(value))
                {
                    baseField.Value = value;
                    NotifyPropertyChanged();
                    OnFieldValueChanged(new EventArgs());
                }
            }
        }
        public override string Name => baseField.Name ?? base.Name;
        public bool IsTagBlockName => baseField.IsBlockName;

        protected AbideTagField(TagContext context, AbideTagBlock owner, Field baseField) : base(context, owner)
        {
            this.baseField = baseField ?? throw new ArgumentNullException(nameof(baseField));
        }
        protected virtual void OnFieldValueChanged(EventArgs e) { }
        public virtual string GetValueString()
        {
            return FieldValue.ToString();
        }
        public virtual void PostProcessField()
        {
        }

        public static AbideTagField CreateFromField(TagContext context, AbideTagBlock owner, Field field)
        {
            switch (field)
            {
                case NumericField _:
                case BaseStringField _:
                    return new StringValueField(context, owner, field);

                case ShortBoundsField _:
                case FloatBoundsField _:
                    return new StringBoundsValueField(context, owner, field);

                case BaseEnumField enumField:
                    return new EnumValueField(context, owner, enumField);

                case BaseBlockIndexField blockIndexField:
                    return new BlockIndexValueField(context, owner, blockIndexField);

                case BaseFlagsField flagsField:
                    return new FlagsValueField(context, owner, flagsField);

                case BlockField blockField:
                    return new TagBlockValueField(context, owner, blockField);

                case StructField structField:
                    return new StructValueField(context, owner, structField);
            }

            return new UnusedTagField(context, owner, field);
        }
    }

    public sealed class UnusedTagField : AbideTagField
    {
        public UnusedTagField(TagContext context, AbideTagBlock owner, Field baseField) : base(context, owner, baseField) { }
    }

    public sealed class StringValueField : AbideTagField
    {
        private Func<object, string> convertToStringFunction = null;
        private Func<string, object> convertFromStringFunction = null;
        private string stringValue = string.Empty;

        public string StringValue
        {
            get => stringValue;
            set
            {
                if (stringValue != value)
                {
                    object result = convertFromStringFunction.Invoke(value);
                    if (result != null)
                    {
                        FieldValue = result;
                        stringValue = value;
                    }

                    NotifyPropertyChanged();
                }
            }
        }

        public StringValueField(TagContext context, AbideTagBlock owner, Field field) : base(context, owner, field)
        {
            convertFromStringFunction = new Func<string, object>(s =>
            {
                switch (field)
                {
                    case NumericField numericField:
                        if (double.TryParse(s, out double d))
                        {
                            return d;
                        }
                        break;

                    case BaseStringField baseStringField:
                        return s;
                }

                return null;
            });

            convertToStringFunction = new Func<object, string>(o =>
            {
                switch (field)
                {
                    case BaseStringField baseStringField:
                        return baseStringField.String;

                    case NumericField numericField:
                        return numericField.Number.ToString();
                }

                return string.Empty;
            });

            stringValue = convertToStringFunction(field.Value);
        }
    }

    public sealed class StringBoundsValueField : AbideTagField
    {
        private readonly Func<string, object> convertFromStringFunction = null;
        private string lowerValue = string.Empty;
        private string upperValue = string.Empty;

        public string LowerValue
        {
            get => lowerValue;
            set
            {
                if (lowerValue != value)
                {
                    object result = convertFromStringFunction.Invoke(value);
                    if (result != null)
                    {
                        lowerValue = value;
                        UpdateFieldValue();
                    }

                    NotifyPropertyChanged();
                }
            }
        }
        public string UpperValue
        {
            get => upperValue;
            set
            {
                if (upperValue != value)
                {
                    object result = convertFromStringFunction.Invoke(value);
                    if (result != null)
                    {
                        upperValue = value;
                        UpdateFieldValue();
                    }

                    NotifyPropertyChanged();
                }
            }
        }

        public StringBoundsValueField(TagContext context, AbideTagBlock owner, Field field) : base(context, owner, field)
        {
            double from = 0, to = 0;
            switch (field)
            {
                case ShortBoundsField shortBoundsField:
                    convertFromStringFunction = StringToShort;
                    from = shortBoundsField.Bounds.Min;
                    to = shortBoundsField.Bounds.Max;
                    break;

                case FloatBoundsField floatBoundsField:
                    convertFromStringFunction = StringToFloat;
                    from = floatBoundsField.Bounds.Min;
                    to = floatBoundsField.Bounds.Max;
                    break;
            }

            lowerValue = from.ToString();
            upperValue = to.ToString();
        }
        private object StringToShort(string s)
        {
            if (short.TryParse(s, out short n))
            {
                return n;
            }

            return null;
        }
        private object StringToFloat(string s)
        {
            if (float.TryParse(s, out float n))
            {
                return n;
            }

            return null;
        }
        private void UpdateFieldValue()
        {
            object lower = convertFromStringFunction.Invoke(lowerValue);
            object upper = convertFromStringFunction.Invoke(upperValue);

            if (lower is short sl && upper is short su)
            {
                FieldValue = new ShortBounds(sl, su);
            }
            else if (lower is float fl && upper is float fu)
            {
                FieldValue = new FloatBounds(fl, fu);
            }
        }
    }

    public sealed class StringTupleValueField : AbideTagField
    {
        private Func<string, object> convertFromStringFunction = null;

        public ObservableCollection<KeyValuePair<string, string>> Components { get; } = new ObservableCollection<KeyValuePair<string, string>>();

        public StringTupleValueField(TagContext context, AbideTagBlock owner, Field field) : base(context, owner, field)
        {

        }
        private void UpdateFieldValue()
        {

        }
    }

    public class TagOption : AbideTagObject
    {
        private readonly string name = string.Empty;

        public override string Name => name;

        public TagOption(string name, TagContext context, AbideTagField owner) : base(context, owner)
        {
            this.name = name;
        }
        public override string ToString()
        {
            return name;
        }
    }
    
    public sealed class EnumValueField : AbideTagField
    {
        private readonly BaseEnumField baseEnumField = null;

        public ObservableCollection<TagOption> Options { get; } = new ObservableCollection<TagOption>();
        public int SelectedIndex
        {
            get => baseEnumField.SelectedIndex;
            set
            {
                if (baseEnumField.SelectedIndex != value)
                {
                    baseEnumField.SelectedIndex = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public EnumValueField(TagContext context, AbideTagBlock owner, BaseEnumField enumField) : base(context, owner, enumField)
        {
            baseEnumField = enumField;
            foreach (var option in enumField.Options)
            {
                Options.Add(new TagOption(option.Name, context, this));
            }
        }
        public override string GetValueString()
        {
            var selectedOption = Options.ElementAtOrDefault(SelectedIndex);
            if (selectedOption != null)
            {
                return selectedOption.Name;
            }

            return base.GetValueString();
        }
    }

    public sealed class BlockIndexValueField : AbideTagField
    {
        private readonly BlockIndexOption nullOption = null;
        private readonly BaseBlockIndexField baseBlockIndexField = null;
        private TagOption selectedOption = null;
        private string instructions = string.Empty;
        private bool isReadOnly = false;

        public ObservableCollection<TagOption> Options { get; } = new ObservableCollection<TagOption>();
        public string Instructions
        {
            get => instructions;
            set
            {
                if (instructions != value)
                {
                    instructions = value;
                    NotifyPropertyChanged();
                    InitializeOptions();
                }
            }
        }
        public TagOption SelectedOption
        {
            get => selectedOption;
            set
            {
                if (selectedOption != value && Options.Contains(value))
                {
                    selectedOption = value;
                    NotifyPropertyChanged();

                    if (selectedOption is BlockIndexOption option)
                    {
                        SelectedIndex = option.Index;
                    }
                }

                if (selectedOption == null)
                {
                    IsReadOnly = true;
                }
            }
        }
        public int SelectedIndex
        {
            get => baseBlockIndexField.SelectedIndex;
            set
            {
                if (baseBlockIndexField.SelectedIndex != value)
                {
                    baseBlockIndexField.SelectedIndex = value;
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

        public BlockIndexValueField(TagContext context, AbideTagBlock owner, BaseBlockIndexField blockIndexField) : base(context, owner, blockIndexField)
        {
            baseBlockIndexField = blockIndexField;
            nullOption = new BlockIndexOption(context, -1, null, this);
            InitializeOptions();
        }
        public override string GetValueString()
        {
            if (SelectedOption != null)
            {
                return SelectedOption.Name;
            }

            return base.GetValueString();
        }
        private void InitializeOptions()
        {
            Options.Clear();
            SelectedOption = null;
            Options.Add(nullOption);

            if (!string.IsNullOrEmpty(Instructions))
            {
                AbideTagObject obj = this;
                using (StringReader reader = new StringReader(instructions))
                {
                    string token = ReadToken(reader);
                    while (!string.IsNullOrEmpty(token))
                    {
                        switch (token.ToUpper())
                        {
                            case "U":
                                obj = obj.Owner;
                                break;

                            case "F":
                                if (obj is AbideTagBlock tagBlock && int.TryParse(ReadToken(reader), out int index))
                                {
                                    if (tagBlock.Fields.Count > index)
                                    {
                                        AbideTagField field = tagBlock.Fields[index];
                                        if (field is TagBlockValueField tagBlockValueField)
                                        {
                                            for (int i = 0; i < tagBlockValueField.TagBlockList.Count; i++)
                                            {
                                                var block = tagBlockValueField.TagBlockList[i];
                                                Options.Add(new BlockIndexOption(Context, i, block, this));
                                            }

                                            SelectedOption = Options.ElementAtOrDefault(SelectedIndex);
                                        }
                                    }
                                }
                                break;
                        }

                        token = ReadToken(reader);
                    }
                }
            }
        }
        private string ReadToken(TextReader reader)
        {
            SkipWhitespace(reader);
            StringBuilder builder = new StringBuilder();

            while (reader.Peek() != -1)
            {
                char peekChar = (char)reader.Peek();
                if (char.IsWhiteSpace(peekChar))
                {
                    break;
                }

                builder.Append(peekChar);
                reader.Read();
            }

            return builder.ToString();
        }
        private void SkipWhitespace(TextReader reader)
        {
            while(reader.Peek() != -1)
            {
                if (char.IsWhiteSpace((char)reader.Peek()))
                {
                    reader.Read();
                }
                else
                {
                    break;
                }
            }
        }

        private class BlockIndexOption : TagOption
        {
            private readonly int index;
            private readonly AbideTagBlock block;

            public override string Name => $"{index}: {block?.DisplayName ?? "null"}";
            public int Index => index;
            public AbideTagBlock Block => block;

            public BlockIndexOption(TagContext context, int index, AbideTagBlock block, BlockIndexValueField owner) : base(string.Empty, context, owner)
            {
                this.index = index;
                this.block = block;
                if (block != null)
                {
                    block.PropertyChanged += Block_PropertyChanged;
                }
            }
            ~BlockIndexOption()
            {
                if (block != null)
                {
                    block.PropertyChanged -= Block_PropertyChanged;
                }
            }
            private void Block_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(block.DisplayName))
                {
                    NotifyPropertyChanged(nameof(Name));
                }
            }
        }
    }

    public sealed class FlagsValueField : AbideTagField
    {
        private readonly BaseFlagsField baseFlagsField = null;

        public ObservableCollection<TagFlagOption> Options { get; } = new ObservableCollection<TagFlagOption>();
        public int Flags
        {
            get => baseFlagsField.Flags;
            private set
            {
                if (baseFlagsField.Flags != value)
                {
                    baseFlagsField.Flags = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public FlagsValueField(TagContext context, AbideTagBlock owner, BaseFlagsField flagsField) : base(context, owner, flagsField)
        {
            baseFlagsField = flagsField;
            foreach (var option in flagsField.Options)
            {
                int flag = 1 << (option.Index + 1);
                var flagOption = new TagFlagOption(option.Name, flag, context, this);
                Options.Add(flagOption);
            }
        }

        public sealed class TagFlagOption : TagOption
        {
            private readonly FlagsValueField flagsValueField;

            public int Flag { get; } = 0;
            public bool IsChecked
            {
                get => (flagsValueField.Flags & Flag) == Flag;
                set
                {
                    if (IsChecked != value)
                    {
                        if (value)
                        {
                            flagsValueField.Flags |= Flag;
                        }
                        else
                        {
                            flagsValueField.Flags &= ~Flag;
                        }

                        NotifyPropertyChanged();
                    }
                }
            }

            public TagFlagOption(string name, int flag, TagContext context, FlagsValueField owner) : base(name, context, owner)
            {
                flagsValueField = owner;
                Flag = flag;
            }
        }
    }

    public sealed class StructValueField : AbideTagField
    {
        private AbideTagBlock structure = null;

        public AbideTagBlock Structure
        {
            get => structure;
            private set
            {
                if (structure != value)
                {
                    structure = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public StructValueField(TagContext context, AbideTagBlock owner, StructField structField) : base(context, owner, structField)
        {
            structure = new AbideTagBlock(context, this, structField.Block);
        }
        public override void PostProcessField()
        {
            structure?.PostprocessTagBlock();
        }
    }

    public sealed class TagBlockValueField : AbideTagField
    {
        private const string BlockListClipboardFormat = "AbideClipboardTagData";

        private readonly BlockField baseBlockfield = null;
        private AbideTagBlock selectedTagBlock = null;
        private bool expanded = false;
        private bool enabled = false;

        public int MaximumBlockCount { get; private set; } = 1;
        public ObservableCollection<AbideTagBlock> TagBlockList { get; } = new ObservableCollection<AbideTagBlock>();
        public ActionCommand AddBlockCommand { get; }
        public ActionCommand RemoveBlockCommand { get; }
        public ActionCommand ClearCommand { get; }
        public ActionCommand CopyCommand { get; }
        public ActionCommand CopyAllCommand { get; }
        public ActionCommand PasteCommand { get; }
        public AbideTagBlock SelectedTagBlock
        {
            get => selectedTagBlock;
            set
            {
                if (selectedTagBlock != value)
                {
                    if (value == null || TagBlockList.Contains(value))
                    {
                        selectedTagBlock = value;
                        NotifyPropertyChanged();
                    }
                }
            }
        }
        public bool IsExpanded
        {
            get => expanded;
            set
            {
                if (expanded != value)
                {
                    expanded = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool Enabled
        {
            get => enabled;
            private set
            {
                if (enabled != value)
                {
                    enabled = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public TagBlockValueField(TagContext context, AbideTagBlock owner, BlockField blockField) : base(context, owner, blockField)
        {
            baseBlockfield = blockField;
            foreach (var block in blockField.BlockList)
            {
                TagBlockList.Add(new AbideTagBlock(context, this, block));
            }

            TagBlockList.CollectionChanged += TagBlockList_CollectionChanged;
            AddBlockCommand = new ActionCommand(AddBlock, GetCanAddBlock);
            RemoveBlockCommand = new ActionCommand(RemoveBlock, GetIsBlockSelected);
            ClearCommand = new ActionCommand(ClearBlocks, GetHasAnyBlocks);
            CopyCommand = new ActionCommand(CopyBlock, GetIsBlockSelected);
            CopyAllCommand = new ActionCommand(CopyAll, GetHasAnyBlocks);
            PasteCommand = new ActionCommand(Paste, GetCanPaste);
            MaximumBlockCount = blockField.BlockList.MaximumCount;
            IsExpanded = blockField.BlockList.Count > 0;
            Enabled = blockField.BlockList.Count > 0;
            SelectedTagBlock = TagBlockList.FirstOrDefault();
        }
        public override void PostProcessField()
        {
            foreach (var block in TagBlockList)
            {
                block.PostprocessTagBlock();
            }
        }
        private void TagBlockList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (TagBlockList.Count == 0)
            {
                SelectedTagBlock = null;
                IsExpanded = false;
                Enabled = false;
            }
            else
            {
                if (!Enabled)
                {
                    if (SelectedTagBlock == null)
                    {
                        SelectedTagBlock = TagBlockList.FirstOrDefault();
                    }

                    IsExpanded = true;
                    Enabled = true;
                }
            }

            AddBlockCommand.RaiseCanExecuteChanged();
            RemoveBlockCommand.RaiseCanExecuteChanged();
            ClearCommand.RaiseCanExecuteChanged();
            CopyCommand.RaiseCanExecuteChanged();
            CopyAllCommand.RaiseCanExecuteChanged();
            PasteCommand.RaiseCanExecuteChanged();
        }
        private void AddBlock(object obj)
        {
            if (baseBlockfield.AddNew(out Block block))
            {
                AbideTagBlock tagBlock = new AbideTagBlock(Context, this, block);
                TagBlockList.Add(tagBlock);
                SelectedTagBlock = tagBlock;
            }
        }
        private void RemoveBlock(object obj)
        {
            int index = TagBlockList.IndexOf(SelectedTagBlock);

            if (index != -1)
            {
                baseBlockfield.BlockList.RemoveAt(index);
                TagBlockList.Remove(SelectedTagBlock);

                if (index < TagBlockList.Count)
                {
                    SelectedTagBlock = TagBlockList[index];
                }
                else
                {
                    SelectedTagBlock = TagBlockList.LastOrDefault();
                }
            }
        }
        private void ClearBlocks(object obj)
        {
            SelectedTagBlock = null;
            baseBlockfield.BlockList.Clear();
            TagBlockList.Clear();
        }
        private void CopyBlock(object obj)
        {
            if (SelectedTagBlock != null)
            {
                int index = TagBlockList.IndexOf(SelectedTagBlock);
                TagBlockList data = Tag.TagBlockList.FromBlockField(baseBlockfield, index, 1);
                using (MemoryStream ms = new MemoryStream())
                {
                    data.Save(ms);
                    Clipboard.SetData(BlockListClipboardFormat, ms.ToArray());
                }
            }
        }
        private void CopyAll(object obj)
        {
            if (TagBlockList.Count > 0)
            {
                TagBlockList data = Tag.TagBlockList.FromBlockField(baseBlockfield, 0, TagBlockList.Count);
                using (MemoryStream ms = new MemoryStream())
                {
                    data.Save(ms);
                    Clipboard.SetData(BlockListClipboardFormat, ms.ToArray());
                }
            }
        }
        private void Paste(object obj)
        {
            if (Clipboard.ContainsData(BlockListClipboardFormat))
            {
                TagBlockList data = new TagBlockList();
                if (Clipboard.GetData(BlockListClipboardFormat) is byte[] buffer)
                {
                    using (MemoryStream ms = new MemoryStream(buffer))
                    {
                        data.Load(ms, Context);
                    }
                }

                var baseBlock = baseBlockfield.Create();
                if (baseBlock.Name == data.BlockName)
                {
                    int index = TagBlockList.IndexOf(SelectedTagBlock);
                    foreach (var block in data)
                    {
                        if (index == -1)
                        {
                            if (baseBlockfield.BlockList.Add(block))
                            {
                                TagBlockList.Add(new AbideTagBlock(Context, this, block));
                            }
                        }
                        else
                        {
                            baseBlockfield.BlockList.Insert(index++, block, out bool success);
                            if (success)
                            {
                                TagBlockList.Insert(index, new AbideTagBlock(Context, this, block));
                            }
                        }
                    }
                }
            }
        }
        private bool GetIsBlockSelected(object obj)
        {
            return SelectedTagBlock != null;
        }
        private bool GetCanAddBlock(object arg)
        {
            return baseBlockfield.BlockList.MaximumCount > baseBlockfield.BlockList.Count;
        }
        private bool GetHasAnyBlocks(object arg)
        {
            return TagBlockList.Count > 0;
        }
        private bool GetCanPaste(object obj)
        {
            return true;
        }
    }
}
