using Abide.AddOnApi;
using Abide.HaloLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Abide.Classes
{
    /// <summary>
    /// Represents a basic AddOn container.
    /// </summary>
    /// <typeparam name="TMap">The Halo map type.</typeparam>
    /// <typeparam name="TEntry">The Halo object index entry type.</typeparam>
    /// <typeparam name="TXbox">The Xbox type.</typeparam>
    public sealed class AddOnContainer<TMap, TEntry, TXbox> : IDisposable
    {
        private readonly List<IAddOn> addOns;
        private readonly List<IHaloAddOn<TMap, TEntry>> haloAddOns;
        private readonly List<ITabPage<TMap, TEntry, TXbox>> tabPages;
        private readonly List<IMenuButton<TMap, TEntry, TXbox>> menuButtons;
        private readonly List<ITool<TMap, TEntry, TXbox>> tools;
        private readonly Dictionary<string, AddOnFactory> factories;
        private readonly MapVersion version;
        private bool disposedValue = false;

        /// <summary>
        /// Initializes a new <see cref="AddOnContainer{TMap, TEntry, TXbox}"/>
        /// </summary>
        public AddOnContainer(MapVersion version)
        {
            //Initialize
            this.version = version;
            addOns = new List<IAddOn>();
            haloAddOns = new List<IHaloAddOn<TMap, TEntry>>();
            tabPages = new List<ITabPage<TMap, TEntry, TXbox>>();
            menuButtons = new List<IMenuButton<TMap, TEntry, TXbox>>();
            tools = new List<ITool<TMap, TEntry, TXbox>>();
            factories = new Dictionary<string, AddOnFactory>();
        }
        /// <summary>
        /// Retrieves all <see cref="AddOnFactory"/> instances within the container.
        /// </summary>
        /// <returns>An array of <see cref="AddOnFactory"/> loaded by the container.</returns>
        public AddOnFactory[] GetFactories()
        {
            //Get Factories
            AddOnFactory[] factories = new AddOnFactory[this.factories.Count];
            this.factories.Values.CopyTo(factories, 0);
            return factories;
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
        /// <returns>An array of <see cref="ITabPage{TMap, TEntry, TXbox}, TEntry, TXbox}"/> instances loaded and initializded by the container.</returns>
        public ITabPage<TMap, TEntry, TXbox>[] GetTabPages()
        {
            return tabPages.ToArray();
        }
        /// <summary>
        /// Retrieves all of the <see cref="IMenuButton{TMap, TEntry, TXbox}"/> instances within the container.
        /// </summary>
        /// <returns>An array of <see cref="IMenuButton{TMap, TEntry, TXbox}, TEntry, TXbox}"/> instances loaded and initializded by the container.</returns>
        public IMenuButton<TMap, TEntry, TXbox>[] GetMenuButtons()
        {
            return menuButtons.ToArray();
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
        /// Adds an assembly to the instance without locking the source assembly file.
        /// </summary>
        /// <param name="filename">The file path to the assembly.</param>
        public void AddAssemblySafe(string filename)
        {
            //Prepare
            AddOnFactory factory = null;
            string directory = Path.GetDirectoryName(filename);
            if (Directory.Exists(directory))
            {
                //Create or get factory...
                if (!factories.ContainsKey(directory))
                {
                    //Create
                    factory = new AddOnFactory() { AddOnDirectory = directory };
                    factories.Add(directory, factory);
                }
                else factory = factories[directory];

                //Load Assembly
                try { factory.LoadAssemblySafe(filename); }
                catch (Exception ex) { Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace); }
            }
        }
        /// <summary>
        /// Adds an assembly to the instance.
        /// </summary>
        /// <param name="filename">The file path to the assembly.</param>
        public void AddAssembly(string filename)
        {
            //Prepare
            AddOnFactory factory = null;
            string directory = Path.GetDirectoryName(filename);

            //Create or get factory...
            if (!factories.ContainsKey(directory))
            {
                //Create
                factory = new AddOnFactory() { AddOnDirectory = directory };
                factories.Add(directory, factory);
            }
            else factory = factories[directory];

            //Load Assembly
            try { factory.LoadAssembly(filename); }
            catch (Exception ex) { Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace); }
        }
        /// <summary>
        /// Adds an assembly to the instance.
        /// </summary>
        /// <param name="assembly">The assembly to add.</param>
        /// <param name="directory">The directory of the assembly.</param>
        public void AddAssembly(Assembly assembly, string directory)
        {
            //Prepare
            AddOnFactory factory = null;

            //Create or get factory...
            if (!factories.ContainsKey(directory))
            {
                //Create
                factory = new AddOnFactory() { AddOnDirectory = directory };
                factories.Add(directory, factory);
            }
            else factory = factories[directory];

            //Load Assembly
            try { factory.LoadAssembly(assembly); }
            catch (Exception ex) { Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace); }
        }
        /// <summary>
        /// Begins the AddOn initialization process.
        /// </summary>
        /// <param name="host">The AddOn host.</param>
        public void BeginInit(IHost host)
        {
            //Loop
            foreach (KeyValuePair<string, AddOnFactory> factory in factories)
                factory_FilterInterfaces(factory.Value, host);
        }
        /// <summary>
        /// Releases all resources used by this <see cref="AddOnContainer{TMap, TEntry, TXbox}"/> instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        private void factory_FilterInterfaces(AddOnFactory factory, IHost host)
        {
            //Check Types
            foreach (Type type in factory.GetAddOnTypes())
            {
                //Prepare...
                var halo = type.GetInterface(typeof(IHaloAddOn<TMap, TEntry>).Name);
                var tool = type.GetInterface(typeof(ITool<TMap, TEntry, TXbox>).Name);
                var menuButton = type.GetInterface(typeof(IMenuButton<TMap, TEntry, TXbox>).Name);
                var tabPage = type.GetInterface(typeof(ITabPage<TMap, TEntry, TXbox>).Name);
                var assemblyName = type.Assembly.GetName().Name;

                //Check Halo based AddOns
                if (halo != null)
                    using (var haloAddOn = factory.CreateInstance<IHaloAddOn<TMap, TEntry>>(assemblyName, type.FullName))
                        if (haloAddOn.Version == version)
                        {
                            //Initialize...
                            if (tool != null) factory_InitializeTool(factory, assemblyName, type.FullName, host);
                            if (menuButton != null) factory_InitializeMenuButton(factory, assemblyName, type.FullName, host);
                            if (tabPage != null) factory_InitializeTabPage(factory, assemblyName, type.FullName, host);
                        }
            }
        }
        private void factory_InitializeTool(AddOnFactory factory, string assemblyName, string typeFullName, IHost host)
        {
            //Create
            ITool<TMap, TEntry, TXbox> tool = factory.CreateInstance<ITool<TMap, TEntry, TXbox>>(assemblyName, typeFullName);
            tool.Initialize(host);

            //Add
            addOns.Add(tool);
            haloAddOns.Add(tool);
            tools.Add(tool);
        }
        private void factory_InitializeMenuButton(AddOnFactory factory, string assemblyName, string typeFullName, IHost host)
        {
            //Create
            IMenuButton<TMap, TEntry, TXbox> menuButton = factory.CreateInstance<IMenuButton<TMap, TEntry, TXbox>>(assemblyName, typeFullName);
            menuButton.Initialize(host);

            //Add
            addOns.Add(menuButton);
            haloAddOns.Add(menuButton);
            menuButtons.Add(menuButton);
        }
        private void factory_InitializeTabPage(AddOnFactory factory, string assemblyName, string typeFullName, IHost host)
        {
            //Create
            ITabPage<TMap, TEntry, TXbox> tabPage = factory.CreateInstance<ITabPage<TMap, TEntry, TXbox>>(assemblyName, typeFullName);
            tabPage.Initialize(host);

            //Add
            addOns.Add(tabPage);
            haloAddOns.Add(tabPage);
            tabPages.Add(tabPage);
        }
        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                //Dispose
                if (disposing)
                    addOns.ForEach(a => a.Dispose());

                //Clear
                addOns.Clear();
                tabPages.Clear();
                menuButtons.Clear();
                tools.Clear();
                factories.Clear();

                disposedValue = true;
            }
        }
    }
}
