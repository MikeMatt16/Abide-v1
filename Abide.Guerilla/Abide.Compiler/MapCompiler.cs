using Abide.Guerilla.Library;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.HaloLibrary.IO;
using Abide.Tag;
using Abide.Tag.Definition;
using Abide.Tag.Guerilla;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Abide.Compiler
{
    /// <summary>
    /// Represents a Halo 2 cache map file compiler.
    /// </summary>
    public sealed class MapCompiler
    {
        /// <summary>
        /// Gets and returns the compiler's globals tag group file.
        /// </summary>
        public TagGroupFile GlobalsFile { get; } = new TagGroupFile();
        /// <summary>
        /// Gets and returns the compiler's scenario tag group file.
        /// </summary>
        public TagGroupFile ScenarioFile { get; } = new TagGroupFile();
        /// <summary>
        /// Gets and returns the output Halo 2 cache map file.
        /// </summary>
        public string OutputMapFile { get; }
        /// <summary>
        /// Gets and returns the compiler host.
        /// </summary>
        public ICompileHost Host { get; }

        private readonly string scenarioFileName, globalsFileName;
        private volatile bool isCompiling;
        private Dictionary<string, TagId> tagIdReferences;
        private Dictionary<string, TagGroupFile> tagGroupReferences;
        private List<string> strings;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapCompiler"/> class using the specified host, scenario file name, globals gile name, and output directory.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="scenarioFile">The map scenario file name.</param>
        /// <param name="globalsFile">The map globals file name.</param>
        /// <param name="mapFileName">The map file name.</param>
        public MapCompiler(ICompileHost host, string scenarioFileName, string globalsFileName, string mapFileName)
        {
            Host = host;
            OutputMapFile = mapFileName;
            this.scenarioFileName = scenarioFileName;
            this.globalsFileName = globalsFileName;
        }
        
        public void Start()
        {
            //Check
            if (isCompiling) return;

            //Start
            isCompiling = true;
            ThreadPool.QueueUserWorkItem(Map_Compile, null);
        }

        private void Map_Compile(object state)
        {
            //Prepare
            StructureBspBlockHeader sbspHeader = new StructureBspBlockHeader();
            Index indexHeader = new Index();

            //Read tags
            ScenarioFile.Load(scenarioFileName);
            GlobalsFile.Load(globalsFileName);

            //Prepare
            tagIdReferences = new Dictionary<string, TagId>();
            tagGroupReferences = new Dictionary<string, TagGroupFile>();
            strings = new List<string>(new string[] { string.Empty });

            //Build References
            Host.Log("Building references...");
            Host.Marquee();

            //Build globals references
            tagGroupReferences.Add(globalsFileName, GlobalsFile);
            foreach (Block tagBlock in GlobalsFile.TagGroup)
                TagBlock_BuildReferenceTree(tagBlock);

            //Build scenario references
            tagGroupReferences.Add(scenarioFileName, ScenarioFile);
            foreach (Block tagBlock in ScenarioFile.TagGroup)
                TagBlock_BuildReferenceTree(tagBlock);

            //Get Tag References
            TagGroupFile[] references = tagGroupReferences.Select(kvp => kvp.Value).ToArray();
            string[] fileNames = tagGroupReferences.Select(kvp => kvp.Key).ToArray();

            //Create Map
            using (FileStream fs = new FileStream(OutputMapFile, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
            using (BinaryWriter mapWriter = new BinaryWriter(fs))
            {
                //Skip header for now
                fs.Seek(Header.Length, SeekOrigin.Begin);

                //Write Sounds
                foreach (TagGroupFile soundFile in references.Where(tg => tg.TagGroup.GroupTag == HaloTags.ugh_))
                {
                    foreach(ITagBlock chunk in ((BaseBlockField)soundFile.TagGroup[0].Fields[9]).BlockList)
                    {

                    }
                }

                //Write Lip Sync geometries
                foreach (TagGroupFile soundFile in references.Where(tg => tg.TagGroup.GroupTag == HaloTags.ugh_))
                {

                }

                //Create Index
                Host.Log("Building index...");
                TagHierarchy[] tagsList = new TagHierarchy[120];
                ObjectEntry[] objectEntries = new ObjectEntry[references.Length];

                //Loop
                TagId id = new TagId(3782475776u);
                for (int i = 0; i < objectEntries.Length; i++)
                {
                    //Set entry
                    objectEntries[i] = new ObjectEntry()
                    {
                        Tag = references[i].TagGroup.GroupTag,
                        Id = id.Dword,
                    };

                    //Check
                    if (references[i] == GlobalsFile) indexHeader.GlobalsId = id.Dword;
                    else if (references[i] == ScenarioFile) indexHeader.ScenarioId = id.Dword;
                    tagIdReferences.Add(fileNames[i], id);

                    //Increment
                    id.HiWord++;
                    id.LoWord++;
                }

                //Setup
                indexHeader.TagCount = (uint)tagsList.Length;
                indexHeader.ObjectCount = (uint)objectEntries.Length;
                indexHeader.ObjectsOffset = Index.IndexMemoryAddress + (uint)(tagsList.Length * TagHierarchy.Length);
                indexHeader.Tags = "tags";

                //Prepare
                using (VirtualStream tagDataStream = new VirtualStream(Index.IndexMemoryAddress))
                using (BinaryWriter writer = new BinaryWriter(tagDataStream))
                using (BinaryReader reader = new BinaryReader(tagDataStream))
                {
                    //Write index
                    writer.Write(indexHeader);
                    foreach (TagHierarchy tag in tagsList)
                        writer.Write(tag);
                    foreach (ObjectEntry entry in objectEntries)
                        writer.Write(entry);

                    //Align
                    tagDataStream.Align(4096);

                    //Get BSP offset
                    long bspVirtualAddress = tagDataStream.Position;
                    List<byte[]> bspBlocks = new List<byte[]>();
                    int bspBlockLength = 0;

                    //Loop through scenario structure BSP references
                    Host.Log("Preparing scenario structure BSPs..."); int ssbspIdx = 0;
                    foreach (var structureBspReference in ((BaseBlockField)ScenarioFile.TagGroup[0].Fields[68]).BlockList)
                    {
                        //Get structure block info
                        ITagBlock structureBlockInfo = (ITagBlock)structureBspReference.Fields[0].Value;
                        structureBlockInfo.Fields[2].Value = (int)bspVirtualAddress;

                        //Get referenced tags
                        string structureBspFileName = structureBspReference.Fields[1].Value.ToString();
                        TagGroupFile structureBspFile = (tagGroupReferences.ContainsKey(structureBspFileName) ? tagGroupReferences[structureBspFileName] : null);
                        string lightmapFileName = structureBspReference.Fields[2].Value.ToString();
                        TagGroupFile lightmapFile = (tagGroupReferences.ContainsKey(lightmapFileName) ? tagGroupReferences[lightmapFileName] : null);

                        //Check
                        if (structureBspFile == null)
                        {
                            //Fail
                            Host.Log($"Faield! Structure BSP reference does not exist in scenario -> structure bsp references[{ssbspIdx}]");
                            Host.Fail();
                            return;
                        }

                        //Convert tag blocks
                        foreach (ITagBlock block in structureBspFile.TagGroup)
                            TagBlock_ConvertGuerillaToCache(block);
                        if (lightmapFile != null)
                            foreach (ITagBlock block in lightmapFile.TagGroup)
                                TagBlock_ConvertGuerillaToCache(block);

                        //Create stream
                        byte[] structureBspBlock = null;
                        sbspHeader.StructureBsp = structureBspFile.TagGroup.GroupTag;
                        using (VirtualStream bspTagDataStream = new VirtualStream(bspVirtualAddress))
                        using (BinaryWriter bspWriter = bspTagDataStream.CreateWriter())
                        {
                            //Skip header for now
                            sbspHeader.StructureBspOffset = (uint)bspTagDataStream.Seek(StructureBspBlockHeader.Length, SeekOrigin.Current);
                            structureBspFile.TagGroup.Write(bspWriter);

                            //Check for lightmap
                            if (lightmapFile != null)
                            {
                                //Align
                                sbspHeader.StructureLightmapOffset = (uint)bspTagDataStream.Align(1024);
                                lightmapFile.TagGroup.Write(bspWriter);
                            }

                            //Align
                            sbspHeader.BlockLength = (int)(bspTagDataStream.Align(1024) - bspVirtualAddress);

                            //Goto
                            bspTagDataStream.Seek(bspVirtualAddress, SeekOrigin.Begin);
                            bspWriter.Write(sbspHeader);

                            //Get block
                            structureBspBlock = bspTagDataStream.ToArray();
                            bspBlocks.Add(structureBspBlock);

                            //Check
                            if (structureBspBlock.Length > bspBlockLength) bspBlockLength = structureBspBlock.Length;
                        }

                        //Set block length in scenario
                        structureBlockInfo.Fields[1].Value = structureBspBlock.Length;

                        //Increment
                        ssbspIdx++;
                    }

                    //Build References
                    Host.Log("Converting fields...");
                    foreach (TagGroupFile reference in references)
                        foreach (ITagBlock tagBlock in reference.TagGroup)
                            TagBlock_ConvertGuerillaToCache(tagBlock);

                    //Complete
                    Host.Complete();
                }
            }
        }
        
        private void TagBlock_BuildReferenceTree(ITagBlock tagBlock)
        {
            //Loop through fields
            foreach (Field field in tagBlock.Fields)
            {
                switch (field.Type)
                {
                    case FieldType.FieldBlock:
                        foreach (ITagBlock block in ((BaseBlockField)field).BlockList)
                            TagBlock_BuildReferenceTree(block);
                        break;
                    case FieldType.FieldStruct:
                        TagBlock_BuildReferenceTree((ITagBlock)field.Value);
                        break;
                    case FieldType.FieldOldStringId:
                    case FieldType.FieldStringId:
                        if (field.Value is StringValue stringId)
                            if (!strings.Contains(stringId.Value))
                                strings.Add(stringId.Value);
                        break;
                    case FieldType.FieldTagReference:
                        if (field.Value is StringValue tagFilePath)
                            if (File.Exists(tagFilePath.Value) && !tagGroupReferences.ContainsKey(tagFilePath.Value))
                                TagReferences_AddTag(tagFilePath.Value);
                        break;
                }
            }
        }

        private void TagReferences_AddTag(string filename)
        {
            //Load
            TagGroupFile tagGroupFile = new TagGroupFile();
            tagGroupFile.Load(filename);

            //Add
            tagGroupReferences.Add(filename, tagGroupFile);

            //Build references
            foreach (Block tagBlock in tagGroupFile.TagGroup)
                TagBlock_BuildReferenceTree(tagBlock);
        }

        private void TagBlock_ConvertGuerillaToCache(ITagBlock tagBlock)
        {
            //Loop
            for (int i = 0; i < tagBlock.Fields.Count; i++)
            {
                //Convert
                ConvertGuerillaToCache(tagBlock, tagBlock.Fields[i]);

                //Check
                switch(tagBlock.Fields[i].Type)
                {
                    case FieldType.FieldBlock:
                        foreach (ITagBlock childTagBlock in ((BaseBlockField)tagBlock.Fields[i]).BlockList)
                            TagBlock_ConvertGuerillaToCache(childTagBlock);
                        break;
                    case FieldType.FieldStruct:
                        TagBlock_ConvertGuerillaToCache((ITagBlock)tagBlock.Fields[i].Value);
                        break;
                }
            }
        }

        private void ConvertGuerillaToCache(ITagBlock tagBlock, Field cacheField)
        {
            //Prepare
            StringValue value = StringValue.Empty;

            //Check
            if (!tagBlock.Fields.Contains(cacheField)) return;

            //Get index
            int fieldIndex = tagBlock.Fields.IndexOf(cacheField);
            Field convertedField = cacheField;

            //Handle
            switch (cacheField.Type)
            {
                case FieldType.FieldStringId:
                    if (cacheField.Value is StringId) break; //Check for early conversion
                    value = cacheField.Value as StringValue ?? StringValue.Empty;
                    convertedField = new Tag.Cache.StringIdField(cacheField.Name)
                    {
                        Value = new StringId((ushort)strings.IndexOf(value.Value), (byte)value.Value.Length)
                    };
                    break;
                case FieldType.FieldOldStringId:
                    if (cacheField.Value is StringId) break; //Check for early conversion
                    value = cacheField.Value as StringValue ?? StringValue.Empty;
                    convertedField = new Tag.Cache.OldStringIdField(cacheField.Name)
                    {
                        Value = new StringId((ushort)strings.IndexOf(value.Value), (byte)value.Value.Length)
                    };
                    break;
                case FieldType.FieldTagReference:
                    if (cacheField.Value is TagId) break; //Check for early conversion
                    value = cacheField.Value as StringValue ?? StringValue.Empty;
                    convertedField = new Tag.Cache.TagReferenceField(cacheField.Name)
                    {
                        Value = value.Value == string.Empty ? TagReference.Null : new TagReference()
                        {
                            Tag = tagGroupReferences[value.Value].TagGroup.GroupTag,
                            Id = tagIdReferences[value.Value]
                        }
                    };
                    break;
            }

            //Change
            tagBlock.Fields[fieldIndex] = convertedField;
        }
    }
}
