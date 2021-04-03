using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Abide.Wpf.Modules.Tag
{
    public abstract class AbideTagObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public TagContext Context { get; private set; }
        public virtual string Name { get; private set; } = string.Empty;
        public AbideTagObject Owner { get; }

        protected AbideTagObject(TagContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
        protected AbideTagObject(TagContext context, AbideTagObject owner) : this(context)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }
        public override string ToString()
        {
            return Name;
        }
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
