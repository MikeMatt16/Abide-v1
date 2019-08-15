using Abide.Tag;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents a struct field container.
    /// </summary>
    public sealed class StructFieldModel : FieldModel
    {
        /// <summary>
        /// Gets or sets the tag block model.
        /// </summary>
        public TagBlockModel TagBlock
        {
            get { return tagBlock; }
            set
            {
                if (tagBlock != value)
                {
                    tagBlock = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Gets or sets the struct tag field that the model wraps.
        /// </summary>
        public new StructField TagField
        {
            get { return (StructField)base.TagField; }
            set
            {
                if (base.TagField != value)
                {
                    base.TagField = value;
                    if (value == null) TagBlock = null;
                    else TagBlock = new TagBlockModel() { Owner = Owner, TagBlock = (ITagBlock)value.Value };
                }
            }
        }

        private TagBlockModel tagBlock = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="StructFieldModel"/> class.
        /// </summary>
        public StructFieldModel() { }
    }
}
