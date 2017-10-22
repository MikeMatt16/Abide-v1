using Abide.HaloLibrary.Halo2Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Abide.Guerilla.Managed
{
    /// <summary>
    /// Represents a H2 Guerilla Reader.
    /// </summary>
    public sealed class GuerillaReader : IEnumerable<TagBlockDefinition>
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
        private readonly Dictionary<string, int> nameLookup;
        private readonly Dictionary<int, int> addressLookup;
        private readonly List<TagGroupDefinition> tagGroups;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuerillaReader"/> class.
        /// </summary>
        public GuerillaReader()
        {
            //Prepare
            tagBlocks = new List<TagBlockDefinition>();
            addressLookup = new Dictionary<int, int>();
            nameLookup = new Dictionary<string, int>();
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
            nameLookup.Clear();
            addressLookup.Clear();
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
                    tagGroups.Add(new TagGroupDefinition(reader.ReadTagGroup()));   //Read Tag group

                    //Read
                    Console.WriteLine("Reading tag_group {0}...", tagGroups[i].GroupTag);
                    Console.WriteLine("Processing...");

                    //Read tag block definition
                    ReadTagBlockDefinition(fs, reader, tagGroups[i].DefinitionAddress, tagGroups[i].GroupTag == HaloTags.snd_, null);

                    //Set
                    tagBlocks[addressLookup[tagGroups[i].DefinitionAddress]].IsTagGroup = true;
                }
            }
        }
        /// <summary>
        /// Post-processes the definitions.
        /// This function PascalCases all tag blocks and changes the display names to Capital First Letters With Spaces.
        /// </summary>
        public void PostProcess()
        {
            foreach (TagBlockDefinition block in tagBlocks)
            {
                block.Name = ConvertToPascalCase(block.Name);
                block.DisplayName = ConvertDisplayName(block.DisplayName);
            }
        }
        /// <summary>
        /// Reads a tag block using the supplied stream, guerilla reader, definition address, sound override boolean, and parent tag block definition.
        /// </summary>
        /// <param name="input">The guerilla stream.</param>
        /// <param name="reader">The guerilla reader.</param>
        /// <param name="address">The address of the tag block definition.</param>
        /// <param name="isSound">The sound override boolean.</param>
        /// <param name="parent">The parent tag block.</param>
        private void ReadTagBlockDefinition(Stream input, H2Guerilla.GuerillaBinaryReader reader, int address, bool isSound, TagBlockDefinition parent)
        {
            //Check
            if (addressLookup.ContainsKey(address)) return;

            //Goto
            input.Seek(address - Guerilla.BaseAddress, SeekOrigin.Begin);
            H2Guerilla.TagBlockDefinition tagBlockDefinition = reader.ReadTagBlockDefinition();
            TagBlockDefinition definition = new TagBlockDefinition(tagBlockDefinition);
            definition.Address = address;

            //Check
            if (isSound)
            {
                definition.Address = 0;
                definition.FieldSetCount = 6;
                definition.FieldSetLatestAddress = 0x906178;
            }

            //Initialize tag field sets
            definition.tagFieldSets = new TagFieldSet[definition.FieldSetCount];
            definition.tagFields = new List<TagFieldDefinition>[definition.FieldSetCount];

            //Add
            if (!nameLookup.ContainsKey(definition.Name)) nameLookup.Add(definition.Name, tagBlocks.Count);
            addressLookup.Add(address, tagBlocks.Count);
            tagBlocks.Add(definition);

            //Processing
            Console.WriteLine("Processing tag_block {0}", definition.Name);

            //Loop through each field set
            for (int i = 0; i < definition.FieldSetCount; i++)
            {
                if (isSound)
                {
                    definition.tagFieldSets[i] = new TagFieldSet();
                    switch (i)
                    {
                        case 0:
                            input.Seek(0x957A60 - Guerilla.BaseAddress, SeekOrigin.Begin);
                            definition.tagFieldSets[i].Read(reader);
                            break;
                        case 1:
                        case 2:
                        case 3:
                            input.Seek(0x957448 - Guerilla.BaseAddress, SeekOrigin.Begin);
                            definition.tagFieldSets[i].Read(reader);
                            break;
                        case 4:
                            definition.tagFieldSets[i].Version.FieldsAddress = 0x906078;
                            definition.tagFieldSets[i].Version.Index = 0;
                            definition.tagFieldSets[i].Version.UpgradeProcedure = 0x49F700;
                            definition.tagFieldSets[i].Version.SizeOf = -1;
                            definition.tagFieldSets[i].Size = 176;
                            definition.tagFieldSets[i].AlignmentBit = 0;
                            definition.tagFieldSets[i].ParentVersionIndex = -1;
                            definition.tagFieldSets[i].FieldsAddress = 0x906078;
                            definition.tagFieldSets[i].SizeString = "sizeof(struct sound_definition_v1)";
                            break;
                        case 5:
                            definition.tagFieldSets[i].Version.FieldsAddress = 0x906178;
                            definition.tagFieldSets[i].Version.Index = 0;
                            definition.tagFieldSets[i].Version.UpgradeProcedure = 0;
                            definition.tagFieldSets[i].Version.SizeOf = -1;
                            definition.tagFieldSets[i].Size = 172;
                            definition.tagFieldSets[i].AlignmentBit = 0;
                            definition.tagFieldSets[i].ParentVersionIndex = -1;
                            definition.tagFieldSets[i].FieldsAddress = 0x906178;
                            definition.tagFieldSets[i].SizeString = "sizeof(sound_definition)";
                            break;
                    }
                }
                else
                {
                    //Goto
                    input.Seek(definition.FieldSetsAddress + (i * 76) - Guerilla.BaseAddress, SeekOrigin.Begin);
                    definition.tagFieldSets[i] = new TagFieldSet();
                    definition.tagFieldSets[i].Read(reader);
                }

                //Check
                if (definition.FieldSetLatestAddress == definition.tagFieldSets[i].Address)
                    definition.TagFieldSetLatestIndex = i;

                //Initialize the tag fields list
                definition.tagFields[i] = new List<TagFieldDefinition>();

                //Goto
                input.Seek(definition.tagFieldSets[i].FieldsAddress - Guerilla.BaseAddress, SeekOrigin.Begin);

                //Loop
                TagFieldDefinition field = new TagFieldDefinition();
                do
                {
                    //Store address
                    long fieldAddress = input.Position;

                    //Read the field type
                    switch ((FieldType)reader.ReadInt16())
                    {
                        case FieldType.FieldTagReference: field = new TagReferenceDefinition(); break;
                        case FieldType.FieldStruct: field = new TagStructDefinition(); break;
                        case FieldType.FieldData: field = new TagDataDefinition(); break;
                        case FieldType.FieldByteFlags:
                        case FieldType.FieldLongFlags:
                        case FieldType.FieldWordFlags:
                        case FieldType.FieldCharEnum:
                        case FieldType.FieldEnum:
                        case FieldType.FieldLongEnum: field = new EnumDefinition(); break;
                        case FieldType.FieldCharBlockIndex2:
                        case FieldType.FieldShortBlockIndex2:
                        case FieldType.FieldLongBlockIndex2: field = new BlockIndexCustomSearchDefinition(); break;
                        case FieldType.FieldExplanation: field = new ExplanationDefinition(); break;
                        default: field = new TagFieldDefinition(); break;
                    }

                    //Goto field
                    input.Seek(fieldAddress, SeekOrigin.Begin);
                    field.Read(reader);
                    
                    //Add
                    definition.tagFields[i].Add(field);

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
                                ReadTagBlockDefinition(input, reader, field.DefinitionAddress, false, definition);
                            break;
                        case FieldType.FieldStruct:
                            ReadTagBlockDefinition(input, reader, ((TagStructDefinition)field).BlockDefinitionAddresss, false, definition);
                            break;
                    }

                    //Goto next field
                    input.Seek(fieldAddress + 16, SeekOrigin.Begin);
                }
                while (field.Type != FieldType.FieldTerminator);
            }
        }
        /// <summary>
        /// Converts an underscore separated name to a pascal cased string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A PascalCasedString.</returns>
        private string ConvertToPascalCase(string name)
        {
            StringBuilder builder = new StringBuilder();
            string[] parts = name.Split('_');
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
            string[] parts = name.Split('_');
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
        /// Searches the tag block list for a tag block with the specified address.
        /// </summary>
        /// <param name="address">The address of the tag block.</param>
        /// <returns>A tag block instance if one is found; otherwise null.</returns>
        public TagBlockDefinition Search(int address)
        {
            //Return
            if (addressLookup.ContainsKey(address)) return tagBlocks[addressLookup[address]];
            return null;
        }
        /// <summary>
        /// Searches the tag block list for a tag block with the specified name.
        /// </summary>
        /// <param name="name">The name of the tag block.</param>
        /// <returns>A tag block instance if one is found; otherwise null.</returns>
        public TagBlockDefinition Search(string name)
        {
            //Return
            if (nameLookup.ContainsKey(name)) return tagBlocks[nameLookup[name]];
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
