using Abide.AddOnApi;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Abide.Wpf.Modules.AddOns
{
    public abstract class AddOnFactory : INotifyPropertyChanged
    {
        private Type[] interfaceTypes;
        private IHost host = null;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<IAddOn> AddOns { get; } = new ObservableCollection<IAddOn>();
        public bool AddOnsInitialized { get; private set; } = false;
        public IHost Host
        {
            get => host;
            set
            {
                //Check
                if (AddOnsInitialized)
                {
                    throw new InvalidOperationException("Unable to set the value of Host " +
                    "once AddOns have been initialized.");
                }

                //Check
                if (host != value)
                {
                    host = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public AddOnFactory(IHost host, params Type[] acceptedInterfaces)
        {
            Host = host;
            interfaceTypes = acceptedInterfaces
                .Where(t => t.GetInterface(typeof(IAddOn).Name) == typeof(IAddOn))
                .ToArray();
        }
        public void InitializeAddOns()
        {
            if (!AddOnsInitialized)
            {
                AddOnsInitialized = true;
                foreach (var type in AssemblyManager.AddOnTypes)
                {
                    foreach (var interfaceType in interfaceTypes)
                    {
                        if (type.GetInterface(interfaceType.Name) == interfaceType)
                        {
                            IAddOn addOn = null;

                            try
                            {
                                addOn = Instantiate<IAddOn>(type);
                            }
                            finally
                            {
                                if (addOn != null)
                                {
                                    LoadAddOn(addOn);
                                    AddOns.Add(addOn);
                                }
                            }
                        }
                    }
                }
            }
        }
        public T Instantiate<T>(Type type) where T : class, IAddOn
        {
            var addOn = (T)AppDomain.CurrentDomain.CreateInstanceAndUnwrap(
                type.Assembly.FullName, type.FullName);
            addOn.Initialize(host);

            return addOn;
        }
        public T Instantiate<T>(string typeName) where T : class, IAddOn
        {
            Type type = Type.GetType(typeName);
            return Instantiate<T>(type);
        }
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void LoadAddOn(IAddOn addOn) { }
    }
}
