using Abide.HaloLibrary.Halo2;
using Abide.Wpf.Modules.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.ModelEditor
{
    public sealed class ModelEditorViewModel : BaseAddOnViewModel
    {
        private HaloModel model;

        public HaloModel Model
        {
            get => model;
            set
            {
                if (model != value)
                {
                    model = value;
                    NotifyPropertyChanged();
                }
            }
        }

        protected override void OnSelectedTagChanged()
        {
            if (SelectedTag.GroupTag == HaloTags.hlmt)
            {
                Model = new HaloModel(SelectedTag);
                Model.Load();
                Model.Process();
            }
        }
    }
}
