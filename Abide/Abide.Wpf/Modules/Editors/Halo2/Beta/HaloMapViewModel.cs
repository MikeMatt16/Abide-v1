using Abide.HaloLibrary.Halo2BetaMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;

namespace Abide.Wpf.Modules.Editors.Halo2.Beta
{
    /// <summary>
    /// Represents a model wrapping a <see cref="HaloLibrary.Halo2BetaMap.MapFile"/> object.
    /// </summary>
    public class HaloMapViewModel : DependencyObject
    {
        private static readonly DependencyPropertyKey MapFilePropertyKey =
            DependencyProperty.RegisterReadOnly("MapFile", typeof(MapFile), typeof(HaloMapViewModel), new PropertyMetadata(MapFilePropertyChanged));
        private static readonly DependencyPropertyKey TagPathNodesPropertyKey =
            DependencyProperty.RegisterReadOnly("TagPathNodes", typeof(TagPathNode.TagPathNodeCollection), typeof(HaloMapViewModel), new PropertyMetadata());
        /// <summary>
        /// Identifies the <see cref="MapFile"/> property.
        /// </summary>
        public static DependencyProperty MapFileProperty =
            MapFilePropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="TagPathNodes"/> property.
        /// </summary>
        public static DependencyProperty TagPathNodesProperty =
            TagPathNodesPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets and returns the map file that this model represents.
        /// </summary>
        public MapFile MapFile
        {
            get { return (MapFile)GetValue(MapFileProperty); }
            private set { SetValue(MapFilePropertyKey, value); }
        }
        /// <summary>
        /// Gets and returns the tag path node collection.
        /// </summary>
        public TagPathNode.TagPathNodeCollection TagPathNodes
        {
            get { return (TagPathNode.TagPathNodeCollection)GetValue(TagPathNodesProperty); }
            private set { SetValue(TagPathNodesPropertyKey, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HaloMapViewModel"/> class.
        /// </summary>
        public HaloMapViewModel() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="HaloMapViewModel"/> class using the specified <see cref="MapFile"/> object.
        /// </summary>
        /// <param name="mapFile">The Halo 2 beta map file.</param>
        public HaloMapViewModel(MapFile mapFile)
        {
            //Set
            MapFile = mapFile;
        }

        private static void MapFilePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Prepare
            TagPathNode rootNode = new TagPathNode();

            //Check object
            if (d is HaloMapViewModel model)
            {
                //Check new value
                if (e.NewValue is MapFile mapFile)
                {
                    //Prepare
                    rootNode.Name = mapFile.Name;

                    //Loop
                    foreach (var entry in mapFile.IndexEntries)
                    {
                        //Get tag path
                        string path = $"{entry.Filename}.{entry.Root}";
                        string[] parts = path.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

                        //Prepare
                        TagPathNode currentNode = rootNode;

                        //Loop through each part of tag path
                        for (int i = 0; i < parts.Length - 1; i++)
                        {
                            if (currentNode.Children.ContainsName(parts[i]))
                                currentNode = currentNode.Children[parts[i]];
                            else currentNode = currentNode.Children.Add(parts[i]);
                        }

                        //Create
                        TagPathNode tagNode = new TagPathNode()
                        {
                            Name = parts[parts.Length - 1],
                            IndexEntry = entry
                        };

                        //Add
                        currentNode.Children.Add(tagNode);
                    }
                }

                //Sort
                rootNode.Children.Sort();

                //Set
                model.TagPathNodes = rootNode.Children;
            }
        }
    }

    /// <summary>
    /// Represents a single node in a tag path.
    /// </summary>
    public sealed class TagPathNode : DependencyObject
    {
        private static readonly DependencyPropertyKey OwnerPropertyKey =
            DependencyProperty.RegisterReadOnly("Owner", typeof(TagPathNodeCollection), typeof(TagPathNode), new PropertyMetadata());
        private static readonly DependencyPropertyKey ChildrenPropertyKey =
            DependencyProperty.RegisterReadOnly("Children", typeof(TagPathNodeCollection), typeof(TagPathNode), new PropertyMetadata());

        /// <summary>
        /// Identifies the <see cref="Owner"/> property.
        /// </summary>
        public static readonly DependencyProperty OwnerProperty =
            OwnerPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="Children"/> property.
        /// </summary>
        public static readonly DependencyProperty ChildrenProperty =
            ChildrenPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="IndexEntry"/> property.
        /// </summary>
        public static readonly DependencyProperty IndexEntryProperty =
            DependencyProperty.Register("IndexEntry", typeof(IndexEntry), typeof(TagPathNode));
        /// <summary>
        /// Identifies the <see cref="Name"/> property.
        /// </summary>
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(TagPathNode));

        /// <summary>
        /// Gets or sets the index entry that this tag path node represents.
        /// </summary>
        public IndexEntry IndexEntry
        {
            get { return (IndexEntry)GetValue(IndexEntryProperty); }
            set { SetValue(IndexEntryProperty, value); }
        }
        /// <summary>
        /// Gets and returns the collection that this <see cref="TagPathNode"/> belongs to.
        /// </summary>
        public TagPathNodeCollection Owner
        {
            get { return (TagPathNodeCollection)GetValue(OwnerProperty); }
            private set { SetValue(OwnerPropertyKey, value); }
        }
        /// <summary>
        /// Gets and returns the children of this <see cref="TagPathNode"/>.
        /// </summary>
        public TagPathNodeCollection Children
        {
            get { return (TagPathNodeCollection)GetValue(ChildrenProperty); }
            private set { SetValue(ChildrenPropertyKey, value); }
        }
        /// <summary>
        /// Gets or sets the name of the node.
        /// </summary>
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagPathNode"/> class.
        /// </summary>
        public TagPathNode()
        {
            Children = new TagPathNodeCollection();
        }
        /// <summary>
        /// Invoked whenever the effective value of any dependency property on this <see cref="TagPathNode"/> has been updated.
        /// </summary>
        /// <param name="e">Event data that will contain the dependency property identifier of interest, the property metadata for the type, and old and new values.</param>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            //Base procedures
            base.OnPropertyChanged(e);

            //Update collection
            if (Owner != null) Owner.OnPropertyChanged(new PropertyChangedEventArgs(e.Property.Name));
        }
        /// <summary>
        /// Returns the <see cref="Name"/> property value.
        /// </summary>
        /// <returns>Tha value of the <see cref="Name"/> property.</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Represents a collection of tag path node elements. 
        /// </summary>
        public sealed class TagPathNodeCollection : IEnumerable<TagPathNode>, INotifyCollectionChanged, INotifyPropertyChanged
        {
            private readonly Dictionary<string, int> nodeNameMap = new Dictionary<string, int>();
            private readonly List<TagPathNode> nodes = new List<TagPathNode>();

