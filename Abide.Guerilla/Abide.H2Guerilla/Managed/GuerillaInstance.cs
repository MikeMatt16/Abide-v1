using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Abide.H2Guerilla.Managed
{
    /// <summary>
    /// Represents a H2 Guerilla instance.
    /// </summary>
    public sealed class GuerillaInstance : IEnumerable<TagBlockDefinition>
    {
        /// <summary>
        /// Gets and returns a tag block definition at the given index.
        /// </summary>
        /// <param name="index">The zero-based index of the tag block definition.</param>
        /// <returns>A tag block definition instance.</returns>
        public TagBlockDefinition this[int index]
        {
            get
            {
                if (index < 0 || index >= tagBlocks.Count) throw new ArgumentOutOfRangeException(nameof(index));
                return tagBlocks[index];
            }
        }
        /// <summary>
        /// Gets and returns the number of tag groups.
        /// </summary>
        public int TagGroupCount
        {
            get { return tagGroups.Count; }
        }
        /// <summary>
        /// Gets and returns the number of tag blocks.
        /// </summary>
        public int TagBlockCount
        {
            get { return tagBlocks.Count; }
        }
        
        private readonly List<TagBlockDefinition> tagBlocks;
        private readonly Dictionary<string, int> blockNameLookup;
        private readonly Dictionary<string, int> groupTagLookup;
        private readonly Dictionary<int, int> blockAddressLookup;
        private readonly Dictionary<int, int> groupAddressLookup;
        private readonly List<TagGroupDefinition> tagGroups;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GuerillaInstance"/> class.
        /// </summary>
        public GuerillaInstance()
        {
            //Prepare
            tagBlocks = new List<TagBlockDefinition>();
            blockNameLookup = new Dictionary<string, int>();
            groupTagLookup = new Dictionary<string, int>();
            blockAddressLookup = new Dictionary<int, int>();
            groupAddressLookup = new Dictionary<int, int>();
            tagGroups = new List<TagGroupDefinition>(Guerilla.NumberOfTagLayouts);
        }
        /// <summary>
        /// Processes the supplied guerilla executable and language library retrieving all tag group and tag block definitions. 
        /// </summary>
        /// <param name="guerillaFileName">The path of the H2Guerilla executable.</param>
        /// <param name="languageFileName">The path of the H2alang dynamic link library.</param>
        public void Process(string guerillaFileName, string languageFileName)
        {
            //Clear
            tagBlocks.Clear();
            blockNameLookup.Clear();
            blockAddressLookup.Clear();
            tagGroups.Clear();

            //Write
            Console.WriteLine("Processing Guerilla...");
            Console.WriteLine("Guerilla path: {0}", guerillaFileName);
            Console.WriteLine("Library path: {0}", languageFileName);

            //Initialize stream
            using (FileStream fs = new FileStream(guerillaFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (H2Guerilla.GuerillaBinaryReader reader = new H2Guerilla.GuerillaBinaryReader(languageFileName, fs))
            {
                //Prepare
                for (int i = 0; i < Guerilla.NumberOfTagLayouts; i++)
                {
                    //Goto
                    fs.Seek(Guerilla.TagLayoutTableAddress - Guerilla.BaseAddress + (i * 4), SeekOrigin.Begin);
                    int layoutAddress = reader.ReadInt32(); //Read layout address

                    //Goto
                    fs.Seek(layoutAddress - Guerilla.BaseAddress, SeekOrigin.Begin);
                    tagGroups.Add(new TagGroupDefinition(reader));   //Read Tag group

                    //Read
                    Console.WriteLine("Reading tag_group {0}...", tagGroups[i].GroupTag);
                    Console.WriteLine("Processing...");

                    //Add
                    if (!groupAddressLookup.ContainsKey(tagGroups[i].DefinitionAddress))
                        groupAddressLookup.Add(tagGroups[i].DefinitionAddress, i);
                    if (!groupTagLookup.ContainsKey(tagGroups[i].GroupTag))
                        groupTagLookup.Add(tagGroups[i].GroupTag, i);

                    //Read tag block definition
                    ReadTagBlockDefinition(fs, reader, tagGroups[i].DefinitionAddress, null);

                    //Set
                    tagBlocks[blockAddressLookup[tagGroups[i].DefinitionAddress]].IsTagGroup = true;
                }
            }
        }
        /// <summary>
        /// Reads a tag block using the supplied stream, guerilla reader, definition address, sound override boolean, and parent tag block definition.
        /// </summary>
        /// <param name="input">The guerilla stream.</param>
        /// <param name="reader">The guerilla reader.</param>
        /// <param name="address">The address of the tag block definition.</param>
        /// <param name="parent">The parent tag block.</param>
        private void ReadTagBlockDefinition(Stream input, H2Guerilla.GuerillaBinaryReader reader, int address, TagBlockDefinition parent)
        {
            //Check
            if (blockAddressLookup.ContainsKey(address)) return;

            //Goto
            input.Seek(address - Guerilla.BaseAddress, SeekOrigin.Begin);
            TagBlockDefinition definition = new TagBlockDefinition(reader) { Address = address, };

            //Initialize tag field sets
            definition.tagFieldSets = new TagFieldSetDefinition[definition.FieldSetCount];

            //Processing
            Console.WriteLine("Processing tag_block {0}", definition.Name);

            //Check
            if (definition.Name == "hud_waypoint_arrow_block" && definition.Address == 9689580)
                definition.Name = "hud_globals_waypoint_arrow_block";

            //Check
            if (definition.Name == "sound_block")
            {
                //Override sound block
                definition.FieldSetCount = 1;
                definition.FieldSetsAddress = 0x957870;
                definition.FieldSetLatestAddress = 0x906178;
                definition.tagFieldSets = new TagFieldSetDefinition[1];
                definition.TagFieldSetLatestIndex = 0;
                definition.tagFieldSets[0] = new TagFieldSetDefinition
                {
                    Version = new TagFieldSetDefinition.FieldSetVersion { FieldsAddress = 0x906178, Index = 0, SizeOf = -1, UpgradeProcedure = 0 },
                    Size = 172,
                    AlignmentBit = 0,
                    ParentVersionIndex = -1,
                    FieldsAddress = 0x906178,
                    SizeString = "sizeof(sound_definition)",
                };
            }
            else
            {
                //Read field set definitions
                for (int i = 0; i < definition.FieldSetCount; i++)
                {
                    //Goto
                    input.Seek(definition.FieldSetsAddress + (i * 76) - Guerilla.BaseAddress, SeekOrigin.Begin);
                    definition.tagFieldSets[i] = new TagFieldSetDefinition();
                    definition.tagFieldSets[i].Read(reader);

                    //Check
                    if (definition.FieldSetLatestAddress == definition.tagFieldSets[i].Address)
                        definition.TagFieldSetLatestIndex = i;
                }
            }

            //Read fields for each field set
            for (int i = 0; i < definition.FieldSetCount; i++)
            {
                //Goto
                input.Seek(definition.tagFieldSets[i].FieldsAddress - Guerilla.BaseAddress, SeekOrigin.Begin);

                //Read fields for field set
                TagFieldDefinition field = null; int index = 0;
                do
                {
                    //Store address
                    long fieldAddress = input.Position;

                    //Read the field type
                    switch ((FieldType)reader.ReadInt16())
                    {
                        case FieldType.FieldSkip: field = new TagFieldDefinition(); break;
                        case FieldType.FieldTagReference: field = new TagFieldTagReferenceDefinition(); break;
                        case FieldType.FieldStruct: field = new TagFieldStructDefinition(); break;
                        case FieldType.FieldData: field = new TagFieldDataDefinition(); break;
                        case FieldType.FieldByteFlags:
                        case FieldType.FieldLongFlags:
                        case FieldType.FieldWordFlags:
                        case FieldType.FieldCharEnum:
                        case FieldType.FieldEnum:
                        case FieldType.FieldLongEnum: field = new TagFieldEnumDefinition(); break;
                        case FieldType.FieldCharBlockIndex2:
                        case FieldType.FieldShortBlockIndex2:
                        case FieldType.FieldLongBlockIndex2: field = new TagFieldBlockIndexCustomSearchDefinition(); break;
                        case FieldType.FieldExplanation: field = new TagFieldExplanationDefinition(); break;
                        case FieldType.FieldArrayStart: field = new TagFieldArrayStartDefinition(); break;
                        default: field = new TagFieldDefinition(); break;
                    }

                    //Goto field
                    input.Seek(fieldAddress, SeekOrigin.Begin);
                    field.Read(reader);

                    //Add
                    definition.FieldSets[i].Fields.Add(field);

                    //Handle special field type
                    switch (field.Type)
                    {
                        case FieldType.FieldByteBlockFlags:
                        case FieldType.FieldWordBlockFlags:
                        case FieldType.FieldLongBlockFlags:
                        case FieldType.FieldCharInteger:
                        case FieldType.FieldShortInteger:
                        case FieldType.FieldLongInteger:
                        case FieldType.FieldBlock:
                            if (field.DefinitionAddress != 0)
                                ReadTagBlockDefinition(input, reader, field.DefinitionAddress, definition);
                            break;
                        case FieldType.FieldStruct:
                            if(field is TagFieldStructDefinition structField)
                                ReadTagBlockDefinition(input, reader, structField.BlockDefinitionAddresss, definition);
                            break;
                    }

                    //Goto next field
                    input.Seek(fieldAddress + field.Nudge, SeekOrigin.Begin);

                    //Increment
                    index++;
                }
                while (field.Type != FieldType.FieldTerminator);
            }
            
            //Check
            switch (definition.Name)
            {
                case "materials_block":
                    definition.TagFieldSetLatestIndex = 0;
                    if (definition.FieldSets[definition.TagFieldSetLatestIndex].Fields.Count == 5)
                    {
                        definition.Name = "physics_model_materials_block";
                        definition.DisplayName = "physics_model_materials_block";
                    }
                    break;
                case "hud_globals_block":
                case "global_new_hud_globals_struct_block":
                case "sound_gestalt_promotions_block":
                case "sound_block":
                case "tag_block_index_struct_block":
                case "vertex_shader_classification_block":
                    definition.TagFieldSetLatestIndex = 0;
                    break;
                case "instantaneous_response_damage_effect_marker_struct_block":
                case "instantaneous_response_damage_effect_struct_block":
                    definition.TagFieldSetLatestIndex = 1;
                    break;
                case "animation_pool_block":
                    definition.TagFieldSetLatestIndex = 4;
                    break;
            }

            //Add
            if (!blockNameLookup.ContainsKey(definition.Name)) blockNameLookup.Add(definition.Name, tagBlocks.Count);
            blockAddressLookup.Add(address, tagBlocks.Count);
            tagBlocks.Add(definition);
        }
        /// <summary>
        /// Converts an underscore separated name to a pascal cased string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A PascalCasedString.</returns>
        private string ConvertToPascalCase(string name)
        {
            StringBuilder builder = new StringBuilder();
            string[] parts = name.Split('_', ' ');
            foreach (string part in parts)
            {
                StringBuilder partBuilder = new StringBuilder(part);
                partBuilder[0] = part.ToUpper()[0];
                builder.Append(partBuilder.ToString());
            }
            return builder.ToString();
        }
        /// <summary>
        /// Converts an underscore separated name to a display name string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A Display Name String.</returns>
        private string ConvertDisplayName(string name)
        {
            StringBuilder builder = new StringBuilder();
            string[] parts = name.Split('_', ' ');
            foreach (string part in parts)
            {
                StringBuilder partBuilder = new StringBuilder(part);
                partBuilder[0] = part.ToUpper()[0];
                builder.Append(partBuilder.ToString() + " ");
            }
            return builder.ToString().Trim();
        }
        /// <summary>
        /// Returns an array of the tag groups currently loaded into this instance.
        /// </summary>
        /// <returns>An array of <see cref="TagGroupDefinition"/> elements.</returns>
        public TagGroupDefinition[] GetTagGroups()
        {
            return tagGroups.ToArray();
        }
        /// <summary>
        /// Returns an array of the tag blocks currently loaded into this instance.
        /// </summary>
        /// <returns>An array of <see cref="TagBlockDefinition"/> elements.</returns>
        public TagBlockDefinition[] GetTagBlocks()
        {
            return tagBlocks.ToArray();
        }
        /// <summary>
        /// Searches the tag group list for a tag group with the specified address.
        /// </summary>
        /// <param name="address">The address of the tag group.</param>
        /// <returns>A tag group instance if one is found; otherwise null.</returns>
        public TagGroupDefinition FindTagGroup(int address)
        {
            //Return
            if (groupAddressLookup.ContainsKey(address)) return tagGroups[groupAddressLookup[address]];
            return null;
        }
        /// <summary>
        /// Searches the tag group list for a tag group with the specified tag group.
        /// </summary>
        /// <param name="tagGroup">The tag group string.</param>
        /// <returns>A tag group instance if one is found; otherwise null.</returns>
        public TagGroupDefinition FindTagGroup(string tagGroup)
        {
            //Return
            if (groupTagLookup.ContainsKey(tagGroup)) return tagGroups[groupTagLookup[tagGroup]];
            return null;
        }
        /// <summary>
        /// Searches the tag block list for a tag block with the specified address.
        /// </summary>
        /// <param name="address">The address of the tag block.</param>
        /// <returns>A tag block instance if one is found; otherwise null.</returns>
        public TagBlockDefinition FindTagBlock(int address)
        {
            //Return
            if (blockAddressLookup.ContainsKey(address)) return tagBlocks[blockAddressLookup[address]];
            return null;
        }
        /// <summary>
        /// Searches the tag block list for a tag block with the specified name.
        /// </summary>
        /// <param name="name">The name of the tag block.</param>
        /// <returns>A tag block instance if one is found; otherwise null.</returns>
        public TagBlockDefinition FindTagBlock(string name)
        {
            //Return
            if (blockNameLookup.ContainsKey(name)) return tagBlocks[blockNameLookup[name]];
            return null;
        }
        /// <summary>
        /// Gets an enumerator that iterates this instance.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<TagBlockDefinition> GetEnumerator()
        {
            return tagBlocks.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return tagBlocks.GetEnumerator();
        }
    }
}
