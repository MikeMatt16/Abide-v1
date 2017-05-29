using System.Windows.Forms;

namespace Abide.AddOnApi
{
    /// <summary>
    /// Defines a generalized settings page AddOn that a class implements to extend the usage of a host application. 
    /// </summary>
    public interface ISettingsPage : IAddOn
    {
        /// <summary>
        /// When implemented, gets and returns the User Interface control for this settings page.
        /// </summary>
        UserControl UserInterface { get; }
    }
}