            /// <summary>
            /// 
            /// </summary>
            public event NotifyCollectionChangedEventHandler CollectionChanged;
            /// <summary>
            /// 
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;
            /// <summary>
            /// 
            /// </summary>
            public int Count
            {
                get { return nodes.Count; }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public TagPathNode this[int index]
            {
                get
                {
                    if (index < 0 || index >= nodes.Count) throw new ArgumentOutOfRangeException(nameof(index));
                    return nodes[index];
                }
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="name"></param>
            /// <returns></returns>
            public TagPathNode this[string name]
            {
                get
                {
                    //Check
                    if (name == null) throw new ArgumentNullException(nameof(name));
                    if (!nodeNameMap.ContainsKey(name))
                        throw new ArgumentException("A node with the specified name does not exist.", nameof(name));

                    //Return
                    return nodes[nodeNameMap[name]];
                }
            }
            /// <summary>
            /// Initializes a new instance of the <see cref="TagPathNodeCollection"/> class.
            /// </summary>
            public TagPathNodeCollection() { }
            /// <summary>
            /// Raises the <see cref="PropertyChanged"/> event.
            /// </summary>
            /// <param name="e">The event data containing the property change status.</param>
            public void OnPropertyChanged(PropertyChangedEventArgs e)
            {
                //Raise PropertyChanged event.
                PropertyChanged?.Invoke(this, e);
            }
            /// <summary>
            /// Raises the <see cref="CollectionChanged"/> event.
            /// </summary>
            /// <param name="e">The event data containing the collection change status.</param>
            public void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
            {
                //Raise CollectionChanged event.
                CollectionChanged?.Invoke(this, e);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="name"></param>
            /// <returns></returns>
            public bool ContainsName(string name)
            {
                return nodeNameMap.ContainsKey(name);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="name"></param>
            /// <returns></returns>
            public TagPathNode Add(string name)
            {
                //Check
                if (name == null) throw new ArgumentNullException(nameof(name));
                if (nodeNameMap.ContainsKey(name))
                    throw new InvalidOperationException("A node with the specified name already exists.");

                //Create node
                TagPathNode node = new TagPathNode() { Name = name };
                Add(node);  //Add node

                //Return
                return node;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            public void Add(TagPathNode node)
            {
                //Check
                if (node == null) throw new ArgumentNullException(nameof(node));
                if (nodeNameMap.ContainsKey(node.Name))
                    throw new InvalidOperationException("A node with the specified name already exists.");

                //Add
                node.Owner = this;
                nodeNameMap.Add(node.Name, nodes.Count);
                nodes.Add(node);

                //Call OnCollectionChanged
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, node));
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="node"></param>
            /// <returns></returns>
            public bool Remove(TagPathNode node)
            {
                //Check
                if (node == null) throw new ArgumentNullException(nameof(node));
                if (!nodeNameMap.ContainsKey(node.Name)) return false;
                if (nodes[nodeNameMap[node.Name]] != node) return false;

                //Remove
                nodes.RemoveAt(nodeNameMap[node.Name]);
                nodeNameMap.Remove(node.Name);
                node.Owner = null;

                //Call OnCollectionChanged
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, node));

                //Return
                return true;
            }
            /// <summary>
            /// 
            /// </summary>
            public void Clear()
            {
                //Clone list
                List<TagPathNode> nodes = new List<TagPathNode>(this.nodes);

                //Clear
                nodes.Clear();
                nodeNameMap.Clear();

                //Call OnCollectionChanged 
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, nodes));
            }
            /// <summary>
            /// 
            /// </summary>
            public void Sort()
            {
                //Sort nodes
                nodes.Sort(TagPathNodeComparitor);

                //Rebuild table and sort children
                nodeNameMap.Clear();
                nodes.ForEach(n =>
                {
                    nodeNameMap.Add(n.Name, nodeNameMap.Count);
                    n.Children.Sort();
                });
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public IEnumerator<TagPathNode> GetEnumerator()
            {
                return nodes.GetEnumerator();
            }

            private int TagPathNodeComparitor(TagPathNode x, TagPathNode y)
            {
                int xc = x.Children.Count, yc = y.Children.Count;
                if (xc > 0 && yc == 0) return -1;
                else if (xc == 0 && yc > 0) return 1;
                else return string.Compare(x.Name, y.Name);
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
