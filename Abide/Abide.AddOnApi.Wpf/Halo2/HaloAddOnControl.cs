using Abide.DebugXbox;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2.Retail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Abide.AddOnApi.Wpf.Halo2
{
    public abstract class HaloAddOnControl : UserControl, IHaloAddOn
    {
        private static readonly DependencyPropertyKey HostPropertyKey =
            DependencyProperty.RegisterReadOnly("Host", typeof(IHost), typeof(HaloAddOnControl), new PropertyMetadata(HostPropertyChanged));
        private static readonly DependencyPropertyKey SelectedEntryPropertyKey =
            DependencyProperty.RegisterReadOnly("SelectedEntry", typeof(IndexEntry), typeof(HaloAddOnControl), new PropertyMetadata(SelectedEntryPropertyChanged));
        private static readonly DependencyPropertyKey MapPropertyKey =
            DependencyProperty.RegisterReadOnly("Map", typeof(HaloMap), typeof(HaloAddOnControl), new PropertyMetadata(MapPropertyChanged));
        private static readonly DependencyPropertyKey XboxPropertyKey =
            DependencyProperty.RegisterReadOnly("Xbox", typeof(Xbox), typeof(HaloAddOnControl), new PropertyMetadata(XboxPropertyChanged));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty HostProperty =
            HostPropertyKey.DependencyProperty;
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty SelectedEntryProperty =
            SelectedEntryPropertyKey.DependencyProperty;
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty MapProperty =
            MapPropertyKey.DependencyProperty;
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty XboxProperty =
            XboxPropertyKey.DependencyProperty;
        /// <summary>
        /// 
        /// </summary>
        public static readonly RoutedEvent MapLoadEvent =
            EventManager.RegisterRoutedEvent("MapLoad", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(HaloAddOnControl));
        /// <summary>
        /// 
        /// </summary>
        public static readonly RoutedEvent SelectedEntryChangedEvent =
            EventManager.RegisterRoutedEvent("SelectedEntryChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(HaloAddOnControl));
        /// <summary>
        /// 
        /// </summary>
        public static readonly RoutedEvent XboxConnectionStateChangedEvent =
            EventManager.RegisterRoutedEvent("XboxConnectionStateChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(HaloAddOnControl));

        /// <summary>
        /// Occurs when a Halo 2 map is loaded.
        /// </summary>
        [Category("Abide AddOn")]
        public event RoutedEventHandler MapLoad
        {
            add { AddHandler(MapLoadEvent, value); }
            remove { RemoveHandler(MapLoadEvent, value); }
        }
        /// <summary>
        /// Occurs when the selected index entry is changed.
        /// </summary>
        [Category("Abide AddOn")]
        public event RoutedEventHandler SelectedEntryChanged
        {
            add { AddHandler(SelectedEntryChangedEvent, value); }
            remove { RemoveHandler(SelectedEntryChangedEvent, value); }
        }
        /// <summary>
        /// Occurs when the connection state of the debug Xbox is changed.
        /// </summary>
        [Category("Abide AddOn")]
        public event RoutedEventHandler XboxConnectionStateChanged
        {
            add { AddHandler(XboxConnectionStateChangedEvent, value); }
            remove { RemoveHandler(XboxConnectionStateChangedEvent, value); }
        }
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
        /// Gets and returns the host of this AddOn.
        /// </summary>
        [Browsable(false)]
        public IHost Host
        {
            get { return (IHost)GetValue(HostProperty); }
            private set { SetValue(HostPropertyKey, value); }
        }
        public IndexEntry SelectedEntry
        {
            get { return (IndexEntry)GetValue(SelectedEntryProperty); }
            private set { SetValue(SelectedEntryPropertyKey, value); }
        }
        public HaloMap Map
        {
            get { return (HaloMap)GetValue(MapProperty); }
            private set { SetValue(MapPropertyKey, value); }
        }
        public Xbox Xbox
        {
            get { return (Xbox)GetValue(XboxProperty); }
            private set { }
        }

        public HaloAddOnControl() { }
        ~HaloAddOnControl()
        {
            Dispose(false);
        }
        protected virtual void OnInitialize() { }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing) { }

        IndexEntry IHaloAddOn<HaloMap, IndexEntry>.SelectedEntry { get { return SelectedEntry; } }
        MapVersion IHaloAddOn<HaloMap, IndexEntry>.Version { get { return MapVersion.Halo2; } }
        HaloMap IHaloAddOn<HaloMap, IndexEntry>.Map { get { return Map; } }
        Xbox IDebugXboxAddOn<Xbox>.Xbox { get { return Xbox; } }
        string IAddOn.Name { get { return AddOnName; } }
        string IAddOn.Author { get { return AddOnAuthor; } }
        string IAddOn.Description { get { return AddOnDescription; } }
        void IHaloAddOn<HaloMap, IndexEntry>.OnMapLoad()
        {
            Map = Host?.Request(this, "GetMap") as HaloMap ?? null;
        }
        void IHaloAddOn<HaloMap, IndexEntry>.OnSelectedEntryChanged()
        {
            SelectedEntry = Host?.Request(this, "SelectedEntry") as IndexEntry ?? null;
        }
        void IDebugXboxAddOn<Xbox>.DebugXboxChanged()
        {
            Xbox = Host?.Request(this, "DebugXbox") as Xbox ?? null;
        }
        void IAddOn.Initialize(IHost host)
        {
            Host = host;
        }

        private static void HostPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is HaloAddOnControl control)
            {
                control.OnInitialize();
            }
        }
        private static void SelectedEntryPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is HaloAddOnControl control)
            {
                control.RaiseEvent(new RoutedEventArgs(SelectedEntryChangedEvent));
            }
        }
        private static void MapPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is HaloAddOnControl control)
            {
                control.RaiseEvent(new RoutedEventArgs(MapLoadEvent));
            }
        }
        private static void XboxPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is HaloAddOnControl control)
            {
                //TODO: Add handler for Xbox connection state change
            }
        }
    }
}
