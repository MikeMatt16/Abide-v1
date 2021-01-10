using Abide.HaloLibrary;
using Abide.HaloLibrary.IO;
using Abide.Tag.Definition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Abide.Tag
{
    public delegate Block BlockSearchProcedure<T>(Block tagBlock, int blockIndex) where T : IConvertible, IComparable, IComparable<T>, IEquatable<T>;

    public sealed class Option
    {
        private readonly ObjectName name;
        public string Name => name.Name ?? string.Empty;
        public string Information => name.Information ?? string.Empty;
        public string Details => name.Details ?? string.Empty;
        public int Index { get; }
        public Option(string name, int index)
        {
            this.name = new ObjectName(name ?? string.Empty);
            Index = index;
        }
    }

    public abstract class Field : ITagField, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ObjectName name;
        private object value = null;

        public FieldType Type { get; }
        public string Name => name.Name ?? string.Empty;
        public string Information => name.Information ?? string.Empty;
        public string Details => name.Details ?? string.Empty;
        public bool IsReadOnly => name.IsReadOnly;
        public bool IsBlockName => name.IsBlockName;
        public abstract int Size
        {
            get;
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
        public long FieldAddress { get; private set; } = 0;
        protected Field(FieldType type, string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            this.name = new ObjectName(name);
            Type = type;
            Value = null;
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
        public void Dispose()
        {
            Dispose(true);
        }
        protected virtual void OnRead(BinaryReader reader) { }
        protected virtual void OnWrite(BinaryWriter writer) { }
        protected virtual void OnPostWrite(BinaryWriter writer) { }
        protected virtual void OnOverwrite(BinaryWriter writer) { }
        protected virtual void Dispose(bool disposing)
        {
            if (Value is IDisposable disposable) disposable.Dispose();
            Value = null;
        }
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            string name = propertyName ?? string.Empty;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
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
        public StringField(string name) : base(FieldType.FieldString, name)
        {
            Value = new String32();
        }
        public new String32 Value
        {
            get => (String32)base.Value;
            set => base.Value = value;
        }
        protected override void OnRead(BinaryReader reader)
        {
            base.OnRead(reader);
            Value = reader.Read<String32>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class LongStringField : Field
    {
        public override int Size => 256;
        public new String256 Value
        {
            get => (String256)base.Value;
            set => base.Value = value;
        }
        public LongStringField(string name) : base(FieldType.FieldLongString, name)
        {
            Value = new String256();
        }
        protected override void OnRead(BinaryReader reader)
        {
            base.OnRead(reader);
            Value = reader.Read<String256>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public abstract class StructField : Field
    {
        public override int Size => Value.Size;
        public new Block Value
        {
            get => (Block)base.Value;
            set => base.Value = value;
        }
        protected StructField(string name, Block tagBlock) : base(FieldType.FieldStruct, name)
        {
            Value = tagBlock;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value.Read(reader);
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            Value.Write(writer);
        }
        protected override void OnPostWrite(BinaryWriter writer)
        {
            Value.PostWrite(writer);
        }
        public abstract Block Create();
    }

    public abstract class BlockField : Field
    {
        public BlockList BlockList { get; }
        public long BlockAddress { get; private set; } = -1;
        public new TagBlock Value
        {
            get => (TagBlock)base.Value;
            set => base.Value = value;
        }
        protected BlockField(string name, int maximumElementCount) : base(FieldType.FieldBlock, name)
        {
            Value = TagBlock.Zero;
            BlockList = new BlockList(maximumElementCount);
        }
        protected sealed override void OnRead(BinaryReader reader)
        {
            BlockList.Clear();
            Value = reader.Read<TagBlock>();
            BlockAddress = Value.Offset;

            if (Value.Count > 0)
            {
                long pos = reader.BaseStream.Position;
                reader.BaseStream.Seek(Value.Offset, SeekOrigin.Begin);

                for (int i = 0; i < Value.Count; i++)
                {
                    if (Add(out Block block))
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

                Value = tagBlock;
            }

            Value = tagBlock;
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
        public abstract Block Add(out bool success);
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
        public override Block Add(out bool success)
        {
            T tagBlock = new T();
            tagBlock.Initialize();

            BlockList.Add(tagBlock, out success);

            if (success)
            {
                return tagBlock;
            }

            return null;
        }
        public override bool Add(out Block block)
        {
            block = new T();
            block.Initialize();
            BlockList.Add(block, out bool success);

            if (!success)
            {
                block = null;
            }

            return success;
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

    public abstract class BaseFlagsField : OptionField
    {
        protected BaseFlagsField(FieldType type, string name, params string[] options) : base(type, name, options) { }
        public abstract bool HasFlag(Option option);
        public abstract object SetFlag(Option option, bool toggle);
    }

    public abstract class OptionField : Field
    {
        public List<Option> Options { get; }
        protected OptionField(FieldType type, string name, params string[] options) : base(type, name)
        {
            Options = new List<Option>();

            for (int i = 0; i < options.Length; i++)
                Options.Add(new Option(options[i], i));
        }
        public string[] GetOptions()
        {
            return Options.Select(o => o.Name).ToArray();
        }
    }

    public sealed class CharIntegerField : Field
    {
        public override int Size => 1;
        public new byte Value
        {
            get => (byte)base.Value;
            set => base.Value = value;
        }
        public CharIntegerField(string name) : base(FieldType.FieldCharInteger, name)
        {
            Value = 0;
        }
        protected override void OnRead(BinaryReader reader)
        {
            base.OnRead(reader);
            Value = reader.ReadByte();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            base.OnWrite(writer);
            writer.Write(Value);
        }
    }

    public sealed class ShortIntegerField : Field
    {
        public override int Size => 2;
        public new short Value
        {
            get => (short)base.Value;
            set => base.Value = value;
        }
        public ShortIntegerField(string name) : base(FieldType.FieldShortInteger, name)
        {
            Value = 0;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.ReadInt16();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class LongIntegerField : Field
    {
        public override int Size => 4;
        public new int Value
        {
            get => (int)base.Value;
            set => base.Value = value;
        }
        public LongIntegerField(string name) : base(FieldType.FieldLongInteger, name)
        {
            Value = 0;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.ReadInt32();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class AngleField : Field
    {
        public override int Size => 4;
        public new float Value
        {
            get => (float)base.Value;
            set => base.Value = value;
        }
        public AngleField(string name) : base(FieldType.FieldAngle, name)
        {
            Value = 0f;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.ReadSingle();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write((float)Value);
        }
    }

    public sealed class TagField : Field
    {
        public override int Size => 4;
        public new TagFourCc Value
        {
            get => (TagFourCc)base.Value;
            set => base.Value = value;
        }
        public TagField(string name) : base(FieldType.FieldTag, name)
        {
            Value = new TagFourCc(string.Empty);
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

    public sealed class CharEnumField : OptionField
    {
        public override int Size => 1;
        public Option Option
        {
            get { return Options[Value]; }
            set { if (Options.Contains(value)) Value = (byte)value.Index; }
        }
        public new byte Value
        {
            get => (byte)base.Value;
            set => base.Value = value;
        }
        public CharEnumField(string name, params string[] options) : base(FieldType.FieldCharEnum, name, options)
        {
            Value = 0;
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
            return $"{base.ToString()} ({Options[Value]?.Name ?? "IndexOutOfRange"})";
        }
    }

    public sealed class EnumField : OptionField
    {
        public override int Size => 2;
        public Option Option
        {
            get { return Options[Value]; }
            set { if (Options.Contains(value)) Value = (short)value.Index; }
        }
        public new short Value
        {
            get => (short)base.Value;
            set => base.Value = value;
        }
        public EnumField(string name, params string[] options) : base(FieldType.FieldEnum, name, options)
        {
            Value = 0;
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
            return $"{base.ToString()} ({Options[Value]?.Name ?? "IndexOutOfRange"})";
        }
    }

    public sealed class LongEnumField : OptionField
    {
        public override int Size => 4;
        public Option Option
        {
            get { return Options[Value]; }
            set { if (Options.Contains(value)) Value = value.Index; }
        }
        public new int Value
        {
            get => (int)base.Value;
            set => base.Value = value;
        }
        public LongEnumField(string name, params string[] options) : base(FieldType.FieldLongEnum, name, options)
        {
            Value = 0;
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
            return $"{base.ToString()} ({Options[Value]?.Name ?? "IndexOutOfRange"})";
        }
    }

    public sealed class LongFlagsField : BaseFlagsField
    {
        public override int Size => 4;
        public new int Value
        {
            get => (int)base.Value;
            set => base.Value = value;
        }
        public LongFlagsField(string name, params string[] options) : base(FieldType.FieldLongFlags, name, options)
        {
            Value = 0;
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
                if ((Value & (1 << (option.Index + 1))) == (1 << (option.Index + 1)))
                    flagged.Add(option.Name);

            return $"{Name} = [{string.Join(",", flagged.ToArray())}]";
        }
        public override bool HasFlag(Option option)
        {
            int flag = (1 << (option.Index + 1));
            return (Value & flag) == flag;
        }
        public override object SetFlag(Option option, bool toggle)
        {
            int flags = Value;
            int flag = 1 << (option.Index + 1);
            flags = flags & (~flag);
            if (toggle) flags |= flag;

            Value = flags;
            return flags;
        }
    }

    public sealed class WordFlagsField : BaseFlagsField
    {
        public override int Size => 2;
        public new short Value
        {
            get => (short)base.Value;
            set => base.Value = value;
        }
        public WordFlagsField(string name, params string[] options) : base(FieldType.FieldWordFlags, name, options)
        {
            Value = 0;
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
                if ((Value & (1 << (option.Index + 1))) == (1 << (option.Index + 1)))
                    flagged.Add(option.Name);

            return $"{Name} = [{string.Join(",", flagged.ToArray())}]";
        }
        public override bool HasFlag(Option option)
        {
            int flag = (1 << (option.Index + 1));
            return (Value & flag) == flag;
        }
        public override object SetFlag(Option option, bool toggle)
        {
            int flags = Value;
            int flag = 1 << (option.Index + 1);
            flags = flags & (~flag);
            if (toggle) flags |= flag;

            Value = (short)flags;
            return (short)flags;
        }
    }

    public sealed class ByteFlagsField : BaseFlagsField
    {
        public override int Size => 1;
        public new byte Value
        {
            get => (byte)base.Value;
            set => base.Value = value;
        }
        public ByteFlagsField(string name, params string[] options) : base(FieldType.FieldByteFlags, name, options)
        {
            Value = 0;
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
                if ((Value & (1 << (option.Index + 1))) == (1 << (option.Index + 1)))
                    flagged.Add(option.Name);

            return $"{Name} = [{string.Join(",", flagged.ToArray())}]";
        }
        public override bool HasFlag(Option option)
        {
            int flag = (1 << (option.Index + 1));
            return (Value & flag) == flag;
        }
        public override object SetFlag(Option option, bool toggle)
        {
            int flags = Value;
            int flag = 1 << (option.Index + 1);
            flags = flags & (~flag);
            if (toggle) flags |= flag;

            Value = (byte)flags;
            return (byte)flags;
        }
    }

    public sealed class Point2dField : Field
    {
        public new Point2 Value
        {
            get => (Point2)base.Value;
            set => base.Value = value;
        }
        public override int Size => 4;
        public Point2dField(string name) : base(FieldType.FieldPoint2D, name)
        {
            Value = Point2.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<Point2>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class Rectangle2dField : Field
    {
        public override int Size => 8;
        public new Rectangle2 Value
        {
            get => (Rectangle2)base.Value;
            set => base.Value = value;
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
        public new ColorRgb Value
        {
            get => (ColorRgb)base.Value;
            set => base.Value = value;
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
        public new ColorArgb Value
        {
            get => (ColorArgb)base.Value;
            set => base.Value = value;
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

    public sealed class RealField : Field
    {
        public override int Size => 4;
        public new float Value
        {
            get => (float)base.Value;
            set => base.Value = value;
        }
        public RealField(string name) : base(FieldType.FieldReal, name)
        {
            Value = 0f;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.ReadSingle();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class RealFractionField : Field
    {
        public override int Size => 4;
        public new float Value
        {
            get => (float)base.Value;
            set => base.Value = value;
        }
        public RealFractionField(string name) : base(FieldType.FieldRealFraction, name)
        {
            Value = 0f;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.ReadSingle();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class RealPoint2dField : Field
    {
        public override int Size => 8;
        public new Point2F Value
        {
            get => (Point2F)base.Value;
            set => base.Value = value;
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
        public new Point3F Value
        {
            get => (Point3F)base.Value;
            set => base.Value = value;
        }
        public RealPoint3dField(string name) : base(FieldType.FieldRealPoint3D, name)
        {
            Value = Point3F.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<Point3F>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class RealVector2dField : Field
    {
        public override int Size => 8;
        public new Vector2 Value
        {
            get => (Vector2)base.Value;
            set => base.Value = value;
        }
        public RealVector2dField(string name) : base(FieldType.FieldRealVector2D, name)
        {
            Value = Vector2.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<Vector2>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class RealVector3dField : Field
    {
        public override int Size => 12;
        public new Vector3 Value
        {
            get => (Vector3)base.Value;
            set => base.Value = value;
        }
        public RealVector3dField(string name) : base(FieldType.FieldRealVector3D, name)
        {
            Value = Vector3.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<Vector3>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class QuaternionField : Field
    {
        public override int Size => 16;
        public new Quaternion Value
        {
            get => (Quaternion)base.Value;
            set => base.Value = value;
        }
        public QuaternionField(string name) : base(FieldType.FieldQuaternion, name)
        {
            Value = Quaternion.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<Quaternion>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class EulerAngles2dField : Field
    {
        public override int Size => 8;
        public new Vector2 Value
        {
            get => (Vector2)base.Value;
            set => base.Value = value;
        }
        public EulerAngles2dField(string name) : base(FieldType.FieldEulerAngles2D, name)
        {
            Value = Vector2.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<Vector2>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class EulerAngles3dField : Field
    {
        public override int Size => 12;
        public new Vector3 Value
        {
            get => (Vector3)base.Value;
            set => base.Value = value;
        }
        public EulerAngles3dField(string name) : base(FieldType.FieldEulerAngles3D, name)
        {
            Value = Vector3.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<Vector3>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class RealPlane2dField : Field
    {
        public override int Size => 12;
        public new Vector3 Value
        {
            get => (Vector3)base.Value;
            set => base.Value = value;
        }
        public RealPlane2dField(string name) : base(FieldType.FieldRealPlane2D, name)
        {
            Value = Vector3.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<Vector3>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class RealPlane3dField : Field
    {
        public override int Size => 16;
        public new Vector4 Value
        {
            get => (Vector4)base.Value;
            set => base.Value = value;
        }
        public RealPlane3dField(string name) : base(FieldType.FieldRealPlane3D, name)
        {
            Value = Vector4.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<Vector4>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class RealRgbColorField : Field
    {
        public override int Size => 12;
        public new ColorRgbF Value
        {
            get => (ColorRgbF)base.Value;
            set => base.Value = value;
        }
        public RealRgbColorField(string name) : base(FieldType.FieldRealRgbColor, name)
        {
            Value = ColorRgbF.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<ColorRgbF>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class RealArgbColorField : Field
    {
        public override int Size => 16;
        public new ColorArgbF Value
        {
            get => (ColorArgbF)base.Value;
            set => base.Value = value;
        }
        public RealArgbColorField(string name) : base(FieldType.FieldRealArgbColor, name)
        {
            Value = ColorArgbF.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<ColorArgbF>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class RealHsvColorField : Field
    {
        public override int Size => 12;
        public new ColorHsv Value
        {
            get => (ColorHsv)base.Value;
            set => base.Value = value;
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
        public new ColorAhsv Value
        {
            get => (ColorAhsv)base.Value;
            set => base.Value = value;
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
        public new ShortBounds Value
        {
            get => (ShortBounds)base.Value;
            set => base.Value = value;
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

    public sealed class AngleBoundsField : Field
    {
        public override int Size => 8;
        public new FloatBounds Value
        {
            get => (FloatBounds)base.Value;
            set => base.Value = value;
        }
        public AngleBoundsField(string name) : base(FieldType.FieldAngleBounds, name)
        {
            Value = FloatBounds.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<FloatBounds>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class RealBoundsField : Field
    {
        public override int Size => 8;
        public new FloatBounds Value
        {
            get => (FloatBounds)base.Value;
            set => base.Value = value;
        }
        public RealBoundsField(string name) : base(FieldType.FieldRealBounds, name)
        {
            Value = FloatBounds.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<FloatBounds>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class RealFractionBoundsField : Field
    {
        public override int Size => 8;
        public new FloatBounds Value
        {
            get => (FloatBounds)base.Value;
            set => base.Value = value;
        }
        public RealFractionBoundsField(string name) : base(FieldType.FieldRealFractionBounds, name)
        {
            Value = FloatBounds.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.Read<FloatBounds>();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class CharBlockIndexField : Field
    {
        public override int Size => 1;
        public BlockSearchProcedure<byte> SearchProcedure { get; set; }
        public new byte Value
        {
            get => (byte)base.Value;
            set => base.Value = value;
        }
        public CharBlockIndexField(string name) : base(FieldType.FieldCharBlockIndex1, name)
        {
            Value = byte.MaxValue;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.ReadByte();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class ShortBlockIndexField : Field
    {
        public override int Size => 2;
        public new short Value
        {
            get => (short)base.Value;
            set => base.Value = value;
        }
        public BlockSearchProcedure<short> SearchProcedure { get; set; }
        public ShortBlockIndexField(string name) : base(FieldType.FieldShortBlockIndex1, name)
        {
            Value = -1;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.ReadInt16();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }

    public sealed class LongBlockIndexField : Field
    {
        public override int Size => 4;
        public new int Value
        {
            get => (int)base.Value;
            set => base.Value = value;
        }
        public BlockSearchProcedure<int> SearchProcedure { get; set; }
        public LongBlockIndexField(string name) : base(FieldType.FieldLongBlockIndex1, name)
        {
            Value = -1;
        }
        protected override void OnRead(BinaryReader reader)
        {
            Value = reader.ReadInt32();
        }
        protected override void OnWrite(BinaryWriter writer)
        {
            writer.Write(Value);
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
        public new TagBlock Value
        {
            get => (TagBlock)base.Value;
            set => base.Value = value;
        }
        public DataField(string name, int elementSize, int alignment = 4) : base(FieldType.FieldData, name)
        {
            ElementSize = elementSize;
            Alignment = alignment;
            Value = TagBlock.Zero;
        }
        protected override void OnRead(BinaryReader reader)
        {
            buffer = new byte[0];
            Value = reader.Read<TagBlock>();

            if (Value.Count > 0)
            {
                if (Value.Offset == 0) System.Diagnostics.Debugger.Break();

                long pos = reader.BaseStream.Position;
                reader.BaseStream.Seek(Value.Offset, SeekOrigin.Begin);
                buffer = reader.ReadBytes((int)(ElementSize * Value.Count));
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

            long pos = writer.BaseStream.Position;
            writer.BaseStream.Position = FieldAddress;
            writer.Write(tagBlock);

            writer.BaseStream.Position = pos;
        }
        protected override void OnOverwrite(BinaryWriter writer)
        {
            if (BufferLength > 0)
            {
                writer.BaseStream.Seek(Value.Offset, SeekOrigin.Begin);
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
            writer.Write(Value);
        }
    }

    public sealed class PadField : Field
    {
        public override int Size => Length;
        public int Length { get; }
        public new byte[] Value
        {
            get => (byte[])base.Value;
            set => base.Value = value;
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
            writer.Write(Value);
        }
    }

    public sealed class SkipField : Field
    {
        public override int Size => Length;
        public int Length { get; }
        public new byte[] Value
        {
            get => (byte[])base.Value;
            set => base.Value = value;
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
            writer.Write(Value);
        }
    }

    public sealed class StructField<T> : StructField where T : Block, new()
    {
        public StructField(string name) : base(name, new T()) { }
        public override Block Create()
        {
            return new T();
        }
    }
}
