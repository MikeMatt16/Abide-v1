using Abide.AddOnApi;
using System;
using System.Collections.Generic;

namespace Abide.Classes
{
    /// <summary>
    /// Represents a generic AddOn container.
    /// </summary>
    public sealed class AddOnContainer : IDisposable
    {
        private readonly List<IAddOn> addOns;
        private bool disposedValue = false;

        /// <summary>
        /// Initializes a new <see cref="AddOnContainer"/>
        /// </summary>
        public AddOnContainer()
        {
            //Initialize
            addOns = new List<IAddOn>();
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
        /// Releases all resources used by this <see cref="AddOnContainer"/> instance.
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
                var addOn = type.GetInterface(typeof(IAddOn).Name);
                var assemblyName = type.Assembly.GetName().Name;

                //Check Settings page
                if (addOn != null)
                    factory_InitializeAddOn(factory, assemblyName, type.FullName, host);
            }
        }
        private void factory_InitializeAddOn(AddOnFactory factory, string assemblyName, string typeFullName, IHost host)
        {
            //Create
            IAddOn addOn = factory.CreateInstance<IAddOn>(assemblyName, typeFullName);
            addOn.Initialize(host);

            //Add
            addOns.Add(addOn);
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

                disposedValue = true;
            }
        }
    }
}
