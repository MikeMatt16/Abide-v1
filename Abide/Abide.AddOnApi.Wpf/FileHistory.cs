using System;
using System.Windows;

namespace Abide.AddOnApi.Wpf
{
    /// <summary>
    /// Represents a file history.
    /// </summary>
    public abstract class FileHistory : Freezable
    {
        /// <summary>
        /// Identifies the <see cref="DateTime"/> property.
        /// </summary>
        public static readonly DependencyProperty DateTimeProperty =
            DependencyProperty.Register("DateTime", typeof(DateTime), typeof(FileHistory));
        /// <summary>
        /// Identifies the <see cref="Description"/> property.
        /// </summary>
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(FileHistory));
        /// <summary>
        /// Identifies the <see cref="FileState"/> property.
        /// </summary>
        public static readonly DependencyProperty FileStateProperty =
            DependencyProperty.Register("FileState", typeof(object), typeof(FileHistory));
        /// <summary>
        /// Gets or sets the date time of this file history.
        /// </summary>
        public DateTime DateTime
        {
            get { return (DateTime)GetValue(DateTimeProperty); }
            set { SetValue(DateTimeProperty, value); }
        }
        /// <summary>
        /// Gets or sets the description of the file state.
        /// </summary>
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
        /// <summary>
        /// Gets or sets the state of the file.
        /// </summary>
        public object FileState
        {
            get { return GetValue(FileStateProperty); }
            set { SetValue(FileStateProperty, value); }
        }
    }
}
