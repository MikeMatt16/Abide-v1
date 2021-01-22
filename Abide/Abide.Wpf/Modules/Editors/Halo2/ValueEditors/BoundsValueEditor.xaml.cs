using Abide.Tag;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.Editors.Halo2.ValueEditors
{
    /// <summary>
    /// Interaction logic for BoundsValueEditor.xaml
    /// </summary>
    public partial class BoundsValueEditor : UserControl
    {
        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register(nameof(From), typeof(string), typeof(BoundsValueEditor), new PropertyMetadata(string.Empty, ValuePropertyChanged));
        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register(nameof(To), typeof(string), typeof(BoundsValueEditor), new PropertyMetadata(string.Empty, ValuePropertyChanged));
        public static readonly DependencyProperty FieldProperty =
            DependencyProperty.Register(nameof(Field), typeof(Field), typeof(BoundsValueEditor), new PropertyMetadata(FieldPropertyChanged));

        private bool propogateChanges = true;

        public Field Field
        {
            get => (Field)GetValue(FieldProperty);
            set => SetValue(FieldProperty, value);
        }
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

        private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BoundsValueEditor editor && editor.propogateChanges)
            {
                var from = (string)editor.GetValue(FromProperty);
                var to = (string)editor.GetValue(ToProperty);

                switch (editor.Field.Type)
                {
                    case FieldType.FieldShortBounds:
                        if (short.TryParse(from, out short fromShort) && short.TryParse(to, out short toShort))
                        {
                            var bounds = (ShortBoundsField)editor.Field;
                            bounds.Bounds = new ShortBounds(fromShort, toShort);
                        }
                        break;

                    case FieldType.FieldRealBounds:
                        if (float.TryParse(from, out float fromReal) && float.TryParse(to, out float toReal))
                        {
                            var bounds = (RealBoundsField)editor.Field;
                            bounds.Bounds = new FloatBounds(fromReal, toReal);
                        }
                        break;

                    case FieldType.FieldAngleBounds:
                        if (float.TryParse(from, out float fromAngle) && float.TryParse(to, out float toAngle))
                        {
                            var bounds = (AngleBoundsField)editor.Field;
                            bounds.Bounds = new FloatBounds(fromAngle, toAngle);
                        }
                        break;

                    case FieldType.FieldRealFractionBounds:
                        if (float.TryParse(from, out float fromFraction) && float.TryParse(to, out float toFraction))
                        {
                            var bounds = (RealFractionBoundsField)editor.Field;
                            bounds.Bounds = new FloatBounds(fromFraction, toFraction);
                        }
                        break;
                }                
            }
        }
        private static void FieldPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BoundsValueEditor editor)
            {
                editor.propogateChanges = false;
                if (e.NewValue is Field field)
                {
                    switch (field.Type)
                    {
                        case FieldType.FieldRealFractionBounds:
                        case FieldType.FieldAngleBounds:
                        case FieldType.FieldRealBounds:
                            var floatBounds = (FloatBounds)field.Value;
                            editor.SetValue(FromProperty, floatBounds.Min.ToString());
                            editor.SetValue(ToProperty, floatBounds.Max.ToString());
                            break;

                        case FieldType.FieldShortBounds:
                            var shortBounds = (ShortBounds)field.Value;
                            editor.SetValue(FromProperty, shortBounds.Min.ToString());
                            editor.SetValue(ToProperty, shortBounds.Max.ToString());
                            break;
                    }
                }
                editor.propogateChanges = true;
            }
        }
    }
}
