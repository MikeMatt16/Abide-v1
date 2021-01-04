using Abide.AddOnApi;
using Abide.AddOnApi.Wpf.Halo2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Abide.Wpf.AddOnDemo
{
    /// <summary>
    /// Interaction logic for TestHaloTool.xaml
    /// </summary>
    [AddOn]
    public partial class TestHaloTool : ToolControl
    {
        public TestHaloTool()
        {
            InitializeComponent();
        }
    }
}
