using Abide.HaloLibrary;
using Abide.Tag;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Abide.Guerilla.Wpf.ViewModel
{
    public class NewTagViewModel : NotifyPropertyChangedViewModel
    {
        /// <summary>
        /// Gets and returns a global list of tag definitions.
        /// </summary>
        public static ObservableCollection<TagDefinition> Tags = null;
        static NewTagViewModel()
        {
            Tags = new ObservableCollection<TagDefinition>(TagGroups.EnumerateTagDefinitions());
        }
        
        /// <summary>
        /// Gets and returns a list of tag definitions.
        /// </summary>
        public ObservableCollection<TagDefinition> TagDefinitions
        {
            get { return Tags; }
        }
        /// <summary>
        /// Gets or sets the selected tag definition.
        /// </summary>
        public TagDefinition SelectedTagDefinition
        {
            get { return selectedTagDefinition; }
            set
            {
                if (selectedTagDefinition != value)
                {
                    selectedTagDefinition = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private TagDefinition selectedTagDefinition = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewTagViewModel"/> class.
        /// </summary>
        public NewTagViewModel()
        {
            //Prepare
            SelectedTagDefinition = Tags.FirstOrDefault();
        }
    }

    /// <summary>
    /// Represents a tag definition
    /// </summary>
    public class TagDefinition
    {
        /// <summary>
        /// Gets or sets the name of the tag definition.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the group tag of the tag definition.
        /// </summary>
        public TagFourCc GroupTag { get; set; }
    }

    internal static class TagGroups
    {
        private static TagDefinition[] tagDefinitions = null;
        private static Type[] tagGroupTypes = null;
        static TagGroups()
        {
            //Get all tag group types in assembly
            tagGroupTypes = typeof(Tag.Guerilla.Generated.TagLookup).Assembly.GetExportedTypes().Where(
                t => t.GetInterfaces().Any(i => i == typeof(ITagGroup))).ToArray();

            //Create tag definitions from tag group types
            tagDefinitions = tagGroupTypes.Select(t =>
                {
                    ITagGroup tagGroup = (ITagGroup)Activator.CreateInstance(t);
                    return new TagDefinition()
                    {
                        GroupTag = tagGroup.GroupTag,
                        Name = tagGroup.Name
                    };
                }).OrderBy(d => d.GroupTag).ToArray();
        }

        public static IEnumerable<TagDefinition> EnumerateTagDefinitions()
        {
            foreach (var tag in tagDefinitions)
                yield return new TagDefinition() { GroupTag = tag.GroupTag, Name = tag.Name };
        }
    }
}
