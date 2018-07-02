using Abide.AddOnApi;
using Abide.AddOnApi.Halo2;
using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using Abide.Tag;
using Abide.Tag.Cache.Generated;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Abide.TagBuilder.Halo2
{
    [AddOn]
    public sealed class TagDuplicateButton : AbideMenuButton
    {
        public TagDuplicateButton() : base()
        {
            //
            // this
            //
            Author = "Click16";
            Description = "Duplicate a tag.";
            Name = "Duplicate Tag";
            Icon = Properties.Resources.tag_duplicate_16;
            Click += TagDuplicator_Click;
        }

        private void TagDuplicator_Click(object sender, EventArgs e)
        {
            //Prepare
            Group newTagGroup = null;
            IndexEntry last = Map.IndexEntries.Last;

            //Check
            if (SelectedEntry != null)
            {
                //Get name
                string tagName = string.Empty;
                using (NameDialog nameDlg = new NameDialog())
                {
                    //Setup
                    nameDlg.TagName = SelectedEntry.Filename;

                    //Show
                    DialogResult result = DialogResult.OK;
                    while (Map.IndexEntries.Count(i => i.Filename == nameDlg.TagName && i.Root == SelectedEntry.Root) > 0 && result == DialogResult.OK)
                        result = nameDlg.ShowDialog();

                    //Check
                    if (result == DialogResult.OK)
                        tagName = nameDlg.TagName;
                    else return;
                }

                //Initialize BinaryReader instance for the selected tag
                using (BinaryReader reader = SelectedEntry.TagData.CreateReader())
                {
                    //Create tag group for selected entry
                    newTagGroup = TagLookup.CreateTagGroup(SelectedEntry.Root);

                    //Goto tag data and read
                    SelectedEntry.TagData.Seek((uint)SelectedEntry.PostProcessedOffset, SeekOrigin.Begin);
                    newTagGroup.Read(reader);
                }

                //Check the root for a scenario structure tag
                if (SelectedEntry.Root == HaloTags.sbsp || SelectedEntry.Root == HaloTags.ltmp)
                {
                    //Scenario dialog
                    using (StructureBspSelectDialog scenarioDlg = new StructureBspSelectDialog(Map.Scenario))
                        if (scenarioDlg.ShowDialog() == DialogResult.OK && scenarioDlg.SelectedBlockIndex >= 0)
                        {
                            //Prepare
                            ScenarioStructureTag bspTag = new ScenarioStructureTag(scenarioDlg.SelectedBlockIndex)
                            {
                                SourceEntry = SelectedEntry,
                                Name = tagName,
                                Id = last.Id,
                                TagGroup = newTagGroup
                            };

                            //Add
                            Map.AddScenarioStructureTags(bspTag);

                            //Reload
                            Host.Request(this, "ReloadMap");
                        }
                }
                else
                {
                    //Prepare
                    Tag tag = new Tag()
                    {
                        SourceEntry = SelectedEntry,
                        Name = tagName,
                        Id = last.Id,
                        TagGroup = newTagGroup
                    };

                    //Add
                    Map.AddTags(tag);

                    //Reload
                    Host.Request(this, "ReloadMap");
                }
            }
        }
    }
}
