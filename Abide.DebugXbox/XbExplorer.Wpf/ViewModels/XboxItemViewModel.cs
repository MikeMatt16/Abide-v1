using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XbExplorer.Wpf.ViewModels
{
    public class XboxItemViewModel : ListViewItemViewModel
    {
        private string xboxName = string.Empty;

        public string XboxName
        {
            get { return xboxName; }
            set
            {
                if (xboxName != value)
                {
                    xboxName = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
