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
        /// </summary>
        TAG[] Filter { get; }
    }
}
