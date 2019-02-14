using System;
using Abide.Tag.Definition;

namespace Abide.Tag.Preprocess
{
    /// <summary>
    /// General tag block preprocessor.
    /// </summary>
    public static class GeneralPreprocess
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
                    BitmapBlock_Preprocess(block);
                    break;
                case "bitmap_data_block":
                    BitmapDataBlock_Preprocess(block);
                    break;
                case "collision_bsp_physics_block":
                    CollisionBspPhysicsBlock_Preprocess(block);
                    break;
                case "decorator_cache_block_block":
                    DecoratorCacheBlockBlock_Preprocess(block);
                    break;
                case "decorator_permutations_block":
                    DecoratorPermutationsBlock_Preprocess(block);
                    break;
                case "decorator_set_block":
                case "particle_model_block":
                    DecoratorSet_ParticleMode_Preprocess(block);
                    break;
                case "globals_block":
                    GlobalsBlock_Preprocess(block, cache);
                    break;
                case "global_geometry_block_info_struct_block":
                    GlobalGeometryBlockInfoStructBlock_Preprocess(block);
                    break;
                case "hs_unit_seat_block":
                    HsUnitSeatBlock_Preprocess(block);
                    break;
                case "model_animation_graph_block":
                    ModelAnimationGraphBlock_Preprocess(block, cache);
                    break;
                case "pixel_shader_fragment_block":
                    PixelShaderFragmentBlock_Preprocess(block);
                    break;
                case "predicted_resource_block":
                    PredictedResourceBlock_Preprocess(block);
                    break;
                case "scenario_cutscene_title_block":
                    ScenarioCutsceneTitleBlock_Preprocess(block);
                    break;
                case "scenario_biped_block":
                case "scenario_crate_block":
                case "scenario_vehicle_block":
                case "scenario_weapon_block":
                case "scenario_scenery_block":
                    ScenarioPlacementBlock_Preprocess(block);
                    break;
                case "scenario_simulation_definition_table_block":
                    ScenarioSimulationDefinitionTableBlock_Preprocess(block);
                    break;
                case "shader_block":
                    ShaderBlock_Preprocess(block);
                    break;
                case "shader_pass_postprocess_implementation_new_block":
                    ShaderPassPostprocessImplementationNewBlock_Preprocess(block);
                    break;
                case "shader_postprocess_bitmap_new_block":
                case "shader_postprocess_definition_new_block":
                    ShaderPostprocessNewBlock_Preprocess(block);
                    break;
                case "sound_globals_block":
                    SoundGlobalsBlock_Preprocess(block);
                    break;
                case "syntax_datum_block":
                    SyntaxDatumBlock_Preprocess(block);
                    break;
                case "tag_block_index_struct_block":
                    TagBlockIndexStructBlock_Preprocess(block);
                    break;
                case "user_interface_screen_widget_definition_block":
                    UserInterfaceScreenWidgetDefinitionBlock_Preprocess(block);
                    break;
                case "vertex_shader_block":
                    VertexShaderBlock_Preprocess(block);
                    break;
                case "vertex_shader_classification_block":
                    VertexShaderClassificationBlock_Preprocess(block);
                    break;
            }
        }
        
        private static void BitmapDataBlock_Preprocess(AbideTagBlock block)
        {
            //Change...
            block.FieldSet[16].FieldType = FieldType.FieldTagIndex;
        }

        private static void BitmapBlock_Preprocess(AbideTagBlock block)
        {
            //Rename field 12
            block.FieldSet[12].Name = "sprite size";

            //Change data fields to pad
            block.FieldSet[17].FieldType = FieldType.FieldPad; block.FieldSet[17].Length = 8;
            block.FieldSet[19].FieldType = FieldType.FieldPad; block.FieldSet[19].Length = 8;

            //Remove WDP fields
            block.FieldSet.RemoveAt(35);
            block.FieldSet.RemoveAt(34);
            block.FieldSet.RemoveAt(33);
            block.FieldSet.RemoveAt(32);
            block.FieldSet.RemoveAt(31);
        }
        
        private static void CollisionBspPhysicsBlock_Preprocess(AbideTagBlock block)
        {
            //Change pad length
            block.FieldSet[18].Length = 4;
        }

        private static void DecoratorCacheBlockBlock_Preprocess(AbideTagBlock block)
        {
            //Remove
            block.FieldSet.RemoveAt(3); //Pad
            block.FieldSet.RemoveAt(2); //Pad
        }

        private static void DecoratorSet_ParticleMode_Preprocess(AbideTagBlock block)
        {
            //Remove 4-byte padding from end
            block.FieldSet.RemoveAt(block.FieldSet.Count - 2);
        }

        private static void DecoratorPermutationsBlock_Preprocess(AbideTagBlock block)
        {
            //Add...
            block.FieldSet.Insert(10, new AbideTagField() { FieldType = FieldType.FieldPad, Length = 1 });  //1-byte padding
            block.FieldSet.Insert(9, new AbideTagField() { FieldType = FieldType.FieldPad, Length = 1 });   //1-byte padding
        }

        private static void GlobalsBlock_Preprocess(AbideTagBlock block, TagDefinitionCollection cache)
        {
            //Create obsolete sound block
            AbideTagBlock globalsObsoleteSoundBlock = null;
            globalsObsoleteSoundBlock = new AbideTagBlock()
            {
                Name = "globals_obsolete_sound_block",
                DisplayName = "globals_obsolete_sound_block",
                FieldSet = new AbideFieldSet(globalsObsoleteSoundBlock)
                { Alignment = 4 },
                MaximumElementCount = short.MaxValue
            };
            globalsObsoleteSoundBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldTagReference,
                GroupTag = (int)new HaloLibrary.TagFourCc("snd!").Dword });
            globalsObsoleteSoundBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldTerminator });

            //Create multilingual unicode globals block
            AbideTagBlock globalsMultilingualUnicodeInfoStructBlock = null;
            globalsMultilingualUnicodeInfoStructBlock = new AbideTagBlock()
            {
                Name = "globals_multilingual_unicode_info_struct_block",
                DisplayName = "globals_multilingual_unicode_info_struct_block",
                FieldSet = new AbideFieldSet(globalsObsoleteSoundBlock)
                { Alignment = 4 },
                MaximumElementCount = 1
            };
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldPad, Length = 8 });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "English string count" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "English strings length" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "English string index offset" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "English strings offset" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldPad, Length = 4 });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldPad, Length = 8 });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Japanese string count" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Japanese strings length" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Japanese string index offset" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Japanese strings offset" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldPad, Length = 4 });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldPad, Length = 8 });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Dutch string count" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Dutch strings length" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Dutch string index offset" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Dutch strings offset" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldPad, Length = 4 });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldPad, Length = 8 });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "French string count" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "French strings length" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "French string index offset" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "French strings offset" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldPad, Length = 4 });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldPad, Length = 8 });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Spanish string count" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Spanish strings length" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Spanish string index offset" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Spanish strings offset" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldPad, Length = 4 });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldPad, Length = 8 });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Italian string count" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Italian strings length" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Italian string index offset" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Italian strings offset" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldPad, Length = 4 });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldPad, Length = 8 });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Korean string count" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Korean strings length" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Korean string index offset" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Korean strings offset" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldPad, Length = 4 });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldPad, Length = 8 });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Chinese string count" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Chinese strings length" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Chinese string index offset" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Chinese strings offset" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldPad, Length = 4 });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldPad, Length = 8 });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Portuguese string count" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Portuguese strings length" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Portuguese string index offset" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "Portuguese strings offset" });
            globalsMultilingualUnicodeInfoStructBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldPad, Length = 4 });

            //Obsolete sound block field
            AbideTagField obsoleteSounds = new AbideTagField()
            {
                Name = "sounds (obsolete)",
                FieldType = FieldType.FieldBlock,
                BlockName = "globals_obsolete_sound_block",
                ReferencedBlock = globalsObsoleteSoundBlock
            };

            //Obsolete sound block field
            AbideTagField multilinualUnicodeInfo = new AbideTagField()
            {
                Name = "multilingual unicode info",
                FieldType = FieldType.FieldStruct,
                StructName = "globals_multilingual_unicode_info_struct_block",
                ReferencedBlock = globalsMultilingualUnicodeInfoStructBlock
            };

            //Add blocks to cache
            cache.Add(globalsObsoleteSoundBlock);
            cache.Add(globalsMultilingualUnicodeInfoStructBlock);

            //Replace...
            block.FieldSet[8] = obsoleteSounds;             //Replace sounds
            block.FieldSet[30] = multilinualUnicodeInfo;    //Replace padding
        }

        private static void GlobalGeometryBlockInfoStructBlock_Preprocess(AbideTagBlock block)
        {
            //Change...
            block.FieldSet[6].FieldType = FieldType.FieldTagIndex;
        }

        private static void HsUnitSeatBlock_Preprocess(AbideTagBlock block)
        {
            //Change...
            block.FieldSet[0].FieldType = FieldType.FieldTagIndex;
        }

        private static void ModelAnimationGraphBlock_Preprocess(AbideTagBlock block, TagDefinitionCollection cache)
        {
            //Create unknown block
            AbideTagBlock xboxUnknownAnimationBlock = null;
            xboxUnknownAnimationBlock = new AbideTagBlock()
            {
                Name = "xbox_unknown_animation_block",
                DisplayName = "xbox_unknown_animaiton_block",
                FieldSet = new AbideFieldSet(xboxUnknownAnimationBlock)
                { Alignment = 4 },
                MaximumElementCount = short.MaxValue
            };
            xboxUnknownAnimationBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger });
            xboxUnknownAnimationBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger });
            xboxUnknownAnimationBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger });
            xboxUnknownAnimationBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger });
            xboxUnknownAnimationBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger });
            xboxUnknownAnimationBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger });

            //Create data block
            AbideTagBlock xboxAnimaitonDataBlock = null;
            xboxAnimaitonDataBlock = new AbideTagBlock()
            {
                Name = "xbox_animation_data_block",
                DisplayName = "xbox_animation_data_block",
                FieldSet = new AbideFieldSet(xboxAnimaitonDataBlock)
                { Alignment = 4 },
                MaximumElementCount = short.MaxValue
            };
            xboxAnimaitonDataBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldTagIndex, Name = "owner index" });
            xboxAnimaitonDataBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "block size" });
            xboxAnimaitonDataBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger, Name = "block offset" });
            xboxAnimaitonDataBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger });
            xboxAnimaitonDataBlock.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldLongInteger });

            //Unknown block field
            AbideTagField unknownBlockField = new AbideTagField()
            {
                Name = "",
                FieldType = FieldType.FieldBlock,
                BlockName = "xbox_unknown_animation_block",
                ReferencedBlock = xboxUnknownAnimationBlock
            };
            //Data block field
            AbideTagField animationDataBlockField = new AbideTagField()
            {
                Name = "animation data",
                FieldType = FieldType.FieldBlock,
                BlockName = "xbox_animation_data_block",
                ReferencedBlock = xboxAnimaitonDataBlock
            };

            //Add blocks to cache
            cache.Add(xboxUnknownAnimationBlock);
            cache.Add(xboxAnimaitonDataBlock);

            //Add fields to block
            block.FieldSet.Insert(6, animationDataBlockField);
            block.FieldSet.Insert(7, unknownBlockField);
        }
        
        private static void PixelShaderFragmentBlock_Preprocess(AbideTagBlock block)
        {
            //Add...
            block.FieldSet.Insert(1, new AbideTagField() { FieldType = FieldType.FieldPad, Length = 1 });   //1-byte padding
        }

        private static void PredictedResourceBlock_Preprocess(AbideTagBlock block)
        {
            //Change...
            block.FieldSet[2].FieldType = FieldType.FieldTagIndex;
        }

        private static void ScenarioCutsceneTitleBlock_Preprocess(AbideTagBlock block)
        {
            //Add...
            block.FieldSet.Insert(9, new AbideTagField() { FieldType = FieldType.FieldPad, Length = 2 });
        }

        private static void ScenarioPlacementBlock_Preprocess(AbideTagBlock block)
        {
            //Add
            block.FieldSet.Insert(5, new AbideTagField() { FieldType = FieldType.FieldPad, Length = 4 });
        }

        private static void ScenarioSimulationDefinitionTableBlock_Preprocess(AbideTagBlock block)
        {
            //Change...
            block.FieldSet[0].FieldType = FieldType.FieldTagIndex;
        }

        private static void ShaderBlock_Preprocess(AbideTagBlock block)
        {
            //Remove...
            block.FieldSet.RemoveAt(17);    //Postprocess Properties
        }

        private static void ShaderPassPostprocessImplementationNewBlock_Preprocess(AbideTagBlock block)
        {
            //Remove...
            block.FieldSet.RemoveAt(25); //pixel shader
            block.FieldSet.RemoveAt(24); //pixel shader switch extern map
            block.FieldSet.RemoveAt(23); //pixel shader index block
        }

        private static void ShaderPostprocessNewBlock_Preprocess(AbideTagBlock block)
        {
            //Change...
            block.FieldSet[0].FieldType = FieldType.FieldTagIndex;
        }

        private static void SoundGlobalsBlock_Preprocess(AbideTagBlock block)
        {
            //Change...
            block.FieldSet[4].FieldType = FieldType.FieldTagIndex;
        }

        private static void SyntaxDatumBlock_Preprocess(AbideTagBlock block)
        {
            //Change...
            block.FieldSet[6].FieldType = FieldType.FieldTagIndex;
        }

        private static void TagBlockIndexStructBlock_Preprocess(AbideTagBlock block)
        {
            block.FieldSet.Clear();
            block.FieldSet.Add(new AbideTagField() { Name = "index", FieldType = FieldType.FieldCharInteger });
            block.FieldSet.Add(new AbideTagField() { Name = "Length", FieldType = FieldType.FieldCharInteger });
            block.FieldSet.Add(new AbideTagField() { FieldType = FieldType.FieldTerminator });
        }

        private static void UserInterfaceScreenWidgetDefinitionBlock_Preprocess(AbideTagBlock block)
        {
            //Remove...
            block.FieldSet.RemoveAt(31);    //Mouse cursor tag reference
            block.FieldSet.RemoveAt(30);    //Mouse cursor explanation
        }

        private static void VertexShaderBlock_Preprocess(AbideTagBlock block)
        {
            //Remove output swizzles
            block.FieldSet.RemoveAt(3);
        }
        
        private static void VertexShaderClassificationBlock_Preprocess(AbideTagBlock block)
        {
            //Change element size of compiled shader
            block.FieldSet[1].ElementSize = 2;

            //Insert pad field
            block.FieldSet.Insert(3, new AbideTagField() { FieldType = FieldType.FieldPad, Length = 8 });
        }
    }
}
