using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Abide.Wpf.Modules.UI
{
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(FloatPanel))]
    [TemplatePart(Name = "PART_Content", Type = typeof(ContentPresenter))]
    public class FloatPanelControl : ContentControl
    {
        public static readonly DependencyProperty PositionProperty = 
            DependencyProperty.RegisterAttached("Position", typeof(FloatPanelPosition), typeof(FloatPanelControl));

        protected override Size MeasureOverride(Size availableSize)
        {
            return availableSize;
        }

        static FloatPanelControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatPanelControl), new FrameworkPropertyMetadata(typeof(FloatPanelControl)));
        }
    }

    public class FloatPanel : HeaderedContentControl
    {

    }

    public enum FloatPanelPosition
    {
        Left,
        Right,
        Bottom
    }
}
