using Abide.Tag;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for BoundsValueEditor.xaml
    /// </summary>
    public partial class BoundsValueEditor : ValueEditorBase
    {
        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register(nameof(From), typeof(string), typeof(BoundsValueEditor), new PropertyMetadata(string.Empty, ValuePropertyChanged));
        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register(nameof(To), typeof(string), typeof(BoundsValueEditor), new PropertyMetadata(string.Empty, ValuePropertyChanged));

        public string From
        {
            get => (string)GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }
        public string To
        {
            get => (string)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        public BoundsValueEditor()
        {
            InitializeComponent();
        }
        protected override void OnFieldPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            switch (Field.Type)
            {
                case FieldType.FieldRealFractionBounds:
                case FieldType.FieldAngleBounds:
                case FieldType.FieldRealBounds:
                    var floatBounds = (FloatBounds)Field.Value;
                    From = floatBounds.Min.ToString();
                    To = floatBounds.Max.ToString();
                    break;

                case FieldType.FieldShortBounds:
                    var shortBounds = (ShortBounds)Field.Value;
                    From = shortBounds.Min.ToString();
                    To = shortBounds.Max.ToString();
                    break;
            }
        }
        private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BoundsValueEditor editor && editor.PropogateChanges)
            {
                var from = (string)editor.GetValue(FromProperty);
                var to = (string)editor.GetValue(ToProperty);

                switch (editor.Field)
                {
                    case ShortBoundsField shortBounds:
                        if (short.TryParse(from, out short fromShort) && short.TryParse(to, out short toShort))
                        {
                            System.Diagnostics.Debugger.Break();
                            shortBounds.Bounds = new ShortBounds(fromShort, toShort);
                        }
                        break;

                    case RealBoundsField realBounds:
                        if (float.TryParse(from, out float fromReal) && float.TryParse(to, out float toReal))
                        {
                            System.Diagnostics.Debugger.Break();
                            realBounds.Bounds = new FloatBounds(fromReal, toReal);
                        }
                        break;
                }         
            }
        }
    }
}
