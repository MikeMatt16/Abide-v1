using Abide.HaloLibrary.Halo2;
using Abide.Wpf.Modules.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.ModelViewer
{
    public sealed class ModelViewerViewModel : BaseAddOnViewModel
    {
        private HaloRenderModel model = null;
        private RenderModelContainer selectedContainer = null;

        public RenderModelContainer SelectedContainer
        {
            get => selectedContainer;
            set
            {
                if (selectedContainer != value)
                {
                    selectedContainer = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public HaloRenderModel RenderModel
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
            if (SelectedTag.GroupTag == HaloTags.mode)
            {
                RenderModel = new HaloRenderModel(SelectedTag);
                RenderModel.Load();
                RenderModel.Process();

                if (RenderModel.ModelContainers.Count > 0)
                    SelectedContainer = RenderModel.ModelContainers.First();
            }
        }
    }
}
