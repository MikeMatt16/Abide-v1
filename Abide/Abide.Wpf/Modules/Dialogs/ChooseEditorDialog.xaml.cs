using Abide.AddOnApi.Wpf;
using System.Collections.ObjectModel;
using System.Windows;

namespace Abide.Wpf.Modules.Dialogs
{
    /// <summary>
    /// Interaction logic for ChooseEditorDialog.xaml
    /// </summary>
    public partial class ChooseEditorDialog : Window
    {
        private static readonly DependencyPropertyKey EditorsPropertyKey =
            DependencyProperty.RegisterReadOnly("Editors", typeof(ObservableCollection<IFileEditor>), typeof(ChooseEditorDialog),
                new PropertyMetadata(new ObservableCollection<IFileEditor>()));
        /// <summary>
        /// Identifies the <see cref="Editors"/> property.
        /// </summary>
        public static readonly DependencyProperty EditorsProperty =
            EditorsPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="SelectedEditor"/> property.
        /// </summary>
        public static readonly DependencyProperty SelectedEditorProperty =
            DependencyProperty.Register("SelectedEditor", typeof(IFileEditor), typeof(ChooseEditorDialog));

        /// <summary>
        /// Gets and returns the list of editors.
        /// </summary>
        public ObservableCollection<IFileEditor> Editors => (ObservableCollection<IFileEditor>)GetValue(EditorsProperty);
        /// <summary>
        /// Gets or sets the currently selected editor.
        /// </summary>
        public IFileEditor SelectedEditor
        {
            get => (IFileEditor)GetValue(SelectedEditorProperty);
            set => SetValue(SelectedEditorProperty, value);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ChooseEditorDialog"/> class.
        /// </summary>
        public ChooseEditorDialog()
        {
            InitializeComponent();
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            //Set dialog result
            DialogResult = true;
        }
    }
}
