using Abide.Tag;
using Abide.Tag.Guerilla.Generated;
using Abide.Wpf.Modules.ViewModel;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Abide.Wpf.Modules.UI
{
    /// <summary>
    /// Interaction logic for TagGroupEditorPanel.xaml
    /// </summary>
    public partial class TagGroupEditorPanel : UserControl
    {
        public static readonly DependencyProperty TagGroupProperty =
            DependencyProperty.Register(nameof(TagGroup), typeof(Group), typeof(TagGroupEditorPanel), new PropertyMetadata(new Bitmap(), TagGroupPropertyChanged));
        public static readonly DependencyProperty CanModifyTagBlocksProperty =
            DependencyProperty.Register(nameof(CanModifyTagBlocks), typeof(bool), typeof(TagGroupEditorPanel));

        public Group TagGroup
        {
            get => (Group)GetValue(TagGroupProperty);
            set => SetValue(TagGroupProperty, value);
        }
        public bool CanModifyTagBlocks
        {
            get => (bool)GetValue(CanModifyTagBlocksProperty);
            set => SetValue(CanModifyTagBlocksProperty, value);
        }

        public TagGroupEditorPanel()
        {
            InitializeComponent();
        }

        private static void TagGroupPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
    }

    internal sealed class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                if (string.IsNullOrEmpty(str.Trim()))
                {
                    return Visibility.Collapsed;
                }
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal sealed class SelectableBlockField : BlockField
    {
        private readonly Type blockType;
        private readonly int size;

        private Block selectedBlock = null;
        private bool isExpanded = false;

        public override int Size => size;
        public bool Enabled => SelectedBlock != null;
        public ActionCommand AddCommand { get; }
        public ActionCommand DeleteCommand { get; }
        public ActionCommand InsertCommand { get; }
        public ActionCommand DeleteAllCommand { get; }
        public ActionCommand CopyCommand { get; }
        public ActionCommand PasteCommand { get; }
        public Block SelectedBlock
        {
            get => selectedBlock;
            set
            {
                if (selectedBlock != value)
                {
                    selectedBlock = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(Enabled));
                    NotifyPropertyChanged(nameof(DeleteCommand));
                    IsExpanded &= Enabled;
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

        public SelectableBlockField(BlockField baseField) : base(baseField.GetName(), baseField.BlockList.MaximumCount)
        {
            var block = baseField.Create();
            blockType = block.GetType();
            size = block.Size;

            Value = baseField.Value;
            foreach (var tagBlock in baseField.BlockList)
            {
                BlockList.Add(tagBlock);
            }

            SelectedBlock = BlockList.FirstOrDefault();
            if (SelectedBlock != null)
            {
                IsExpanded = true;
            }

            AddCommand = new ActionCommand(AddBlock, CanAddBlock);
            DeleteCommand = new ActionCommand(DeleteBlock, CanDeleteBlock);
            DeleteAllCommand = new ActionCommand(DeleteAll);
        }
        private void DeleteAll(object obj)
        {
            BlockList.Clear();
            SelectedBlock = null;
        }
        private void AddBlock(object obj)
        {
            bool isEmpty = BlockList.Count == 0;
            if (Add(out Block block))
            {
                SelectedBlock = block;

                if (isEmpty)
                {
                    IsExpanded = true;
                }
            }
        }
        private void DeleteBlock(object obj)
        {
            if (SelectedBlock != null)
            {
                int index = BlockList.IndexOf(SelectedBlock);
                if (BlockList.Remove(SelectedBlock))
                {
                    if (index >= BlockList.Count)
                    {
                        SelectedBlock = BlockList.LastOrDefault();
                    }
                    else
                    {
                        SelectedBlock = BlockList[index];
                    }
                }
            }
        }
        private bool CanDeleteBlock(object arg)
        {
            return SelectedBlock != null;
        }
        private bool CanAddBlock(object arg)
        {
            return BlockList.Count < BlockList.MaximumCount;
        }
        public override bool Add(out Block block)
        {
            block = null;
            if (BlockList.Count < BlockList.MaximumCount)
            {
                block = Create();
                return BlockList.Add(block);
            }

            return false;
        }
        public override Block Create()
        {
            return (Block)Activator.CreateInstance(blockType);
        }
    }

    internal class FieldTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StandardFieldTemplate { get; set; } = null;
        public DataTemplate BlockFieldTemplate { get; set; } = null;
        public DataTemplate StructFieldTemplate { get; set; } = null;
        public DataTemplate DataFieldTemplate { get; set; } = null;
        public DataTemplate ExplanationFieldTemplate { get; set; } = null;

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Field tagField)
            {
                switch (tagField.Type)
                {
                    case FieldType.FieldPad:
                    case FieldType.FieldUselessPad:
                    case FieldType.FieldSkip:
                        return new DataTemplate();

                    case FieldType.FieldBlock:
                        return BlockFieldTemplate;
                    case FieldType.FieldData:
                        return DataFieldTemplate;
                    case FieldType.FieldExplanation:
                        return ExplanationFieldTemplate;
                    case FieldType.FieldStruct:
                        return StructFieldTemplate;
                    default:
                        return StandardFieldTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }

    internal class FieldEditorTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StringEditor { get; set; } = null;
        public DataTemplate EnumEditor { get; set; } = null;
        public DataTemplate FlagsEditor { get; set; } = null;
        public DataTemplate BoundsEditor { get; set; } = null;
        public DataTemplate TagReferenceEditor { get; set; } = null;
        public DataTemplate Vector4Editor { get; set; } = null;
        public DataTemplate Vector3Editor { get; set; } = null;
        public DataTemplate Vector2Editor { get; set; } = null;
        public DataTemplate Point2Editor { get; set; } = null;
        public DataTemplate Point3Editor { get; set; } = null;
        public DataTemplate BlockIndexEditor { get; set; } = null;

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Field tagField)
            {
                switch (tagField.Type)
                {
                    case FieldType.FieldTag:
                    case FieldType.FieldString:
                    case FieldType.FieldStringId:
                    case FieldType.FieldOldStringId:
                    case FieldType.FieldCharInteger:
                    case FieldType.FieldShortInteger:
                    case FieldType.FieldLongInteger:
                    case FieldType.FieldAngle:
                    case FieldType.FieldReal:
                    case FieldType.FieldRealFraction:
                        return StringEditor;

                    case FieldType.FieldLongString:
                        break;

                    case FieldType.FieldCharEnum:
                    case FieldType.FieldEnum:
                    case FieldType.FieldLongEnum:
                        return EnumEditor;

                    case FieldType.FieldLongFlags:
                    case FieldType.FieldWordFlags:
                    case FieldType.FieldByteFlags:
                        return FlagsEditor;

                    case FieldType.FieldPoint2D:
                    case FieldType.FieldRealPoint2D:
                        return Point2Editor;

                    case FieldType.FieldRectangle2D:
                        break;
                    case FieldType.FieldRgbColor:
                        break;
                    case FieldType.FieldArgbColor:
                        break;

                    case FieldType.FieldRealPoint3D:
                        return Point3Editor;

                    case FieldType.FieldRealVector2D:
                    case FieldType.FieldEulerAngles2D:
                        return Vector2Editor;

                    case FieldType.FieldRealPlane2D:
                    case FieldType.FieldRealVector3D:
                    case FieldType.FieldEulerAngles3D:
                        return Vector3Editor;

                    case FieldType.FieldQuaternion:
                    case FieldType.FieldRealPlane3D:
                        return Vector4Editor;

                    case FieldType.FieldRealRgbColor:
                        break;
                    case FieldType.FieldRealArgbColor:
                        break;
                    case FieldType.FieldRealHsvColor:
                        break;
                    case FieldType.FieldRealAhsvColor:
                        break;

                    case FieldType.FieldShortBounds:
                    case FieldType.FieldAngleBounds:
                    case FieldType.FieldRealBounds:
                    case FieldType.FieldRealFractionBounds:
                        return BoundsEditor;

                    case FieldType.FieldTagReference:
                        return TagReferenceEditor;

                    case FieldType.FieldLongBlockFlags:
                        break;
                    case FieldType.FieldWordBlockFlags:
                        break;
                    case FieldType.FieldByteBlockFlags:
                        break;

                    case FieldType.FieldCharBlockIndex1:
                    case FieldType.FieldCharBlockIndex2:
                    case FieldType.FieldShortBlockIndex1:
                    case FieldType.FieldShortBlockIndex2:
                    case FieldType.FieldLongBlockIndex1:
                    case FieldType.FieldLongBlockIndex2:
                        return BlockIndexEditor;

                    case FieldType.FieldVertexBuffer:
                        break;
                    case FieldType.FieldTagIndex:
                        break;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}
