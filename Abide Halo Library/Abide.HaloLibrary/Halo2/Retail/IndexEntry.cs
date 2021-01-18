using Abide.HaloLibrary.Halo2.Retail.Tag.Generated;
using Abide.HaloLibrary.Halo2Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;

namespace Abide.HaloLibrary.Halo2.Retail
{
    /// <summary>
    /// Represents a Halo 2 retail map object index entry.
    /// </summary>
    [Serializable]
    public sealed class IndexEntry : IDisposable
    {
        /// <summary>
        /// Creates a new instance of the <see cref="IndexEntry"/> class.
        /// </summary>
        public IndexEntry()
        {
            Data = new HaloMapDataContainer();
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDisposed { get; private set; } = false;
        /// <summary>
        /// Gets and returns this tag's string container.
        /// </summary>
        public StringContainer Strings { get; } = new StringContainer();
        /// <summary>
        /// Gets and returns this tag's resource container.
        /// </summary>
        public TagResourceContainer Resources { get; } = new TagResourceContainer();
        /// <summary>
        /// Gets or sets the root tag group of the entry.
        /// </summary>
        public TagFourCc Root
        {
            get { return Tag.Root; }
        }
        /// <summary>
        /// Gets and returns the tag ID of the entry.
        /// </summary>
        public TagId Id { get; set; }
        /// <summary>
        /// Gets and returns the tag hierarchy of the entry.
        /// </summary>
        public TagHierarchy Tag { get; set; }
        /// <summary>
        /// Gets and returns the file name of the entry.
        /// </summary>
        public string Filename { get; set; }
        /// <summary>
        /// Gets and returns the address of the data referenced by the entry.
        /// </summary>
        public long Address { get; set; }
        /// <summary>
        /// Gets and returns the length of the data referenced by the entry.
        /// </summary>
        public long Length { get; set; }
        /// <summary>
        /// Gets and returns a stream that contains the data referenced by the entry.
        /// </summary>
        public HaloMapDataContainer Data { get; internal set; }
        /// <summary>
        /// Releases all resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed) return;
            IsDisposed = true;

            if (Data != null)
                Data.Dispose();

            Resources.Dispose();
            Strings.Clear();
        }

        /// <summary>
        /// Returns a string representation of this instance.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return $"0x{Id} \"{Filename}.{Root}\" Address: {Address} Length: {Length}";
        }
    }

    /// <summary>
    /// Represents a container for tag resource data.
    /// </summary>
    [Serializable]
    public sealed class TagResourceContainer : IEnumerable<HaloMapDataContainer>, IDisposable
    {
        private readonly List<HaloMapDataContainer> resources = new List<HaloMapDataContainer>();
        private readonly Dictionary<long, int> resourceLookup = new Dictionary<long, int>();

        /// <summary>
        /// Gets and returns the number of resources contained within this container.
        /// </summary>
        public int Count
        {
            get { return resources.Count; }
        }
        /// <summary>
        /// Initializes a new instance of <see cref="TagResourceContainer"/> class.
        /// </summary>
        public TagResourceContainer() { }
        /// <summary>
        /// Returns a value that determines if a resource at the given address exists in the container.
        /// </summary>
        /// <param name="offset">The offset of the resource.</param>
        /// <returns><see langword="true"/> if this container contains a resource at the given offset; otherwise, <see langword="false"/>.</returns>
        public bool Contains(long offset)
        {
            if (resourceLookup.ContainsKey(offset)) return true;
            return false;
        }
        /// <summary>
        /// Returns a data container for the resource at the given offset.
        /// </summary>
        /// <param name="offset">The offset of the resource.</param>
        /// <returns>A <see cref="HaloMapDataContainer"/> that contains the resource at the specified offset.</returns>
        public HaloMapDataContainer GetResource(int offset)
        {
            //Check
            if (!resourceLookup.ContainsKey(offset))
                throw new InvalidOperationException("The given raw offset does not exist in the container.");

            //Return
            return resources[resourceLookup[offset]];
        }
        /// <summary>
        /// Returns a value that determines whether a resource exists at the given offset.
        /// </summary>
        /// <param name="offset">The offset of the resource.</param>
        /// <param name="data">The <see cref="HaloMapDataContainer"/> that contains the resource at the specified offset.</param>
        /// <returns><see langword="true"/> if this container contains a resource at the given offset; otherwise, <see langword="false"/>.</returns>
        public bool TryGetResource(long offset, out HaloMapDataContainer data)
        {
            //Setup
            data = null;

            //Check
            if (!resourceLookup.ContainsKey(offset)) return false;
            data = resources[resourceLookup[offset]];
            return true;
        }
        /// <summary>
        /// Attempts to add a resource to the container.
        /// </summary>
        /// <param name="offset">The offset of the resource.</param>
        /// <param name="data">The resource data.</param>
        /// <returns><see langword="true"/> if the resource was successfully added to the container; otherwise, <see langword="false"/>.</returns>
        public bool AddResource(long offset, byte[] data)
        {
            //Check
            if (resourceLookup.ContainsKey(offset)) return false;

            //Create container
            HaloMapDataContainer rawContainer = new HaloMapDataContainer();

            //Add raw to list
            resourceLookup.Add(offset, resources.Count);
            resources.Add(rawContainer);

            //Set buffer
            rawContainer.SetBuffer(data);
            return true;
        }
        /// <summary>
        /// Releases all resources used by this instance.
        /// </summary>
        public void Dispose()
        {
            //Loop through resources
            foreach (var resource in resources)
            {
                resource.Dispose();
                resource.SetBuffer(null);
            }
        }
        /// <summary>
        /// Returns an enumerator that iterates through this instance.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<HaloMapDataContainer> GetEnumerator()
        {
            return resources.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
