using System;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.UI
{
    public class ColorPicker : Control
    {
        public static readonly DependencyProperty HexColorProperty = DependencyProperty.Register("HexColor", typeof(string), typeof(ColorPicker),
            new PropertyMetadata(HexColorPropertyChanged));
        public static readonly DependencyProperty RedProperty = DependencyProperty.Register("Red", typeof(int), typeof(ColorPicker),
            new PropertyMetadata(RedPropertyChanged));
        public static readonly DependencyProperty GreenProperty = DependencyProperty.Register("Green", typeof(int), typeof(ColorPicker),
            new PropertyMetadata(GreenPropertyChanged));
        public static readonly DependencyProperty BlueProperty = DependencyProperty.Register("Blue", typeof(int), typeof(ColorPicker),
            new PropertyMetadata(BluePropertyChanged));
        public static readonly DependencyProperty AlphaProperty = DependencyProperty.Register("Alpha", typeof(int), typeof(ColorPicker),
            new PropertyMetadata(AlphaPropertyChanged));
        public static readonly DependencyProperty HueProperty = DependencyProperty.Register("Hue", typeof(float), typeof(ColorPicker),
            new PropertyMetadata(AlphaPropertyChanged));
        public static readonly DependencyProperty SaturationProperty = DependencyProperty.Register("Saturation", typeof(float), typeof(ColorPicker),
            new PropertyMetadata(SaturationPropertyChanged));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(float), typeof(ColorPicker),
            new PropertyMetadata(ValuePropertyChanged));

        static ColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPicker), new FrameworkPropertyMetadata(typeof(ColorPicker)));
        }
        private static void HexColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static void RedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static void GreenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static void BluePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static void AlphaPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static void SaturationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
    }
}
