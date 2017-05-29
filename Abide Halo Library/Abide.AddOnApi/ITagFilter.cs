using Abide.HaloLibrary;

namespace Abide.AddOnApi
{
    /// <summary>
    /// Defines a generalized Halo Object Entry tag root filter.
    /// </summary>
    public interface ITagFilter
    {
        /// <summary>
        /// When implemented, gets and returns an array of <see cref="TAG"/> structures to filter Object Index Entry tag roots.
        /// This value can not be null.
        /// </summary>
        TAG[] Filter { get; }
        /// <summary>
        /// When implemented, gets and returns true if the tag filter should be used, otherwise the tag filter will be ignored.
        /// </summary>
        bool ApplyFilter { get; }
    }
}
