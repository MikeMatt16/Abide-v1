using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Input;

namespace Abide.Guerilla.Wpf.ViewModel
{
    /// <summary>
    /// Represents a tag hierarchy model.
    /// </summary>
    public class TagHierarchyModel : NotifyPropertyChangedViewModel
    {
        /// <summary>
        /// Gets and returns a list of tree elements in the tag hierarchy.
        /// </summary>
        public ObservableCollection<TreeObjectModel> Elements { get; } = new ObservableCollection<TreeObjectModel>();
    }

    /// <summary>
    /// Represents a directory 
    /// </summary>
    public class DirectoryObjectModel : TreeObjectModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryObjectModel"/> class.
        /// </summary>
        public DirectoryObjectModel()
        {
            //Set image
            ImagePath = "Resources/folder-16.png";
        }
    }

    /// <summary>
    /// Represents a file.
    /// </summary>
    public class FileObjectModel : TreeObjectModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileObjectModel"/> class.
        /// </summary>
        public FileObjectModel()
        {
            //Set image
            ImagePath = "Resources/tag-16.png";
        }
    }

    /// <summary>
    /// Represents a tree object.
    /// </summary>
    public class TreeObjectModel : NotifyPropertyChangedViewModel
    {
        /// <summary>
        /// Gets and returns the search command.
        /// </summary>
        public ICommand SearchCommand { get; }
        /// <summary>
        /// Gets and returns the name of the tree object.
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                if(name!= value)
                {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Gets and returns the image path for the tree object.
        /// </summary>
        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if(imagePath != value)
                {
                    imagePath = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Gets and returns a list of tree elements in the tree hierarchy.
        /// </summary>
        public ObservableCollection<TreeObjectModel> Elements { get; } = new ObservableCollection<TreeObjectModel>();
        /// <summary>
        /// Gets or sets the full path of the element.
        /// </summary>
        public string FullPath { get; set; }

        private string imagePath = null;
        private string name = null;

        public TreeObjectModel()
        {
            SearchCommand = new RelayCommand(p => Search());
        }

        private void Search()
        {
            throw new NotImplementedException();
        }
    }
}
