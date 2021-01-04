using Abide.Tag;
using Abide.Tag.Definition;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents a tag block container.
    /// </summary>
    public class TagBlockModel : TagModel
    {
        private static readonly FileNameToShortStringConverter fileNameConverter = new FileNameToShortStringConverter();

        /// <summary>
        /// Gets and returns the list of fields in the tag block.
        /// </summary>
        public ObservableCollection<FieldModel> Fields { get; } = new ObservableCollection<FieldModel>();
        /// <summary>
        /// Gets or sets the name of the tag block.
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                if(name != value)
                {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Gets and returns the visibility state of this tag block.
        /// </summary>
        public Visibility Visiblity
        {
            get { return visibility; }
            set
            {
                if (visibility != value)
                {
                    visibility = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Occurs when a tag reference is requested to be opened.
        /// </summary>
        public event TagReferenceEventHandler OpenTagReferenceRequested;
        /// <summary>
        /// Occurs when the tag block has been changed.
        /// </summary>
        public event EventHandler TagBlockChanged;
        /// <summary>
        /// Occurs when the value of a tag field contained in this tag block has been changed.
        /// </summary>
        public event EventHandler FieldValueChanged;
        /// <summary>
        /// Gets or sets the tag block.
        /// </summary>
        public ITagBlock TagBlock
        {
            get { return tagBlock; }
            set
            {
                bool changed = tagBlock != value;
                tagBlock = value;
                if (changed)
                {
                    NotifyPropertyChanged();

                    //Load fields
                    Fields.Clear();
                    if (value != null)
                    {
                        //Get name
                        Name = value.BlockName;

                        //Loop through fields
                        foreach (ITagField field in value)
                        {
                            //Handle type
                            switch (field.Type)
                            {
                                case FieldType.FieldExplanation:
                                    Fields.Add(new ExplanationFieldModel() { Owner = Owner, TagField = (ExplanationField)field });
                                    break;

                                case FieldType.FieldStruct:
                                    Fields.Add(new StructFieldModel() { Owner = Owner, TagField = (StructField)field });
                                    break;

                                case FieldType.FieldBlock:
                                    Fields.Add(new BlockFieldModel() { Owner = Owner, TagField = (BlockField)field });
                                    break;

                                case FieldType.FieldString:
                                case FieldType.FieldLongString:
                                case FieldType.FieldStringId:
                                case FieldType.FieldOldStringId:
                                    Fields.Add(new ValueFieldModel() { Owner = Owner, TagField = field, InputBoxWidth = 250 });
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
                                    Fields.Add(new ValueFieldModel() { Owner = Owner, TagField = field });
                                    break;

                                case FieldType.FieldShortBounds:
                                case FieldType.FieldAngleBounds:
                                case FieldType.FieldRealBounds:
                                case FieldType.FieldRealFractionBounds:
                                    Fields.Add(new BoundsFieldModel() { Owner = Owner, TagField = field });
                                    break;

                                case FieldType.FieldCharEnum:
                                case FieldType.FieldEnum:
                                case FieldType.FieldLongEnum:
                                    Fields.Add(new EnumFieldModel() { Owner = Owner, TagField = field });
                                    break;

                                case FieldType.FieldTagIndex:
                                case FieldType.FieldTagReference:
                                    Fields.Add(new TagReferenceFieldModel() { Owner = Owner, TagField = field });
                                    break;

                                case FieldType.FieldData:
                                    break;

                                case FieldType.FieldLongFlags:
                                case FieldType.FieldWordFlags:
                                case FieldType.FieldByteFlags:
                                    Fields.Add(new FlagsFieldModel() { Owner = Owner, TagField = field });
                                    break;

                                case FieldType.FieldEulerAngles3D:
                                case FieldType.FieldRealVector3D:
                                case FieldType.FieldRealRgbColor:
                                case FieldType.FieldRealHsvColor:
                                case FieldType.FieldRealPoint3D:
                                case FieldType.FieldRealPlane2D:
                                case FieldType.FieldRgbColor:
                                    Fields.Add(new Tuple3FieldModel() { Owner = Owner, TagField = field });
                                    break;

                                case FieldType.FieldEulerAngles2D:
                                case FieldType.FieldRealVector2D:
                                case FieldType.FieldRealPoint2D:
                                case FieldType.FieldPoint2D:
                                    Fields.Add(new Tuple2FieldModel() { Owner = Owner, TagField = field });
                                    break;

                                case FieldType.FieldRealArgbColor:
                                case FieldType.FieldRealAhsvColor:
                                case FieldType.FieldRealPlane3D:
                                case FieldType.FieldRectangle2D:
                                case FieldType.FieldQuaternion:
                                case FieldType.FieldArgbColor:
                                    Fields.Add(new Tuple4FieldModel() { Owner = Owner, TagField = field });
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
                    else Name = string.Empty;

                    //Notify change
                    NotifyTagBlockChanged();
                }
            }
        }
        
        private ITagBlock tagBlock = null;
        private Visibility visibility = Visibility.Visible;
        private string name = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagBlockModel"/> class.
        /// </summary>
        public TagBlockModel() { }
        /// <summary>
        /// Returns a string that represents this tag block model.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return Name;
        }
        /// <summary>
        /// Pushes a notification that the tag block has been changed.
        /// </summary>
        private void NotifyTagBlockChanged()
        {
            //Prepare
            EventArgs e = new EventArgs();

            //Call OnTagBlockChanged
            OnTagBlockChanged(e);

            //Raise event
            TagBlockChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Pushes a notification that the value of a tag field contained within the tag block has been changed.
        /// </summary>
        public void NotifyValueChanged()
        {
            //Prepare
            EventArgs e = new EventArgs();

            //Set dirty
            Owner.IsDirty = true;

            //Notify property changed...
            NotifyPropertyChanged(nameof(Name));

            //Call OnFieldValueChanged
            OnFieldValueChanged(e);

            //Raise event
            FieldValueChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Pushes a notification that the specified tag reference is requesting to be opened.
        /// </summary>
        public void NotifyOpenTagReferenceRequested(string tagReference)
        {
            //Prepare
            TagReferenceEventArgs e = new TagReferenceEventArgs(tagReference);

            //Call OnOpenTagReferenceRequested
            OnOpenTagReferenceRequested(e);

            //Raise event
            OpenTagReferenceRequested?.Invoke(this, e);
        }
        /// <summary>
        /// Occurs when the the value of a tag field contained in this tag block has been changed.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnFieldValueChanged(EventArgs e)
        {
            //Do nothing
        }
        /// <summary>
        /// Occurs when the tag block has been changed.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnTagBlockChanged(EventArgs e)
        {
            //Do nothing
        }
        /// <summary>
        /// Occurs when a tag reference is requested to be opened.
        /// </summary>
        /// <param name="e">A <see cref="TagReferenceEventArgs"/> that contains the event data.</param>
        protected virtual void OnOpenTagReferenceRequested(TagReferenceEventArgs e)
        {
            //Do nothing
        }
    }
}
