using Abide.Tag;
using System;
using System.Windows;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents a tag block container.
    /// </summary>
    public class TagBlockModel : TagModel
    {
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
                    NotifyTagBlockChanged();
                }
            }
        }

        private ITagBlock tagBlock = null;
        private Visibility visibility = Visibility.Visible;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagBlockModel"/> class.
        /// </summary>
        public TagBlockModel() { }
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
