using Abide.Tag;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for Vector3ValueEditor.xaml
    /// </summary>
    public partial class Vector3ValueEditor : UserControl
    {
        public static readonly DependencyProperty FieldProperty =
            DependencyProperty.Register(nameof(Field), typeof(Field), typeof(Vector3ValueEditor), new PropertyMetadata(FieldPropertyChanged));
        public static readonly DependencyProperty IProperty =
            DependencyProperty.Register(nameof(I), typeof(string), typeof(Vector3ValueEditor), new PropertyMetadata(ComponentPropertyChanged));
        public static readonly DependencyProperty JProperty =
            DependencyProperty.Register(nameof(J), typeof(string), typeof(Vector3ValueEditor), new PropertyMetadata(ComponentPropertyChanged));
        public static readonly DependencyProperty KProperty =
            DependencyProperty.Register(nameof(K), typeof(string), typeof(Vector3ValueEditor), new PropertyMetadata(ComponentPropertyChanged));

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
        public string K
        {
            get => (string)GetValue(KProperty);
            set => SetValue(KProperty, value);
        }

        public Vector3ValueEditor()
        {
            InitializeComponent();
        }

        private static void FieldPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Vector3ValueEditor editor)
            {
                if (e.NewValue is Field field)
                {
                    Vector3 vector;
                    switch (field.Type)
                    {
                        case FieldType.FieldRealPlane2D:
                            vector = ((RealPlane2dField)field).Vector;
                            editor.propogateChanges = false;
                            editor.I = vector.I.ToString();
                            editor.J = vector.J.ToString();
                            editor.propogateChanges = true;
                            editor.K = vector.K.ToString();
                            break;
                        case FieldType.FieldRealVector3D:
                            vector = ((RealVector3dField)field).Vector;
                            editor.propogateChanges = false;
                            editor.I = vector.I.ToString();
                            editor.J = vector.J.ToString();
                            editor.propogateChanges = true;
                            editor.K = vector.K.ToString();
                            break;
                        case FieldType.FieldEulerAngles3D:
                            vector = ((EulerAngles3dField)field).Vector;
                            editor.propogateChanges = false;
                            editor.I = vector.I.ToString();
                            editor.J = vector.J.ToString();
                            editor.propogateChanges = true;
                            editor.K = vector.K.ToString();
                            break;
                    }
                }
            }
        }
        private static void ComponentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Vector3ValueEditor editor && editor.propogateChanges)
            {
                if (float.TryParse(editor.I, out float i) && float.TryParse(editor.J, out float j) && float.TryParse(editor.K, out float k))
                {
                    switch (editor.Field)
                    {
                        case RealPlane2dField realPlane2DField:
                            realPlane2DField.Vector = new Vector3(i, j, k);
                            break;
                        case RealVector3dField realVector3DField:
                            realVector3DField.Vector = new Vector3(i, j, k);
                            break;
                        case EulerAngles3dField eulerAngles3DField:
                            eulerAngles3DField.Vector = new Vector3(i, j, k);
                            break;
                    }
                }
            }
        }
    }
}
