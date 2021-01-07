using System;

namespace Abide.Wpf.Modules.ViewModel
{
    public abstract class ProjectViewModel : BaseViewModel
    {
        public ProjectViewModel() { }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ProjectTypeAttribute : Attribute
    {
        public Guid Guid { get; }
        public string ProjectTypeName { get; }
        public string ProjectExtension { get; }

        public ProjectTypeAttribute(string guid, string projectTypeName, string projectExtension)
        {
            Guid = Guid.Parse(guid);
            ProjectTypeName = projectTypeName;
            ProjectExtension = projectExtension;
        }
    }
}
