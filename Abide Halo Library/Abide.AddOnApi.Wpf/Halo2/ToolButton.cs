using System.Windows;
using System.Windows.Input;

namespace Abide.AddOnApi.Wpf.Halo2
{
    public abstract class ToolButton : HaloAddOnControl, IToolButton
    {
        private static readonly DependencyPropertyKey ClickCommandPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ClickCommand), typeof(ICommand), typeof(ToolButton), new PropertyMetadata());

        public static DependencyProperty ClickCommandProperty = ClickCommandPropertyKey.DependencyProperty;

        public ICommand ClickCommand
        {
            get => (ICommand)GetValue(ClickCommandProperty);
            protected set => SetValue(ClickCommandPropertyKey, value);
        }
    }
}
