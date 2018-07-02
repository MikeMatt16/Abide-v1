using Abide.AddOnApi;
using Abide.AddOnApi.Halo2;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.Tag;
using Abide.Tag.Cache.Generated;
using System;
using System.Windows.Forms;

namespace Abide.TagBuilder.Halo2
{
    [AddOn]
    public sealed class NewTagButton : AbideMenuButton
    {
        public NewTagButton() : base()
        {
            //
            // this
            //
            Author = "Click16";
            Description = "Create a new tag.";
            Name = "Create New Tag";
            Icon = Properties.Resources.tag_new_16;
            Click += NewTagButton_Click;
        }

        private void NewTagButton_Click(object sender, EventArgs e)
        {
            //Get last
            IndexEntry last = Map.IndexEntries.Last;
            if (last.Root != HaloTags.ugh_) return;
            
            //Prepare
            using (GroupDialog groupDlg = new GroupDialog())
            {
                //Show
                if (groupDlg.ShowDialog() == DialogResult.OK)
                    using (NameDialog nameDialog = new NameDialog())
                        if (nameDialog.ShowDialog() == DialogResult.OK)
                        {
                            //Prepare
                            string tagName = nameDialog.TagName;
                            Group tagGroup = TagLookup.CreateTagGroup(groupDlg.SelectedGroup);

                            //Check
                            if (tagGroup != null)
                                if (groupDlg.SelectedGroup == HaloTags.ltmp || groupDlg.SelectedGroup == HaloTags.sbsp)
                                {
                                    //Ask for which BSP index to build into
                                    using (StructureBspSelectDialog bspSelectDlg = new StructureBspSelectDialog(Map.Scenario))
                                        if (bspSelectDlg.ShowDialog() == DialogResult.OK && bspSelectDlg.SelectedBlockIndex >= 0)
                                        {
                                            //Create tag
                                            ScenarioStructureTag tag = new ScenarioStructureTag(bspSelectDlg.SelectedBlockIndex)
                                            {
                                                TagGroup = tagGroup,
                                                Name = tagName,
                                                Id = last.Id,
                                            };

                                            //Add
                                            Map.AddScenarioStructureTags(tag);
                                        }
                                }
                                else
                                {
                                    //Create tag
                                    Tag tag = new Tag()
                                    {
                                        TagGroup = tagGroup,
                                        Name = tagName,
                                        Id = last.Id,
                                    };

                                    //Add
                                    Map.AddTags(tag);
                                }

                            //Reload
                            Host.Request(this, "ReloadMap");
                        }
            }
        }

        private void Tag_NewScenarioStructureTag(int index, string name, Group newTagGroup)
        {
            //Prepare
            TagId newId = Map.IndexEntries.Last.Id;
            ScenarioStructureTag newTag = new ScenarioStructureTag(index)
            {
                Id = newId,
                Name = name,
                TagGroup = newTagGroup
            };

            //Add
            Map.AddScenarioStructureTags(newTag);

            //Reload
            Host.Request(this, "ReloadMap");
        }

        private void Tag_NewTag(string name, Group newTagGroup)
        {
            //Prepare
            TagId newId = Map.IndexEntries.Last.Id;
            Tag newTag = new Tag()
            {
                Id = newId,
                Name = name,
                TagGroup = newTagGroup
            };

            //Add
            Map.AddTags(newTag);

            //Reload
            Host.Request(this, "ReloadMap");
        }
    }
}
