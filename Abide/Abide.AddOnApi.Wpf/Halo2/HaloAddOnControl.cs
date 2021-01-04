using Abide.DebugXbox;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2.Retail;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Abide.AddOnApi.Wpf.Halo2
{
    public abstract class HaloAddOnControl : UserControl, IHaloAddOn
    {
        private static readonly DependencyPropertyKey HostPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Host), typeof(IHost), typeof(HaloAddOnControl), new PropertyMetadata(HostPropertyChanged));
        private static readonly DependencyPropertyKey SelectedEntryPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(SelectedEntry), typeof(HaloTag), typeof(HaloAddOnControl), new PropertyMetadata(SelectedTagPropertyChanged));
        private static readonly DependencyPropertyKey MapPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Map), typeof(HaloMapFile), typeof(HaloAddOnControl), new PropertyMetadata(MapPropertyChanged));
        private static readonly DependencyPropertyKey XboxPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Xbox), typeof(Xbox), typeof(HaloAddOnControl), new PropertyMetadata(XboxPropertyChanged));

        public static readonly DependencyProperty HostProperty =
            HostPropertyKey.DependencyProperty;
        public static readonly DependencyProperty SelectedEntryProperty =
            SelectedEntryPropertyKey.DependencyProperty;
        public static readonly DependencyProperty MapProperty =
            MapPropertyKey.DependencyProperty;
        public static readonly DependencyProperty XboxProperty =
            XboxPropertyKey.DependencyProperty;
        public static readonly RoutedEvent MapLoadEvent =
            EventManager.RegisterRoutedEvent(nameof(MapLoad), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(HaloAddOnControl));
        public static readonly RoutedEvent SelectedTagChangedEvent =
            EventManager.RegisterRoutedEvent(nameof(SelectedEntryChanged), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(HaloAddOnControl));
        public static readonly RoutedEvent XboxConnectionStateChangedEvent =
            EventManager.RegisterRoutedEvent(nameof(XboxConnectionStateChanged), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(HaloAddOnControl));

        [Category("Abide AddOn")]
        public event RoutedEventHandler MapLoad
        {
            add { AddHandler(MapLoadEvent, value); }
            remove { RemoveHandler(MapLoadEvent, value); }
        }
        [Category("Abide AddOn")]
        public event RoutedEventHandler SelectedEntryChanged
        {
            add { AddHandler(SelectedTagChangedEvent, value); }
            remove { RemoveHandler(SelectedTagChangedEvent, value); }
        }
        [Category("Abide AddOn")]
        public event RoutedEventHandler XboxConnectionStateChanged
        {
            add { AddHandler(XboxConnectionStateChangedEvent, value); }
            remove { RemoveHandler(XboxConnectionStateChangedEvent, value); }
        }
        [Category("Abide AddOn")]
        public string AddOnName { get; set; } = string.Empty;
        [Category("Abide AddOn")]
        public string AddOnDescription { get; set; } = string.Empty;
        [Category("Abide AddOn")]
        public string AddOnAuthor { get; set; } = string.Empty;
        [Browsable(false)]
        public IHost Host
        {
            get => (IHost)GetValue(HostProperty);
            private set => SetValue(HostPropertyKey, value);
        }
        public HaloTag SelectedEntry
        {
            get => (HaloTag)GetValue(SelectedEntryProperty);
            private set => SetValue(SelectedEntryPropertyKey, value);
        }
        public HaloMapFile Map
        {
            get => (HaloMapFile)GetValue(MapProperty);
            private set => SetValue(MapPropertyKey, value);
        }
        public Xbox Xbox
        {
            get => (Xbox)GetValue(XboxProperty);
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

        HaloTag IHaloAddOn<HaloMapFile, HaloTag>.SelectedEntry { get { return SelectedEntry; } }
        MapVersion IHaloAddOn<HaloMapFile, HaloTag>.Version { get { return MapVersion.Halo2; } }
        HaloMapFile IHaloAddOn<HaloMapFile, HaloTag>.Map { get { return Map; } }
        Xbox IDebugXboxAddOn<Xbox>.Xbox { get { return Xbox; } }
        string IAddOn.Name { get { return AddOnName; } }
        string IAddOn.Author { get { return AddOnAuthor; } }
        string IAddOn.Description { get { return AddOnDescription; } }
        void IHaloAddOn<HaloMapFile, HaloTag>.OnMapLoad()
        {
            Map = Host?.Request(this, "GetMap") as HaloMapFile ?? null;
        }
        void IHaloAddOn<HaloMapFile, HaloTag>.OnSelectedEntryChanged()
        {
            SelectedEntry = Host?.Request(this, "SelectedEntry") as HaloTag ?? null;
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
        private static void SelectedTagPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is HaloAddOnControl control)
            {
                control.RaiseEvent(new RoutedEventArgs(SelectedTagChangedEvent));
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
