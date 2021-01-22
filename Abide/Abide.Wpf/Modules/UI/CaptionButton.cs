using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Abide.Wpf.Modules.UI
{
    public class CaptionButton : ButtonBase
    {
        public static readonly DependencyProperty ActionProperty = DependencyProperty.Register(nameof(Action),
            typeof(CaptionButtonAction), typeof(CaptionButton), new FrameworkPropertyMetadata(CaptionButtonAction.None));

        public CaptionButtonAction Action
        {
            get => (CaptionButtonAction)GetValue(ActionProperty);
            set => SetValue(ActionProperty, value);
        }
        public CaptionButton() { }

        protected override void OnClick()
        {
            Window window = null;
            DependencyObject element = this;
            while (VisualTreeHelper.GetParent(element) != null)
            {
                element = VisualTreeHelper.GetParent(element);
                if (element is Window wnd)
                {
                    window = wnd;
                    break;
                }
            }

            if (window != null)
            {
                switch (Action)
                {
                    case CaptionButtonAction.Close:
                        window.Close();
                        return;
                        
                    case CaptionButtonAction.MaximizeRestore:
                        if (window.WindowState == WindowState.Maximized)
                        {
                            window.WindowState = WindowState.Normal;
                        }
                        else
                        {
                            window.WindowState = WindowState.Maximized;
                        }
                        return;
                        
                    case CaptionButtonAction.Minimize:
                        window.WindowState = WindowState.Minimized;
                        return;
                }
            }

            base.OnClick();
        }

        static CaptionButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CaptionButton), new FrameworkPropertyMetadata(typeof(CaptionButton)));
        }
    }

    public enum CaptionButtonAction
    {
        None,
        Close,
        MaximizeRestore,
        Minimize
    }
}
