using Abide.Guerilla.Wpf.ViewModel;
using Abide.Tag;
using Abide.Tag.Definition;
using Abide.Tag.Guerilla;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Guerilla.Wpf.Controls
{
    /// <summary>
    /// Interaction logic for TagBlockControl.xaml
    /// </summary>
    public partial class TagBlockControl : UserControl
    {
        /// <summary>
        /// Gets and returns the tag block model.
        /// </summary>
        private TagBlockModel Model
        {
            get { if (DataContext is TagBlockModel model) return model; return null; }
        }

        private Type currentTagBlockType = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagBlockControl"/> class.
        /// </summary>
        public TagBlockControl()
        {
            InitializeComponent();
        }

        private void TagBlockControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Check old value
            if (e.OldValue is TagBlockModel oldModel)
                TagBlock_Unload(oldModel);

            //Check new value
            if (e.NewValue is TagBlockModel newModel)
                TagBlock_Load(newModel);
        }
        private void TagBlockModel_TagBlockChanged(object sender, EventArgs e)
        {
            //Check
            if (currentTagBlockType != Model.TagBlock.GetType())
                mainStackPanel.Children.Clear();    //Clear

            //Load Controls
            TagBlock_LoadControls(Model.TagBlock);
        }
        private void TagBlock_Unload(TagBlockModel tagBlockModel)
        {
            //Unsubscribe to tag block changed event
            tagBlockModel.TagBlockChanged -= TagBlockModel_TagBlockChanged;
        }
        private void TagBlock_Load(TagBlockModel tagBlockModel)
        {
            //Subscribe to tag block changed event
            tagBlockModel.TagBlockChanged += TagBlockModel_TagBlockChanged;

            //Verify tag block
            if (tagBlockModel.TagBlock != null)
            {
                //Check
                if (currentTagBlockType != tagBlockModel.TagBlock.GetType())
                    mainStackPanel.Children.Clear();    //Clear

                //Load Controls
                TagBlock_LoadControls(tagBlockModel.TagBlock);
            }
        }
        private void TagBlock_LoadControls(ITagBlock tagBlock)
        {
            //Prepare
            FrameworkElement control = null;
            List<Field> fields = tagBlock.Fields;
            UIElementCollection children = mainStackPanel.Children;
            TagBlockModel structTagBlockModel = null;
            FieldModel fieldModel = null;

            //Loop through fields
            for (int i = 0; i < fields.Count; i++)
            {
                //Check if the children of the stack panel already contain the field data context
                if (children.Count > i && children[i] is FrameworkElement element && element.DataContext is FieldModel)
                {
                    //Get model
                    fieldModel = (FieldModel)element.DataContext;

                    //Update
                    fieldModel.Owner = Model.Owner;
                    fieldModel.TagField = fields[i];
                }
                else
                {
                    switch (fields[i].Type)
                    {
                        case FieldType.FieldExplanation:
                            fieldModel = new ExplanationFieldModel() { Owner = Model.Owner, TagField = (ExplanationField)fields[i] };
                            control = new ExplanationControl() { DataContext = fieldModel };
                            children.Add(control);
                            break;

                        case FieldType.FieldStruct:
                            structTagBlockModel = new TagBlockModel() { Owner = Model.Owner, TagBlock = (ITagBlock)fields[i].Value };
                            control = new TagBlockControl() { DataContext = structTagBlockModel };
                            children.Add(control);
                            break;

                        case FieldType.FieldBlock:
                            fieldModel = new BlockFieldModel() { Owner = Model.Owner, TagField = (BlockField)fields[i] };
                            control = new BlockControl() { DataContext = fieldModel };
                            children.Add(control);
                            break;

                        case FieldType.FieldString:
                        case FieldType.FieldLongString:
                        case FieldType.FieldStringId:
                        case FieldType.FieldOldStringId:
                            fieldModel = new ValueFieldModel() { Owner = Model.Owner, TagField = fields[i], InputBoxWidth = 250 };
                            control = new ValueControl() { DataContext = fieldModel };
                            children.Add(control);
                            break;

                        case FieldType.FieldShortBlockIndex1:
                        case FieldType.FieldShortBlockIndex2:
                        case FieldType.FieldCharBlockIndex1:
                        case FieldType.FieldCharBlockIndex2:
                        case FieldType.FieldLongBlockIndex1:
                        case FieldType.FieldLongBlockIndex2:
                        case FieldType.FieldRealFraction:
                        case FieldType.FieldShortInteger:
                        case FieldType.FieldCharInteger:
                        case FieldType.FieldLongInteger:
                        case FieldType.FieldAngle:
                        case FieldType.FieldReal:
                        case FieldType.FieldTag:
                            fieldModel = new ValueFieldModel() { Owner = Model.Owner, TagField = fields[i] };
                            control = new ValueControl() { DataContext = fieldModel };
                            children.Add(control);
                            break;

                        case FieldType.FieldShortBounds:
                        case FieldType.FieldAngleBounds:
                        case FieldType.FieldRealBounds:
                        case FieldType.FieldRealFractionBounds:
                            fieldModel = new BoundsFieldModel() { Owner = Model.Owner, TagField = fields[i] };
                            control = new BoundsControl() { DataContext = fieldModel };
                            children.Add(control);
                            break;

                        case FieldType.FieldCharEnum:
                        case FieldType.FieldEnum:
                        case FieldType.FieldLongEnum:
                            fieldModel = new EnumFieldModel() { Owner = Model.Owner, TagField = fields[i] };
                            control = new EnumControl() { DataContext = fieldModel };
                            children.Add(control);
                            break;

                        case FieldType.FieldTagIndex:
                        case FieldType.FieldTagReference:
                            fieldModel = new TagReferenceFieldModel() { Owner = Model.Owner, TagField = fields[i] };
                            control = new TagReferenceControl() { DataContext = fieldModel };
                            children.Add(control);
                            break;

                        case FieldType.FieldData:
                            break;
                            
                        case FieldType.FieldLongFlags:
                        case FieldType.FieldWordFlags:
                        case FieldType.FieldByteFlags:
                            fieldModel = new FlagsFieldModel() { Owner = Model.Owner, TagField = fields[i] };
                            control = new FlagsControl() { DataContext = fieldModel };
                            children.Add(control);
                            break;

                        case FieldType.FieldEulerAngles3D:
                        case FieldType.FieldRealVector3D:
                        case FieldType.FieldRealRgbColor:
                        case FieldType.FieldRealHsvColor:
                        case FieldType.FieldRealPoint3D:
                        case FieldType.FieldRealPlane2D:
                        case FieldType.FieldRgbColor:
                            fieldModel = new Tuple3FieldModel() { Owner = Model.Owner, TagField = fields[i] };
                            control = new Tuple3Control() { DataContext = fieldModel };
                            children.Add(control);
                            break;

                        case FieldType.FieldEulerAngles2D:
                        case FieldType.FieldRealVector2D:
                        case FieldType.FieldRealPoint2D:
                        case FieldType.FieldPoint2D:
                            fieldModel = new Tuple2FieldModel() { Owner = Model.Owner, TagField = fields[i] };
                            control = new Tuple2Control() { DataContext = fieldModel };
                            children.Add(control);
                            break;

                        case FieldType.FieldRealArgbColor:
                        case FieldType.FieldRealAhsvColor:
                        case FieldType.FieldRealPlane3D:
                        case FieldType.FieldRectangle2D:
                        case FieldType.FieldQuaternion:
                        case FieldType.FieldArgbColor:
                            fieldModel = new Tuple4FieldModel() { Owner = Model.Owner, TagField = fields[i] };
                            control = new Tuple4Control() { DataContext = fieldModel };
                            children.Add(control);
                            break;
                            
                        case FieldType.FieldVertexBuffer:
                            break;

                        case FieldType.FieldLongBlockFlags:
                            break;
                        case FieldType.FieldWordBlockFlags:
                            break;
                        case FieldType.FieldByteBlockFlags:
                            break;
                    }
                }
            }
        }
    }
}
