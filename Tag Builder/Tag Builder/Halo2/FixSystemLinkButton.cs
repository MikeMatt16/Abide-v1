using Abide.AddOnApi;
using Abide.AddOnApi.Halo2;
using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using Abide.Tag;
using Abide.Tag.Cache.Generated;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Abide.TagBuilder.Halo2
{
    [AddOn]
    public sealed class FixSystemLinkButton : AbideMenuButton
    {
        public FixSystemLinkButton()
        {
            //
            // this
            //
            Author = "Click16";
            Description = "Do the syslink fix thing.";
            Name = "Fix System Link";
            Icon = Properties.Resources.fix_systemlink_16;
            Click += FixSystemLinkButton_Click;
        }

        private void FixSystemLinkButton_Click(object sender, EventArgs e)
        {
            /*
             * Heh I guess Entity is good for something :P
             */

            //Prepare
            ITagGroup tagGroup = null;
            ITagGroup scenario = new Scenario();
            ITagBlock scenarioBlock = null, simulationDefinitionTableElementBlock = null;
            BlockField simulationDefinitionTableField = null;
            List<IndexEntry> simulationDefinitionEntries = new List<IndexEntry>();
            bool success = false;

            //Build table
            foreach (IndexEntry entry in Map.IndexEntries)
            {
                switch (entry.Root)
                {
                    case "bipd":
                    case "bloc":
                    case "ctrl":
                    case "jpt!":
                    case "mach":
                    case "scen":
                    case "ssce":
                    case "vehi":
                        simulationDefinitionEntries.Add(entry);
                        break;
                    case "eqip":
                    case "garb":
                    case "proj":
                        simulationDefinitionEntries.Add(entry);
                        simulationDefinitionEntries.Add(entry);
                        break;
                    case "weap":
                        simulationDefinitionEntries.Add(entry);
                        simulationDefinitionEntries.Add(entry);
                        simulationDefinitionEntries.Add(entry);
                        break;
                }
            }

            //Read scenario
            using (BinaryReader reader = Map.Scenario.TagData.CreateReader())
            {
                reader.BaseStream.Seek((uint)Map.Scenario.PostProcessedOffset, SeekOrigin.Begin);
                scenario.Read(reader);
            }

            //Re-create simulation definition table
            scenarioBlock = scenario[0];
            simulationDefinitionTableField = (BlockField)scenarioBlock[143];
            simulationDefinitionTableField.BlockList.Clear();
            foreach (IndexEntry entry in simulationDefinitionEntries)
            {
                //Attempt to add tag block
                simulationDefinitionTableElementBlock = simulationDefinitionTableField.Add(out success);
                if (success) simulationDefinitionTableElementBlock[0].Value = entry.Id;
            }

            //Rebuild map
            using (VirtualStream tagDataStream = new VirtualStream(Map.TagDataStream.MemoryAddress))
            using (BinaryWriter writer = tagDataStream.CreateWriter())
            using (BinaryReader reader = Map.TagDataStream.CreateReader())
            {
                //Loop
                foreach (IndexEntry entry in Map.IndexEntries.Where(ie => ie.Offset > 0 && ie.Size > 0))
                {
                    //Read (unless it's our modified scenario)
                    if (entry != Map.Scenario)
                    {
                        tagGroup = TagLookup.CreateTagGroup(entry.Root);
                        reader.BaseStream.Seek(entry.Offset, SeekOrigin.Begin);
                        tagGroup.Read(reader);
                    }
                    else tagGroup = scenario;

                    //Create buffer
                    using (VirtualStream stream = new VirtualStream(tagDataStream.Position))
                    using (BinaryWriter tagWriter = stream.CreateWriter())
                    using (BinaryReader tagReader = stream.CreateReader())
                    {
                        //Write
                        tagGroup.Write(tagWriter);

                        //Recalculate raw addresses
                        Helper.RecalculateRawAddresses(entry.Raws, entry.Root, stream, tagReader, tagWriter);

                        //Setup tag
                        entry.Offset = (uint)stream.MemoryAddress;
                        entry.Size = (int)stream.Length;

                        //Write to tag data stream
                        writer.Write(stream.ToArray());
                    }
                }

                //Align
                tagDataStream.Align(4096);

                //Swap
                Map.SwapTagBuffer(tagDataStream.ToArray(), tagDataStream.MemoryAddress);
            }
        }
    }
}
