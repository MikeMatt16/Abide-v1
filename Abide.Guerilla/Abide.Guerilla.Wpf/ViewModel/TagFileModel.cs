using Abide.Guerilla.Library;
using Abide.Guerilla.Wpf.Controls;
using Abide.HaloLibrary;
using Abide.Tag;
using Abide.Tag.Definition;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents a tag file item.
    /// </summary>
    public class TagFileModel : FileModel
    {
        /// <summary>
        /// Gets and returns the tag block list.
        /// </summary>
        public ObservableCollection<TagBlockModel> TagBlocks { get; } = new ObservableCollection<TagBlockModel>();
        /// <summary>
        /// Occurs when the tag group has been changed.
        /// </summary>
        public event EventHandler TagGroupChanged;
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
                if(tagGroupFile.TagGroup != value)
                {
                    tagGroupFile.TagGroup = value;
                    NotifyPropertyChanged();

                    //Raise event
                    EventArgs e = new EventArgs();
                    TagGroupChanged?.Invoke(this, e);

                    //Call OnTagGroupChanged
                    OnTagGroupChanged(e);

                    //Add
                    TagBlocks.Clear();
                    foreach (ITagBlock tagBlock in value)
                        TagBlocks.Add(new TagBlockModel() { Owner = this, TagBlock = tagBlock });
                }
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
            //Setup
            this.tagGroupFile = tagGroupFile;

            //Loop
            foreach (ITagBlock tagBlock in tagGroupFile.TagGroup)
                TagBlocks.Add(new TagBlockModel() { Owner = this, TagBlock = tagBlock });
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
            //Check
            if (!File.Exists(FileName))
            {
                //Create
                SaveFileDialog saveDlg = new SaveFileDialog()
                {
                    InitialDirectory = Path.Combine(RegistrySettings.WorkspaceDirectory, "tags"),
                    Filter = FileFilter,
                    FileName = FileName
                };
                saveDlg.CustomPlaces.Add(new FileDialogCustomPlace(Path.Combine(RegistrySettings.WorkspaceDirectory, "tags")));

                //Show
                if (saveDlg.ShowDialog() ?? false)
                    FileName = saveDlg.FileName;
                else return;
            }

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
        /// <summary>
        /// Occurs when the <see cref="TagGroup"/> property has been changed.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnTagGroupChanged(EventArgs e) { }

        //private void LoadModel()
        //{
        //    //Prepare
        //    List<FieldModel> fieldList = new List<FieldModel>();

        //    //Add
        //    foreach (ITagBlock tagBlock in TagGroup)
        //        fieldList.AddRange(LoadTagBlock(tagBlock));

        //    //Clear
        //    Fields.Clear();

        //    //Add
        //    foreach (var field in fieldList)
        //        Fields.Add(field);
        //}
        //private List<FieldModel> LoadTagBlock(ITagBlock tagBlock)
        //{
        //    //Prepare
        //    List<FieldModel> list = new List<FieldModel>();

        //    //Loop through fields
        //    for (int i = 0; i < tagBlock.Fields.Count; i++)
        //    {
        //        //Handle type
        //        switch (tagBlock.Fields[i].Type)
        //        {
        //            case FieldType.FieldExplanation:
        //                list.Add(new ExplanationFieldModel() { Owner = this, TagField = tagBlock.Fields[i] });
        //                break;

        //            case FieldType.FieldStruct:
        //                list.AddRange(LoadTagBlock((ITagBlock)tagBlock.Fields[i].Value));
        //                break;

        //            case FieldType.FieldBlock:
        //                list.Add(new BlockFieldModel() { Owner = this, TagField = (BlockField)tagBlock.Fields[i] });
        //                break;

        //            case FieldType.FieldString:
        //            case FieldType.FieldLongString:
        //            case FieldType.FieldStringId:
        //            case FieldType.FieldOldStringId:
        //                list.Add(new ValueFieldModel() { Owner = this, TagField = tagBlock.Fields[i], InputBoxWidth = 250 });
        //                break;

        //            case FieldType.FieldShortBlockIndex1:
        //            case FieldType.FieldShortBlockIndex2:
        //            case FieldType.FieldCharBlockIndex1:
        //            case FieldType.FieldCharBlockIndex2:
        //            case FieldType.FieldLongBlockIndex1:
        //            case FieldType.FieldLongBlockIndex2:
        //            case FieldType.FieldRealFraction:
        //            case FieldType.FieldShortInteger:
        //            case FieldType.FieldCharInteger:
        //            case FieldType.FieldLongInteger:
        //            case FieldType.FieldAngle:
        //            case FieldType.FieldReal:
        //            case FieldType.FieldTag:
        //                list.Add(new ValueFieldModel() { Owner = this, TagField = tagBlock.Fields[i] });
        //                break;

        //            case FieldType.FieldShortBounds:
        //            case FieldType.FieldAngleBounds:
        //            case FieldType.FieldRealBounds:
        //            case FieldType.FieldRealFractionBounds:
        //                list.Add(new BoundsFieldModel() { Owner = this, TagField = tagBlock.Fields[i] });
        //                break;

        //            case FieldType.FieldCharEnum:
        //            case FieldType.FieldEnum:
        //            case FieldType.FieldLongEnum:
        //                list.Add(new EnumFieldModel() { Owner = this, TagField = tagBlock.Fields[i] });
        //                break;

        //            case FieldType.FieldTagIndex:
        //            case FieldType.FieldTagReference:
        //                list.Add(new TagReferenceFieldModel() { Owner = this, TagField = tagBlock.Fields[i] });
        //                break;

        //            case FieldType.FieldData:
        //                break;

        //            case FieldType.FieldLongFlags:
        //            case FieldType.FieldWordFlags:
        //            case FieldType.FieldByteFlags:
        //                list.Add(new FlagsFieldModel() { Owner = this, TagField = tagBlock.Fields[i] });
        //                break;

        //            case FieldType.FieldEulerAngles3D:
        //            case FieldType.FieldRealVector3D:
        //            case FieldType.FieldRealRgbColor:
        //            case FieldType.FieldRealHsvColor:
        //            case FieldType.FieldRealPoint3D:
        //            case FieldType.FieldRealPlane2D:
        //            case FieldType.FieldRgbColor:
        //                list.Add(new Tuple3FieldModel() { Owner = this, TagField = tagBlock.Fields[i] });
        //                break;

        //            case FieldType.FieldEulerAngles2D:
        //            case FieldType.FieldRealVector2D:
        //            case FieldType.FieldRealPoint2D:
        //            case FieldType.FieldPoint2D:
        //                list.Add(new Tuple2FieldModel() { Owner = this, TagField = tagBlock.Fields[i] });
        //                break;

        //            case FieldType.FieldRealArgbColor:
        //            case FieldType.FieldRealAhsvColor:
        //            case FieldType.FieldRealPlane3D:
        //            case FieldType.FieldRectangle2D:
        //            case FieldType.FieldQuaternion:
        //            case FieldType.FieldArgbColor:
        //                list.Add(new Tuple4FieldModel() { Owner = this, TagField = tagBlock.Fields[i] });
        //                break;

        //            case FieldType.FieldVertexBuffer:
        //                break;

        //            case FieldType.FieldLongBlockFlags:
        //                break;
        //            case FieldType.FieldWordBlockFlags:
        //                break;
        //            case FieldType.FieldByteBlockFlags:
        //                break;
        //        }
        //    }

        //    //Return
        //    return list;
        //}
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
