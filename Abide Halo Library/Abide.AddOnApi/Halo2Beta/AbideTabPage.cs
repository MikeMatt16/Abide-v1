using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2BetaMap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using YeloDebug;

namespace Abide.AddOnApi.Halo2Beta
{
    /// <summary>
    /// Provides an empty Halo 2 <see cref="AbideTabPage"/> AddOn control which implements the <see cref="ITabPage{MapFile, IndexEntry, Xbox}"/> interface.
    /// </summary>
    public class AbideTabPage : UserControl, ITabPage<MapFile, IndexEntry, Xbox>
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
        /// Gets or sets the tag filter of the AddOn.
        /// </summary>
        [Category("Abide"), Description("The tag filter of the AddOn."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<Tag> TagFilter
        {
            get { return tagFilter; }
            set { tagFilter = value; }
        }
        /// <summary>
        /// Gets or sets whether to use the AddOn's tag filter.
        /// </summary>
        [Category("Abide"), Description("Sets the usabilty of the AddOn's tag filter.")]
        public bool ApplyTagFilter
        {
            get { return applyTagFilter; }
            set { applyTagFilter = value; }
        }
        /// <summary>
        /// Gets or sets the AddOn's tab page text.
        /// </summary>
        [Category("Abide"), Description("The name of the AddOn."), Browsable(true)]
        public string TabPageText
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// Gets and returns the AddOn host.
        /// </summary>
        [Browsable(false)]
        public IHost Host
        {
            get { return host; }
        }
        /// <summary>
        /// Gets and returns the current Halo Map.
        /// This value can be null.
        /// </summary>
        [Browsable(false)]
        public MapFile Map
        {
            get { return (MapFile)host?.Request(this, "Map"); }
        }
        /// <summary>
        /// Gets and returns the currently selected Index Entry.
        /// This value can be null.
        /// </summary>
        [Browsable(false)]
        public IndexEntry SelectedEntry
        {
            get { return (IndexEntry)host?.Request(this, "SelectedEntry"); }
        }
        /// <summary>
        /// Gets and returns the current Debug Xbox.
        /// This value can be null.
        /// </summary>
        [Browsable(false)]
        public Xbox Xbox
        {
            get { return (Xbox)host?.Request(this, "Xbox"); }
        }

        private event EventHandler mapLoad;
        private event EventHandler selectedEntryChanged;
        private event EventHandler xboxChanged;
        private event EventHandler<AddOnHostEventArgs> initialize;
        private List<Tag> tagFilter = new List<Tag>();
        private bool applyTagFilter = false;
        private string name = string.Empty;
        private string description = string.Empty;
        private string author = string.Empty;
        private IHost host;

        /// <summary>
        /// Initializes a new <see cref="AbideTabPage"/> instance.
        /// </summary>
        public AbideTabPage()
        {
            name = Name;
            base.Dock = DockStyle.Fill;
        }
        /// <summary>
        /// Raises the <see cref="Initialize"/> event.
        /// </summary>
        /// <param name="e">An <see cref="AddOnHostEventArgs"/> that contains the event data.</param>
        protected virtual void OnIntialize(AddOnHostEventArgs e)
        {
            //Invoke
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
            get { return author; }
        }
        string IAddOn.Description
        {
            get { return description; }
        }
        Control ITabPage<MapFile, IndexEntry, Xbox>.UserInterface
        {
            get { return this; }
        }
        Tag[] ITagFilter.Filter
        {
            get { return tagFilter.ToArray(); }
        }
        bool ITagFilter.ApplyFilter
        {
            get { return applyTagFilter; }
        }
        MapFile IHaloAddOn<MapFile, IndexEntry>.Map
        {
            get { return Map; }
        }
        string IAddOn.Name
        {
            get { return name; }
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
            this.host = host;

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
