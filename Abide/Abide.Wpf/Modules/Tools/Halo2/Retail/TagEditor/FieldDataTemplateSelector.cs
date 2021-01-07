using Abide.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.TagEditor
{
    public sealed class FieldDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TagBlockTemplate { get; set; } = null;
        public DataTemplate StringIdTemplate { get; set; } = null;
        public DataTemplate TagRefTemplate { get; set; } = null;
        public DataTemplate StringTemplate { get; set; } = null;
        public DataTemplate ValueTemplate { get; set; } = null;
        public DataTemplate EnumTemplate { get; set; } = null;
        public DataTemplate FlagsTemplate { get; set; } = null;
        public DataTemplate BlockTemplate { get; set; } = null;
        public DataTemplate Point2dTemplate { get; set; } = null;
        public DataTemplate UnknownTemplate { get; set; } = null;

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is TagFieldViewModel field)
            {
                switch (field.Type)
                {
                    case FieldType.FieldBlock:
                        return BlockTemplate;

                    case FieldType.FieldString:
                    case FieldType.FieldLongString:
                        return StringTemplate;

                    case FieldType.FieldStringId:
                    case FieldType.FieldOldStringId:
                        return StringIdTemplate;

                    case FieldType.FieldShortInteger:
                    case FieldType.FieldCharInteger:
                    case FieldType.FieldLongInteger:
                    case FieldType.FieldAngle:
                    case FieldType.FieldReal:
                    case FieldType.FieldTag:
                    case FieldType.FieldRealFraction:
                        return ValueTemplate;

                    case FieldType.FieldCharBlockIndex1:
                    case FieldType.FieldCharBlockIndex2:
                    case FieldType.FieldShortBlockIndex1:
                    case FieldType.FieldShortBlockIndex2:
                    case FieldType.FieldLongBlockIndex1:
                    case FieldType.FieldLongBlockIndex2:
                        return ValueTemplate;

                    case FieldType.FieldCharEnum:
                    case FieldType.FieldEnum:
                    case FieldType.FieldLongEnum:
                        return EnumTemplate;

                    case FieldType.FieldLongFlags:
                    case FieldType.FieldWordFlags:
                    case FieldType.FieldByteFlags:
                        return FlagsTemplate;

                    case FieldType.FieldLongBlockFlags:
                    case FieldType.FieldWordBlockFlags:
                    case FieldType.FieldByteBlockFlags:
                        return FlagsTemplate;

                    case FieldType.FieldTagReference:
                    case FieldType.FieldTagIndex:
                        return TagRefTemplate;

                    case FieldType.FieldPoint2D:
                        return Point2dTemplate;

                    case FieldType.FieldRectangle2D:
                    case FieldType.FieldRgbColor:
                    case FieldType.FieldArgbColor:
                    case FieldType.FieldRealPoint2D:
                    case FieldType.FieldRealPoint3D:
                    case FieldType.FieldRealVector2D:
                    case FieldType.FieldRealVector3D:
                    case FieldType.FieldQuaternion:
                    case FieldType.FieldEulerAngles2D:
                    case FieldType.FieldEulerAngles3D:
                    case FieldType.FieldRealPlane2D:
                    case FieldType.FieldRealPlane3D:
                    case FieldType.FieldRealRgbColor:
                    case FieldType.FieldRealArgbColor:
                    case FieldType.FieldRealHsvColor:
                    case FieldType.FieldRealAhsvColor:
                    case FieldType.FieldShortBounds:
                    case FieldType.FieldAngleBounds:
                    case FieldType.FieldRealBounds:
                    case FieldType.FieldRealFractionBounds:
                        return UnknownTemplate;
                }
            }

            return null;
        }
    }
}
