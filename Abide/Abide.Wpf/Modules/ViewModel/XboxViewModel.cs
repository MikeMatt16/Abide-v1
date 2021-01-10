using Abide.DebugXbox;
using System.Windows;

namespace Abide.Wpf.Modules.ViewModel
{
    public sealed class XboxViewModel : BaseViewModel
    {
        public static readonly DependencyProperty XboxProperty =
            DependencyProperty.Register(nameof(Xbox), typeof(Xbox), typeof(XboxViewModel));
        public Xbox Xbox
        {
            get => (Xbox)GetValue(XboxProperty);
            set => SetValue(XboxProperty, value);
        }

        private readonly string displayString;

        public XboxViewModel() { }
        public XboxViewModel(Xbox xbox)
        {
            Xbox = xbox;
        }
        public XboxViewModel(string displayString)
        {
            this.displayString = displayString;
        }
        public override string ToString()
        {
            if (Xbox != null && !string.IsNullOrEmpty(Xbox.Name))
            {
                return Xbox.Name;
            }

            if(!string.IsNullOrEmpty(displayString))
            {
                return displayString;
            }

            return string.Empty;
        }
    }
}
