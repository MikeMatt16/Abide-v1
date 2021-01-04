using System;

namespace Donek.Core.JSON
{
    /// <summary>
    /// 
    /// </summary>
    public static class JsonFormatter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Format(string text)
        {
            return $"\"{text}\"";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string Format(byte number)
        {
            return number.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string Format(sbyte number)
        {
            return number.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string Format(short number)
        {
            return number.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string Format(ushort number)
        {
            return number.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string Format(int number)
        {
            return number.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string Format(uint number)
        {
            return number.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string Format(long number)
        {
            return number.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string Format(ulong number)
        {
            return number.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string Format(float number)
        {
            return number.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string Format(double number)
        {
            return number.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string Format(decimal number)
        {
            return number.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="boolean"></param>
        /// <returns></returns>
        public static string Format(bool boolean)
        {
            return boolean.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string Format(DateTime dateTime)
        {
            return dateTime.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public static string Format(JsonObject @object)
        {
            if (@object == null) return string.Empty;
            return @object.Stringify();
        }
    }
}
