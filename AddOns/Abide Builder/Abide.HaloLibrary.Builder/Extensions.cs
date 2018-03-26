using System;

namespace Abide.HaloLibrary.Builder
{
    /// <summary>
    /// Provides extension and helper functions for the System namespace.
    /// </summary>
    internal static class SystemExtensions
    {
        /// <summary>
        /// Reverses a given string.
        /// </summary>
        /// <param name="str">The string to reverse.</param>
        /// <returns>A reversed string.</returns>
        public static string Reverse(this string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
        /// <summary>
        /// Pads up a uint to match a multiple of a supplied length.
        /// The resulting value will always be greater than or equal to the supplied number.
        /// </summary>
        /// <param name="number">The number to pad.</param>
        /// <param name="length">The length to pad to.</param>
        /// <returns>A number that is a multiple of length, but is greater than or equal to number.</returns>
        public static uint PadTo(this uint number, uint length)
        {
            uint remainder = number % length;
            if (remainder == 0)
                return number;
            else return number + (length - remainder);
        }
        /// <summary>
        /// Pads up an int to match a multiple of a supplied length.
        /// The resulting value will always be greater than or equal to the supplied number.
        /// </summary>
        /// <param name="number">The number to pad.</param>
        /// <param name="length">The length to pad to.</param>
        /// <returns>A number that is a multiple of length, but is greater than or equal to number.</returns>
        public static int PadTo(this int number, int length)
        {
            int remainder = number % length;
            if (remainder == 0)
                return number;
            else return number + (length - remainder);
        }
        /// <summary>
        /// Pads up a ulong to match a multiple of a supplied length.
        /// The resulting value will always be greater than or equal to the supplied number.
        /// </summary>
        /// <param name="number">The number to pad.</param>
        /// <param name="length">The length to pad to.</param>
        /// <returns>A number that is a multiple of length, but is greater than or equal to number.</returns>
        public static ulong PadTo(this ulong number, ulong length)
        {
            ulong remainder = number % length;
            if (remainder == 0)
                return number;
            else return number + (length - remainder);
        }
        /// <summary>
        /// Pads up a long to match a multiple of a supplied length.
        /// The resulting value will always be greater than or equal to the supplied number.
        /// </summary>
        /// <param name="number">The number to pad.</param>
        /// <param name="length">The length to pad to.</param>
        /// <returns>A number that is a multiple of length, but is greater than or equal to number.</returns>
        public static long PadTo(this long number, long length)
        {
            long remainder = number % length;
            if (remainder == 0)
                return number;
            else return number + (length - remainder);
        }
    }
}
