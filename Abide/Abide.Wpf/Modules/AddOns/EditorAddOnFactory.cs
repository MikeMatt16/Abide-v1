using Abide.AddOnApi;
using Abide.AddOnApi.Wpf;
using System;
using System.ComponentModel;

namespace Abide.Wpf.Modules.AddOns
{
    /// <summary>
    /// Represents an object that can create instances of <see cref="IFileEditor"/> AddOn types.
    /// </summary>
    public sealed class EditorAddOnFactory : AddOnFactory, IHost
    {
        private class DummyHost : IHost
        {

            public object Invoke(Delegate method)
            {
                //Call delegate
                return method?.DynamicInvoke();
            }
            public object Invoke(Delegate method, object[] args)
            {
                //Call delegate
                return method?.DynamicInvoke(args);
            }
            public object Request(IAddOn sender, string request, params object[] args)
            {
                //Return
                return null;
            }

            bool ISynchronizeInvoke.InvokeRequired => throw new NotImplementedException();
            IAsyncResult ISynchronizeInvoke.BeginInvoke(Delegate method, object[] args)
            {
                throw new NotImplementedException();
            }
            object ISynchronizeInvoke.EndInvoke(IAsyncResult result)
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets and returns a list of file editor AddOns.
        /// </summary>
        public AddOnCollection<IFileEditor> FileEditors { get; } = new AddOnCollection<IFileEditor>();
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorAddOnFactory"/> class.
        /// </summary>
        public EditorAddOnFactory() : base(new DummyHost()) { }
        protected override void LoadAddOn(Type type)
        {
            //Prepare
            IFileEditor editor;

            //Check
            if (type.GetInterface(typeof(IFileEditor).Name) != null)
            {
                //Instantiate
                try { editor = Instantiate<IFileEditor>(type, Host); }
                catch { editor = null; }

                //Add to list
                FileEditors.Add(editor);
            }
        }

        object IHost.Invoke(Delegate method)
        {
            return method?.DynamicInvoke();
        }
        object IHost.Request(IAddOn sender, string request, params object[] args)
        {
            //Return
            return null;
        }
        object ISynchronizeInvoke.Invoke(Delegate method, object[] args)
        {
            return method?.DynamicInvoke(args);
        }
        IAsyncResult ISynchronizeInvoke.BeginInvoke(Delegate method, object[] args)
        {
            throw new NotImplementedException();
        }
        object ISynchronizeInvoke.EndInvoke(IAsyncResult result)
        {
            throw new NotImplementedException();
        }
        bool ISynchronizeInvoke.InvokeRequired => throw new NotImplementedException();
    }
}
