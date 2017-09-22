using Abide.AddOnApi;
using Abide.HaloLibrary;
using System;
using System.Collections.Generic;

namespace Abide.Classes
{
    /// <summary>
    /// Represents a specialized Halo AddOn container.
    /// </summary>
    /// <typeparam name="TMap">The Halo map type.</typeparam>
    /// <typeparam name="TEntry">The Halo object index entry type.</typeparam>
    /// <typeparam name="TXbox">The Xbox type.</typeparam>
    public sealed class HaloAddOnContainer<TMap, TEntry, TXbox> : IDisposable
    {
        /// <summary>
        /// Gets and returns true if the container has been disposed; otherwise false.
        /// </summary>
        public bool IsDisposed
        {
            get { return isDisposed; }
        }

        private readonly List<IAddOn> addOns;
        private readonly List<IHaloAddOn<TMap, TEntry>> haloAddOns;
        private readonly List<ITabPage<TMap, TEntry, TXbox>> tabPages;
        private readonly List<IMenuButton<TMap, TEntry, TXbox>> menuButtons;
        private readonly List<IContextMenuItem<TMap, TEntry, TXbox>> contextMenuItems;
        private readonly List<ITool<TMap, TEntry, TXbox>> tools;
        private readonly Dictionary<AddOnFactory, List<Exception>> errors;
        private readonly MapVersion version;
        private bool isDisposed = false;

