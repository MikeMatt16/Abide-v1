using Abide.HaloLibrary.Halo2.Beta;
using Abide.Wpf.Modules.AddOns;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Abide.Wpf.Modules.Editors.Halo2.Beta
{
    /// <summary>
    /// Represents a model wrapping a <see cref="HaloLibrary.Halo2BetaMap.MapFile"/> object.
    /// </summary>
    public class HaloMapViewModel : DependencyObject
    {
        private static readonly DependencyPropertyKey SelctedTagPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(SelectedTag), typeof(HaloTag), typeof(HaloMapViewModel), new PropertyMetadata(SelectedIndexEntryPropertyChanged));
        private static readonly DependencyPropertyKey MapPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Map), typeof(HaloMapFile), typeof(HaloMapViewModel), new PropertyMetadata(MapPropertyChanged));
        private static readonly DependencyPropertyKey TagPathNodesPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(TagPathNodes), typeof(TagPathNode.TagPathNodeCollection), typeof(HaloMapViewModel), new PropertyMetadata());
        private static readonly DependencyPropertyKey ToolListPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ToolList), typeof(ObservableCollection<ToolAddOn>), typeof(HaloMapViewModel), new PropertyMetadata());
        private static readonly DependencyPropertyKey IsCollapsedPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(IsCollapsed), typeof(bool), typeof(HaloMapViewModel), new PropertyMetadata(true));
        /// <summary>
        /// Identifies the <see cref="IsCollapsed"/> property.
        /// </summary>
        public static readonly DependencyProperty IsCollapsedProperty =
            IsCollapsedPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="Map"/> property.
        /// </summary>
        public static readonly DependencyProperty MapProperty =
            MapPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="SelectedTag"/> property.
        /// </summary>
        public static readonly DependencyProperty SelectedTagProperty =
            SelctedTagPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="TagFilter"/> property.
        /// </summary>
        public static readonly DependencyProperty TagFilterProperty =
            DependencyProperty.Register(nameof(TagFilter), typeof(string), typeof(HaloMapViewModel), new PropertyMetadata(string.Empty, TagFilterPropertyChanged));
        /// <summary>
        /// Identifies the <see cref="TagPathNodes"/> property.
        /// </summary>
        public static readonly DependencyProperty TagPathNodesProperty =
            TagPathNodesPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="ToolList"/> property.
        /// </summary>
        public static readonly DependencyProperty ToolListProperty =
            ToolListPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifies the <see cref="SelectedNode"/> property.
        /// </summary>
        public static readonly DependencyProperty SelectedNodeProperty =
            DependencyProperty.Register(nameof(SelectedNode), typeof(TagPathNode), typeof(HaloMapViewModel), new PropertyMetadata(SelectedNodePropertyChanged));

        /// <summary>
        /// Gets and returns whether or not the tag node tree should be expanded.
        /// This property is currently unused.
        /// </summary>
        public bool IsCollapsed
        {
            get => (bool)GetValue(IsCollapsedProperty);
            private set => SetValue(IsCollapsedPropertyKey, value);
        }
        /// <summary>
        /// Gets and returns the map file that this model represents.
        /// </summary>
        public HaloMapFile Map
        {
            get => (HaloMapFile)GetValue(MapProperty);
            private set => SetValue(MapPropertyKey, value);
        }
        /// <summary>
        /// Gets and returns the selected tag.
        /// </summary>
        public HaloTag SelectedTag
        {
            get => (HaloTag)GetValue(SelectedTagProperty);
            private set => SetValue(SelctedTagPropertyKey, value);
        }
        /// <summary>
        /// Gets or sets the currently selected tag path node.
        /// </summary>
        public TagPathNode SelectedNode
        {
            get => (TagPathNode)GetValue(SelectedNodeProperty);
            set => SetValue(SelectedNodeProperty, value);
        }
        /// <summary>
        /// Gets or sets the current tag filter.
        /// </summary>
        public string TagFilter
        {
            get => (string)GetValue(TagFilterProperty);
            set => SetValue(TagFilterProperty, value);
        }
        /// <summary>
        /// Gets and returns the tag path node collection.
        /// </summary>
        public TagPathNode.TagPathNodeCollection TagPathNodes
        {
            get => (TagPathNode.TagPathNodeCollection)GetValue(TagPathNodesProperty);
            private set => SetValue(TagPathNodesPropertyKey, value);
        }
        /// <summary>
        /// Gets and returns the tool AddOn list.
        /// </summary>
        public ObservableCollection<ToolAddOn> ToolList
        {
            get => (ObservableCollection<ToolAddOn>)GetValue(ToolListProperty);
            private set => SetValue(ToolListPropertyKey, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HaloMapViewModel"/> class.
        /// </summary>
        public HaloMapViewModel() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="HaloMapViewModel"/> class using the specified <see cref="Map"/> object.
        /// </summary>
        /// <param name="map">The Halo 2 beta map file.</param>
        public HaloMapViewModel(HaloMapFile map)
        {
            //Set
            Map = map;
        }

        private TagPathNode.TagPathNodeCollection CreateTagTree(string filter = "")
        {
            TagPathNode rootNode = new TagPathNode();

            foreach (var tag in GetTags(Map, filter))
            {
                string path = $"{tag.TagName}.{tag.GroupTag}";
                string[] parts = path.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                TagPathNode currentNode = rootNode;

                for (int i = 0; i < parts.Length - 1; i++)
                {
                    if (currentNode.Children.ContainsName(parts[i]))
                        currentNode = currentNode.Children[parts[i]];
                    else currentNode = currentNode.Children.Add(parts[i], !IsCollapsed);
                }

                currentNode.Children.Add(parts[parts.Length - 1], false, tag);
            }

            return rootNode.Children;
        }
        private static IEnumerable<HaloTag> GetTags(HaloMapFile haloMap, string filter)
        {
            if (string.IsNullOrEmpty(filter))
                return haloMap.GetTagsEnumerator();

            return haloMap.GetTagsEnumerator().Where(e =>
            {
                string tagName = $"{e.TagName}.{e.GroupTag}";
                foreach (var part in tagName.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (part.StartsWith(filter, StringComparison.OrdinalIgnoreCase))
                        return true;
                }

                return false;
            });
        }
        private static void MapPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is HaloMapViewModel model)
            {
                TagPathNode.TagPathNodeCollection nodes = model.CreateTagTree();
                nodes.Sort();

                model.TagPathNodes = nodes;
                if (nodes.Count > 0)
                    model.SelectedNode = nodes[0];
            }
        }
        private static void SelectedNodePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Check
            if (d is HaloMapViewModel model)
            {
                //Get node
                if (e.NewValue is TagPathNode node)
                {
                    //Check
                    if (node.Tag != null)
                        model.SelectedTag = node.Tag;
                }
            }
        }
        private static void SelectedIndexEntryPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Check
            if (d is HaloMapViewModel model)
            {
            }
        }
        private static void TagFilterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is HaloMapViewModel model)
            {
                if (e.NewValue is string filter)
                {
                    model.IsCollapsed = string.IsNullOrEmpty(filter);

                    TagPathNode.TagPathNodeCollection nodes = model.CreateTagTree(filter);
                    nodes.Sort();
                    model.TagPathNodes = nodes;

                    if (nodes.Count > 0)
                        model.SelectedNode = nodes[0];
                }
            }
        }
    }

    /// <summary>
    /// Represents a wrapper for a <see cref="AddOnApi.Wpf.Halo2.ITool"/> AddOn.
    /// </summary>
    public sealed class ToolAddOn : DependencyObject
    {
        /// <summary>
        /// Identifies the <see cref="Name"/> property.
        /// </summary>
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(ToolAddOn), new PropertyMetadata(string.Empty));
        /// <summary>
        /// Identifies the <see cref="Description"/> property.
        /// </summary>
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(ToolAddOn), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Gets and returns the <see cref="AddOnApi.Wpf.Halo2.ITool"/> object assicated with this object.
        /// </summary>
        public AddOnApi.Wpf.Halo2.ITool Tool { get; }
        /// <summary>
        /// Gets or sets the name of the AddOn.
        /// </summary>
        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }
        /// <summary>
        /// Gets or sets the description of the AddOn.
        /// </summary>
        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolAddOn"/> class using the specified tool AddOn.
        /// </summary>
        /// <param name="tool">The tool AddOn.</param>
        public ToolAddOn(AddOnApi.Wpf.Halo2.ITool tool)
        {
            Tool = tool ?? throw new ArgumentNullException(nameof(tool));
        }
    }

    /// <summary>
    /// Represents a single node in a tag path.
    /// </summary>
    public sealed class TagPathNode : DependencyObject
    {
        private static readonly DependencyPropertyKey OwnerPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Owner), typeof(TagPathNodeCollection), typeof(TagPathNode), new PropertyMetadata());
        private static readonly DependencyPropertyKey ChildrenPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Children), typeof(TagPathNodeCollection), typeof(TagPathNode), new PropertyMetadata());

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
        /// Identifies the <see cref="Tag"/> property.
        /// </summary>
        public static readonly DependencyProperty TagProperty =
            DependencyProperty.Register(nameof(Tag), typeof(HaloTag), typeof(TagPathNode));
        /// <summary>
        /// Identifies the <see cref="ImageSource"/> property.
        /// </summary>
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(TagPathNode));
        /// <summary>
        /// Identifies the <see cref="Name"/> property.
        /// </summary>
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register(nameof(Name), typeof(string), typeof(TagPathNode));
        /// <summary>
        /// Identifies the <see cref="IsExpanded"/> property.
        /// </summary>
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register(nameof(IsExpanded), typeof(bool), typeof(TagPathNode));

        /// <summary>
        /// Gets or sets the tag that this tag path node represents.
        /// </summary>
        public HaloTag Tag
        {
            get => (HaloTag)GetValue(TagProperty);
            set => SetValue(TagProperty, value);
        }
        /// <summary>
        /// Gets and returns the collection that this <see cref="TagPathNode"/> belongs to.
        /// </summary>
        public TagPathNodeCollection Owner
        {
            get => (TagPathNodeCollection)GetValue(OwnerProperty);
            private set => SetValue(OwnerPropertyKey, value);
        }
        /// <summary>
        /// Gets and returns the children of this <see cref="TagPathNode"/>.
        /// </summary>
        public TagPathNodeCollection Children
        {
            get => (TagPathNodeCollection)GetValue(ChildrenProperty);
            private set => SetValue(ChildrenPropertyKey, value);
        }
        /// <summary>
        /// Gets or sets the image source of the node.
        /// </summary>
        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }
        /// <summary>
        /// Gets or sets the name of the node.
        /// </summary>
        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }
        /// <summary>
        /// Gets or sets whether this tag node element is expanded or collapsed.
        /// </summary>
        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
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
            public int Count => nodes.Count;
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
                if (name == null) throw new ArgumentNullException(nameof(name));
                if (nodeNameMap.ContainsKey(name))
                    throw new InvalidOperationException("A node with the specified name already exists.");

                TagPathNode node = new TagPathNode() { Name = name };
                Add(node);  //Add node

                return node;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="name"></param>
            /// <param name="isExpanded"></param>
            /// <returns></returns>
            public TagPathNode Add(string name, bool isExpanded)
            {
                if (name == null) throw new ArgumentNullException(nameof(name));
                if (nodeNameMap.ContainsKey(name))
                    throw new InvalidOperationException("A node with the specified name already exists.");

                TagPathNode node = new TagPathNode()
                {
                    Name = name,
                    IsExpanded = isExpanded
                };
                Add(node);

                return node;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="name"></param>
            /// <param name="isExpanded"></param>
            /// <param name="tag"></param>
            /// <returns></returns>
            public TagPathNode Add(string name, bool isExpanded, HaloTag tag)
            {
                if (name == null) throw new ArgumentNullException(nameof(name));
                if (nodeNameMap.ContainsKey(name))
                    throw new InvalidOperationException("A node with the specified name already exists.");

                TagPathNode node = new TagPathNode()
                {
                    Name = name,
                    IsExpanded = isExpanded,
                    Tag = tag
                };
                Add(node);

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
