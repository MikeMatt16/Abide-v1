using System;

namespace Abide.Classes
{
    /// <summary>
    /// Represents our store of global event handlers and global methods.
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// Occurs when the application's Debug Xbox state is changed.
        /// </summary>
        public static event EventHandler XboxChanged
        {
            add { xboxChanged += value; }
            remove { xboxChanged -= value; }
        }

        private static event EventHandler xboxChanged;

        /// <summary>
        /// Raises the <see cref="XboxChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> that contains the event data.</param>
        public static void OnXboxChanged(EventArgs e)
        {
            //Invoke?
            xboxChanged?.Invoke(null, e);
        }
    }
}
