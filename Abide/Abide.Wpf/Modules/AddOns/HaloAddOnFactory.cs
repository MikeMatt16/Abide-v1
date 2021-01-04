using Abide.AddOnApi;
using Abide.AddOnApi.Wpf.Halo2;
using System.Collections.ObjectModel;

namespace Abide.Wpf.Modules.AddOns
{
    public sealed class HaloAddOnFactory : AddOnFactory
    {
        public ObservableCollection<IHaloAddOn> HaloAddOns { get; } = new ObservableCollection<IHaloAddOn>();
        public ObservableCollection<ITool> ToolAddOns { get; } = new ObservableCollection<ITool>();
        public ObservableCollection<IToolButton> ToolButtonAddOns { get; } = new ObservableCollection<IToolButton>();

        public HaloAddOnFactory(IHost host) : base(host, typeof(ITool), typeof(IToolButton)) { }
        protected override void LoadAddOn(IAddOn addOn)
        {
            switch (addOn)
            {
                case ITool tool:
                    ToolAddOns.Add(tool);
                    HaloAddOns.Add(tool);
                    break;

                case IToolButton toolButton:
                    ToolButtonAddOns.Add(toolButton);
                    HaloAddOns.Add(toolButton);
                    break;
            }
        }
    }
}
