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

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is TagFieldViewModel field)
            {
                switch (field.Type)
                {
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldBlock:
                        return BlockTemplate;

                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldString:
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldLongString:
                        return StringTemplate;

                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldStringId:
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldOldStringId:
                        return StringIdTemplate;

                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldStruct:
                        return TagBlockTemplate;

                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldShortInteger:
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldCharInteger:
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldLongInteger:
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldAngle:
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldReal:
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldTag:
                        return ValueTemplate;

                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldCharEnum:
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldEnum:
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldLongEnum:
                        return EnumTemplate;

                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldLongFlags:
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldWordFlags:
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldByteFlags:
                        return FlagsTemplate;

                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldTagReference:
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldTagIndex:
                        return TagRefTemplate;

                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldPoint2D:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldRectangle2D:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldRgbColor:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldArgbColor:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldRealFraction:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldRealPoint2D:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldRealPoint3D:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldRealVector2D:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldRealVector3D:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldQuaternion:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldEulerAngles2D:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldEulerAngles3D:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldRealPlane2D:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldRealPlane3D:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldRealRgbColor:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldRealArgbColor:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldRealHsvColor:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldRealAhsvColor:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldShortBounds:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldAngleBounds:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldRealBounds:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldRealFractionBounds:
                        break;
                    
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldLongBlockFlags:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldWordBlockFlags:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldByteBlockFlags:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldCharBlockIndex1:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldCharBlockIndex2:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldShortBlockIndex1:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldShortBlockIndex2:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldLongBlockIndex1:
                        break;
                    case HaloLibrary.Halo2.Retail.Tag.FieldType.FieldLongBlockIndex2:
                        break;
                }
            }

            return null;
        }
    }
}
