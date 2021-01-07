using Abide.HaloLibrary.Halo2;
using Abide.HaloLibrary.Halo2.Retail;
using Abide.Wpf.Modules.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.ModelEditor
{
    public sealed class HaloModel : BaseViewModel
    {
        private HaloTag tag;

        public HaloTag Tag
        {
            get => tag;
            private set
            {
                if (tag != value)
                {
                    tag = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public HaloModel(HaloTag tag)
        {
            Tag = tag ?? throw new ArgumentNullException(nameof(tag));

            if (tag.GroupTag != HaloTags.hlmt)
            {
                throw new ArgumentException("Specified tag is not a model.", nameof(tag));
            }
        }
        public void Load()
        {

        }
        public void Process()
        {

        }
    }
}
