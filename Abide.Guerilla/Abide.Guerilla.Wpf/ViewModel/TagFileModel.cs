using Abide.Guerilla.Library;
using Abide.HaloLibrary;
using Abide.Tag;
using System.IO;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents a tag file item.
    /// </summary>
    public class TagFileModel : FileModel
    {
        /// <summary>
        /// Occurs when a tag reference is requested to be opened.
        /// </summary>
        public event TagReferenceEventHandler OpenTagReferenceRequested;
        /// <summary>
        /// Gets and returns a file filter for this tag group file.
        /// </summary>
        public override string FileFilter
        {
            get
            {
                if (TagGroup == null) return "All Files (*.*)|*.*";
                return $"{TagGroup.Name} files (*.{TagGroup.Name})|*.{TagGroup.Name}";
            }
        }
        /// <summary>
        /// Gets or sets the group tag.
        /// </summary>
        public TagFourCc GroupTag
        {
            get
            {
                return tagGroupFile?.TagGroup?.GroupTag ?? (TagFourCc)0;
            }
        }
        /// <summary>
        /// Gets or sets the tag group.
        /// </summary>
        public ITagGroup TagGroup
        {
            get { return tagGroupFile.TagGroup; }
            set
            {
                bool changed = tagGroupFile.TagGroup != value;
                tagGroupFile.TagGroup = value;
                if (changed) NotifyPropertyChanged();
            }
        }

        private AbideTagGroupFile tagGroupFile = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagFileModel"/> class using the specified file name and tag group.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="groupTag">The tag group.</param>
        public TagFileModel(string fileName, AbideTagGroupFile tagGroupFile = null) : base(fileName)
        {
            this.tagGroupFile = tagGroupFile;
        }
        /// <summary>
        /// Loads the tag group from the file.
        /// </summary>
        public override void LoadFromFile()
        {
            //Check
            if (File.Exists(FileName))
                tagGroupFile.Load(FileName);
        }
        /// <summary>
        /// Saves the tag group to the file.
        /// </summary>
        public override void SaveToFile()
        {
            //Save
            tagGroupFile.Save(FileName);

            //Remove dirty
            IsDirty = false;
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
        /// Occurs when a tag reference is requested to be opened.
        /// </summary>
        /// <param name="e">A <see cref="TagReferenceEventArgs"/> that contains the event data.</param>
        protected virtual void OnOpenTagReferenceRequested(TagReferenceEventArgs e)
        {
            //Do nothing
        }
    }

    /// <summary>
    /// Represents a generic tag model.
    /// </summary>
    public abstract class TagModel : NotifyPropertyChangedViewModel
    {
        /// <summary>
        /// Gets or sets the tag file that this instance belongs to.
        /// </summary>
        public TagFileModel Owner
        {
            get { return owner; }
            set
            {
                bool changed = owner != value;
                owner = value;
                if (changed)
                {
                    NotifyPropertyChanged();
                    NotifyOwnerChanged();
                }
            }
        }

        private TagFileModel owner = null;

        /// <summary>
        /// Occurs when the owner has been changed.
        /// </summary>
        protected virtual void NotifyOwnerChanged()
        {
            //Do nothing
        }
    }
}
