using Abide.AddOnApi;
using Abide.AddOnApi.Halo2;
using Abide.Ifp;
using System;
using System.IO;

namespace Tag_Editor
{
    public partial class TagEditor : AbideTool
    {
        private readonly DataWrapper wrapper;

        public TagEditor()
        {
            InitializeComponent();
            wrapper = new DataWrapper();
        }

        private void TagEditor_SelectedEntryChanged(object sender, EventArgs e)
        {
            //Check selected entry
            if (SelectedEntry == null) return;
            IfpDocument doc = new IfpDocument();

            //Get Plugin
            string pluginName = Path.Combine(Files.Halo2PluginsDirectory, $"{SelectedEntry.Root.Tag.Replace('<', '_').Replace('>', '_')}.ent");
            if (File.Exists(pluginName)) doc.Load(pluginName);

            //Clear
            wrapper.Clear();

            //Load
            using(BinaryReader reader = new BinaryReader(SelectedEntry.TagData))
            {
            }
        }

        private void TagEditor_Initialize(object sender, AddOnHostEventArgs e)
        {
        }
    }
}
