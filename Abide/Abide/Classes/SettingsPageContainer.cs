using Abide.AddOnApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Abide.Classes
{
    /// <summary>
    /// Represents a specialized Settings AddOn container.
    /// </summary>
    public sealed class SettingsPageContainer
    {

        private readonly List<IAddOn> addOns;
        private readonly List<ISettingsPage> settingsPages;
        private readonly Dictionary<string, AddOnFactory> factories;
        private bool disposedValue = false;

        /// <summary>
        /// Initializes a new <see cref="SettingsPageContainer"/>
        /// </summary>
        public SettingsPageContainer()
        {
            //Initialize
            addOns = new List<IAddOn>();
            settingsPages = new List<ISettingsPage>();
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
        /// Retrieves all of the <see cref="ISettingsPage"/> instances within the container.
        /// </summary>
        /// <returns>An array of <see cref="ISettingsPage"/> instances loaded and initializded by the container.</returns>
        public ISettingsPage[] GetSettingsPages()
        {
            return settingsPages.ToArray();
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
        /// Releases all resources used by this <see cref="SettingsPageContainer"/> instance.
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
                var settingsPage = type.GetInterface(typeof(ISettingsPage).Name);
                var assemblyName = type.Assembly.GetName().Name;

                //Check Settings page
                if (settingsPage != null)
                    factory_InitializeSettingsPage(factory, assemblyName, type.FullName, host);
            }
        }
        private void factory_InitializeSettingsPage(AddOnFactory factory, string assemblyName, string typeFullName, IHost host)
        {
            //Create
            ISettingsPage settingsPage = factory.CreateInstance<ISettingsPage>(assemblyName, typeFullName);
            settingsPage.Initialize(host);

            //Add
            addOns.Add(settingsPage);
            settingsPages.Add(settingsPage);
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
                settingsPages.Clear();
                factories.Clear();

                disposedValue = true;
            }
        }
    }
}
