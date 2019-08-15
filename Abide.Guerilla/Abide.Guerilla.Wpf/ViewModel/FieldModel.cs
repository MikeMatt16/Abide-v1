using Abide.Tag;
using System;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents a tag field container.
    /// </summary>
    public class FieldModel : TagModel
    {
        /// <summary>
        /// Occurs when a tag reference is requested to be opened.
        /// </summary>
        public event TagReferenceEventHandler OpenTagReferenceRequested;
        /// <summary>
        /// Occurs when the tag field value has been changed.
        /// </summary>
        public event EventHandler ValueChanged;
        /// <summary>
        /// Occurs when the tag field has been changed.
        /// </summary>
        public event EventHandler TagFieldChanged;

        /// <summary>
        /// Gets or sets the value of the tag field.
        /// </summary>
        public object Value
        {
            get { return tagField?.Value ?? null; }
            set
            {
                //Check
                if (tagField == null) return;

                //Check
                if (!tagField.Value.Equals(value))
                {
                    //Change
                    tagField.Value = value;
                    NotifyPropertyChanged();
                    NotifyValueChanged();
                }
            }
        }
        /// <summary>
        /// Gets or sets the tag field.
        /// </summary>
        public Field TagField
        {
            get { return tagField; }
            set
            {
                bool changed = tagField != value;
                tagField = value;
                if (changed)
                {
                    NotifyPropertyChanged();
                    Name = value?.Name ?? string.Empty;

                    NotifyTagFieldChanged();
                }
            }
        }
        /// <summary>
        /// Gets and returns the name string.
        /// </summary>
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
        /// <summary>
        /// Gets and returns the tool tip string.
        /// </summary>
        public string ToolTip
        {
            get
            {
                if (tagField == null) return null;
                if (string.IsNullOrEmpty(tagField.Information)) return null;
                return tagField.Information;
            }
        }
        /// <summary>
        /// Gets and returns the details string.
        /// </summary>
        public string Details
        {
            get
            {
                if (TagField == null) return null;
                if (string.IsNullOrEmpty(tagField.Details)) return null;
                return tagField.Details;
            }
        }

        private Field tagField = null;
        private string name = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldModel"/> class.
        /// </summary>
        public FieldModel() { }
        /// <summary>
        /// Pushes a notification that the specified tag reference is requesting to be opened.
        /// </summary>
        public void NotifyOpenTagReferenceRequested(string tagReference)
        {
            //Prepare
            TagReferenceEventArgs e = new TagReferenceEventArgs(tagReference);

            //Request from owner
            Owner.NotifyOpenTagReferenceRequested(tagReference);

            //Call OnOpenTagReferenceRequested
            OnOpenTagReferenceRequested(e);

            //Raise event
            OpenTagReferenceRequested?.Invoke(this, e);
        }
        /// <summary>
        /// Pushes a notification that the tag field has been changed.
        /// </summary>
        private void NotifyTagFieldChanged()
        {
            //Notify changes for dependent properties
            NotifyPropertyChanged(nameof(Value));
            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(ToolTip));
            NotifyPropertyChanged(nameof(Details));

            //Prepare
            EventArgs e = new EventArgs();
            
            //Call OnValueChanged
            OnTagFieldChanged(e);

            //Raise event
            TagFieldChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Pushes a notification that the tag field value has been changed.
        /// </summary>
        protected void NotifyValueChanged()
        {
            //Mark dirty
            Owner.IsDirty = true;

            //Prepare
            EventArgs e = new EventArgs();

            //Call OnValueChanged
            OnValueChanged(e);

            //Raise event
            ValueChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Occurs when a tag reference is requested to be opened.
        /// </summary>
        /// <param name="e">A <see cref="TagReferenceEventArgs"/> that contains the event data.</param>
        protected virtual void OnOpenTagReferenceRequested(TagReferenceEventArgs e)
        {
            //Do nothing
        }
        /// <summary>
        /// Occurs when the tag field has been changed.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnTagFieldChanged(EventArgs e)
        {
            //Do nothing
        }
        /// <summary>
        /// Occurs when the tag field value has been changed.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnValueChanged(EventArgs e)
        {
            //Do nothing
        }
    }
}