        /// <summary>
        /// Initializes a new <see cref="HaloAddOnContainer{TMap, TEntry, TXbox}"/>
        /// </summary>
        public HaloAddOnContainer(MapVersion version)
        {
            //Initialize
            this.version = version;
            addOns = new List<IAddOn>();
            haloAddOns = new List<IHaloAddOn<TMap, TEntry>>();
            tabPages = new List<ITabPage<TMap, TEntry, TXbox>>();
            menuButtons = new List<IMenuButton<TMap, TEntry, TXbox>>();
            contextMenuItems = new List<IContextMenuItem<TMap, TEntry, TXbox>>();
            tools = new List<ITool<TMap, TEntry, TXbox>>();
            errors = new Dictionary<AddOnFactory, List<Exception>>();
        }
        /// <summary>
        /// Retrieves all of the <see cref="IAddOn"/> instances within the container.
        /// </summary>
        /// <returns>An array of <see cref="IAddOn"/> instances loaded and initializded by the container.</returns>
        public IAddOn[] GetAddOns()
        {
            return addOns.ToArray();
        }
        /// <summary>
        /// Retrieves all of the <see cref="ITool{TMap, TEntry, TXbox}"/> instances within the container.
        /// </summary>
        /// <returns>An array of <see cref="ITool{TMap, TEntry, TXbox}"/> instances loaded and initializded by the container.</returns>
        public ITool<TMap, TEntry, TXbox>[] GetTools()
        {
            return tools.ToArray();
        }
        /// <summary>
        /// Retrieves all of the <see cref="ITabPage{TMap, TEntry, TXbox}"/> instances within the container.
        /// </summary>
        /// <returns>An array of <see cref="ITabPage{TMap, TEntry, TXbox}"/> instances loaded and initializded by the container.</returns>
        public ITabPage<TMap, TEntry, TXbox>[] GetTabPages()
        {
            return tabPages.ToArray();
        }
        /// <summary>
        /// Retrieves all of the <see cref="IMenuButton{TMap, TEntry, TXbox}"/> instances within the container.
        /// </summary>
        /// <returns>An array of <see cref="IMenuButton{TMap, TEntry, TXbox}"/> instances loaded and initializded by the container.</returns>
        public IMenuButton<TMap, TEntry, TXbox>[] GetMenuButtons()
        {
            return menuButtons.ToArray();
        }
        /// <summary>
        /// Retrieves all of the <see cref="IContextMenuItem{TMap, TEntry, TXbox}"/> instances within the container.
        /// </summary>
        /// <returns>An array of <see cref="IContextMenuItem{TMap, TEntry, TXbox}"/> instances loaded and initializded by the container.</returns>
        public IContextMenuItem<TMap, TEntry, TXbox>[] GetContextMenuItems()
        {
            return contextMenuItems.ToArray();
        }
        /// <summary>
        /// Retrieves all of the <see cref="IHaloAddOn{TMap, TEntry}"/> instances within the container.
        /// </summary>
        /// <returns>An array of <see cref="IHaloAddOn{TMap, TEntry}"/> instances loaded and initializded by the container.</returns>
        public IHaloAddOn<TMap, TEntry>[] GetHaloAddOns()
        {
            return haloAddOns.ToArray();
        }
        /// <summary>
        /// Begins the AddOn initialization process.
        /// </summary>
        /// <param name="host">The AddOn host.</param>
        public void BeginInit(IHost host)
        {
            //Loop
            foreach (AddOnFactory factory in Program.Container.GetFactories())
                Factory_FilterInterfaces(factory, host);
        }
        /// <summary>
        /// Releases all resources used by this <see cref="HaloAddOnContainer{TMap, TEntry, TXbox}"/> instance.
        /// </summary>
        public void Dispose()
        {
            //Check
            if (isDisposed) return;

            //Dispose children
            addOns.ForEach(a => a.Dispose());

            //Clear
            addOns.Clear();
            tabPages.Clear();
            menuButtons.Clear();
            contextMenuItems.Clear();
            tools.Clear();
            errors.Clear();

            //Set
            isDisposed = true;
        }
        private void Factory_FilterInterfaces(AddOnFactory factory, IHost host)
        {
            //Check Types
            foreach (Type type in factory.GetAddOnTypes())
            {
                //Prepare...
                var halo = type.GetInterface(typeof(IHaloAddOn<TMap, TEntry>).Name);
                var tool = type.GetInterface(typeof(ITool<TMap, TEntry, TXbox>).Name);
                var menuButton = type.GetInterface(typeof(IMenuButton<TMap, TEntry, TXbox>).Name);
                var contextMenuItem = type.GetInterface(typeof(IContextMenuItem<TMap, TEntry, TXbox>).Name);
                var tabPage = type.GetInterface(typeof(ITabPage<TMap, TEntry, TXbox>).Name);
                var assemblyName = type.Assembly.GetName().Name;
                if (!errors.ContainsKey(factory)) errors.Add(factory, new List<Exception>());

                //Check Halo based AddOns
                if (halo != null)
                    using (var haloAddOn = factory.CreateInstance<IHaloAddOn<TMap, TEntry>>(assemblyName, type.FullName))
                        if (haloAddOn != null && haloAddOn.Version == version)
                        {
                            //Initialize...
                            try
                            {
                                if (tool != null) Factory_InitializeTool(factory, assemblyName, type.FullName, host);
                                if (menuButton != null) Factory_InitializeMenuButton(factory, assemblyName, type.FullName, host);
                                if (contextMenuItem != null) Factory_InitializeContextMenuItem(factory, assemblyName, type.FullName, host);
                                if (tabPage != null) Factory_InitializeTabPage(factory, assemblyName, type.FullName, host);
                            }
                            catch (Exception ex) { errors[factory].Add(ex); }
                        }
            }
        }
        private void Factory_InitializeTool(AddOnFactory factory, string assemblyName, string typeFullName, IHost host)
        {
            //Create
            ITool<TMap, TEntry, TXbox> tool = factory.CreateInstance<ITool<TMap, TEntry, TXbox>>(assemblyName, typeFullName);
            tool.Initialize(host);

            //Add
            addOns.Add(tool);
            haloAddOns.Add(tool);
            tools.Add(tool);
        }
        private void Factory_InitializeMenuButton(AddOnFactory factory, string assemblyName, string typeFullName, IHost host)
        {
            //Create
            IMenuButton<TMap, TEntry, TXbox> menuButton = factory.CreateInstance<IMenuButton<TMap, TEntry, TXbox>>(assemblyName, typeFullName);
            menuButton.Initialize(host);

            //Add
            addOns.Add(menuButton);
            haloAddOns.Add(menuButton);
            menuButtons.Add(menuButton);
        }
        private void Factory_InitializeTabPage(AddOnFactory factory, string assemblyName, string typeFullName, IHost host)
        {
            //Create
            ITabPage<TMap, TEntry, TXbox> tabPage = factory.CreateInstance<ITabPage<TMap, TEntry, TXbox>>(assemblyName, typeFullName);
            tabPage.Initialize(host);

            //Add
            addOns.Add(tabPage);
            haloAddOns.Add(tabPage);
            tabPages.Add(tabPage);
        }
        private void Factory_InitializeContextMenuItem(AddOnFactory factory, string assemblyName, string typeFullName, IHost host)
        {
            //Create
            IContextMenuItem<TMap, TEntry, TXbox> contextMenuItem = factory.CreateInstance<IContextMenuItem<TMap, TEntry, TXbox>>(assemblyName, typeFullName);
            contextMenuItem.Initialize(host);

            //Add
            addOns.Add(contextMenuItem);
            haloAddOns.Add(contextMenuItem);
            contextMenuItems.Add(contextMenuItem);
        }
    }
}
