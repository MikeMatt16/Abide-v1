using Abide.DebugXbox;
using Abide.HaloLibrary.Halo2.Retail;

namespace Abide.AddOnApi.Wpf.Halo2
{
    /// <summary>
    /// Defines a generalized Halo 2 retail AddOn..
    /// </summary>
    public interface IHaloAddOn : IAddOn, IHaloAddOn<HaloMapFile, HaloTag>, IDebugXboxAddOn<Xbox>
    {
    }
}
