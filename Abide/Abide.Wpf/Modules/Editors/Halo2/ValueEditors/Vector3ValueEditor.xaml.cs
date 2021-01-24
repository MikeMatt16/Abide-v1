using Abide.Tag;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for Vector3ValueEditor.xaml
    /// </summary>
    public partial class Vector3ValueEditor : ValueEditorBase
    {
        public static readonly DependencyProperty IProperty =
            DependencyProperty.Register(nameof(I), typeof(string), typeof(Vector3ValueEditor), new PropertyMetadata(ComponentPropertyChanged));
        public static readonly DependencyProperty JProperty =
            DependencyProperty.Register(nameof(J), typeof(string), typeof(Vector3ValueEditor), new PropertyMetadata(ComponentPropertyChanged));
        public static readonly DependencyProperty KProperty =
            DependencyProperty.Register(nameof(K), typeof(string), typeof(Vector3ValueEditor), new PropertyMetadata(ComponentPropertyChanged));

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
        protected override void OnFieldPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            switch (e.NewValue)
            {
                case RealPlane2dField realPlane2DField:
                    I = realPlane2DField.Vector.I.ToString();
                    J = realPlane2DField.Vector.J.ToString();
                    I = realPlane2DField.Vector.K.ToString();
                    break;

                case RealVector3dField realVector3dField:
                    I = realVector3dField.Vector.I.ToString();
                    J = realVector3dField.Vector.J.ToString();
                    I = realVector3dField.Vector.K.ToString();
                    break;

                case EulerAngles3dField eulerAngles3DField:
                    I = eulerAngles3DField.Vector.I.ToString();
                    J = eulerAngles3DField.Vector.J.ToString();
                    I = eulerAngles3DField.Vector.K.ToString();
                    break;
            }
        }
        private static void ComponentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Vector3ValueEditor editor && editor.PropogateChanges)
            {
                if (float.TryParse(editor.I, out float i) && float.TryParse(editor.J, out float j) && float.TryParse(editor.K, out float k))
                {
                    switch (editor.Field)
                    {
                        case RealPlane2dField realPlane2DField:
                            System.Diagnostics.Debugger.Break();
                            realPlane2DField.Vector = new Vector3(i, j, k);
                            break;
                        case RealVector3dField realVector3DField:
                            System.Diagnostics.Debugger.Break();
                            realVector3DField.Vector = new Vector3(i, j, k);
                            break;
                        case EulerAngles3dField eulerAngles3DField:
                            System.Diagnostics.Debugger.Break();
                            eulerAngles3DField.Vector = new Vector3(i, j, k);
                            break;
                    }
                }
            }
        }
    }
}
