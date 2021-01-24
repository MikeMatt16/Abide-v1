using Abide.Tag;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for Point3ValueEditor.xaml
    /// </summary>
    public partial class Point3ValueEditor : ValueEditorBase
    {
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register(nameof(X), typeof(string), typeof(Point3ValueEditor), new PropertyMetadata(ComponentPropertyChanged));
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register(nameof(Y), typeof(string), typeof(Point3ValueEditor), new PropertyMetadata(ComponentPropertyChanged));
        public static readonly DependencyProperty ZProperty =
            DependencyProperty.Register(nameof(Z), typeof(string), typeof(Point3ValueEditor), new PropertyMetadata(ComponentPropertyChanged));

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
        protected override void OnFieldPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is RealPoint3dField field)
            {
                X = field.Point.X.ToString();
                Y = field.Point.Y.ToString();
                Z = field.Point.Z.ToString();
            }
        }
        private static void ComponentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Point3ValueEditor editor && editor.PropogateChanges)
            {
                switch (editor.Field)
                {
                    case RealPoint3dField realPoint3DField:
                        if (float.TryParse(editor.X, out float x) && float.TryParse(editor.Y, out float y) && float.TryParse(editor.Z, out float z))
                        {
                            System.Diagnostics.Debugger.Break();
                            realPoint3DField.Point = new Point3F(x, y, z);
                        }
                        break;
                }
            }
        }
    }
}
