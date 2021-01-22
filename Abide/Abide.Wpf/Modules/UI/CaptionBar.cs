using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Abide.Wpf.Modules.UI
{
    public class CaptionBar : ContentControl
    {
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof(ImageSource),
            typeof(ImageSource), typeof(CaptionBar), new PropertyMetadata());
        public static readonly DependencyProperty ShowIconProperty = DependencyProperty.Register(nameof(ShowIcon),
            typeof(bool), typeof(CaptionBar), new PropertyMetadata(true));
        public static readonly DependencyProperty CanMinimizeProperty = DependencyProperty.Register(nameof(CanMinimize),
            typeof(bool), typeof(CaptionBar), new PropertyMetadata(true));
        public static readonly DependencyProperty CanMaxRestoreProperty = DependencyProperty.Register(nameof(CanMaxRestore),
            typeof(bool), typeof(CaptionBar), new PropertyMetadata(true));
        public static readonly DependencyProperty CanCloseProperty = DependencyProperty.Register(nameof(CanClose),
            typeof(bool), typeof(CaptionBar), new PropertyMetadata(true));
        private static readonly DependencyProperty IsWindowActiveProperty = DependencyProperty.Register(nameof(IsWindowActive),
            typeof(bool), typeof(CaptionBar), new PropertyMetadata(true));

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }
        public bool ShowIcon
        {
            get => (bool)GetValue(ShowIconProperty);
            set => SetValue(ShowIconProperty, value);
        }
        public bool CanMinimize
        {
            get => (bool)GetValue(CanMinimizeProperty);
            set => SetValue(CanMinimizeProperty, value);
        }
        public bool CanMaxRestore
        {
            get => (bool)GetValue(CanMaxRestoreProperty);
            set => SetValue(CanMaxRestoreProperty, value);
        }
        public bool CanClose
        {
            get => (bool)GetValue(CanCloseProperty);
            set => SetValue(CanCloseProperty, value);
        }
        public bool IsWindowActive
        {
            get => (bool)GetValue(IsWindowActiveProperty);
            set => SetValue(IsWindowActiveProperty, value);
        }
        
        protected override Size MeasureOverride(Size constraint)
        {
            return constraint;
        }

        static CaptionBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CaptionBar), new FrameworkPropertyMetadata(typeof(CaptionBar)));
        }
    }
}
