using Abide.DebugXbox;
using Abide.HaloLibrary.Halo2.Retail;
using Abide.Ifp;
using Abide.Tag;
using Abide.Tag.Cache.Generated;
using Abide.Wpf.Modules.ViewModel;
using Abide.Wpf.Modules.Win32;
using Abide.Wpf.Properties;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.TagEditor
{
    public sealed class TagEditorViewModel : BaseAddOnViewModel
    {
        private static readonly DependencyPropertyKey TagGroupPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(TagGroup), typeof(TagGroupViewModel), typeof(TagEditorViewModel), new PropertyMetadata());

        public static readonly DependencyProperty SelectedPluginSetProperty =
            DependencyProperty.Register(nameof(SelectedPluginSet), typeof(PluginSet), typeof(TagEditorViewModel), new PropertyMetadata(SelectedPluginSetChanged));

        public static readonly DependencyProperty XboxProperty =
            DependencyProperty.Register(nameof(Xbox), typeof(Xbox), typeof(TagEditorViewModel));
        public static readonly DependencyProperty TagGroupProperty =
            TagGroupPropertyKey.DependencyProperty;

        private TagData tagData = null;

        public ObservableCollection<PluginSet> PluginSets { get; } = new ObservableCollection<PluginSet>();
        public PluginSet SelectedPluginSet
        {
            get => (PluginSet)GetValue(SelectedPluginSetProperty);
            set => SetValue(SelectedPluginSetProperty, value);
        }
        public TagGroupViewModel TagGroup
        {
            get => (TagGroupViewModel)GetValue(TagGroupProperty);
            private set => SetValue(TagGroupPropertyKey, value);
        }
        public Xbox Xbox
        {
            get => (Xbox)GetValue(XboxProperty);
            set => SetValue(XboxProperty, value);
        }
        public ICommand SaveCommand { get; }
        public ICommand PokeCommand { get; }

        public TagEditorViewModel()
        {
            SaveCommand = new ActionCommand(SaveTag);
            PokeCommand = new ActionCommand(PokeTag, o =>
            {
                if (Xbox == null)
                {
                    var xboxes = NameAnsweringProtocol.Discover(10);
                    if (xboxes.Any())
                    {
                        Xbox = xboxes.First();
                        Xbox.Connect();
                    }
                }

                return Xbox?.Connected ?? false;
            });

            if (Directory.Exists(AbideRegistry.Halo2PluginsDirectory))
            {
                PluginSets.Add(new PluginSet("Primary Plugins", AbideRegistry.Halo2PluginsDirectory));
            }

            foreach (var pluginSet in AbideRegistry.Halo2PluginSets)
            {
                if (Directory.Exists(pluginSet.Item2))
                {
                    PluginSets.Add(new PluginSet(pluginSet.Item1, pluginSet.Item2));
                }
            }

            PluginSets.Add(new PluginSet("Built-in Tag Layouts", "<3"));


            if (PluginSets.Count > 0)
            {
                SelectedPluginSet = PluginSets
                    .FirstOrDefault(s => s.Name == Settings.Default.LastChosenHalo2PluginSet)
                    ?? PluginSets[0];
            }
        }

        private void PokeTag(object obj)
        {
            using (var stream = Xbox.MemoryStream)
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                TagGroup.TagGroup.Overwrite(writer);
            }
        }

        private void SaveTag(object obj)
        {
            if (tagData != null)
            {
                using (var writer = tagData.Stream.CreateWriter())
                {
                    TagGroup.TagGroup.Overwrite(writer);
                }

                Map.OverwriteTagData(tagData);
                Map.RecalculateChecksum();
            }
        }

        protected override void OnMapChange()
        {
            TagGroup = new TagGroupViewModel(Map);
        }
        protected override void OnSelectedTagChanged()
        {
            if (SelectedTag != null)
            {
                Group tagGroup = TagLookup.CreateTagGroup(SelectedTag.Tag);
                if (SelectedPluginSet.ContainsTag(SelectedTag.Tag))
                {
                    tagGroup = new IfpTagGroup(SelectedPluginSet[SelectedTag.Tag], tagGroup.GroupName);
                }

                if (tagGroup != null)
                {
                    if (tagData != null)
                    {
                        tagData.Dispose();
                    }

                    tagData = Map.ReadTagData(SelectedTag);
                    BinaryReader reader = tagData.Stream.CreateReader();
                    bool success = false;

                    try
                    {
                        _ = reader.BaseStream.Seek(SelectedTag.MemoryAddress, SeekOrigin.Begin);
                        tagGroup.Read(reader);
                        success = true;
                    }
                    finally
                    {
                        if (!success)
                        {
                            tagGroup = null;
                        }
                    }
                }

                TagGroup.SetTagGroup(tagGroup);
            }
        }

        private static void SelectedPluginSetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TagEditorViewModel tagEditor && e.NewValue is PluginSet pluginSet)
            {
                Settings.Default.LastChosenHalo2PluginSet = pluginSet.Name;
                Settings.Default.Save();

                if (tagEditor.TagGroup != null)
                {
                    Group tagGroup;
                    var group = tagEditor.TagGroup;
                    var document = pluginSet[group.GroupTag];
                    if (document != null)
                    {
                        tagGroup = new IfpTagGroup(document);
                    }
                    else
                    {
                        tagGroup = TagLookup.CreateTagGroup(group.GroupTag);
                    }

                    if (tagGroup != null)
                    {
                        using (var data = tagEditor.Map.ReadTagData(tagEditor.SelectedTag))
                        {
                            BinaryReader reader = data.Stream.CreateReader();
                            bool success = false;

                            try
                            {
                                _ = reader.BaseStream.Seek(tagEditor.SelectedTag.MemoryAddress, SeekOrigin.Begin);
                                tagGroup.Read(reader);
                                success = true;
                            }
                            finally
                            {
                                if (!success)
                                {
                                    tagGroup = null;
                                }
                            }
                        }

                        group.SetTagGroup(tagGroup);
                    }
                }
            }
        }

        public class PluginSet : BaseViewModel
        {
            private readonly Dictionary<string, IfpDocument> plugins = new Dictionary<string, IfpDocument>();

            public IfpDocument this[string tag]
            {
                get
                {
                    tag = tag.Replace("<", "_").Replace(">", "_").PadRight(4, ' ');
                    if (plugins.ContainsKey(tag))
                    {
                        return plugins[tag];
                    }

                    return null;
                }
            }
            public string Name { get; }
            public string DirectoryPath { get; }

            public PluginSet(string name, string directoryPath)
            {
                Name = name;
                DirectoryPath = directoryPath;

                try
                {
                    if (Directory.Exists(directoryPath))
                    {
                        foreach (string file in Directory.GetFiles(directoryPath))
                        {
                            if (Path.GetExtension(file) == ".ifp" || Path.GetExtension(file) == ".ent")
                            {
                                string tag = Path.GetFileName(file).Substring(0, 4);
                                IfpDocument document = new IfpDocument();
                                document.Load(file);

                                if (!plugins.ContainsKey(tag))
                                {
                                    plugins.Add(tag, document);
                                }
                            }
                        }
                    }
                }
                catch { }
            }
            public bool ContainsTag(string tag)
            {
                return plugins.ContainsKey(tag);
            }
            public override string ToString()
            {
                return Name ?? base.ToString();
            }
        }
    }
}
