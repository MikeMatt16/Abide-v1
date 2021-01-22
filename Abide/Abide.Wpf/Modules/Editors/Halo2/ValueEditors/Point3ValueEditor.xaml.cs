using Abide.Tag;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for Point3ValueEditor.xaml
    /// </summary>
    public partial class Point3ValueEditor : UserControl
    {
        public static readonly DependencyProperty FieldProperty =
               DependencyProperty.Register(nameof(Field), typeof(Field), typeof(Point3ValueEditor), new PropertyMetadata(FieldPropertyChanged));
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register(nameof(X), typeof(string), typeof(Point3ValueEditor), new PropertyMetadata(ComponentPropertyChanged));
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register(nameof(Y), typeof(string), typeof(Point3ValueEditor), new PropertyMetadata(ComponentPropertyChanged));
        public static readonly DependencyProperty ZProperty =
            DependencyProperty.Register(nameof(Z), typeof(string), typeof(Point3ValueEditor), new PropertyMetadata(ComponentPropertyChanged));

        private bool propogateChanges = false;

        public Field Field
        {
            get => (Field)GetValue(FieldProperty);
            set => SetValue(FieldProperty, value);
        }
        public string X
        {
            get => (string)GetValue(XProperty);
            set => SetValue(XProperty, value);
        }
        public string Y
        {
            get => (string)GetValue(YProperty);
            set => SetValue(YProperty, value);
        }
        public string Z
        {
            get => (string)GetValue(ZProperty);
            set => SetValue(ZProperty, value);
        }

        public Point3ValueEditor()
        {
            InitializeComponent();
        }

        private static void FieldPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Point3ValueEditor editor)
            {
                if (e.NewValue is Field field)
                {
                    switch (field.Type)
                    {
                        case FieldType.FieldRealPoint3D:
                            var point = ((RealPoint3dField)field).Point;
                            editor.propogateChanges = false;
                            editor.X = point.X.ToString();
                            editor.Y = point.X.ToString();
                            editor.propogateChanges = true;
                            editor.Z = point.X.ToString();
                            break;
                    }
                }
            }
        }
        private static void ComponentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Point3ValueEditor editor && editor.propogateChanges)
            {
                switch (editor.Field)
                {
                    case RealPoint3dField realPoint3DField:
                        if (float.TryParse(editor.X, out float x) && float.TryParse(editor.Y, out float y) && float.TryParse(editor.Z, out float z))
                        {
                            realPoint3DField.Point = new Point3F(x, y, z);
                        }
                        break;
                }
            }
        }
    }
}
