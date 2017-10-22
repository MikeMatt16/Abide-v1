using Abide.HaloLibrary.Halo2Map;
using System.Collections.Generic;
using System.IO;

namespace Abide.Guerilla.Managed
{
    /// <summary>
    /// Represents a H2 Guerilla Reader.
    /// </summary>
    public sealed class GuerillaReader
    {
        /// <summary>
        /// Gets and returns a dictionary of tag block definitions indexed by their addresses.
        /// </summary>
        public Dictionary<int, TagBlockDefinition> TagBlockDefinitions
        {
            get { return tagBlockDefinitions; }
        }

        private readonly Dictionary<int, TagBlockDefinition> tagBlockDefinitions;
        private readonly TagGroupDefinition[] tagGroups;

        /// <summary>
        /// Initializes a new  instance of the <see cref="GuerillaReader"/> class using the supplied guerilla executable file name and language library file name.
        /// </summary>
        /// <param name="guerillaFileName">The path of the H2Guerilla executable.</param>
        /// <param name="languageFileName">The path of the H2alang dynamic link library.</param>
        public GuerillaReader(string guerillaFileName, string languageFileName)
        {
            //Prepare
            tagBlockDefinitions = new Dictionary<int, TagBlockDefinition>();
            tagGroups = new TagGroupDefinition[Guerilla.NumberOfTagLayouts];

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
                    tagGroups[i] = new TagGroupDefinition(reader.ReadTagGroup());   //Read Tag group

                    //Read tag block definition
                    ReadTagBlockDefinition(fs, reader, tagGroups[i].DefinitionAddress, tagGroups[i].GroupTag == HaloTags.snd_, null);

                    //Set
                    tagBlockDefinitions[tagGroups[i].DefinitionAddress].IsTagGroup = true;
                }
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
            if (tagBlockDefinitions.ContainsKey(address)) return;

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
            tagBlockDefinitions.Add(address, definition);

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
        }
    }
}
