using Abide.Tag.Definition;

namespace Abide.Tag.Preprocess
{
    /// <summary>
    /// Cache tag block preprocessor.
    /// </summary>
    public static class CachePreprocess
    {
        /// <summary>
        /// Preprocesses a tag block.
        /// </summary>
        /// <param name="block">The tag block to preprocess.</param>
        /// <param name="cache">The tag cache that <paramref name="block"/> belongs to.</param>
        public static void Preprocess(AbideTagBlock block, TagDefinitionCollection cache)
        {
            //Check Block
            switch (block.Name)
            {
                case "bitmap_block":
                    BitmapBlock_Preprocess(block, cache);
                    break;
                case "scenario_structure_bsp_reference_block":
                    ScenarioStructureBspReferenceBlock_Preprocess(block, cache);
                    break;
                case "sound_block":
                    SoundBlock_Preprocess(block, cache);
                    break;
            }
        }

        private static void BitmapBlock_Preprocess(AbideTagBlock block, TagDefinitionCollection cache)
        {
            //Change data fields to pad
            block.FieldSet[17].FieldType = FieldType.FieldPad; block.FieldSet[17].Length = 8;
            block.FieldSet[19].FieldType = FieldType.FieldPad; block.FieldSet[19].Length = 8;
        }

        private static void ScenarioStructureBspReferenceBlock_Preprocess(AbideTagBlock block, TagDefinitionCollection cache)
        {
            //Create struct
            AbideTagBlock scenarioStructureBspInfoStructBlock = new AbideTagBlock()
            {
                Name = "scenario_structure_bsp_info_struct_block",
                DisplayName = "scenario_structure_bsp_info_struct_block",
                MaximumElementCount = 1,
            };
            scenarioStructureBspInfoStructBlock.FieldSet.Add(new AbideTagField() { Name = "block offset", FieldType = FieldType.FieldLongInteger });
            scenarioStructureBspInfoStructBlock.FieldSet.Add(new AbideTagField() { Name = "block length", FieldType = FieldType.FieldLongInteger });
            scenarioStructureBspInfoStructBlock.FieldSet.Add(new AbideTagField() { Name = "block address", FieldType = FieldType.FieldLongInteger });
            scenarioStructureBspInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger });
            scenarioStructureBspInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldTerminator });
            
            //Replace 16-byte padding
            block.FieldSet[0] = new AbideTagField()
            {
                FieldType = FieldType.FieldStruct,
                Name = "structure block info",
                StructName = "scenario_structure_bsp_info_struct_block",
                ReferencedBlock = scenarioStructureBspInfoStructBlock
            };

            //Add block to cache
            cache.Add(scenarioStructureBspInfoStructBlock);
        }

        private static void SoundBlock_Preprocess(AbideTagBlock block, TagDefinitionCollection cache)
        {
            //Get sound tag group
            AbideTagGroup sound = cache.GetTagGroup("snd!");

            //Change definition to cache_file_sound_block
            sound.BlockName = "cache_file_sound_block";
        }
    }
}
