using Abide.AddOnApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Abide.Wpf.Modules.AddOns
{
    /// <summary>
    /// Represents an object that can create instances of specified <see cref="IAddOn"/> AddOn types.
    /// </summary>
    public abstract class AddOnFactory : INotifyPropertyChanged
    {
        private IHost host = null;

        /// <summary>
        /// Occurs when a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Gets and returns the list of loaded <see cref="IAddOn"/> instances.
        /// </summary>
        public AddOnCollection<IAddOn> AddOns { get; } = new AddOnCollection<IAddOn>();
        /// <summary>
        /// Gets and returns a value that indicates whether or not the AddOns have been initialized.
        /// </summary>
        public bool AddOnsInitialized { get; private set; } = false;
        /// <summary>
        /// Gets and returns the AddOn host.
        /// </summary>
        public IHost Host
        {
            get { return host; }
            set
            {
                //Check
                if (AddOnsInitialized) throw new InvalidOperationException("Unable to set the value of Host " +
                    "once AddOns have been initialized.");

                //Check
                if (host != value)
                {
                    host = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AddOnFactory"/> class with the optional host.
        /// </summary>
        /// <param name="host">The AddOn host.</param>
        public AddOnFactory(IHost host = null)
        {
            //Set host
            Host = host;
        }
        /// <summary>
        /// Loads all of the AddOn types in the <see cref="AssemblyManager"/>.
        /// </summary>
        public void InitializeAddOns()
        {
            //Check
            if (AddOnsInitialized) return;

            //Loop through types
            foreach (Type type in AssemblyManager.AddOnTypes)
                LoadType(type);

            //Set
            AddOnsInitialized = true;
        }
        /// <summary>
        /// Creates and returns a new instance of a specified type.
        /// </summary>
        /// <typeparam name="T">The <see cref="IAddOn"/> based type to instantiate.</typeparam>
        /// <param name="type">The type to create a new instance of.</param>
        /// <param name="host">The AddOn host.</param>
        /// <returns>A new instance of the <typeparamref name="T"/> type.</returns>
        public T Instantiate<T>(Type type, IHost host) where T : class, IAddOn
        {
            //Prepare
            T addOn;

            //Create instance
            try { addOn = (T)AppDomain.CurrentDomain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName); }
            catch (Exception ex) { throw new InvalidOperationException($"Unable to create instance of {type.AssemblyQualifiedName}.", ex); }

            //Initialize
            try { addOn.Initialize(host); }
            catch (Exception ex) { throw new InvalidOperationException($"Error initializing instance of {type.AssemblyQualifiedName}.", ex); }

            //Return
            return addOn;
        }
        /// <summary>
        /// Creates and returns a new instance of a specified type using the supplied type name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName">The assembly-qualified name of the type to create.</param>
        /// <param name="host">The AddOn host.</param>
        /// <returns>A new instance of the <typeparamref name="T"/> type.</returns>
        public T Instantiate<T>(string typeName, IHost host) where T : class, IAddOn
        {
            Type type = Type.GetType(typeName);
            return Instantiate<T>(type, host);
        }
        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event using the optional property name.
        /// </summary>
        /// <param name="propertyName">The name of the property that was changed.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            //Raise event
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// When implemented, loads any custom <see cref="IAddOn"/> types.
        /// </summary>
        /// <param name="type">The type attempt to load. This type will always implement <see cref="IAddOn"/>.</param>
        protected virtual void LoadAddOn(Type type)
        {
            //Prepare
            IAddOn addOn;

            //Instantiate
            try { addOn = Instantiate<IAddOn>(type, host); }
            catch { addOn = null; }

            //Check
            if (addOn != null) AddOns.Add(addOn);
        }
        private void LoadType(Type type)
        {
            //Check type
            if (type.GetInterface(typeof(IAddOn).Name) != null)
                LoadAddOn(type);    //Load AddOn
        }
    }

    /// <summary>
    /// Represents a collection of AddOn instances.
    /// </summary>
    /// <typeparam name="T">The AddOn type.</typeparam>
    public class AddOnCollection<T> : IEnumerable<T>, INotifyCollectionChanged
        where T : IAddOn
    {
        private readonly List<T> addOns = new List<T>();

        /// <summary>
        /// Occurs when the state of the collection is changed.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollctionChanged;
        /// <summary>
        /// Gets and returns the number of AddOns in this collection.
        /// </summary>
        public int Count
        {
            get { return addOns.Count; }
        }
        /// <summary>
        /// Gets and returns the AddOn at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the AddOn element.</param>
        /// <returns>An AddOn element.</returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= addOns.Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return addOns[index];
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AddOnCollection{T}"/> class.
        /// </summary>
        public AddOnCollection()
        {
        }
        /// <summary>
        /// Adds an AddOn to the end of this collection.
        /// </summary>
        /// <param name="item">The AddOn to add.</param>
        public void Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            addOns.Add(item);   //Add
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }
        /// <summary>
        /// Removes an AddOn from the end of this collection.
        /// </summary>
        /// <param name="item">The AddOn to remove.</param>
        /// <returns><see langword="true"/> if the <paramref name="item"/> was removed from the collection; otherwise, <see langword="false"/>.</returns>
        public bool Remove(T item)
        {
            bool removed = addOns.Remove(item); //Remove
            if (removed) OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove));

            return removed;
        }
        /// <summary>
        /// Clears all AddOns from this collection.
        /// </summary>
        public void Clear()
        {
            addOns.Clear(); //Clear list
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        /// <summary>
        /// Returns an enumerator that iterates through this collection.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return addOns.GetEnumerator();
        }
        /// <summary>
        /// Raises the <see cref="CollctionChanged"/> event.
        /// </summary>
        /// <param name="e">The event data that contains the changes to the collection.</param>
        protected void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            //Raise event
            CollctionChanged?.Invoke(this, e);
        }

        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
        {
            add { CollctionChanged += value; }
            remove { CollctionChanged -= value; }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
