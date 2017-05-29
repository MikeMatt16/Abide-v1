using Abide.HaloLibrary;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Abide.AddOnApi
{
    /// <summary>
    /// Provides an empty <see cref="Tool{TMap, TEntry, TXbox}"/> AddOn control which implements the <see cref="ITool{Map, Entry, Xbox}"/> interface.
    /// </summary>
    /// <typeparam name="TMap">The Halo Map type to be used by the interface.</typeparam>
    /// <typeparam name="TEntry">The Object Index Entry type to be used by the interface.</typeparam>
    /// <typeparam name="TXbox">The Debug Xbox type to be used by the interface.</typeparam>
    public class Tool<TMap, TEntry, TXbox> : UserControl, ITool<TMap, TEntry, TXbox>
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
        /// Gets or sets the Halo Map version the AddOn is compatible with.
        /// </summary>
        [Category("Abide"), Description("The Halo Map version the AddOn is compatible with.")]
        public MapVersion MapVersion
        {
            get { return mapVersion; }
            set { mapVersion = value; }
        }
        /// <summary>
        /// Gets or sets the description of the AddOn.
        /// </summary>
        [Category("Abide"), Description("The description of the AddOn.")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        /// <summary>
        /// Gets or sets the author of the AddOn.
        /// </summary>
        [Category("Abide"), Description("The author of the AddOn.")]
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        /// <summary>
        /// Gets or sets the display icon of the AddOn.
        /// </summary>
        [Category("Abide"), Description("The display icon of the AddOn.")]
        public Image Icon
        {
            get { return icon; }
            set { icon = value; }
        }
        /// <summary>
        /// Gets or sets the name of the AddOn.
        /// </summary>
        [Category("Abide"), Description("The name of the AddOn."), Browsable(true)]
        public string ToolName
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// Gets and returns the current Halo Map.
        /// This value can be null.
        /// </summary>
        [Browsable(false)]
        public TMap Map
        {
            get { return (TMap)host?.Request(this, "Map"); }
        }
        /// <summary>
        /// Gets and returns the currently selected Index Entry.
        /// This value can be null.
        /// </summary>
        [Browsable(false)]
        public TEntry SelectedEntry
        {
            get { return (TEntry)host?.Request(this, "SelectedEntry"); }
        }
        /// <summary>
        /// Gets and returns the current Debug Xbox.
        /// This value can be null.
        /// </summary>
        [Browsable(false)]
        public TXbox Xbox
        {
            get { return (TXbox)host?.Request(this, "Xbox"); }
        }
        /// <summary>
        /// Gets and returns the current AddOn host.
        /// </summary>
        [Browsable(false)]
        public IHost Host
        {
            get { return host; }
        }

        private event EventHandler mapLoad;
        private event EventHandler selectedEntryChanged;
        private event EventHandler xboxChanged;
        private event EventHandler<AddOnHostEventArgs> initialize;
        private MapVersion mapVersion = MapVersion.Halo2;
        private string name = string.Empty;
        private string description = string.Empty;
        private string author = string.Empty;
        private Image icon = null;
        private IHost host;

        /// <summary>
        /// Initializes a new <see cref="Tool"/> instance.
        /// </summary>
        public Tool()
        {
            name = Name;
        }
        /// <summary>
        /// Occurs when the AddOn instance is being initialized.
        /// </summary>
        /// <param name="host">The AddOn host event arguments.</param>
        protected virtual void OnIntialize(AddOnHostEventArgs e)
        {
            //Set Host
            host = e.Host;

            //Trigger
            initialize?.Invoke(this, e);
        }
        /// <summary>
        /// Occurs when the host instance loads or reloads its Halo Map instance.
        /// </summary>
        /// <param name="e">The Event arguments.</param>
        protected virtual void OnMapLoad(EventArgs e)
        {
            //Invoke
            mapLoad?.Invoke(this, e);
        }
        /// <summary>
        /// Occurs when the host instance changes its debug Xbox connection.
        /// </summary>
        /// <param name="e">The Event Arguments.</param>
        protected virtual void OnXboxChanged(EventArgs e)
        {
            //Invoke
            xboxChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Occurs when the host instance changes its selected Halo Index entry.
        /// </summary>
        /// <param name="e">The Event Arguments.</param>
        protected virtual void OnSelectedEntryChanged(EventArgs e)
        {
            //Invoke
            selectedEntryChanged?.Invoke(this, e);
        }

        string IAddOn.Author
        {
            get { return author; }
        }
        string IAddOn.Description
        {
            get { return description; }
        }
        Control ITool<TMap, TEntry, TXbox>.UserInterface
        {
            get { return this; }
        }
        Image ITool<TMap, TEntry, TXbox>.Icon
        {
            get { return icon; }
        }
        TMap IHaloAddOn<TMap, TEntry>.Map
        {
            get { return Map; }
        }
        string IAddOn.Name
        {
            get { return name; }
        }
        TEntry IHaloAddOn<TMap, TEntry>.SelectedEntry
        {
            get { return SelectedEntry; }
        }
        MapVersion IHaloAddOn<TMap, TEntry>.Version
        {
            get { return mapVersion; }
        }
        TXbox IDebugXboxAddOn<TXbox>.Xbox
        {
            get { return Xbox; }
        }

        void IDebugXboxAddOn<TXbox>.DebugXboxChanged()
        {
            //Create Args
            var e = new EventArgs();

            //Xbox Changed
            OnXboxChanged(new EventArgs());

            //Invoke
            xboxChanged?.Invoke(this, e);
        }
        void IAddOn.Initialize(IHost host)
        {
            //Create Args
            var e = new AddOnHostEventArgs(host);

            //Initialize
            OnIntialize(e);

            //Trigger
            initialize?.Invoke(this, e);
        }
        void IHaloAddOn<TMap, TEntry>.OnMapLoad()
        {
            //Create Args
            var e = new EventArgs();

            //Map Load
            OnMapLoad(e);

            //Invoke
            mapLoad?.Invoke(this, e);
        }
        void IHaloAddOn<TMap, TEntry>.OnSelectedEntryChanged()
        {
            //Create Args
            var e = new EventArgs();

            //Selected Entry Changed
            OnSelectedEntryChanged(e);

            //Invoke
            selectedEntryChanged?.Invoke(this, e);
        }
        void IDisposable.Dispose()
        {
            //Dispose
            Dispose();
        }
    }
}