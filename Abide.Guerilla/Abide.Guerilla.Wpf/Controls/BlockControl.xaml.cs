using Abide.Guerilla.Wpf.ViewModel;
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

namespace Abide.Guerilla.Wpf.Controls
{
    /// <summary>
    /// Interaction logic for BlockControl.xaml
    /// </summary>
    public partial class BlockControl : UserControl
    {
        public BlockControl()
        {
            InitializeComponent();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Unsubscribe
            if (e.OldValue is BlockFieldModel)
                ((BlockFieldModel)e.OldValue).TagFieldChanged -= BlockControl_TagFieldChanged;

            //Subscribe to new changes
            if(e.NewValue is BlockFieldModel)
                ((BlockFieldModel)e.NewValue).TagFieldChanged += BlockControl_TagFieldChanged;
        }

        private void BlockControl_TagFieldChanged(object sender, EventArgs e)
        {
        }
    }
}
