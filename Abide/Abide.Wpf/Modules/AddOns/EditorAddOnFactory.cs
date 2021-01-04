using Abide.AddOnApi;
using Abide.AddOnApi.Wpf;
using System.Collections.ObjectModel;

namespace Abide.Wpf.Modules.AddOns
{
    public sealed class EditorAddOnFactory : AddOnFactory
    {
        public ObservableCollection<IFileEditor> FileEditors { get; } = new ObservableCollection<IFileEditor>();
        public ObservableCollection<IProjectEditor> ProjectEditors { get; } = new ObservableCollection<IProjectEditor>();

        public EditorAddOnFactory(IHost host) : base(host, typeof(IFileEditor), typeof(IProjectEditor)) { }
        protected override void LoadAddOn(IAddOn addOn)
        {
            switch (addOn)
            {
                case IFileEditor fileEditor:
                    FileEditors.Add(fileEditor);
                    break;

                case IProjectEditor projectEditor:
                    ProjectEditors.Add(projectEditor);
                    break;
            }
        }
    }
}
