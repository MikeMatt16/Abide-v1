namespace Abide.AddOnApi.Wpf
{
    /// <summary>
    /// Defines an editor that can edit files within the host application.
    /// </summary>
    public interface IFileEditor : IAddOn, IElementSupport
    {
        /// <summary>
        /// Gets and returns the extension of the file type.
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
    }
}
