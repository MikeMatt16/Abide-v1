using Abide.Tag;
using System.Windows;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for Point2ValueEditor.xaml
    /// </summary>
    public partial class Point2ValueEditor : ValueEditorBase
    {
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register(nameof(X), typeof(string), typeof(Point2ValueEditor), new PropertyMetadata(ComponentPropertyChanged));
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register(nameof(Y), typeof(string), typeof(Point2ValueEditor), new PropertyMetadata(ComponentPropertyChanged));

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
        protected override void OnFieldPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            switch (e.NewValue)
            {
                case Point2dField point2DField:
                    X = point2DField.Point.X.ToString();
                    Y = point2DField.Point.Y.ToString();
                    break;

                case RealPoint2dField realPoint2DField:
                    X = realPoint2DField.Point.X.ToString();
                    Y = realPoint2DField.Point.Y.ToString();
                    break;
            }
        }

        private static void ComponentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Point2ValueEditor editor && editor.PropogateChanges)
            {
                switch (editor.Field)
                {
                    case RealPoint2dField realPoint2dField:
                        if (float.TryParse(editor.X, out float xFloat) && float.TryParse(editor.Y, out float yFloat))
                        {
                            System.Diagnostics.Debugger.Break();
                            realPoint2dField.Point = new Point2F(xFloat, yFloat);
                        }
                        break;

                    case Point2dField point2dField:
                        if (short.TryParse(editor.X, out short xShort) && short.TryParse(editor.Y, out short yShort))
                        {
                            System.Diagnostics.Debugger.Break();
                            point2dField.Point = new Point2(xShort, yShort);
                        }
                        break;
                }
            }
        }
    }
}
