using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using YeloDebug;

namespace Abide.AddOnApi.Wpf.Halo2
{
    public class ToolControl : UserControl, ITool
    {
        /// <summary>
        /// Gets and returns the AddOn host.
        /// </summary>
        [Browsable(false)]
        public IHost Host { get; private set; }
        /// <summary>
        /// Gets or sets the name of the AddOn.
        /// </summary>
        [Category("Abide AddOn")]
        public string AddOnName { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the description of the AddOn.
        /// </summary>
        [Category("Abide AddOn")]
        public string AddOnDescription { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the author of the AddOn.
        /// </summary>
        [Category("Abide AddOn")]
        public string AddOnAuthor { get; set; } = string.Empty;
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolControl"/> class.
        /// </summary>
        public ToolControl() { }
        FrameworkElement IElementSupport.Element { get { return this; } }
        MapVersion IHaloAddOn<MapFile, IndexEntry>.Version { get { return MapVersion.Halo2; } }
        IndexEntry IHaloAddOn<MapFile, IndexEntry>.SelectedEntry { get { return (IndexEntry)Host.Request(this, "GetSelectedEntry"); } }
        MapFile IHaloAddOn<MapFile, IndexEntry>.Map { get { return (MapFile)Host.Request(this, "GetMap"); } }
        Xbox IDebugXboxAddOn<Xbox>.Xbox { get { return (Xbox)Host.Request(this, "GetXbox"); } }
        string IAddOn.Name { get { return AddOnName; } }
        string IAddOn.Description { get { return AddOnDescription; } }
        string IAddOn.Author { get { return AddOnAuthor; } }

        void IAddOn.Initialize(IHost host)
        {
            //Set host
            Host = host;

            //TODO: Notify change
        }
        void IHaloAddOn<MapFile, IndexEntry>.OnMapLoad()
        {
            //TODO: Notify change
        }
        void IHaloAddOn<MapFile, IndexEntry>.OnSelectedEntryChanged()
        {
            //TODO: Notify change
        }
        void IDebugXboxAddOn<Xbox>.DebugXboxChanged()
        {
            //TODO: Notify change
        }
        void IDisposable.Dispose()
        {
            //Do nothing.
        }
    }
}
