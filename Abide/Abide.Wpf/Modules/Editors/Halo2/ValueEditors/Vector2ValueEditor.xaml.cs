using Abide.Tag;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for Vector2ValueEditor.xaml
    /// </summary>
    public partial class Vector2ValueEditor : UserControl
    {
        public static readonly DependencyProperty FieldProperty =
            DependencyProperty.Register(nameof(Field), typeof(Field), typeof(Vector2ValueEditor), new PropertyMetadata(FieldPropertyChanged));
        public static readonly DependencyProperty IProperty =
            DependencyProperty.Register(nameof(I), typeof(string), typeof(Vector2ValueEditor), new PropertyMetadata(ComponentPropertyChanged));
        public static readonly DependencyProperty JProperty =
            DependencyProperty.Register(nameof(J), typeof(string), typeof(Vector2ValueEditor), new PropertyMetadata(ComponentPropertyChanged));

        private bool propogateChanges = false;

        public Field Field
        {
            get => (Field)GetValue(FieldProperty);
            set => SetValue(FieldProperty, value);
        }
        public string I
        {
            get => (string)GetValue(IProperty);
            set => SetValue(IProperty, value);
        }
        public string J
        {
            get => (string)GetValue(JProperty);
            set => SetValue(JProperty, value);
        }

        public Vector2ValueEditor()
        {
            InitializeComponent();
        }

        private static void FieldPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Vector2ValueEditor editor)
            {
                if (e.NewValue is Field field)
                {
                    Vector2 vector;
                    switch (field.Type)
                    {
                        case FieldType.FieldRealVector2D:
                            vector = ((RealVector2dField)field).Vector;
                            editor.propogateChanges = false;
                            editor.I = vector.I.ToString();
                            editor.propogateChanges = true;
                            editor.J = vector.J.ToString();
                            break;
                        case FieldType.FieldEulerAngles2D:
                            vector = ((EulerAngles2dField)field).Vector;
                            editor.propogateChanges = false;
                            editor.I = vector.I.ToString();
                            editor.propogateChanges = true;
                            editor.J = vector.J.ToString();
                            break;
                    }
                }
            }
        }
        private static void ComponentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Vector2ValueEditor editor && editor.propogateChanges)
            {
                if (float.TryParse(editor.I, out float i) && float.TryParse(editor.J, out float j))
                {
                    switch (editor.Field)
                    {
                        case RealVector2dField realVector2DField:
                            realVector2DField.Vector = new Vector2(i, j);
                            break;
                        case EulerAngles2dField eulerAngles2DField:
                            eulerAngles2DField.Vector = new Vector2(i, j);
                            break;
                    }
                }
            }
        }
    }
}
