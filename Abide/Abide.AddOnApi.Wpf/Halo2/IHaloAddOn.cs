﻿using Abide.HaloLibrary.Halo2Map;
using YeloDebug;

namespace Abide.AddOnApi.Wpf.Halo2
{
    /// <summary>
    /// Defines a generalized Halo 2 retail AddOn..
    /// </summary>
    public interface IHaloAddOn : IAddOn, IHaloAddOn<MapFile, IndexEntry>, IDebugXboxAddOn<Xbox>
    {
    }
}
