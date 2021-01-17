using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2.Retail;
using Abide.HaloLibrary.Halo2.Retail.Tag;
using Abide.Tag;
using Abide.Wpf.Modules.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.TagEditor
{
    public sealed class MapValueConverter : BaseViewModel, IValueConverter
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(BaseAddOnViewModel), typeof(MapValueConverter));

        public BaseAddOnViewModel ViewModel
        {
            get => (BaseAddOnViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (ViewModel?.Map == null)
            {
                return null;
            }

            if (value is StringId stringId)
            {
                return ViewModel.Map.GetStringById(stringId);
            }
            else if (value is TagId tagId)
            {
                var tag = ViewModel.Map.GetTagById(tagId);
                if (tag != null)
                {
                    return $"{tag.TagName}.{tag.Tag}";
                }
                else
                {
                    return "null";
                }
            }
            else if (value is TagReference tagRef)
            {
                var tag = ViewModel.Map.GetTagById(tagRef.Id);
                if (tag != null)
                {
                    return $"{tag.TagName}.{tagRef.Tag}";
                }
                else
                {
                    return "null";
                }
            }

            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class BasicValueConverter : BaseViewModel, IMultiValueConverter
    {
        private FieldType type = FieldType.FieldString;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string str = string.Empty;
            if (values.Length >= 1 && values[0] != null)
            {
                str = values[0].ToString();
            }

            if (values.Length >= 2 && values[1] is FieldType type)
            {
                this.type = type;
            }

            return str;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            object[] objects = new object[2];
            objects[1] = type;

            string valueStr = string.Empty;
            if (value != null)
            {
                valueStr = value.ToString();
            }

            switch (type)
            {
                case FieldType.FieldString:
                case FieldType.FieldLongString:
                    objects[0] = valueStr; break;

                case FieldType.FieldCharInteger:
                    objects[0] = sbyte.Parse(valueStr); break;

                case FieldType.FieldShortInteger:
                    objects[0] = short.Parse(valueStr); break;

                case FieldType.FieldLongInteger:
                    objects[0] = int.Parse(valueStr); break;

                case FieldType.FieldAngle:
                case FieldType.FieldReal:
                case FieldType.FieldRealFraction:
                    objects[0] = float.Parse(valueStr); break;
            }

            return objects;
        }
    }

    public sealed class EnumValueConverter : BaseViewModel, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 2 && int.TryParse(values[0].ToString(), out int index) && values[1] is IList<TagOptionViewModel> options)
            {
                if (index < 0)
                {
                    return null;
                }
                else if (options.Count > index)
                {
                    return options[index];
                }
            }

            return null;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is TagOptionViewModel option)
            {
                return new object[] { option.Index };
            }

            return null;
        }
    }

    public enum SpecificValue
    {
        Point2dX,
        Point2dY,
    }
}
