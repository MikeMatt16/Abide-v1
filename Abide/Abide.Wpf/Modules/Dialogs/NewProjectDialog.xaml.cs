using Abide.Wpf.Modules.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Abide.Wpf.Modules.Dialogs
{
    /// <summary>
    /// Interaction logic for NewProjectDialog.xaml
    /// </summary>
    public partial class NewProjectDialog : GlowWindowHost
    {
        private static readonly DependencyPropertyKey CanCreatePropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(CanCreate), typeof(bool), typeof(NewProjectDialog), new PropertyMetadata(false));

        /// <summary>
        /// Identifies the <see cref="Location"/> property.
        /// </summary>
        public static readonly DependencyProperty LocationProperty =
            DependencyProperty.Register(nameof(Location), typeof(string), typeof(NewProjectDialog), new PropertyMetadata(LocationPropertyChanged));
        /// <summary>
        /// Identifies the <see cref="SolutionName"/> property.
        /// </summary>
        public static readonly DependencyProperty SolutionNameProperty =
            DependencyProperty.Register(nameof(SolutionName), typeof(string), typeof(NewProjectDialog), new PropertyMetadata(SolutionNamePropertyChanged));

        /// <summary>
        /// Identifies the <see cref="CanCreate"/> property.
        /// </summary>
        public static readonly DependencyProperty CanCreateProperty = CanCreatePropertyKey.DependencyProperty;

        private static void SolutionNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NewProjectDialog dialog)
            {
                dialog.EvaluateCreateConditions();
            }
        }
        private static void LocationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NewProjectDialog dialog)
            {
                dialog.EvaluateCreateConditions();
            }
        }

        public bool CanCreate
        {
            get => (bool)GetValue(CanCreateProperty);
            private set => SetValue(CanCreatePropertyKey, value);
        }
        public string Location
        {
            get => (string)GetValue(LocationProperty);
            set => SetValue(LocationProperty, value);
        }
        public string SolutionName
        {
            get => (string)GetValue(SolutionNameProperty);
            set => SetValue(SolutionNameProperty, value);
        }

        public NewProjectDialog()
        {
            InitializeComponent();
        }
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (CanCreate)
            {
                DialogResult = true;
                Close();
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        private void EvaluateCreateConditions()
        {
            // Prepare
            bool canCreate = true;
            List<char> invalidChars = Path.GetInvalidFileNameChars().Union(Path.GetInvalidPathChars()).Distinct().ToList();

            // First checks
            if (string.IsNullOrEmpty(SolutionName))
            {
                canCreate = false;
            }

            if (string.IsNullOrEmpty(Location))
            {
                canCreate = false;
            }

            // Check every character against the invalid chars list
            if (!string.IsNullOrEmpty(SolutionName))
            {
                canCreate &= !SolutionName.Any(c => invalidChars.Contains(c));
            }

            // Set property
            CanCreate = canCreate;
        }
    }
}
