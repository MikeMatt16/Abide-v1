using Abide.HaloLibrary;
using Abide.HaloLibrary.IO;
using Abide.Tag.Definition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Abide.Tag
{
    public delegate Block BlockSearchProcedure<T>(Block tagBlock, int blockIndex) where T : IConvertible, IComparable, IComparable<T>, IEquatable<T>;

    public sealed class Option
    {
        private readonly ObjectName objectName = new ObjectName(string.Empty);
        private readonly Block blockReference;

        public string Name => objectName.Name ?? string.Empty;
        public string Information => objectName.Information ?? string.Empty;
        public string Details => objectName.Details ?? string.Empty;
        public int Index { get; set; }
        public Option(string name, int index)
        {
            objectName = new ObjectName(name ?? string.Empty);
            Index = index;
        }
        public Option(Block block, int index)
        {
            blockReference = block ?? throw new ArgumentNullException(nameof(block));
            Index = index;
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder($"{Index}:");
            if (objectName != null)
            {
                builder.Append(' ');
                builder.Append(objectName.Name);
            }
            else if (blockReference != null)
            {
                builder.Append(' ');
                builder.Append(blockReference.DisplayName);
            }

            return builder.ToString();
        }
    }

    public abstract class Field : TagObject, ITagField
    {
        private readonly ObjectName name;
        private object previousValue = null;

        public abstract int Size { get; }
        public FieldType Type { get; }
        public override string Name => name.Name ?? string.Empty;
        public string Information => name.Information ?? string.Empty;
        public string Details => name.Details ?? string.Empty;
        public bool IsReadOnly => name.IsReadOnly;
        public bool IsBlockName => name.IsBlockName;
        public object PreviousValue => previousValue;
        public long FieldAddress { get; private set; } = 0;
        public object Value
        {
            get => GetProperty();
            set => SetProperty(value);
        }

        protected Field(FieldType type, string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            this.name = new ObjectName(name);
            Type = type;
        }
        public override string ToString()
        {
            return $"{Name} = {Value}";
        }
        public void Read(BinaryReader reader)
        {
            FieldAddress = reader.BaseStream.Position;
            OnRead(reader);
        }
        public void Write(BinaryWriter writer)
        {
            FieldAddress = writer.BaseStream.Position;
            OnWrite(writer);
        }
        public void Overwrite(BinaryWriter writer)
        {
            OnOverwrite(writer);
            writer.BaseStream.Seek(FieldAddress, SeekOrigin.Begin);
            OnWrite(writer);
        }
        public void PostWrite(BinaryWriter writer)
        {
            OnPostWrite(writer);
        }
        public string GetName()
        {
            return name.ToString();
        }
        public virtual object GetValue()
        {
            return Value;
        }
        public virtual string GetValueString()
        {
            return Value?.ToString() ?? string.Empty;
        }
        public virtual bool SetValue(object value)
        {
            Value = value;
            return true;
        }
        protected virtual void OnRead(BinaryReader reader) { }
        protected virtual void OnWrite(BinaryWriter writer) { }
        protected virtual void OnPostWrite(BinaryWriter writer) { }
        protected virtual void OnOverwrite(BinaryWriter writer) { }
    }

    public sealed class ExplanationField : Field
    {
        public override int Size => 0;
        public string Explanation { get; } = string.Empty;
        public ExplanationField(string name, string explanation) : base(FieldType.FieldExplanation, name)
        {
            Explanation = explanation;
        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }

    public sealed class StringField : Field
    {
        public override int Size => 32;
        public String32 String
        {
            get => (String32)Value;
            set
            {
                if (Value is String32 str)
                {
                    if (str.String == value.String)
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public StringField(string name) : base(FieldType.FieldString, name)
        {
            String = new String32();
        }
        protected override void OnRead(BinaryReader reader)
        {
            String = reader.Read<String32>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(String);
        }
    }

    public sealed class LongStringField : Field
    {
        public override int Size => 256;
        public String256 String
        {
            get => (String256)Value;
            set
            {
                if (Value is String256 str)
                {
                    if (str.String == value.String)
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public LongStringField(string name) : base(FieldType.FieldLongString, name)
        {
            String = new String256();
        }
        protected override void OnRead(BinaryReader reader)
        {
            String = reader.Read<String256>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(String);
        }
    }

    public abstract class StructField : Field
    {
        public override int Size => Block.Size;
        public Block Block
        {
            get => (Block)Value;
            set
            {
                if (Value != value)
                {
                    Value = value;
                    NotifyPropertyChanged();
                }
            }
        }
        protected StructField(string name) : base(FieldType.FieldStruct, name)
        {
            Block = Create();

            if (Block != null)
            {
                Block.Owner = this;
            }
        }
        protected override void OnRead(BinaryReader reader)
        {
            Block.Read(reader);
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            Block.Write(writer);
        }
        protected override void OnPostWrite(BinaryWriter writer)
        {
            Block.PostWrite(writer);
        }
        public abstract Block Create();
    }

    public abstract class BlockField : Field
    {
        public BlockList BlockList { get; }
        public long BlockAddress { get; private set; } = -1;
        public TagBlock Handle
        {
            get => (TagBlock)Value;
            set
            {
                if (Value is TagBlock tagBlock)
                {
                    if (tagBlock.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public Block SelectedBlock
        {
            get => GetProperty() as Block;
            set => SetProperty(value);
        }
        protected BlockField(string name, int maximumElementCount) : base(FieldType.FieldBlock, name)
        {
            Value = TagBlock.Zero;
            BlockList = new BlockList(maximumElementCount);
            SelectedBlock = null;
        }
        protected sealed override void OnRead(BinaryReader reader)
        {
            BlockList.Clear();
            Handle = reader.ReadTagBlock();
            BlockAddress = Handle.Offset;

            if (Handle.Count > 0)
            {
                long pos = reader.BaseStream.Position;
                reader.BaseStream.Seek(Handle.Offset, SeekOrigin.Begin);

                for (int i = 0; i < Handle.Count; i++)
                {
                    if (Add(out Block block))
                    {
                        block.Read(reader);
                        block.Owner = this;
                    }
                }

                reader.BaseStream.Position = pos;
            }
        }
        protected sealed override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
        protected sealed override void OnPostWrite(BinaryWriter writer)
        {
            TagBlock tagBlock = TagBlock.Zero;

            if (BlockList.Count > 0)
            {
                tagBlock.Count = (uint)BlockList.Count;
                using (VirtualStream vs = new VirtualStream(writer.BaseStream.Position))
                using (BinaryWriter virtualWriter = new BinaryWriter(vs))
                {
                    BlockAddress = vs.Align(BlockList[0].Alignment);
                    tagBlock.Offset = (uint)BlockAddress;

                    foreach (var block in BlockList)
                    {
                        block.Write(virtualWriter);
                    }

                    writer.Write(vs.ToArray());
                }

                foreach (var block in BlockList)
                {
                    block.PostWrite(writer);
                }
            }

            Handle = tagBlock;
            long pos = writer.BaseStream.Position;
            writer.BaseStream.Position = FieldAddress;
            writer.Write(tagBlock);
            writer.BaseStream.Position = pos;
        }
        protected override void OnOverwrite(BinaryWriter writer)
        {
            foreach (var block in BlockList)
            {
                block.Overwrite(writer);
            }
        }
        public abstract bool Add(out Block block);
        public abstract Block Create();
    }

    public sealed class BlockField<T> : BlockField, IEnumerable<T> where T : Block, new()
    {
        internal static int identIndex = 0;
        public T this[int index]
        {
            get
            {
                if (BlockList.Count > index && index >= 0)
                    return BlockList[index] as T;
                throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
        public override int Size => 8;
        public BlockField(string name, int maximumElementCount) : base(name, maximumElementCount)
        {
            Value = TagBlock.Zero;
        }
        public override bool Add(out Block block)
        {
            block = new T();
            block.Initialize();

            if (BlockList.Add(block))
            {
                return true;
            }

            block = null;
            return true;
        }
        public override Block Create()
        {
            T tagBlock = new T();
            tagBlock.Initialize();

            return tagBlock;
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < BlockList.Count; i++)
            {
                yield return BlockList[i] as T;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < BlockList.Count; i++)
            {
                yield return BlockList[i];
            }
        }
    }

    public abstract class OptionField : Field
    {
        public ObservableCollection<Option> Options { get; } = new ObservableCollection<Option>();
        protected OptionField(FieldType type, string name, params string[] options) : base(type, name)
        {
            for (int i = 0; i < options.Length; i++)
                Options.Add(new Option(options[i], i));
        }
    }

    public abstract class BaseFlagsField : OptionField
    {
        protected BaseFlagsField(FieldType type, string name, params string[] options) : base(type, name, options) { }
        public abstract bool HasFlag(Option option);
        public abstract object SetFlag(Option option, bool toggle);
        public List<Option> GetFlags()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class BaseEnumField : OptionField
    {
        public abstract Option SelectedOption { get; set; }
        protected BaseEnumField(FieldType type, string name, params string[] options) : base(type, name, options) { }
        public override string GetValueString()
        {
            if (SelectedOption != null)
            {
                return SelectedOption.Name;
            }

            return base.ToString();
        }
    }

    public abstract class BaseBlockIndexField : BaseEnumField
    {
        public string Instructions
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        protected BaseBlockIndexField(FieldType type, string name) : base(type, name)
        {
            Instructions = string.Empty;
        }
        protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Instructions))
            {
                OnInstructionsPropertyChanged(e);
            }

            base.OnNotifyPropertyChanged(e);
        }
        protected virtual void OnInstructionsPropertyChanged(PropertyChangedEventArgs e)
        {
            TagObject obj = this;
            var ex = new FormatException();
            using (StringReader reader = new StringReader(Instructions ?? string.Empty))
            {
                string token;
                while (reader.Peek() != -1)
                {
                    token = ReadToken(reader);

                    switch (token.ToUpper())
                    {
                        case "U":
                            if (obj.Owner != null)
                            {
                                obj = obj.Owner;
                            }
                            else
                            {
                                throw ex;
                            }
                            break;

                        case "F":
                            if (reader.Peek() != -1)
                            {
                                var indexToken = ReadToken(reader);
                                if (int.TryParse(indexToken, out int i) && obj is Block)
                                {
                                    obj = ((Block)obj).Fields[i];
                                }
                            }
                            else
                            {
                                throw ex;
                            }
                            break;

                        case "BL":
                            if (obj is BlockField blockField)
                            {
                                for (int j = 0; j < blockField.BlockList.Count; j++)
                                {
                                    var block = blockField.BlockList[j];
                                    Options.Add(new Option(block, j));
                                }
                            }
                            else
                            {
                                throw ex;
                            }
                            break;

                        default:
                            throw ex;
                    }
                }
            }
        }

        private string ReadToken(TextReader reader)
        {
            StringBuilder tokenBuilder = new StringBuilder();

            SkipWhitespace(reader);
            while (reader.Peek() != -1)
            {
                char peekChar = (char)reader.Peek();
                if (!char.IsWhiteSpace(peekChar))
                {
                    tokenBuilder.Append((char)reader.Read());
                }
                else
                {
                    break;
                }
            }

            return tokenBuilder.ToString();
        }
        private void SkipWhitespace(TextReader reader)
        {
            while (reader.Peek() != -1 && char.IsWhiteSpace((char)reader.Peek()))
            {
                _ = reader.Read();
            }
        }
    }

    public abstract class NumberField<T> : Field where T : struct, IComparable, IComparable<T>, IEquatable<T>
    {
        public T Number
        {
            get => (T)Value;
            set
            {
                if (Value is T num)
                {
                    if (num.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        protected NumberField(FieldType type, string name) : base(type, name)
        {
            Number = default;
        }
    }

    public sealed class CharIntegerField : NumberField<byte>
    {
        public override int Size => 1;
        public CharIntegerField(string name) : base(FieldType.FieldCharInteger, name) { }
        protected override void OnRead(BinaryReader reader)
        {
            Number = reader.ReadByte();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Number);
        }
    }

    public sealed class ShortIntegerField : NumberField<short>
    {
        public override int Size => 2;
        public ShortIntegerField(string name) : base(FieldType.FieldShortInteger, name) { }
        protected override void OnRead(BinaryReader reader)
        {
            Number = reader.ReadInt16();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Number);
        }
    }

    public sealed class LongIntegerField : NumberField<int>
    {
        public override int Size => 4;
        public LongIntegerField(string name) : base(FieldType.FieldLongInteger, name) { }
        protected override void OnRead(BinaryReader reader)
        {
            Number = reader.ReadInt32();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Number);
        }
    }

    public sealed class AngleField : NumberField<float>
    {
        public override int Size => 4;
        public AngleField(string name) : base(FieldType.FieldAngle, name) { }
        protected override void OnRead(BinaryReader reader)
        {
            Number = reader.ReadSingle();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Number);
        }
    }

    public sealed class RealField : NumberField<float>
    {
        public override int Size => 4;
        public RealField(string name) : base(FieldType.FieldReal, name)
        {
            Value = 0f;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Number = reader.ReadSingle();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Number);
        }
    }

    public sealed class RealFractionField : NumberField<float>
    {
        public override int Size => 4;
        public RealFractionField(string name) : base(FieldType.FieldRealFraction, name)
        {
            Value = 0f;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Number = reader.ReadSingle();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Number);
        }
    }

    public sealed class TagField : Field
    {
        public override int Size => 4;
        public TagFourCc Tag
        {
            get => (TagFourCc)Value;
            set
            {
                if (Value is TagFourCc tag)
                {
                    if (tag.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public TagField(string name) : base(FieldType.FieldTag, name)
        {
            Tag = new TagFourCc();
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<TagFourCc>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write<TagFourCc>(Value);
        }
    }

    public sealed class CharEnumField : BaseEnumField
    {
        public override int Size => 1;
        public override Option SelectedOption
        {
            get => Options[SelectedIndex];
            set
            {
                if (Options.Contains(value) && SelectedIndex != value.Index)
                {
                    SelectedIndex = (byte)value.Index;
                    NotifyPropertyChanged();
                }
            }
        }
        public byte SelectedIndex
        {
            get => (byte)Value;
            set
            {
                if (Value is byte i)
                {
                    if (i.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public CharEnumField(string name, params string[] options) : base(FieldType.FieldCharEnum, name, options)
        {
            SelectedIndex = 0;
        }
        protected override void OnRead(BinaryReader reader)
        {
            SelectedIndex = reader.ReadByte();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(SelectedIndex);
        }
        public override string ToString()
        {
            return $"{base.ToString()} ({Options[SelectedIndex]?.Name ?? "IndexOutOfRange"})";
        }
    }

    public sealed class EnumField : BaseEnumField
    {
        public override int Size => 2;
        public override Option SelectedOption
        {
            get => Options[SelectedIndex];
            set
            {
                if (Options.Contains(value) && SelectedIndex != value.Index)
                {
                    SelectedIndex = (short)value.Index;
                    NotifyPropertyChanged();
                }
            }
        }
        public short SelectedIndex
        {
            get => (short)Value;
            set
            {
                if (Value is short i)
                {
                    if (i.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public EnumField(string name, params string[] options) : base(FieldType.FieldEnum, name, options)
        {
            SelectedIndex = 0;
        }
        protected override void OnRead(BinaryReader reader)
        {
            SelectedIndex = reader.ReadInt16();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(SelectedIndex);
        }
        public override string ToString()
        {
            return $"{base.ToString()} ({Options[SelectedIndex]?.Name ?? "IndexOutOfRange"})";
        }
    }

    public sealed class LongEnumField : BaseEnumField
    {
        public override int Size => 4;
        public override Option SelectedOption
        {
            get => Options[SelectedIndex];
            set
            {
                if (Options.Contains(value) && SelectedIndex != value.Index)
                {
                    SelectedIndex = (int)value.Index;
                    NotifyPropertyChanged();
                }
            }
        }
        public int SelectedIndex
        {
            get => (int)Value;
            set
            {
                if (Value is int i)
                {
                    if (i.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public LongEnumField(string name, params string[] options) : base(FieldType.FieldLongEnum, name, options)
        {
            SelectedIndex = 0;
        }
        protected override void OnRead(BinaryReader reader)
        {
            SelectedIndex = reader.ReadInt32();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(SelectedIndex);
        }
        public override string ToString()
        {
            return $"{base.ToString()} ({Options[SelectedIndex]?.Name ?? "IndexOutOfRange"})";
        }
    }

    public sealed class LongFlagsField : BaseFlagsField
    {
        public override int Size => 4;
        public int Flags
        {
            get => (int)Value;
            set
            {
                if (Value is int flags)
                {
                    if (flags.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public LongFlagsField(string name, params string[] options) : base(FieldType.FieldLongFlags, name, options)
        {
            Flags = 0;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.ReadInt32();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
        public override string ToString()
        {
            List<string> flagged = new List<string>();
            foreach (Option option in Options)
                if ((Flags & (1 << (option.Index + 1))) == (1 << (option.Index + 1)))
                    flagged.Add(option.Name);

            return $"{Name} = [{string.Join(",", flagged.ToArray())}]";
        }
        public override bool HasFlag(Option option)
        {
            int flag = (1 << (option.Index + 1));
            return (Flags & flag) == flag;
        }
        public override object SetFlag(Option option, bool toggle)
        {
            int flags = Flags;
            int flag = 1 << (option.Index + 1);
            flags &= ~flag;

            if (toggle)
            {
                flags |= flag;
            }

            Flags = flags;
            return flags;
        }
    }

    public sealed class WordFlagsField : BaseFlagsField
    {
        public override int Size => 2;
        public short Flags
        {
            get => (short)Value;
            set
            {
                if (Value is short flags)
                {
                    if (flags.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public WordFlagsField(string name, params string[] options) : base(FieldType.FieldWordFlags, name, options)
        {
            Flags = 0;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.ReadInt16();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
        public override string ToString()
        {
            List<string> flagged = new List<string>();
            foreach (Option option in Options)
                if ((Flags & (1 << (option.Index + 1))) == (1 << (option.Index + 1)))
                    flagged.Add(option.Name);

            return $"{Name} = [{string.Join(",", flagged.ToArray())}]";
        }
        public override bool HasFlag(Option option)
        {
            int flag = (1 << (option.Index + 1));
            return (Flags & flag) == flag;
        }
        public override object SetFlag(Option option, bool toggle)
        {
            int flags = Flags;
            int flag = 1 << (option.Index + 1);
            flags = flags & (~flag);
            if (toggle) flags |= flag;

            Flags = (short)flags;
            return (short)flags;
        }
    }

    public sealed class ByteFlagsField : BaseFlagsField
    {
        public override int Size => 1;
        public byte Flags
        {
            get => (byte)Value;
            set
            {
                if (Value is byte flags)
                {
                    if (flags.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public ByteFlagsField(string name, params string[] options) : base(FieldType.FieldByteFlags, name, options)
        {
            Flags = 0;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.ReadByte();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
        public override string ToString()
        {
            List<string> flagged = new List<string>();
            foreach (Option option in Options)
                if ((Flags & (1 << (option.Index + 1))) == (1 << (option.Index + 1)))
                    flagged.Add(option.Name);

            return $"{Name} = [{string.Join(",", flagged.ToArray())}]";
        }
        public override bool HasFlag(Option option)
        {
            int flag = (1 << (option.Index + 1));
            return (Flags & flag) == flag;
        }
        public override object SetFlag(Option option, bool toggle)
        {
            int flags = Flags;
            int flag = 1 << (option.Index + 1);
            flags = flags & (~flag);
            if (toggle) flags |= flag;

            Flags = (byte)flags;
            return (byte)flags;
        }
    }

    public sealed class Point2dField : Field
    {
        public Point2 Point
        {
            get => (Point2)Value;
            set
            {
                if (Value is Point2 point)
                {
                    if (point.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public override int Size => 4;
        public Point2dField(string name) : base(FieldType.FieldPoint2D, name)
        {
            Point = Point2.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Point = reader.Read<Point2>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Point);
        }
    }

    public sealed class Rectangle2dField : Field
    {
        public override int Size => 8;
        public Rectangle2 Rectangle
        {
            get => (Rectangle2)Value;
            set
            {
                if (Value is Rectangle2 rectangle)
                {
                    if (rectangle.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public Rectangle2dField(string name) : base(FieldType.FieldRectangle2D, name)
        {
            Value = Rectangle2.Empty;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<Rectangle2>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class RgbColorField : Field
    {
        public override int Size => 3;
        public ColorRgb Color
        {
            get => (ColorRgb)Value;
            set
            {
                if (Value is ColorRgb color)
                {
                    if (color.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public RgbColorField(string name) : base(FieldType.FieldRgbColor, name)
        {
            Value = ColorRgb.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<ColorRgb>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class ArgbColorField : Field
    {
        public override int Size => 4;
        public ColorArgb Color
        {
            get => (ColorArgb)Value;
            set
            {
                if (Value is ColorArgb color)
                {
                    if (color.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public ArgbColorField(string name) : base(FieldType.FieldArgbColor, name)
        {
            Value = ColorArgb.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<ColorArgb>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class RealPoint2dField : Field
    {
        public override int Size => 8;
        public Point2F Point
        {
            get => (Point2F)Value;
            set
            {
                if (Value is Point2F point)
                {
                    if (point.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public RealPoint2dField(string name) : base(FieldType.FieldRealPoint2D, name)
        {
            Value = Point2F.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<Point2F>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class RealPoint3dField : Field
    {
        public override int Size => 12;
        public Point3F Point
        {
            get => (Point3F)Value;
            set
            {
                if (Value is Point3F point)
                {
                    if (point.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public RealPoint3dField(string name) : base(FieldType.FieldRealPoint3D, name)
        {
            Point = Point3F.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Point = reader.Read<Point3F>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Point);
        }
    }

    public sealed class RealVector2dField : Field
    {
        public override int Size => 8;
        public Vector2 Vector
        {
            get => (Vector2)Value;
            set
            {
                if (Value is Vector2 vector)
                {
                    if (vector.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public RealVector2dField(string name) : base(FieldType.FieldRealVector2D, name)
        {
            Vector = Vector2.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Vector = reader.Read<Vector2>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Vector);
        }
    }

    public sealed class RealVector3dField : Field
    {
        public override int Size => 12;
        public Vector3 Vector
        {
            get => (Vector3)Value;
            set
            {
                if (Value is Vector3 vector)
                {
                    if (vector.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public RealVector3dField(string name) : base(FieldType.FieldRealVector3D, name)
        {
            Vector = Vector3.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Vector = reader.Read<Vector3>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Vector);
        }
    }

    public sealed class QuaternionField : Field
    {
        public override int Size => 16;
        public Quaternion Quaternion
        {
            get => (Quaternion)Value;
            set
            {
                if (Value is Quaternion quaternion)
                {
                    if (quaternion.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public QuaternionField(string name) : base(FieldType.FieldQuaternion, name)
        {
            Quaternion = Quaternion.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Quaternion = reader.Read<Quaternion>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Quaternion);
        }
    }

    public sealed class EulerAngles2dField : Field
    {
        public override int Size => 8;
        public Vector2 Vector
        {
            get => (Vector2)Value;
            set
            {
                if (Value is Vector2 vector)
                {
                    if (vector.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public EulerAngles2dField(string name) : base(FieldType.FieldEulerAngles2D, name)
        {
            Vector = Vector2.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Vector = reader.Read<Vector2>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Vector);
        }
    }

    public sealed class EulerAngles3dField : Field
    {
        public override int Size => 12;
        public Vector3 Vector
        {
            get => (Vector3)Value;
            set
            {
                if (Value is Vector3 vector)
                {
                    if (vector.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public EulerAngles3dField(string name) : base(FieldType.FieldEulerAngles3D, name)
        {
            Vector = Vector3.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Vector = reader.Read<Vector3>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Vector);
        }
    }

    public sealed class RealPlane2dField : Field
    {
        public override int Size => 12;
        public Vector3 Vector
        {
            get => (Vector3)Value;
            set
            {
                if (Value is Vector3 vector)
                {
                    if (vector.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public RealPlane2dField(string name) : base(FieldType.FieldRealPlane2D, name)
        {
            Vector = Vector3.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Vector = reader.Read<Vector3>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Vector);
        }
    }

    public sealed class RealPlane3dField : Field
    {
        public override int Size => 16;
        public Vector4 Vector
        {
            get => (Vector4)Value;
            set
            {
                if (Value is Vector4 vector)
                {
                    if (vector.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public RealPlane3dField(string name) : base(FieldType.FieldRealPlane3D, name)
        {
            Vector = Vector4.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Vector = reader.Read<Vector4>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Vector);
        }
    }

    public sealed class RealRgbColorField : Field
    {
        public override int Size => 12;
        public ColorRgbF Color
        {
            get => (ColorRgbF)Value;
            set
            {
                if (Value is ColorRgbF color)
                {
                    if (color.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public RealRgbColorField(string name) : base(FieldType.FieldRealRgbColor, name)
        {
            Color = ColorRgbF.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Color = reader.Read<ColorRgbF>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Color);
        }
    }

    public sealed class RealArgbColorField : Field
    {
        public override int Size => 16;
        public ColorArgbF Color
        {
            get => (ColorArgbF)Value;
            set
            {
                if (Value is ColorArgbF color)
                {
                    if (color.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public RealArgbColorField(string name) : base(FieldType.FieldRealArgbColor, name)
        {
            Color = ColorArgbF.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Color = reader.Read<ColorArgbF>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Color);
        }
    }

    public sealed class RealHsvColorField : Field
    {
        public override int Size => 12;
        public ColorHsv Color
        {
            get => (ColorHsv)Value;
            set
            {
                if (Value is ColorHsv color)
                {
                    if (color.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public RealHsvColorField(string name) : base(FieldType.FieldRealHsvColor, name)
        {
            Value = ColorHsv.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<ColorHsv>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class RealAhsvColorField : Field
    {
        public override int Size => 16;
        public ColorAhsv Color
        {
            get => (ColorAhsv)Value;
            set
            {
                if (Value is ColorAhsv color)
                {
                    if (color.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public RealAhsvColorField(string name) : base(FieldType.FieldRealAhsvColor, name)
        {
            Value = ColorAhsv.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<ColorAhsv>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class ShortBoundsField : Field
    {
        public override int Size => 4;
        public ShortBounds Bounds
        {
            get => (ShortBounds)Value;
            set
            {
                if (Value is ShortBounds bounds)
                {
                    if (bounds.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public ShortBoundsField(string name) : base(FieldType.FieldShortBounds, name)
        {
            Value = ShortBounds.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<ShortBounds>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public abstract class FloatBoundsField : Field
    {
        public sealed override int Size => 8;
        public FloatBounds Bounds
        {
            get => (FloatBounds)Value;
            set
            {
                if (Value is FloatBounds bounds)
                {
                    if (bounds.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        protected FloatBoundsField(FieldType type, string name) : base(type, name)
        {
            Value = FloatBounds.Zero;
        }
        protected sealed override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<FloatBounds>();
        }
        protected sealed override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class AngleBoundsField : FloatBoundsField
    {
        public AngleBoundsField(string name) : base(FieldType.FieldAngleBounds, name)
        {
            Value = FloatBounds.Zero;
        }
    }

    public sealed class RealBoundsField : FloatBoundsField
    {
        public RealBoundsField(string name) : base(FieldType.FieldRealBounds, name)
        {
            Value = FloatBounds.Zero;
        }
    }

    public sealed class RealFractionBoundsField : FloatBoundsField
    {
        public RealFractionBoundsField(string name) : base(FieldType.FieldRealFractionBounds, name)
        {
            Value = FloatBounds.Zero;
        }
    }

    public sealed class CharBlockIndexField : BaseBlockIndexField
    {
        public override int Size => 1;
        public sbyte BlockIndex
        {
            get => (sbyte)Value;
            set
            {
                if (Value is sbyte i)
                {
                    if (i.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public override Option SelectedOption
        {
            get => Options.ElementAtOrDefault(BlockIndex + 1);
            set
            {
                if (Options.Contains(value))
                {
                    int index = Options.IndexOf(value);
                    BlockIndex = (sbyte)index;
                    NotifyPropertyChanged();
                }
            }
        }
        public BlockSearchProcedure<short> SearchProcedure { get; set; }
        public CharBlockIndexField(string name) : base(FieldType.FieldCharBlockIndex1, name)
        {
            BlockIndex = -1;
            Options.Add(new Option("Null", -1));
        }
        protected override void OnRead(BinaryReader reader)
        {
            BlockIndex = reader.ReadSByte();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(BlockIndex);
        }
    }

    public sealed class ShortBlockIndexField : BaseBlockIndexField
    {
        public override int Size => 2;
        public short BlockIndex
        {
            get => (short)Value;
            set
            {
                if (Value is short i)
                {
                    if (i.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public override Option SelectedOption
        {
            get => Options.ElementAtOrDefault(BlockIndex + 1);
            set
            {
                if (Options.Contains(value))
                {
                    int index = Options.IndexOf(value);
                    BlockIndex = (short)index;
                    NotifyPropertyChanged();
                }
            }
        }
        public BlockSearchProcedure<short> SearchProcedure { get; set; }
        public ShortBlockIndexField(string name) : base(FieldType.FieldShortBlockIndex1, name)
        {
            BlockIndex = -1;
            Options.Add(new Option("Null", -1));
        }
        protected override void OnRead(BinaryReader reader)
        {
            BlockIndex = reader.ReadInt16();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(BlockIndex);
        }
    }

    public sealed class LongBlockIndexField : BaseBlockIndexField
    {
        public override int Size => 4;
        public int BlockIndex
        {
            get => (int)Value;
            set
            {
                if (Value is int i)
                {
                    if (i.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public override Option SelectedOption
        {
            get => Options.ElementAtOrDefault(BlockIndex + 1);
            set
            {
                if (Options.Contains(value))
                {
                    int index = Options.IndexOf(value);
                    BlockIndex = (int)index;
                    NotifyPropertyChanged();
                }
            }
        }
        public BlockSearchProcedure<short> SearchProcedure { get; set; }
        public LongBlockIndexField(string name) : base(FieldType.FieldLongBlockIndex1, name)
        {
            BlockIndex = -1;
            Options.Add(new Option("Null", -1));
        }
        protected override void OnRead(BinaryReader reader)
        {
            BlockIndex = reader.ReadInt32();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(BlockIndex);
        }
    }

    public sealed class DataField : Field
    {
        private byte[] buffer = new byte[0];

        public override int Size => 8;
        public int BufferLength => buffer.Length;
        public int Alignment { get; }
        public int ElementSize { get; }
        public long DataAddress { get; private set; }
        public TagBlock Handle
        {
            get => (TagBlock)Value;
            set
            {
                if (Value is TagBlock tagBlock)
                {
                    if (tagBlock.Equals(value))
                    {
                        return;
                    }
                }

                Value = value;
                NotifyPropertyChanged();
            }
        }
        public DataField(string name, int elementSize, int alignment = 4) : base(FieldType.FieldData, name)
        {
            ElementSize = elementSize;
            Alignment = alignment;
            Handle = TagBlock.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            buffer = new byte[0];
            Value = reader.Read<TagBlock>();

            if (Handle.Count > 0)
            {
                if (Handle.Offset == 0) System.Diagnostics.Debugger.Break();

                long pos = reader.BaseStream.Position;
                reader.BaseStream.Seek(Handle.Offset, SeekOrigin.Begin);
                buffer = reader.ReadBytes((int)(ElementSize * Handle.Count));
                reader.BaseStream.Position = pos;
            }
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(TagBlock.Zero);
        }
        protected override void OnPostWrite(BinaryWriter writer)
        {
            TagBlock tagBlock = TagBlock.Zero;

            if (buffer.Length > 0)
            {
                DataAddress = writer.BaseStream.Align(Alignment);
                tagBlock.Count = (uint)(buffer.Length / ElementSize);
                tagBlock.Offset = (uint)writer.BaseStream.Position;

                writer.Write(buffer);
            }

            Handle = tagBlock;
            long pos = writer.BaseStream.Position;
            writer.BaseStream.Position = FieldAddress;
            writer.Write(tagBlock);

            writer.BaseStream.Position = pos;
        }
        protected override void OnOverwrite(BinaryWriter writer)
        {
            if (BufferLength > 0)
            {
                writer.BaseStream.Seek(Handle.Offset, SeekOrigin.Begin);
                writer.Write(buffer);
            }
        }
        public byte[] GetBuffer()
        {
            return buffer;
        }
        public void SetBuffer(byte[] buffer)
        {
            this.buffer = (byte[])buffer.Clone() ?? throw new ArgumentNullException(nameof(buffer));
        }
    }

    public sealed class VertexBufferField : Field
    {
        public override int Size => 32;
        public VertexBufferField(string name) : base(FieldType.FieldVertexBuffer, name)
        {
            Value = new VertexBuffer();
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<VertexBuffer>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write<VertexBuffer>(Value);
        }
    }

    public sealed class PadField : Field
    {
        public override int Size => Length;
        public int Length { get; }
        public byte[] Data
        {
            get => (byte[])Value;
        }
        public PadField(string name, int length) : base(FieldType.FieldPad, name)
        {
            Length = length;
            Value = new byte[length];
        }
        public override string ToString()
        {
            return $"Padding ({Length})";
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.ReadBytes(Length);
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Data);
        }
    }

    public sealed class SkipField : Field
    {
        public override int Size => Length;
        public int Length { get; }
        public byte[] Data
        {
            get => (byte[])Value;
        }
        public SkipField(string name, int length) : base(FieldType.FieldSkip, name)
        {
            Length = length;
            Value = new byte[length];
        }
        public override string ToString()
        {
            return $"Skip ({Length})";
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.ReadBytes(Length);
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Data);
        }
    }

    public sealed class StructField<T> : StructField where T : Block, new()
    {
        public StructField(string name) : base(name) { }
        public override Block Create()
        {
            return new T();
        }
    }
}
