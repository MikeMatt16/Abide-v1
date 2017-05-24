using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace YeloDebug
{
	/// <summary>
	/// Miscellaneous helper functions.
	/// </summary>
	public static class Util
	{
        /// <summary>
        /// Converts a timestamp to its local DateTime equivalent.
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime TimeStampToLocalDateTime(uint timestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0) + new TimeSpan(0, 0, (int)timestamp);
        }

        /// <summary>
        /// Converts a timestamp to its universal DateTime equivalent.
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime TimeStampToUniversalDateTime(uint timestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, (int)timestamp);
        }

        public static void DataToFile(ref byte[] data, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write)) fs.Write(data, 0, data.Length);
        }

		public static byte[] StringToHexBytes(string str)
		{
			byte[] Hex = new byte[str.Length / 2];

			for (int i = 0; i < str.Length / 2; i++)
				Hex[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);

			return Hex;
		}

        public static string HexBytesToString(byte[] hexBytes)
        {
            string hexStr = string.Empty;
            for (int i = 0; i < hexBytes.Length; i++)
                hexStr += Convert.ToString(hexBytes[i], 16).PadLeft(2, '0').ToUpper();
            return hexStr;
        }


        // methods to parse responses
        public static object GetResponseInfo(string response, int index)
        {
            char[] delimiters = { ' ', '\r' };
            string value = (response.Substring(response.IndexOf('=') + 1).Split('='))[index];
            string val = value.Remove(value.IndexOfAny(delimiters));

            if (val[0] == '\"') return (string)val.Substring(1, val.Length - 2);
            else if (val.Length > 2 && val.Remove(2) == "0x") return (uint)Convert.ToUInt32(val.Substring(2), 16);
            else return (uint)Convert.ToUInt32(val);
        }

        public static List<object> ExtractResponseInformation(string responseLine)
        {
            List<object> responses = new List<object>();
            char[] delimiters = {' ', '\r'};

            string[] values = responseLine.Substring(responseLine.IndexOf('=') + 1).Split('=');
            foreach (string value in values)
            {
                string val = value.Remove(value.IndexOfAny(delimiters));

                if (val[0] == '\"') responses.Add((string)val.Substring(1, val.Length - 2));
                else if (val.Length > 2 && val.Remove(2) == "0x") responses.Add((uint)Convert.ToUInt32(val.Substring(2), 16));
                else responses.Add((uint)Convert.ToUInt32(val));
            }
            return responses;
        }


        public static uint CelsiusToFahrenheit(uint degrees)
        {
            return (uint)(1.8f * degrees + 32);
        }

        public static unsafe uint FloatToUInt32(float value) { return *(uint*)&value; }

        public static byte[] ObjectToByteArray(object obj)
        {
            byte[] data = new byte[4];
            MemoryStream ms = new MemoryStream(data);
            BinaryWriter bw = new BinaryWriter(ms);

            switch (Type.GetTypeCode(obj.GetType()))
            {
                case TypeCode.Boolean:
                case TypeCode.Byte:
                case TypeCode.Char:
                case TypeCode.Int16:
                case TypeCode.Int32:
                default:
                    bw.Write(Convert.ToUInt32(obj));
                    break;
                case TypeCode.Single:
                    bw.Write(FloatToUInt32((float)obj));
                    break;

            }
            return data;
        }
	};
}