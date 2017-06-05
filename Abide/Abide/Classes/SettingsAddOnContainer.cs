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
    public sealed class SettingsAddOnContainer : IDisposable
    {
        private readonly List<IAddOn> addOns;
        private readonly List<ISettingsPage> settingsPages;
        private readonly Dictionary<AddOnFactory, List<Exception>> errors;
        private bool disposedValue = false;

        /// <summary>
        /// Initializes a new <see cref="SettingsAddOnContainer"/>
        /// </summary>
        public SettingsAddOnContainer()
        {
            //Initialize
            addOns = new List<IAddOn>();
            settingsPages = new List<ISettingsPage>();
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
        /// Retrieves all of the <see cref="ISettingsPage"/> instances within the container.
        /// </summary>
        /// <returns>An array of <see cref="ISettingsPage"/> instances loaded and initializded by the container.</returns>
        public ISettingsPage[] GetSettingsPages()
        {
            return settingsPages.ToArray();
        }
        /// <summary>
        /// Begins the AddOn initialization process.
        /// </summary>
        /// <param name="host">The AddOn host.</param>
        public void BeginInit(IHost host)
        {
            //Loop
            foreach (AddOnFactory factory in Program.Container.GetFactories())
                factory_FilterInterfaces(factory, host);
        }
        /// <summary>
        /// Releases all resources used by this <see cref="SettingsAddOnContainer"/> instance.
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
                errors.Add(factory, new List<Exception>());

                //Check Settings page
                if (settingsPage != null)
                    try { factory_InitializeSettingsPage(factory, assemblyName, type.FullName, host); }
                    catch(Exception ex) { errors[factory].Add(ex); }
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

                disposedValue = true;
            }
        }
    }
}
