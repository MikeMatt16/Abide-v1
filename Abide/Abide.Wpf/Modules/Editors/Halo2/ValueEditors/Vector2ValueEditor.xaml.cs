using Abide.Tag;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for Vector2ValueEditor.xaml
    /// </summary>
    public partial class Vector2ValueEditor : ValueEditorBase
    {
        public static readonly DependencyProperty IProperty =
            DependencyProperty.Register(nameof(I), typeof(string), typeof(Vector2ValueEditor), new PropertyMetadata(ComponentPropertyChanged));
        public static readonly DependencyProperty JProperty =
            DependencyProperty.Register(nameof(J), typeof(string), typeof(Vector2ValueEditor), new PropertyMetadata(ComponentPropertyChanged));

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
        protected override void OnFieldPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            switch (e.NewValue)
            {
                case RealVector2dField realVector2DField:
                    I = realVector2DField.Vector.I.ToString();
                    J = realVector2DField.Vector.J.ToString();
                    break;

                case EulerAngles2dField eulerAngles2DField:
                    I = eulerAngles2DField.Vector.I.ToString();
                    J = eulerAngles2DField.Vector.J.ToString(); 
                    break;
            }
        }
        private static void ComponentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Vector2ValueEditor editor && editor.PropogateChanges)
            {
                if (float.TryParse(editor.I, out float i) && float.TryParse(editor.J, out float j))
                {
                    switch (editor.Field)
                    {
                        case RealVector2dField realVector2DField:
                            System.Diagnostics.Debugger.Break();
                            realVector2DField.Vector = new Vector2(i, j);
                            break;
                        case EulerAngles2dField eulerAngles2DField:
                            System.Diagnostics.Debugger.Break();
                            eulerAngles2DField.Vector = new Vector2(i, j);
                            break;
                    }
                }
            }
        }
    }
}
