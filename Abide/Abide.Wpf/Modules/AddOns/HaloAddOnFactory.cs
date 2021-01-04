using Abide.AddOnApi;
using Abide.AddOnApi.Wpf.Halo2;
using System;

namespace Abide.Wpf.Modules.AddOns
{
    /// <summary>
    /// Represents an object that can create instances of <see cref="IHaloAddOn"/> AddOn types.
    /// </summary>
    public sealed class HaloAddOnFactory : AddOnFactory
    {
        /// <summary>
        /// Gets and returns a list of Halo 2 AddOns.
        /// </summary>
        public AddOnCollection<IHaloAddOn> HaloAddOns { get; } = new AddOnCollection<IHaloAddOn>();
        /// <summary>
        /// Gets and returns a list of Halo 2 tool AddOns.
        /// </summary>
        public AddOnCollection<ITool> ToolAddOns { get; } = new AddOnCollection<ITool>();
        /// <summary>
        /// Initializes a new instance of the <see cref="HaloAddOnFactory"/> class.
        /// </summary>
        public HaloAddOnFactory(IHost host) : base(host) { }
        protected override void LoadAddOn(Type type)
        {
            //Prepare
            ITool toolAddOn;

            //Check
            if (type.GetInterface(typeof(IHaloAddOn).FullName) != null) //Check if type implements IHaloAddOn
            {
                //Check for ITool implementation
                if (type.GetInterface(typeof(ITool).FullName) != null)
                {
                    //Instantiate
                    try { toolAddOn = Instantiate<ITool>(type, Host); }
                    catch { toolAddOn = null; }

                    //Add to list
                    if (toolAddOn != null) AddToolAddOn(toolAddOn);
                }
            }
        }
        private void AddToolAddOn(ITool tool)
        {
            if (tool == null) throw new ArgumentNullException(nameof(tool));
            HaloAddOns.Add(tool);
            ToolAddOns.Add(tool);
        }
    }
}
