using Abide.Tag;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for ColorValueEditor.xaml
    /// </summary>
    public partial class ColorValueEditor : UserControl
    {
        public static readonly DependencyProperty FieldProperty =
            DependencyProperty.Register(nameof(Field), typeof(Field), typeof(ColorValueEditor), new PropertyMetadata(FieldPropertyChanged));

        public Field Field
        {
            get => (Field)GetValue(FieldProperty);
            set => SetValue(FieldProperty, value);
        }

        public ColorValueEditor()
        {
            InitializeComponent();
        }

        private static void FieldPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorValueEditor editor)
            {
                if (e.NewValue is Field field)
                {
                    // todo implement
                    switch (editor.Field.Type)
                    {

                    }
                }
            }
        }
    }
}
