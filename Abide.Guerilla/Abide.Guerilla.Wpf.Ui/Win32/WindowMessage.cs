using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abide.Guerilla.Wpf.Ui.Win32
{
    /// <summary>
    /// Represents a window message value.
    /// </summary>
    public struct WindowMessage : IEquatable<WindowMessage>
    {
        private uint hWnd, msg, wParam, lParam;

        /// <summary>
        /// Gets or sets the result of the window message.
        /// </summary>
        public IntPtr Result { get; set; }
        /// <summary>
        /// Gets and returns the handle of the window that sent this message.
        /// </summary>
        public IntPtr HWnd
        {
            get { return new IntPtr(unchecked((int)hWnd)); }
        }
        /// <summary>
        /// Gets and returns the window message.
        /// </summary>
        public int Msg
        {
            get { return unchecked((int)msg); }
        }
        /// <summary>
        /// Gets and returns additional message-specific information.
        /// </summary>
        public IntPtr WParam
        {
            get { return new IntPtr(unchecked((int)wParam)); }
        }
        /// <summary>
        /// Gets and returns additional message-specific information.
        /// </summary>
        public IntPtr LParam
        {
            get { return new IntPtr(unchecked((int)lParam)); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                hash *= 17 * hWnd.GetHashCode();
                hash *= 31 * msg.GetHashCode();
                hash *= 47 * wParam.GetHashCode();
                hash *= 11 * lParam.GetHashCode();
                return hash;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is WindowMessage message)
                return Equals(this, message);
            return base.Equals(obj);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(WindowMessage other)
        {
            return Equals(this, other);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg1"></param>
        /// <param name="msg2"></param>
        /// <returns></returns>
        private static bool Equals(WindowMessage msg1, WindowMessage msg2)
        {
            return false;
        }
        /// <summary>
        /// Creates and returns a new instance of the <see cref="WindowMessage"/> structure based on the provided values.
        /// </summary>
        /// <param name="hWnd">The handle to the window procedure to receive the message.</param>
        /// <param name="msg">The message.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns></returns>
        public static WindowMessage CreateMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            return new WindowMessage()
            {
                msg = msg,
                hWnd = unchecked((uint)hWnd.ToInt32()),
                wParam = unchecked((uint)wParam.ToInt32()),
                lParam = unchecked((uint)lParam.ToInt32()),
            };
        }
    }
}
