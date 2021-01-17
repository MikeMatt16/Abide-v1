using System.Windows;

namespace Abide.AddOnApi.Wpf.Halo2
{
    /// <summary>
    /// Provides an empty Halo 2 tool user control.
    /// </summary>
    public class ToolControl : HaloAddOnControl, ITool
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolControl"/> class.
        /// </summary>
        public ToolControl() { }

        FrameworkElement IElementSupport.Element { get { return this; } }
    }
}
