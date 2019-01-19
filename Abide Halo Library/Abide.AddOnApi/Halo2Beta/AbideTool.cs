using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2BetaMap;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using YeloDebug;

namespace Abide.AddOnApi.Halo2Beta
{
    /// <summary>
    /// Provides an empty Halo 2 <see cref="AbideTool"/> control that implements the <see cref="ITool{TMap, TEntry, TXbox}"/> interface.
    /// </summary>
    public class AbideTool : UserControl, ITool<MapFile, IndexEntry, Xbox>
    {
        /// <summary>
        /// Occurs when the AddOn instance is initialized.
        /// </summary>
        [Category("Abide"), Description("Occurs when the AddOn instance is initialized.")]
        public event EventHandler<AddOnHostEventArgs> Initialize
        {
            add { initialize += value; }
            remove { initialize -= value; }
        }
        /// <summary>
        /// Occurs when the host instance loads or reloads a Halo Map instance.
        /// </summary>
        [Category("Abide"), Description("Occurs when the host instance loads or reloads a Halo Map instance.")]
        public event EventHandler MapLoad
        {
            add { mapLoad += value; }
            remove { mapLoad -= value; }
        }
        /// <summary>
        /// Occurs when the host instance changes is changes the selected Index Entry.
        /// </summary>
        [Category("Abide"), Description("Occurs when the host instance changes is changes the selected Index Entry.")]
        public event EventHandler SelectedEntryChanged
        {
            add { selectedEntryChanged += value; }
            remove { selectedEntryChanged -= value; }
        }
        /// <summary>
        /// Occurs when the host instance changes its Xbox connection.
        /// </summary>
        [Category("Abide"), Description("Occurs when the host instance changes its Xbox connection.")]
        public event EventHandler XboxChanged
        {
            add { xboxChanged += value; }
            remove { xboxChanged -= value; }
        }
        /// <summary>
        /// Gets or sets the description of the AddOn.
        /// </summary>
        [Category("Abide"), Description("The description of the AddOn.")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the author of the AddOn.
        /// </summary>
        [Category("Abide"), Description("The author of the AddOn.")]
        public string Author { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the display icon of the AddOn.
        /// </summary>
        [Category("Abide"), Description("The display icon of the AddOn.")]
        public Image Icon { get; set; } = null;
        /// <summary>
        /// Gets or sets the name of the AddOn.
        /// </summary>
        [Category("Abide"), Description("The name of the AddOn."), Browsable(true)]
        public string ToolName { get; set; } = string.Empty;
        /// <summary>
        /// Gets and returns the current Halo Map.
        /// This value can be null.
        /// </summary>
        [Browsable(false)]
        public MapFile Map
        {
            get { return (MapFile)Host?.Request(this, "Map"); }
        }
        /// <summary>
        /// Gets and returns the currently selected Index Entry.
        /// This value can be null.
        /// </summary>
        [Browsable(false)]
        public IndexEntry SelectedEntry
        {
            get { return (IndexEntry)Host?.Request(this, "SelectedEntry"); }
        }
        /// <summary>
        /// Gets and returns the current Debug Xbox.
        /// This value can be null.
        /// </summary>
        [Browsable(false)]
        public Xbox Xbox
        {
            get { return (Xbox)Host?.Request(this, "Xbox"); }
        }
        /// <summary>
        /// Gets and returns the AddOn host.
        /// </summary>
        [Browsable(false)]
        public IHost Host { get; private set; }

        private event EventHandler mapLoad;
        private event EventHandler selectedEntryChanged;
        private event EventHandler xboxChanged;
        private event EventHandler<AddOnHostEventArgs> initialize;

        /// <summary>
        /// Initializes a new <see cref="AbideTool"/> instance.
        /// </summary>
        public AbideTool()
        {
            ToolName = Name;
            base.Dock = DockStyle.Fill;
        }
        /// <summary>
        /// Raises the <see cref="Initialize"/> event.
        /// </summary>
        /// <param name="e">An <see cref="AddOnHostEventArgs"/> that contains the event data.</param>
        protected virtual void OnIntialize(AddOnHostEventArgs e)
        {
            //Invoke?
            initialize?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the <see cref="MapLoad"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnMapLoad(EventArgs e)
        {
            //Invoke
            mapLoad?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the <see cref="XboxChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnXboxChanged(EventArgs e)
        {
            //Invoke
            xboxChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the <see cref="SelectedEntryChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnSelectedEntryChanged(EventArgs e)
        {
            //Invoke
            selectedEntryChanged?.Invoke(this, e);
        }

        string IAddOn.Author
        {
            get { return Author; }
        }
        string IAddOn.Description
        {
            get { return Description; }
        }
        Control ITool<MapFile, IndexEntry, Xbox>.UserInterface
        {
            get { return this; }
        }
        Image ITool<MapFile, IndexEntry, Xbox>.Icon
        {
            get { return Icon; }
        }
        MapFile IHaloAddOn<MapFile, IndexEntry>.Map
        {
            get { return Map; }
        }
        string IAddOn.Name
        {
            get { return ToolName; }
        }
        IndexEntry IHaloAddOn<MapFile, IndexEntry>.SelectedEntry
        {
            get { return SelectedEntry; }
        }
        MapVersion IHaloAddOn<MapFile, IndexEntry>.Version
        {
            get { return MapVersion.Halo2b; }
        }
        Xbox IDebugXboxAddOn<Xbox>.Xbox
        {
            get { return Xbox; }
        }

        void IDebugXboxAddOn<Xbox>.DebugXboxChanged()
        {
            //Create Args
            var e = new EventArgs();

            //Xbox Changed
            OnXboxChanged(new EventArgs());
        }
        void IAddOn.Initialize(IHost host)
        {
            //Set
            Host = host;

            //Create Args
            var e = new AddOnHostEventArgs(host);

            //Initialize
            OnIntialize(e);
        }
        void IHaloAddOn<MapFile, IndexEntry>.OnMapLoad()
        {
            //Create Args
            var e = new EventArgs();

            //Map Load
            OnMapLoad(e);
        }
        void IHaloAddOn<MapFile, IndexEntry>.OnSelectedEntryChanged()
        {
            //Create Args
            var e = new EventArgs();

            //Selected Entry Changed
            OnSelectedEntryChanged(e);
        }
        void IDisposable.Dispose()
        {
            //Dispose
            Dispose();
        }
    }
}
