using Abide.Tag;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for Point2ValueEditor.xaml
    /// </summary>
    public partial class Point2ValueEditor : UserControl
    {
        public static readonly DependencyProperty FieldProperty =
               DependencyProperty.Register(nameof(Field), typeof(Field), typeof(Point2ValueEditor), new PropertyMetadata(FieldPropertyChanged));
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register(nameof(X), typeof(string), typeof(Point2ValueEditor), new PropertyMetadata(ComponentPropertyChanged));
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register(nameof(Y), typeof(string), typeof(Point2ValueEditor), new PropertyMetadata(ComponentPropertyChanged));

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

        public Point2ValueEditor()
        {
            InitializeComponent();
        }

        private static void FieldPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Point2ValueEditor editor)
            {
                if (e.NewValue is Field field)
                {
                    switch (field.Type)
                    {
                        case FieldType.FieldPoint2D:
                            var point = ((Point2dField)field).Point;
                            editor.propogateChanges = false;
                            editor.X = point.X.ToString();
                            editor.propogateChanges = true;
                            editor.Y = point.Y.ToString();
                            break;
                        case FieldType.FieldRealPoint2D:
                            var pointF = ((RealPoint2dField)field).Point;
                            editor.propogateChanges = false;
                            editor.X = pointF.X.ToString();
                            editor.propogateChanges = true;
                            editor.Y = pointF.Y.ToString();
                            break;
                    }
                }
            }
        }
        private static void ComponentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Point2ValueEditor editor && editor.propogateChanges)
            {
                switch (editor.Field)
                {
                    case RealPoint2dField realPoint2dField:
                        if (float.TryParse(editor.X, out float xFloat) && float.TryParse(editor.Y, out float yFloat))
                        {
                            realPoint2dField.Point = new Point2F(xFloat, yFloat);
                        }
                        break;
                    case Point2dField point2dField:
                        if (short.TryParse(editor.X, out short xShort) && short.TryParse(editor.Y, out short yShort))
                        {
                            point2dField.Point = new Point2(xShort, yShort);
                        }
                        break;
                }
            }
        }
    }
}
