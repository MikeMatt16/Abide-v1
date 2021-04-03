using Abide.HaloLibrary;
using Abide.HaloLibrary.IO;
using Abide.Tag.Definition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace Abide.Tag
{
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

    public abstract class Field : ITagField
    {
        private readonly ObjectName name;

        public object Value { get; set; }
        public abstract int Size { get; }
        public FieldType Type { get; }
        public string Name => name.Name ?? string.Empty;
        public string Information => name.Information ?? string.Empty;
        public string Details => name.Details ?? string.Empty;
        public bool IsReadOnly => name.IsReadOnly;
        public bool IsBlockName => name.IsBlockName;
        public long FieldAddress { get; private set; } = 0;

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
        public object Clone()
        {
            var clone = CloneField();
            clone.FieldAddress = FieldAddress;

            if (Value is ICloneable cloneableValue)
            {
                clone.Value = cloneableValue.Clone();
            }
            else
            {
                clone.Value = Value;
            }

            return clone;
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
        protected abstract Field CloneField();
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
        protected override Field CloneField()
        {
            return new ExplanationField(GetName(), Explanation);
        }
    }

    public abstract class BaseStringField : Field
    {
        public abstract string String { get; set; }
        protected BaseStringField(FieldType type, string name) :base(type, name)
        {
            String = string.Empty;
        }
    }

    public sealed class StringField : BaseStringField
    {
        public override int Size => 32;
        public override string String
        {
            get => (string)(String32)Value;
            set => Value = (String32)value;
        }
        public StringField(string name) : base(FieldType.FieldString, name) { }
        protected override void OnRead(BinaryReader reader)
        {
            String = (string)reader.Read<String32>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write((String32)String);
        }
        protected override Field CloneField()
        {
            return new StringField(GetName());
        }
    }

    public sealed class LongStringField : BaseStringField
    {
        public override int Size => 256;
        public override string String
        {
            get => (string)(String256)Value;
            set => Value = (String256)value;
        }
        public LongStringField(string name) : base(FieldType.FieldLongString, name) { }
        protected override void OnRead(BinaryReader reader)
        {
            String = (string)reader.Read<String256>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write((String256)String);
        }
        protected override Field CloneField()
        {
            return new LongStringField(GetName());
        }
    }

    public abstract class StructField : Field
    {
        public override int Size => Block.Size;
        public Block Block
        {
            get => (Block)Value;
            set => Value = value;
        }
        protected StructField(string name) : base(FieldType.FieldStruct, name)
        {
            Block = Create();
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
        public sealed override int Size => 8;
        public BlockList BlockList { get; }
        public long BlockAddress { get; protected set; } = -1;
        public TagBlock Handle
        {
            get => (TagBlock)Value;
            set => Value = value;
        }
        protected BlockField(string name, int maximumElementCount) : base(FieldType.FieldBlock, name)
        {
            Handle = TagBlock.Zero;
            BlockList = new BlockList(maximumElementCount);
        }
        public bool AddNew(out Block block)
        {
            block = Create();
            block.Initialize();

            if (BlockList.Add(block))
            {
                return true;
            }

            block = null;
            return false;
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
                    if (AddNew(out Block block))
                    {
                        block.Read(reader);
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
        public abstract Block Create();
    }

    public sealed class BlockField<T> : BlockField, IEnumerable<T> where T : Block, new()
    {
        public BlockField(string name, int maximumElementCount) : base(name, maximumElementCount) { }
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
        protected override Field CloneField()
        {
            var f = new BlockField<T>(GetName(), BlockList.MaximumCount) { BlockAddress = BlockAddress };

            foreach (var block in BlockList)
            {
                if (!f.BlockList.Add((Block)block.Clone()))
                {
                    Console.WriteLine("Unable to add block while cloning block field.");
                    System.Diagnostics.Debugger.Break();
                    break;
                }
            }

            return f;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < BlockList.Count; i++)
            {
                yield return BlockList[i];
            }
        }
    }

    public sealed class StructField<T> : StructField where T : Block, new()
    {
        public StructField(string name) : base(name) { }
        public override Block Create()
        {
            return new T();
        }
        protected override Field CloneField()
        {
            return new StructField<T>(GetName());
        }
    }

    public abstract class OptionField : Field
    {
        public ObservableCollection<Option> Options { get; } = new ObservableCollection<Option>();
        protected OptionField(FieldType type, string name) : base(type, name) { }
        protected OptionField(FieldType type, string name, params string[] options) : base(type, name)
        {
            for (int i = 0; i < options.Length; i++)
                Options.Add(new Option(options[i], i));
        }
    }

    public abstract class BaseFlagsField : OptionField
    {
        public abstract int Flags { get; set; }
        protected BaseFlagsField(FieldType type, string name, params string[] options) : base(type, name, options) { }
        public abstract bool HasFlag(Option option);
        public abstract object SetFlag(Option option, bool toggle);
    }

    public abstract class BaseEnumField : OptionField
    {
        public Option SelectedOption
        {
            get => Options[SelectedIndex];
            set => SelectedIndex = Options.IndexOf(value);
        }
        public abstract int SelectedIndex { get; set; }
        protected BaseEnumField(FieldType type, string name, params string[] options) : base(type, name, options) { }
    }

    public abstract class BaseBlockIndexField : OptionField
    {
        public abstract int SelectedIndex { get; set; }
        protected BaseBlockIndexField(FieldType type, string name) : base(type, name)
        {
            Options.Add(new Option("Null", -1));
            SelectedIndex = -1;
        }
    }

    public abstract class NumericField : Field
    {
        public abstract double Number { get; set; }
        protected NumericField(FieldType type, string name) : base(type, name)
        {
            Number = 0;
        }
    }

    public sealed class CharIntegerField : NumericField
    {
        public override int Size => 1;
        public override double Number
        {
            get => (byte)Value;
            set => Value = (byte)value;
        }
        public CharIntegerField(string name) : base(FieldType.FieldCharInteger, name) { }
        protected override void OnRead(BinaryReader reader)
        {
            Number = reader.ReadByte();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write((byte)Number);
        }
        protected override Field CloneField()
        {
            return new CharIntegerField(GetName());
        }
    }

    public sealed class ShortIntegerField : NumericField
    {
        public override int Size => 2;
        public override double Number
        {
            get => (short)Value;
            set => Value = (short)value;
        }
        public ShortIntegerField(string name) : base(FieldType.FieldShortInteger, name) { }
        protected override void OnRead(BinaryReader reader)
        {
            Number = reader.ReadInt16();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write((short)Number);
        }
        protected override Field CloneField()
        {
            return new ShortIntegerField(GetName());
        }
    }

    public sealed class LongIntegerField : NumericField
    {
        public override int Size => 4;
        public override double Number
        {
            get => (int)Value;
            set => Value = (int)value;
        }
        public LongIntegerField(string name) : base(FieldType.FieldLongInteger, name) { }
        protected override void OnRead(BinaryReader reader)
        {
            Number = reader.ReadInt32();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write((int)Number);
        }
        protected override Field CloneField()
        {
            return new LongIntegerField(GetName());
        }
    }

    public abstract class BaseFloatField : NumericField
    {
        public sealed override int Size => 4;
        public sealed override double Number
        {
            get => (float)Value;
            set => Value = (float)value;
        }
        protected BaseFloatField(FieldType type, string name) : base(type, name) { }
        protected sealed override void OnRead(BinaryReader reader)
        {
            Number = reader.ReadSingle();
        }
        protected sealed override void OnWrite(BinaryWriter writer)
        {
            writer.Write((float)Number);
        }
    }

    public sealed class AngleField : BaseFloatField
    {
        public AngleField(string name) : base(FieldType.FieldAngle, name) { }
        protected override Field CloneField()
        {
            return new AngleField(GetName());
        }
    }

    public sealed class RealField : BaseFloatField
    {
        public RealField(string name) : base(FieldType.FieldReal, name)
        {
            Value = 0f;
        }
        protected override Field CloneField()
        {
            return new RealField(GetName());
        }
    }

    public sealed class RealFractionField : BaseFloatField
    {
        public RealFractionField(string name) : base(FieldType.FieldRealFraction, name)
        {
            Value = 0f;
        }
        protected override Field CloneField()
        {
            return new RealFractionField(GetName());
        }
    }

    public sealed class TagField : BaseStringField
    {
        public override int Size => 4;
        public override string String
        {
            get => (TagFourCc)Value;
            set => Value = new TagFourCc(value);
        }
        public TagField(string name) : base(FieldType.FieldTag, name) { }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<TagFourCc>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write<TagFourCc>(Value);
        }
        protected override Field CloneField()
        {
            return new TagField(GetName());
        }
    }

    public sealed class CharEnumField : BaseEnumField
    {
        public override int Size => 1;
        public override int SelectedIndex
        {
            get => (byte)Value;
            set => Value = (byte)value;
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
        protected override Field CloneField()
        {
            var e = new CharEnumField(GetName());
            foreach (var option in Options)
            {
                e.Options.Add(new Option(option.Name, option.Index));
            }
            return e;
        }
    }

    public sealed class EnumField : BaseEnumField
    {
        public override int Size => 2;
        public override int SelectedIndex
        {
            get => (short)Value;
            set => Value = (short)value;
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
        protected override Field CloneField()
        {
            var e = new EnumField(GetName());
            foreach (var option in Options)
            {
                e.Options.Add(new Option(option.Name, option.Index));
            }
            return e;
        }
    }

    public sealed class LongEnumField : BaseEnumField
    {
        public override int Size => 4;
        public override int SelectedIndex
        {
            get => (int)Value;
            set => Value = (int)value;
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
        protected override Field CloneField()
        {
            var e = new LongEnumField(GetName());
            foreach (var option in Options)
            {
                e.Options.Add(new Option(option.Name, option.Index));
            }
            return e;
        }
    }

    public sealed class LongFlagsField : BaseFlagsField
    {
        public override int Size => 4;
        public override int Flags
        {
            get => (int)Value;
            set => Value = value;
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
            int flag = 1 << (option.Index + 1);
            return (Flags & flag) == flag;
        }
        public override object SetFlag(Option option, bool toggle)
        {
            int flag = 1 << (option.Index + 1);
            int flags = Flags & ~flag;
            if (toggle) flags |= flag;

            Flags = flags;
            return flags;
        }
        protected override Field CloneField()
        {
            var e = new LongFlagsField(GetName());
            foreach (var option in Options)
            {
                e.Options.Add(new Option(option.Name, option.Index));
            }
            return e;
        }
    }

    public sealed class WordFlagsField : BaseFlagsField
    {
        public override int Size => 2;
        public override int Flags
        {
            get => (short)Value;
            set => Value = (short)value;
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
            int flag = 1 << (option.Index + 1);
            return (Flags & flag) == flag;
        }
        public override object SetFlag(Option option, bool toggle)
        {
            int flag = 1 << (option.Index + 1);
            int flags = Flags & ~flag;
            if (toggle) flags |= flag;

            Flags = (short)flags;
            return (short)flags;
        }
        protected override Field CloneField()
        {
            var e = new WordFlagsField(GetName());
            foreach (var option in Options)
            {
                e.Options.Add(new Option(option.Name, option.Index));
            }
            return e;
        }
    }

    public sealed class ByteFlagsField : BaseFlagsField
    {
        public override int Size => 1;
        public override int Flags
        {
            get => (byte)Value;
            set => Value = (byte)value;
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
            int flag = 1 << (option.Index + 1);
            return (Flags & flag) == flag;
        }
        public override object SetFlag(Option option, bool toggle)
        {
            int flag = 1 << (option.Index + 1);
            int flags = Flags & ~flag;
            if (toggle) flags |= flag;

            Flags = (byte)flags;
            return (byte)flags;
        }
        protected override Field CloneField()
        {
            var e = new ByteFlagsField(GetName());
            foreach (var option in Options)
            {
                e.Options.Add(new Option(option.Name, option.Index));
            }
            return e;
        }
    }

    public sealed class Point2dField : Field
    {
        public Point2 Point
        {
            get => (Point2)Value;
            set => Value = value;
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
        protected override Field CloneField()
        {
            return new Point2dField(GetName());
        }
    }

    public sealed class Rectangle2dField : Field
    {
        public override int Size => 8;
        public Rectangle2 Rectangle
        {
            get => (Rectangle2)Value;
            set => Value = value;
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
        protected override Field CloneField()
        {
            return new Rectangle2dField(GetName());
        }
    }

    public sealed class RgbColorField : Field
    {
        public override int Size => 3;
        public ColorRgb Color
        {
            get => (ColorRgb)Value;
            set => Value = value;
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
        protected override Field CloneField()
        {
            return new RgbColorField(GetName());
        }
    }

    public sealed class ArgbColorField : Field
    {
        public override int Size => 4;
        public ColorArgb Color
        {
            get => (ColorArgb)Value;
            set => Value = value;
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
        protected override Field CloneField()
        {
            return new ArgbColorField(GetName());
        }
    }

    public sealed class RealPoint2dField : Field
    {
        public override int Size => 8;
        public Point2F Point
        {
            get => (Point2F)Value;
            set => Value = value;
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
        protected override Field CloneField()
        {
            return new RealPoint2dField(GetName());
        }
    }

    public sealed class RealPoint3dField : Field
    {
        public override int Size => 12;
        public Point3F Point
        {
            get => (Point3F)Value;
            set => Value = value;
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
        protected override Field CloneField()
        {
            return new RealPoint3dField(GetName());
        }
    }

    public sealed class RealVector2dField : Field
    {
        public override int Size => 8;
        public Vector2 Vector
        {
            get => (Vector2)Value;
            set => Value = value;
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
        protected override Field CloneField()
        {
            return new RealVector2dField(GetName());
        }
    }

    public sealed class RealVector3dField : Field
    {
        public override int Size => 12;
        public Vector3 Vector
        {
            get => (Vector3)Value;
            set => Value = value;
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
        protected override Field CloneField()
        {
            return new RealVector3dField(GetName());
        }
    }

    public sealed class QuaternionField : Field
    {
        public override int Size => 16;
        public Quaternion Quaternion
        {
            get => (Quaternion)Value;
            set => Value = value;
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
        protected override Field CloneField()
        {
            return new QuaternionField(GetName());
        }
    }

    public sealed class EulerAngles2dField : Field
    {
        public override int Size => 8;
        public Vector2 Vector
        {
            get => (Vector2)Value;
            set => Value = value;
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
        protected override Field CloneField()
        {
            return new EulerAngles2dField(GetName());
        }
    }

    public sealed class EulerAngles3dField : Field
    {
        public override int Size => 12;
        public Vector3 Vector
        {
            get => (Vector3)Value;
            set => Value = value;
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
        protected override Field CloneField()
        {
            return new EulerAngles3dField(GetName());
        }
    }

    public sealed class RealPlane2dField : Field
    {
        public override int Size => 12;
        public Vector3 Vector
        {
            get => (Vector3)Value;
            set => Value = value;
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
        protected override Field CloneField()
        {
            return new RealPlane2dField(GetName());
        }
    }

    public sealed class RealPlane3dField : Field
    {
        public override int Size => 16;
        public Vector4 Vector
        {
            get => (Vector4)Value;
            set => Value = value;
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
        protected override Field CloneField()
        {
            return new RealPlane3dField(GetName());
        }
    }

    public sealed class RealRgbColorField : Field
    {
        public override int Size => 12;
        public ColorRgbF Color
        {
            get => (ColorRgbF)Value;
            set => Value = value;
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
        protected override Field CloneField()
        {
            return new RealRgbColorField(GetName());
        }
    }

    public sealed class RealArgbColorField : Field
    {
        public override int Size => 16;
        public ColorArgbF Color
        {
            get => (ColorArgbF)Value;
            set => Value = value;
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
        protected override Field CloneField()
        {
            return new RealArgbColorField(GetName());
        }
    }

    public sealed class RealHsvColorField : Field
    {
        public override int Size => 12;
        public ColorHsv Color
        {
            get => (ColorHsv)Value;
            set => Value = value;
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
        protected override Field CloneField()
        {
            return new RealHsvColorField(GetName());
        }
    }

    public sealed class RealAhsvColorField : Field
    {
        public override int Size => 16;
        public ColorAhsv Color
        {
            get => (ColorAhsv)Value;
            set => Value = value;
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
        protected override Field CloneField()
        {
            return new RealAhsvColorField(GetName());
        }
    }

    public sealed class ShortBoundsField : Field
    {
        public override int Size => 4;
        public ShortBounds Bounds
        {
            get => (ShortBounds)Value;
            set => Value = value;
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
        protected override Field CloneField()
        {
            return new ShortBoundsField(GetName());
        }
    }

    public abstract class FloatBoundsField : Field
    {
        public sealed override int Size => 8;
        public FloatBounds Bounds
        {
            get => (FloatBounds)Value;
            set => Value = value;
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
        protected override Field CloneField()
        {
            return new AngleBoundsField(GetName());
        }
    }

    public sealed class RealBoundsField : FloatBoundsField
    {
        public RealBoundsField(string name) : base(FieldType.FieldRealBounds, name)
        {
            Value = FloatBounds.Zero;
        }
        protected override Field CloneField()
        {
            return new RealBoundsField(GetName());
        }
    }

    public sealed class RealFractionBoundsField : FloatBoundsField
    {
        public RealFractionBoundsField(string name) : base(FieldType.FieldRealFractionBounds, name)
        {
            Value = FloatBounds.Zero;
        }
        protected override Field CloneField()
        {
            return new RealFractionBoundsField(GetName());
        }
    }

    public sealed class CharBlockIndexField : BaseBlockIndexField
    {
        public override int Size => 1;
        public override int SelectedIndex
        {
            get => (sbyte)Value;
            set => Value = (sbyte)value;
        }
        public CharBlockIndexField(string name) : base(FieldType.FieldCharBlockIndex1, name) { }
        protected override void OnRead(BinaryReader reader)
        {
            SelectedIndex = reader.ReadSByte();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write((sbyte)SelectedIndex);
        }
        protected override Field CloneField()
        {
            return new CharBlockIndexField(GetName());
        }
    }

    public sealed class ShortBlockIndexField : BaseBlockIndexField
    {
        public override int Size => 2;
        public override int SelectedIndex
        {
            get => (short)Value;
            set => Value = (short)value;
        }
        public ShortBlockIndexField(string name) : base(FieldType.FieldShortBlockIndex1, name) { }
        protected override void OnRead(BinaryReader reader)
        {
            SelectedIndex = reader.ReadInt16();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write((short)SelectedIndex);
        }
        protected override Field CloneField()
        {
            return new ShortBlockIndexField(GetName());
        }
    }

    public sealed class LongBlockIndexField : BaseBlockIndexField
    {
        public override int Size => 4;
        public override int SelectedIndex
        {
            get => (int)Value;
            set => Value = value;
        }
        public LongBlockIndexField(string name) : base(FieldType.FieldLongBlockIndex1, name) { }
        protected override void OnRead(BinaryReader reader)
        {
            SelectedIndex = reader.ReadInt32();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(SelectedIndex);
        }
        protected override Field CloneField()
        {
            return new LongBlockIndexField(GetName());
        }
    }

    public sealed class DataField : Field
    {
        private byte[] buffer = new byte[0];

        public override int Size => 8;
        public int BufferLength => buffer.Length;
        public int Alignment { get; }
        public int ElementSize { get; }
        public long DataAddress { get; set; }
        public TagBlock Handle
        {
            get => (TagBlock)Value;
            set => Value = value;
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
            Handle = reader.ReadTagBlock();

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
        protected override Field CloneField()
        {
            var d = new DataField(GetName(), ElementSize, Alignment) { DataAddress = DataAddress };
            d.SetBuffer(buffer);

            return d;
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
        protected override Field CloneField()
        {
            return new VertexBufferField(GetName());
        }
    }

    public abstract class BufferField : Field
    {
        private byte[] buffer = null;

        public sealed override int Size => Length;

        public int Length { get; }
        public byte[] Data
        {
            get => (byte[])buffer.Clone();
        }

        protected BufferField(FieldType type, string name, int length) : base(type, name)
        {
            Length = length;
            buffer = new byte[Length];
        }
        public void SetData(byte[] data)
        {
            if (data != null && data.Length >= Length)
            {
                Array.Copy(data, 0, buffer, 0, Length);
            }
            else
            {
                throw new ArgumentException("Invalid array.", nameof(data));
            }
        }
        protected sealed override void OnRead(BinaryReader reader)
        {
            buffer = reader.ReadBytes(Length);
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(buffer);
        }
    }

    public sealed class PadField : BufferField
    {
        public PadField(string name, int length) : base(FieldType.FieldPad, name, length) { }
        protected override Field CloneField()
        {
            var p = new PadField(GetName(), Length);
            p.SetData(Data);
            return p;
        }
    }

    public sealed class SkipField : BufferField
    {
        public SkipField(string name, int length) : base(FieldType.FieldSkip, name, length) { }
        protected override Field CloneField()
        {
            var p = new SkipField(GetName(), Length);
            p.SetData(Data);
            return p;
        }
    }
}
