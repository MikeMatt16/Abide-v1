using System;
using System.Globalization;
using System.Windows.Data;

namespace Abide.Guerilla.Wpf
{
    public class FileNameToShortStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Get character count
            int charCount = 50;
            if (parameter is int) charCount = (int)parameter;
            if (parameter is string stringLength) int.TryParse(stringLength, out charCount);

            //Check
            if(targetType == typeof(string) && value is string fileName)
            {
                //Check
                if (fileName.Length < charCount) return fileName;

                //Convert
                int i = fileName.LastIndexOf('\\');
                if (i < 0) return fileName;
                string tokenRight = fileName.Substring(i);
                string tokenCenter = @"...";
                int length = charCount - (tokenRight.Length + tokenCenter.Length);
                if (length < 0) return tokenRight;

                string tokenLeft = fileName.Substring(0, length);
                return string.Concat(tokenLeft, tokenCenter, tokenRight); ;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException();
        }
    }
}
