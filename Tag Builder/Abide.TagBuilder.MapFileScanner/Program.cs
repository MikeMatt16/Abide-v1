using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using Abide.Tag;
using Abide.Tag.Cache.Generated;
using Abide.Tag.Definition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Abide.TagBuilder.MapFileScanner
{
    class Program
    {
        static MapFile map = new MapFile();

        static void Main(string[] args)
        {
            //Get filename
            if (args.Length == 0) return;
            string filename = args[0].Replace("\"", string.Empty);

            //Write
            Console.WriteLine("Scanning {0}", filename);

            //Load
            map.Load(filename);
            Map_Scan(map);

            //Done
            Console.WriteLine();
            Console.WriteLine("Done.");
            Console.WriteLine("Press any key to exit.");

            //Wait for input to close
            Console.ReadKey();
        }

        private static void Map_Scan(MapFile map)
        {
            //Loop
            foreach (IndexEntry entry in map.IndexEntries)
            {
                //Create tag group
                using (Group tagGroup = TagLookup.CreateTagGroup(entry.Root))
                {
                    //Read
                    entry.TagData.Seek((uint)entry.PostProcessedOffset, SeekOrigin.Begin);
                    using (BinaryReader reader = entry.TagData.CreateReader())
                        tagGroup.Read(reader);

                    //Loop
                    foreach (ITagBlock tagBlock in tagGroup.TagBlocks)
                        TagBlock_Search(tagBlock, entry);
                }
            }
        }

        private static void TagBlock_Search(ITagBlock tagBlock, IndexEntry source)
        {
            int index = 0;

            //Loop
            foreach (Field tagField in tagBlock.Fields)
            {
                switch (tagField.Type)
                {
                    case FieldType.FieldLongInteger:
                        if (tagField is LongIntegerField intField && intField.Value is int integer)
                            if (map.IndexEntries.ContainsID(integer))
                            {
                                Console.WriteLine("Found Tag ID as a {0} within {1}. Index {2} ({3}.{4})", "field_long_integer", tagBlock.Name, index, map.IndexEntries[(TagId)integer].Filename, map.IndexEntries[(TagId)integer].Root);
                                Console.WriteLine("From {0}.{1}", source.Filename, source.Root);
                            }
                        break;
                    case FieldType.FieldPad:
                        if (tagField is PadField padField && padField.Length == 4 && padField.Value is byte[] pad)
                            if (map.IndexEntries.ContainsID(BitConverter.ToInt32(pad, 0)))
                            {
                                Console.WriteLine("Found Tag ID as a {0} within {1}. Index {2} ({3}.{4})", "field_pad", tagBlock.Name, index, map.IndexEntries[(TagId)BitConverter.ToInt32(pad, 0)].Filename, map.IndexEntries[(TagId)BitConverter.ToInt32(pad, 0)].Root);
                                Console.WriteLine("From {0}.{1}", source.Filename, source.Root);
                            }
                        break;
                    case FieldType.FieldSkip:
                        if (tagField is SkipField skipField && skipField.Length == 4 && skipField.Value is byte[] skip)
                            if (map.IndexEntries.ContainsID(BitConverter.ToInt32(skip, 0)))
                            {
                                Console.WriteLine("Found Tag ID as a {0} within {1}. Index {2} ({3}.{4})", "field_skip", tagBlock.Name, index, map.IndexEntries[(TagId)BitConverter.ToInt32(skip, 0)].Filename, map.IndexEntries[(TagId)BitConverter.ToInt32(skip, 0)].Root);
                                Console.WriteLine("From {0}.{1}", source.Filename, source.Root);
                            }
                        break;

                    case FieldType.FieldStruct:
                        if (tagField.Value is ITagBlock structBlock)
                            TagBlock_Search(structBlock, source);
                        break;
                    case FieldType.FieldBlock:
                        if (tagField is BlockField blockField)
                            foreach (ITagBlock block in blockField.BlockList)
                                TagBlock_Search(block, source);
                        break;
                }

                //Increment
                index++;
            }
        }
    }
}
