using System.IO;

namespace Abide.AddOnApi.Wpf
{
    /// <summary>
    /// Defines an editor that can edit projects within the host application.
    /// </summary>
    public interface IProjectEditor : IAddOn, IElementSupport
    {
        /// <summary>
        /// Gets and returns the extension of the project file this editor supports.
        /// </summary>
        string Extension { get; }
        /// <summary>
        /// Gets and returns the type name of the file.
        /// </summary>
        string TypeName { get; }
        /// <summary>
        /// When implemented, determines whether or not this editor is compatible with the specified file.
        /// </summary>
        /// <param name="path">The file to be validated.</param>
        /// <returns></returns>
        bool IsValidEditor(string path);
        /// <summary>
        /// When implemented, initializes the editor with the specified file name.
        /// </summary>
        /// <param name="path">The file to be used to initialize the editor.</param>
        void Initialize(string path);
        /// <summary>
        /// When implemented, creates a new project using the specified stream and working directory.
        /// </summary>
        /// <param name="stream">The stream to have the default project structure written to.</param>
        /// <param name="workingDirectory">The working directory of the project.</param>
        void CreateProject(Stream stream, string workingDirectory);
    }
}
