using Abide.Tag;
using System.Windows;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for Vector4ValueEditor.xaml
    /// </summary>
    public partial class Vector4ValueEditor : ValueEditorBase
    {
        public static readonly DependencyProperty IProperty =
            DependencyProperty.Register(nameof(I), typeof(string), typeof(Vector4ValueEditor), new PropertyMetadata(ComponentPropertyChanged));
        public static readonly DependencyProperty JProperty =
            DependencyProperty.Register(nameof(J), typeof(string), typeof(Vector4ValueEditor), new PropertyMetadata(ComponentPropertyChanged));
        public static readonly DependencyProperty KProperty =
            DependencyProperty.Register(nameof(K), typeof(string), typeof(Vector4ValueEditor), new PropertyMetadata(ComponentPropertyChanged));
        public static readonly DependencyProperty WProperty =
            DependencyProperty.Register(nameof(W), typeof(string), typeof(Vector4ValueEditor), new PropertyMetadata(ComponentPropertyChanged));

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
        protected override void OnFieldPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            switch (e.NewValue)
            {
                case RealPlane3dField realPlane3DField:
                    I = realPlane3DField.Vector.I.ToString();
                    J = realPlane3DField.Vector.J.ToString();
                    K = realPlane3DField.Vector.K.ToString();
                    W = realPlane3DField.Vector.W.ToString();
                    break;

                case QuaternionField quaternionField:
                    W = quaternionField.Quaternion.W.ToString();
                    I = quaternionField.Quaternion.I.ToString();
                    J = quaternionField.Quaternion.J.ToString();
                    K = quaternionField.Quaternion.K.ToString();
                    break;
            }
        }
        private static void ComponentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Vector4ValueEditor editor && editor.PropogateChanges)
            {
                if(float.TryParse(editor.I, out float i) && float.TryParse(editor.J, out float j) &&
                    float.TryParse(editor.K, out float k) && float.TryParse(editor.W, out float w))
                {
                    switch (editor.Field)
                    {
                        case QuaternionField quaternionField:
                            System.Diagnostics.Debugger.Break();
                            quaternionField.Quaternion = new Quaternion(w, i, j, k);
                            break;
                        case RealPlane3dField realPlane3DField:
                            System.Diagnostics.Debugger.Break();
                            realPlane3DField.Vector = new Vector4(i, j, k, w);
                            break;
                    }
                }
            }
        }
    }
}
