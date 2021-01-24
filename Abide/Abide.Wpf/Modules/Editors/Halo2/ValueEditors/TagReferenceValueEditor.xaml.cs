using Abide.Guerilla.Library;
using Abide.Tag.Guerilla;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using File = System.IO.File;
using Path = System.IO.Path;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for TagReferenceEditor.xaml
    /// </summary>
    public partial class TagReferenceValueEditor : ValueEditorBase
    {
        private static readonly DependencyPropertyKey IsInvalidPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(IsInvalid), typeof(bool), typeof(TagReferenceValueEditor), new PropertyMetadata(false));
        private static string TagsDirectory => Path.Combine(RegistrySettings.WorkspaceDirectory, "tags");

        public static readonly DependencyProperty FilePathProperty =
            DependencyProperty.Register(nameof(FilePath), typeof(string), typeof(TagReferenceValueEditor), new PropertyMetadata(FilePathPropertyChanged));
        public static readonly DependencyProperty IsInvalidProperty = IsInvalidPropertyKey.DependencyProperty;

        private TagReferenceField referenceField;

        public string FilePath
        {
            get => (string)GetValue(FilePathProperty);
            set => SetValue(FilePathProperty, value);
        }
        public bool IsInvalid
        {
            get => (bool)GetValue(IsInvalidProperty);
            private set => SetValue(IsInvalidPropertyKey, value);
        }

        public TagReferenceValueEditor()
        {
            InitializeComponent();
        }
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog() { InitialDirectory = TagsDirectory };
            if (File.Exists(FilePath))
            {
                openDlg.InitialDirectory = Path.GetDirectoryName(FilePath);
                openDlg.FileName = Path.GetFileName(FilePath);
            }

            if (openDlg.ShowDialog() ?? false)
            {
                if (openDlg.FileName.StartsWith(TagsDirectory))
                {
                    FilePath = openDlg.FileName;
                }
                else
                {
                    MessageBox.Show("Invalid file selected.");
                }
            }
        }
        protected override void OnFieldPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is TagReferenceField field)
            {
                referenceField = field;
                string filePath = Path.Combine(TagsDirectory, field.String);
                if (File.Exists(filePath))
                {
                    FilePath = filePath;
                }
            }
        }
        private static void FilePathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TagReferenceValueEditor editor && editor.PropogateChanges)
            {
                if (e.NewValue is string filePath)
                {
                    if (File.Exists(filePath) && filePath.StartsWith(TagsDirectory))
                    {
                        editor.referenceField.String = filePath.Substring(TagsDirectory.Length + 1);
                    }

                    if (File.Exists(filePath) || string.IsNullOrEmpty(filePath))
                    {
                        editor.IsInvalid = false;
                    }
                    else
                    {
                        editor.IsInvalid = true;
                    }
                }
            }
        }
    }

    internal sealed class ShortStringPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string path)
            {
                string tagsDirectory = Path.Combine(RegistrySettings.WorkspaceDirectory, "tags");
                if (path.StartsWith(tagsDirectory))
                {
                    return path.Substring(tagsDirectory.Length + 1);
                }
                else
                {
                    return path;
                }
            }

            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string tagPath)
            {
                return Path.Combine(RegistrySettings.WorkspaceDirectory, "tags", tagPath);
            }

            return value;
        }
    }
}
