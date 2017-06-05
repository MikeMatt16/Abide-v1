using Abide.Ifp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tag_Editor
{
    /// <summary>
    /// Represents a Halo Tag Data Wrapper
    /// </summary>
    public sealed class DataWrapper : IList<DataObject>, ICollection<DataObject>, IEnumerable<DataObject>
    {
        /// <summary>
        /// Gets the total object count in this instance.
        /// </summary>
        public int Count
        {
            get
            {
                return ((IList<DataObject>)metaObjects).Count;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return ((IList<DataObject>)metaObjects).IsReadOnly;
            }
        }
        /// <summary>
        /// Gets or sets a <see cref="DataObject"/> based on it's unique ID.
        /// </summary>
        /// <param name="uniqueId">The unique ID to search for.</param>
        /// <returns>A <see cref="DataObject"/> whose unique ID matches the provided value.
        /// Returns null if a <see cref="DataObject"/> is not matched.</returns>
        public DataObject this[int uniqueId]
        {
            get
            {
                int index = metaObjects.FindIndex(o => o.UniqueId == uniqueId);
                if (index >= 0)
                    return metaObjects[index];
                else
                    return null;
            }
            set
            {
                int index = metaObjects.FindIndex(o => o.UniqueId == uniqueId);
                if (index >= 0)
                    metaObjects[index] = value;
            }
        }

        private List<DataObject> metaObjects;

        /// <summary>
        /// Initializes a new <see cref="DataWrapper"/> instance.
        /// </summary>
        public DataWrapper()
        {
            //Setup
            metaObjects = new List<DataObject>();
        }
        /// <summary>
        /// Adds a <see cref="DataObject"/> instance to the wrapper.
        /// </summary>
        /// <param name="item">The <see cref="DataObject"/> to add.</param>
        public void Add(DataObject item)
        {
            ((IList<DataObject>)metaObjects).Add(item);
        }
        public void Clear()
        {
            ((IList<DataObject>)metaObjects).Clear();
        }
        public bool Contains(DataObject item)
        {
            return ((IList<DataObject>)metaObjects).Contains(item);
        }
        public void CopyTo(DataObject[] array, int arrayIndex)
        {
            ((IList<DataObject>)metaObjects).CopyTo(array, arrayIndex);
        }
        public IEnumerator<DataObject> GetEnumerator()
        {
            return ((IList<DataObject>)metaObjects).GetEnumerator();
        }
        public int IndexOf(DataObject item)
        {
            return ((IList<DataObject>)metaObjects).IndexOf(item);
        }
        public void Insert(int index, DataObject item)
        {
            ((IList<DataObject>)metaObjects).Insert(index, item);
        }
        public bool Remove(DataObject item)
        {
            return ((IList<DataObject>)metaObjects).Remove(item);
        }
        public void RemoveAt(int index)
        {
            ((IList<DataObject>)metaObjects).RemoveAt(index);
        }
        public void OrderBy(Func<DataObject, object> func)
        {
            metaObjects = metaObjects.OrderBy(func).ToList();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<DataObject>)metaObjects).GetEnumerator();
        }
    }
    
    /// <summary>
    /// Represents a Halo Tag Data Object.
    /// </summary>
    public sealed class DataObject
    {
        /// <summary>
        /// The zero-based offset of the object.
        /// </summary>
        public long Position
        {
            get { return position; }
            set { position = value; }
        }
        /// <summary>
        /// The unique id of the object.
        /// </summary>
        public int UniqueId
        {
            get { return uniqueId; }
        }
        /// <summary>
        /// Gets and returns whether this object has a parent <see cref="HaloMetaObject"/>.
        /// </summary>
        public bool HasParent
        {
            get { return parent != null; }
        }
        /// <summary>
        /// The <see cref="HaloMetaObject"/> instance that represents the object.
        /// </summary>
        public IfpNode Element
        {
            get { return element; }
        }
        /// <summary>
        /// The parent <see cref="HaloMetaObject"/> instance that contains the object.
        /// If null, this object doesn't have a parent.
        /// </summary>
        public IfpNode Parent
        {
            get { return parent; }
        }

        private long position = -1;
        private readonly int uniqueId;
        private readonly IfpNode element;
        private readonly IfpNode parent;

        public DataObject(IfpNode element, IfpNode parent, int uniqueId, long position) :
            this(element, parent, uniqueId)
        {
            //Setup
            this.position = position;
        }
        public DataObject(IfpNode element, IfpNode parent, int uniqueId) :
            this(element, uniqueId)
        {
            //Setup
            this.parent = parent;
        }
        public DataObject(IfpNode element, int uniqueId, long position) :
            this(element, uniqueId)
        {
            //Setup
            this.position = position;
        }
        public DataObject(IfpNode element, int uniqueId)
        {
            //Setup
            this.uniqueId = uniqueId;
            this.element = element;
        }
        public override string ToString()
        {
            if (element != null)
                return element.ToString();
            return base.ToString();
        }
        public string GetHtmlId()
        {
            //Prepare
            string htmlId = string.Empty;

            //Check Type
            switch (element.Type)
            {
                case IfpNodeType.TagBlock:
                    htmlId = "chunkSelect" + uniqueId.ToString();
                    break;

                case IfpNodeType.Byte:
                case IfpNodeType.SignedByte:
                case IfpNodeType.Short:
                case IfpNodeType.UnsignedShort:
                case IfpNodeType.Int:
                case IfpNodeType.UnsignedInt:
                case IfpNodeType.Long:
                case IfpNodeType.UnsignedLong:
                case IfpNodeType.Single:
                case IfpNodeType.Double:
                    htmlId = "value" + uniqueId.ToString();
                    break;

                case IfpNodeType.Enumerator8:
                case IfpNodeType.Enumerator16:
                case IfpNodeType.Enumerator32:
                case IfpNodeType.Enumerator64:
                    htmlId = "enumSelect" + uniqueId.ToString();
                    break;

                case IfpNodeType.Bitfield8:
                case IfpNodeType.Bitfield16:
                case IfpNodeType.Bitfield32:
                case IfpNodeType.Bitfield64:
                    htmlId = "bitmask" + uniqueId.ToString();
                    break;

                case IfpNodeType.TagId:
                    htmlId = "tag" + uniqueId.ToString();
                    break;
                case IfpNodeType.StringId:
                    htmlId = "stringId" + uniqueId.ToString();
                    break;

                case IfpNodeType.String32:
                case IfpNodeType.String64:
                    htmlId = "string" + uniqueId.ToString();
                    break;

                case IfpNodeType.Unicode128:
                case IfpNodeType.Unicode256:
                    htmlId = "unicode" + uniqueId.ToString();
                    break;
            }

            //Return
            return htmlId;
        }
    }
}
