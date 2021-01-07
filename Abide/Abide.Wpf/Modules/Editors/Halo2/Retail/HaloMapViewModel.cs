using Abide.AddOnApi;
using Abide.AddOnApi.Wpf.Halo2;
using Abide.HaloLibrary.Halo2.Retail;
using Abide.Wpf.Modules.AddOns;
using Abide.Wpf.Modules.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Abide.Wpf.Modules.Editors.Halo2.Retail
{
    public sealed class HaloMapViewModel : DependencyObject, IHost
    {
        private static readonly DependencyPropertyKey SelctedTagPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(SelectedTag), typeof(HaloTag), typeof(HaloMapViewModel), new PropertyMetadata(SelectedIndexEntryPropertyChanged));
        private static readonly DependencyPropertyKey FactoryPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Factory), typeof(HaloAddOnFactory), typeof(HaloMapViewModel), new PropertyMetadata(null));
        private static readonly DependencyPropertyKey MapPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Map), typeof(HaloMapFile), typeof(HaloMapViewModel), new PropertyMetadata(MapPropertyChanged));
        private static readonly DependencyPropertyKey TagPathNodesPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(TagPathNodes), typeof(TagPathNode.TagPathNodeCollection), typeof(HaloMapViewModel), new PropertyMetadata());
        private static readonly DependencyPropertyKey ToolListPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ToolList), typeof(ObservableCollection<ToolAddOn>), typeof(HaloMapViewModel), new PropertyMetadata());
        private static readonly DependencyPropertyKey ToolButtonListPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ToolButtonList), typeof(ObservableCollection<ToolButtonAddOn>), typeof(HaloMapViewModel), new PropertyMetadata());
        private static readonly DependencyPropertyKey IsCollapsedPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(IsCollapsed), typeof(bool), typeof(HaloMapViewModel), new PropertyMetadata(true));

        public static readonly DependencyProperty IsCollapsedProperty =
            IsCollapsedPropertyKey.DependencyProperty;
        public static readonly DependencyProperty FactoryProperty =
            FactoryPropertyKey.DependencyProperty;
        public static readonly DependencyProperty MapProperty =
            MapPropertyKey.DependencyProperty;
        public static readonly DependencyProperty SelectedTagProperty =
            SelctedTagPropertyKey.DependencyProperty;
        public static readonly DependencyProperty TagFilterProperty =
            DependencyProperty.Register(nameof(TagFilter), typeof(string), typeof(HaloMapViewModel), new PropertyMetadata(string.Empty, TagFilterPropertyChanged));
        public static readonly DependencyProperty TagPathNodesProperty =
            TagPathNodesPropertyKey.DependencyProperty;
        public static readonly DependencyProperty ToolListProperty =
            ToolListPropertyKey.DependencyProperty;
        public static readonly DependencyProperty ToolButtonListProperty =
            ToolButtonListPropertyKey.DependencyProperty;
        public static readonly DependencyProperty SelectedNodeProperty =
            DependencyProperty.Register(nameof(SelectedNode), typeof(TagPathNode), typeof(HaloMapViewModel), new PropertyMetadata(SelectedNodePropertyChanged));

        public bool IsCollapsed
        {
            get => (bool)GetValue(IsCollapsedProperty);
            private set => SetValue(IsCollapsedPropertyKey, value);
        }
        public HaloAddOnFactory Factory
        {
            get => (HaloAddOnFactory)GetValue(FactoryProperty);
            private set => SetValue(FactoryPropertyKey, value);
        }
        public HaloMapFile Map
        {
            get => (HaloMapFile)GetValue(MapProperty);
            private set => SetValue(MapPropertyKey, value);
        }
        public HaloTag SelectedTag
        {
            get => (HaloTag)GetValue(SelectedTagProperty);
            private set => SetValue(SelctedTagPropertyKey, value);
        }
        public TagPathNode SelectedNode
        {
            get => (TagPathNode)GetValue(SelectedNodeProperty);
            set => SetValue(SelectedNodeProperty, value);
        }
        public string TagFilter
        {
            get => (string)GetValue(TagFilterProperty);
            set => SetValue(TagFilterProperty, value);
        }
        public TagPathNode.TagPathNodeCollection TagPathNodes
        {
            get => (TagPathNode.TagPathNodeCollection)GetValue(TagPathNodesProperty);
            private set => SetValue(TagPathNodesPropertyKey, value);
        }
        public ObservableCollection<ToolAddOn> ToolList
        {
            get => (ObservableCollection<ToolAddOn>)GetValue(ToolListProperty);
            private set => SetValue(ToolListPropertyKey, value);
        }
        public ObservableCollection<ToolButtonAddOn> ToolButtonList
        {
            get => (ObservableCollection<ToolButtonAddOn>)GetValue(ToolButtonListProperty);
            private set => SetValue(ToolButtonListPropertyKey, value);
        }

        public HaloMapViewModel()
        {
            Factory = new HaloAddOnFactory(this);
            Factory.InitializeAddOns();

            ToolList = new ObservableCollection<ToolAddOn>();
            foreach (var addOn in Factory.ToolAddOns)
            {
                ToolList.Add(new ToolAddOn(addOn)
                {
                    Name = addOn.Name,
                    Description = addOn.Description,
                });
            }

            ToolButtonList = new ObservableCollection<ToolButtonAddOn>();
            foreach (var addOn in Factory.ToolButtonAddOns)
            {
                ToolButtonList.Add(new ToolButtonAddOn(addOn)
                {
                    Name = addOn.Name,
                    Description = addOn.Description,
                    ClickCommand = addOn.ClickCommand
                });
            }
        }
        public HaloMapViewModel(HaloMapFile haloMap) : this()
        {
            haloMap.Load();
            Map = haloMap;
        }

        private TagPathNode.TagPathNodeCollection CreateTagTree(string filter = "")
        {
            TagPathNode rootNode = new TagPathNode();

            if (AbideRegistry.TagViewType == AbideRegistry.TagView.TagPath)
            {
                foreach (var tag in GetTags(Map, filter))
                {
                    string path = $"{tag.TagName}.{tag.GroupTag}";
                    string[] parts = path.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                    TagPathNode currentNode = rootNode;

                    for (int i = 0; i < parts.Length - 1; i++)
                    {
                        if (currentNode.Children.ContainsName(parts[i]))
                        {
                            currentNode = currentNode.Children[parts[i]];
                        }
                        else
                        {
                            currentNode = currentNode.Children.Add(parts[i], !IsCollapsed);
                        }
                    }

                    _ = currentNode.Children.Add(parts[parts.Length - 1], false, tag);
                }
            }
            else
            {
                foreach (var tag in GetTags(Map, filter))
                {
                    TagPathNode currentNode = rootNode;
                    string path = $"{tag.TagName}.{tag.GroupTag}";
                    if (!rootNode.Children.ContainsName(tag.GroupTag))
                    {
                        currentNode = rootNode.Children.Add(tag.GroupTag);
                    }
                    else
                    {
                        currentNode = rootNode.Children[tag.GroupTag];
                    }

                    _ = currentNode.Children.Add(path, false, tag);
                }
            }

            return rootNode.Children;
        }
        private static IEnumerable<HaloTag> GetTags(HaloMapFile haloMap, string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return haloMap.GetTagsEnumerator();
            }

            return haloMap.GetTagsEnumerator().Where(e =>
            {
                string tagName = $"{e.TagName}.{e.GroupTag}";
                foreach (var part in tagName.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (part.StartsWith(filter, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }

                return false;
            });
        }
        private static void MapPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is HaloMapViewModel model)
            {
                foreach (var addOn in model.Factory.HaloAddOns)
                {
                    addOn.OnMapLoad();
                }

                TagPathNode.TagPathNodeCollection nodes = model.CreateTagTree();
                nodes.Sort();

                model.TagPathNodes = nodes;
                if (nodes.Count > 0)
                {
                    model.SelectedNode = nodes[0];
                }
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
                    {
                        model.SelectedTag = node.Tag;
                    }
                }
            }
        }
        private static void SelectedIndexEntryPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Check
            if (d is HaloMapViewModel model)
            {
                //Loop
                foreach (var addOn in model.Factory.HaloAddOns)
                {
                    addOn.OnSelectedEntryChanged(); //Call OnSelectedEntryChanged
                }
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
                    {
                        model.SelectedNode = nodes[0];
                    }
                }
            }
        }
        object IHost.Request(IAddOn sender, string request, params object[] args)
        {
            switch (request)
            {
                case "Map":
                case "GetMap":
                    return GetValue(MapProperty);

                case "SelectedTag":
                case "SelectedEntry":
                case "GetSelectedTag":
                case "GetSelectedEntry":
                    return GetValue(SelectedTagProperty);

                default:
                    Console.WriteLine("Unknown AddOn request: {0}", request);
                    System.Diagnostics.Debugger.Break();
                    break;
            }

            return null;
        }

        bool ISynchronizeInvoke.InvokeRequired => throw new NotImplementedException();
        object IHost.Invoke(Delegate method)
        {
            throw new NotImplementedException();
        }
        IAsyncResult ISynchronizeInvoke.BeginInvoke(Delegate method, object[] args)
        {
            throw new NotImplementedException();
        }
        object ISynchronizeInvoke.EndInvoke(IAsyncResult result)
        {
            throw new NotImplementedException();
        }
        object ISynchronizeInvoke.Invoke(Delegate method, object[] args)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Represents a wrapper for a <see cref="ITool"/> AddOn.
    /// </summary>
    public sealed class ToolAddOn : DependencyObject
    {
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register(nameof(Name), typeof(string), typeof(ToolAddOn), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(nameof(Description), typeof(string), typeof(ToolAddOn), new PropertyMetadata(string.Empty));

        public ITool Tool { get; }
        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }
        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public ToolAddOn(ITool tool)
        {
            Tool = tool ?? throw new ArgumentNullException(nameof(tool));
        }
    }

    public sealed class ToolButtonAddOn : DependencyObject
    {
        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.Register(nameof(ClickCommand), typeof(ICommand), typeof(ToolButtonAddOn), new PropertyMetadata());
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register(nameof(Name), typeof(string), typeof(ToolButtonAddOn), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(nameof(Description), typeof(string), typeof(ToolButtonAddOn), new PropertyMetadata(string.Empty));

        public IToolButton ToolButton { get; }
        public ICommand ClickCommand
        {
            get => (ICommand)GetValue(ClickCommandProperty);
            set => SetValue(ClickCommandProperty, value);
        }
        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }
        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public ToolButtonAddOn(IToolButton toolButton)
        {
            ToolButton = toolButton ?? throw new ArgumentNullException(nameof(toolButton));
        }
    }

    public sealed class TagPathNode : DependencyObject
    {
        private static readonly DependencyPropertyKey OwnerPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Owner), typeof(TagPathNodeCollection), typeof(TagPathNode), new PropertyMetadata());
        private static readonly DependencyPropertyKey ChildrenPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Children), typeof(TagPathNodeCollection), typeof(TagPathNode), new PropertyMetadata());

        public static readonly DependencyProperty OwnerProperty =
            OwnerPropertyKey.DependencyProperty;
        public static readonly DependencyProperty ChildrenProperty =
            ChildrenPropertyKey.DependencyProperty;
        public static readonly DependencyProperty TagProperty =
            DependencyProperty.Register(nameof(Tag), typeof(HaloTag), typeof(TagPathNode));
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(TagPathNode));
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register(nameof(Name), typeof(string), typeof(TagPathNode));
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register(nameof(IsExpanded), typeof(bool), typeof(TagPathNode));

        public HaloTag Tag
        {
            get => (HaloTag)GetValue(TagProperty);
            set => SetValue(TagProperty, value);
        }
        public TagPathNodeCollection Owner
        {
            get => (TagPathNodeCollection)GetValue(OwnerProperty);
            private set => SetValue(OwnerPropertyKey, value);
        }
        public TagPathNodeCollection Children
        {
            get => (TagPathNodeCollection)GetValue(ChildrenProperty);
            private set => SetValue(ChildrenPropertyKey, value);
        }
        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }
        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }
        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }

        public TagPathNode()
        {
            Children = new TagPathNodeCollection();
        }
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            //Base procedures
            base.OnPropertyChanged(e);

            //Update collection
            if (Owner != null)
            {
                Owner.OnPropertyChanged(new PropertyChangedEventArgs(e.Property.Name));
            }
        }
        public override string ToString()
        {
            return Name;
        }

        public sealed class TagPathNodeCollection : IEnumerable<TagPathNode>, INotifyCollectionChanged, INotifyPropertyChanged
        {
            private readonly Dictionary<string, int> nodeNameMap = new Dictionary<string, int>();
            private readonly List<TagPathNode> nodes = new List<TagPathNode>();

            public event NotifyCollectionChangedEventHandler CollectionChanged;
            public event PropertyChangedEventHandler PropertyChanged;

            public int Count => nodes.Count;
            public TagPathNode this[int index]
            {
                get
                {
                    if (index < 0 || index >= nodes.Count)
                    {
                        throw new ArgumentOutOfRangeException(nameof(index));
                    }

                    return nodes[index];
                }
            }
            public TagPathNode this[string name]
            {
                get
                {
                    //Check
                    if (name == null)
                    {
                        throw new ArgumentNullException(nameof(name));
                    }

                    if (!nodeNameMap.ContainsKey(name))
                    {
                        throw new ArgumentException("A node with the specified name does not exist.", nameof(name));
                    }

                    //Return
                    return nodes[nodeNameMap[name]];
                }
            }
            public TagPathNodeCollection() { }
            public void OnPropertyChanged(PropertyChangedEventArgs e)
            {
                //Raise PropertyChanged event.
                PropertyChanged?.Invoke(this, e);
            }
            public void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
            {
                //Raise CollectionChanged event.
                CollectionChanged?.Invoke(this, e);
            }
            public bool ContainsName(string name)
            {
                return nodeNameMap.ContainsKey(name);
            }
            public TagPathNode Add(string name)
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                if (nodeNameMap.ContainsKey(name))
                {
                    throw new InvalidOperationException("A node with the specified name already exists.");
                }

                TagPathNode node = new TagPathNode() { Name = name };
                Add(node);  //Add node

                return node;
            }
            public TagPathNode Add(string name, bool isExpanded)
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                if (nodeNameMap.ContainsKey(name))
                {
                    throw new InvalidOperationException("A node with the specified name already exists.");
                }

                TagPathNode node = new TagPathNode()
                {
                    Name = name,
                    IsExpanded = isExpanded
                };
                Add(node);

                return node;
            }
            public TagPathNode Add(string name, bool isExpanded, HaloTag tag)
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                if (nodeNameMap.ContainsKey(name))
                {
                    throw new InvalidOperationException("A node with the specified name already exists.");
                }

                TagPathNode node = new TagPathNode()
                {
                    Name = name,
                    IsExpanded = isExpanded,
                    Tag = tag
                };
                Add(node);

                return node;
            }
            public void Add(TagPathNode node)
            {
                //Check
                if (node == null)
                {
                    throw new ArgumentNullException(nameof(node));
                }

                if (nodeNameMap.ContainsKey(node.Name))
                {
                    throw new InvalidOperationException("A node with the specified name already exists.");
                }

                //Add
                node.Owner = this;
                nodeNameMap.Add(node.Name, nodes.Count);
                nodes.Add(node);

                //Call OnCollectionChanged
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, node));
            }
            public bool Remove(TagPathNode node)
            {
                //Check
                if (node == null)
                {
                    throw new ArgumentNullException(nameof(node));
                }

                if (!nodeNameMap.ContainsKey(node.Name))
                {
                    return false;
                }

                if (nodes[nodeNameMap[node.Name]] != node)
                {
                    return false;
                }

                //Remove
                nodes.RemoveAt(nodeNameMap[node.Name]);
                _ = nodeNameMap.Remove(node.Name);
                node.Owner = null;

                //Call OnCollectionChanged
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, node));

                //Return
                return true;
            }
            public void Clear()
            {
                List<TagPathNode> nodes = new List<TagPathNode>(this.nodes);

                nodes.Clear();
                nodeNameMap.Clear();

                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, nodes));
            }
            public void Sort()
            {
                nodes.Sort(TagPathNodeComparitor);
                nodeNameMap.Clear();

                nodes.ForEach(n =>
                {
                    nodeNameMap.Add(n.Name, nodeNameMap.Count);
                    n.Children.Sort();
                });
            }
            public IEnumerator<TagPathNode> GetEnumerator()
            {
                return nodes.GetEnumerator();
            }

            private int TagPathNodeComparitor(TagPathNode x, TagPathNode y)
            {
                int xc = x.Children.Count, yc = y.Children.Count;
                if (xc > 0 && yc == 0)
                {
                    return -1;
                }
                else if (xc == 0 && yc > 0)
                {
                    return 1;
                }
                else
                {
                    return string.Compare(x.Name, y.Name);
                }
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
