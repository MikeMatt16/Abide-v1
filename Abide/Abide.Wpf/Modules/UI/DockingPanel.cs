using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.UI
{
    public class DockingPanel : HeaderedContentControl
    {
        public DockingPanel()
        {
        }

        static DockingPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DockingPanel), new FrameworkPropertyMetadata(typeof(DockingPanel)));
        }
    }
}
