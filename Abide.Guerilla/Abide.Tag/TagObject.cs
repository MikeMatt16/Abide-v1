using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Abide.Tag
{
    public abstract class TagObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly Dictionary<string, object> propertyTable = new Dictionary<string, object>();

        public TagObject Owner { get; internal set; }
        public abstract string Name { get; }

        protected TagObject() { }
        protected T GetProperty<T>([CallerMemberName] string propertyName = "")
        {
            if (propertyTable.ContainsKey(propertyName))
            {
                T prop;

                try
                {
                    prop = (T)propertyTable[propertyName];
                }
                catch { throw; }

                return prop;
            }

            return default;
        }
        protected object GetProperty([CallerMemberName] string propertyName = "")
        {
            if (propertyTable.ContainsKey(propertyName))
                return propertyTable[propertyName];

            return null;
        }
        protected void SetProperty(object value, [CallerMemberName] string propertyName = "")
        {
            if (!propertyTable.ContainsKey(propertyName))
                propertyTable.Add(propertyName, null);

            propertyTable[propertyName] = value;
            OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
