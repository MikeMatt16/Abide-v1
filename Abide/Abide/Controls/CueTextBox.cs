using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Abide.Controls
{
    /// <summary>
    /// Represents a Windows text box control with a queue string.
    /// </summary>
    internal class CueTextBox : TextBox
    {
        /// <summary>
        /// Occurs when the cue string is changed.
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the cue string is changed.")]
        public event EventHandler CueChanged
        {
            add { cueChanged += value; }
            remove { cueChanged -= value; }
        }
        /// <summary>
        /// Gets or sets the cue string that appears if the text box is empty.
        /// </summary>
        [Category("Appearance"), Description("The cue string that appears if the text box is empty.")]
        public string Cue
        {
            get { return cue; }
            set
            {
                bool changed = value != cue;
                cue = value;
                if (changed) OnCueChanged(new EventArgs());
            }
        }
        
        private event EventHandler cueChanged;
        private Color cueColor = SystemColors.ButtonShadow;
        private string cue = "...";

        /// <summary>
        /// Initializes a new instance of the <see cref="CueTextBox"/> class.
        /// </summary>
        public CueTextBox() : base()
        {

        }

        /// <summary>
        /// Raises the <see cref="Control.HandleCreated"/> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> that contains event information.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            //Prepare
            IntPtr strAddr = IntPtr.Zero;

            //Base handle created procedures
            base.OnHandleCreated(e);

            //Check
            if (IsHandleCreated && !string.IsNullOrEmpty(cue))
            {
                //Copy string into unmanaged memory...
                strAddr = Marshal.StringToHGlobalUni(cue);

                //Send Message
                SendMessage(Handle, 0x1501, (IntPtr)1, strAddr);

                //Free unmanaged memory...
                Marshal.FreeHGlobal(strAddr);
            }
        }
        /// <summary>
        /// Raises the <see cref="CueChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="EventArgs"/> that contains event information.</param>
        protected virtual void OnCueChanged(EventArgs e)
        {
            //Prepare
            IntPtr strAddr = IntPtr.Zero;

            //Invoke
            cueChanged?.Invoke(this, e);
            
            //Check
            if (IsHandleCreated && !string.IsNullOrEmpty(cue))
            {
                //Copy string into unmanaged memory...
                strAddr = Marshal.StringToHGlobalUni(cue);

                //Send Message
                SendMessage(Handle, 0x1501, (IntPtr)1, strAddr);

                //Free unmanaged memory...
                Marshal.FreeHGlobal(strAddr);
            }

            //Invalidate
            Invalidate();
        }
        /// <summary>
        /// Draws the cue string to the text box.
        /// </summary>
        private void DrawCueString()
        {
            
        }

        [DllImport("user32.dll", EntryPoint = "SendMessageW")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
    }
}
