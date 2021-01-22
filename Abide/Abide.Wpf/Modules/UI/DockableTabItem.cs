using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Wpf.Modules.UI
{
    public class DockableTabItem : TabItem
    {
        public DockableTabItem()
        {

        }

        static DockableTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DockableTabItem), new FrameworkPropertyMetadata(typeof(DockableTabItem)));
        }
    }
}
