using Abide.Tag;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for Vector4ValueEditor.xaml
    /// </summary>
    public partial class Vector4ValueEditor : UserControl
    {
        public static readonly DependencyProperty FieldProperty =
            DependencyProperty.Register(nameof(Field), typeof(Field), typeof(Vector4ValueEditor), new PropertyMetadata(FieldPropertyChanged));
        public static readonly DependencyProperty IProperty =
            DependencyProperty.Register(nameof(I), typeof(string), typeof(Vector4ValueEditor), new PropertyMetadata(ComponentPropertyChanged));
        public static readonly DependencyProperty JProperty =
            DependencyProperty.Register(nameof(J), typeof(string), typeof(Vector4ValueEditor), new PropertyMetadata(ComponentPropertyChanged));
        public static readonly DependencyProperty KProperty =
            DependencyProperty.Register(nameof(K), typeof(string), typeof(Vector4ValueEditor), new PropertyMetadata(ComponentPropertyChanged));
        public static readonly DependencyProperty WProperty =
            DependencyProperty.Register(nameof(W), typeof(string), typeof(Vector4ValueEditor), new PropertyMetadata(ComponentPropertyChanged));

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
        public string W
        {
            get => (string)GetValue(WProperty);
            set => SetValue(WProperty, value);
        }

        public Vector4ValueEditor()
        {
            InitializeComponent();
        }

        private static void FieldPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Vector4ValueEditor editor)
            {
                if (e.NewValue is Field field)
                {
                    switch (field.Type)
                    {
                        case FieldType.FieldRealPlane3D:
                            var vector = ((RealPlane3dField)field).Vector;
                            editor.propogateChanges = false;
                            editor.I = vector.I.ToString();
                            editor.J = vector.J.ToString();
                            editor.K = vector.K.ToString();
                            editor.propogateChanges = true;
                            editor.W = vector.W.ToString();
                            break;

                        case FieldType.FieldQuaternion:
                            var quaternion = ((QuaternionField)field).Quaternion;
                            editor.propogateChanges = false;
                            editor.I = quaternion.I.ToString();
                            editor.J = quaternion.J.ToString();
                            editor.K = quaternion.K.ToString();
                            editor.propogateChanges = true;
                            editor.W = quaternion.W.ToString();
                            break;
                    }
                }
            }
        }
        private static void ComponentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Vector4ValueEditor editor && editor.propogateChanges)
            {
                if(float.TryParse(editor.I, out float i) && float.TryParse(editor.J, out float j) &&
                    float.TryParse(editor.K, out float k) && float.TryParse(editor.W, out float w))
                {
                    switch (editor.Field)
                    {
                        case QuaternionField quaternionField:
                            quaternionField.Quaternion = new Quaternion(w, i, j, k);
                            break;
                        case RealPlane3dField realPlane3DField:
                            realPlane3DField.Vector = new Vector4(i, j, k, w);
                            break;
                    }
                }
            }
        }
    }
}
