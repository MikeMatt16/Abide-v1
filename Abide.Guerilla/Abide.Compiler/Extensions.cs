using Abide.HaloLibrary;
using Abide.Tag;
using Abide.Tag.Definition;
using System;
using System.Text;

namespace Abide.Compiler
{
    public static class CompilerField
    {
        /// <summary>
        /// Returns the field's block list.
        /// </summary>
        /// <param name="field">The field to retrieve the block list of.</param>
        /// <returns>A <see cref="BlockList"/> instance if the field is of type <see cref="FieldType.FieldBlock"/>; otherwise <see langword="null"/>.</returns>
        public static BlockList GetBlockList(this Field field)
        {
            if (field is BlockField blockField)
                return blockField.BlockList;
            return null;
        }

        /// <summary>
        /// Returns the field's data buffer.
        /// </summary>
        /// <param name="field">The field to retrieve the data of.</param>
        /// <returns>An array of 8-bit unsigned integers if the field is of the type <see cref="FieldType.FieldData"/>; otherwise <see langword="null"/>.</returns>
        public static byte[] GetData(this Field field)
        {
            if (field is DataField dataField)
                return dataField.GetBuffer();
            return null;
        }

        /// <summary>
        /// Returns the field's structure tag block.
        /// </summary>
        /// <param name="field">The field to retrieve the structure tag block of.</param>
        /// <returns>A <see cref="ITagBlock"/> instance if the field is of type <see cref="FieldType.FieldStruct"/>; otherwise <see langword="null"/>.</returns>
        public static Block GetStruct(this Field field)
        {
            if (field is StructField structField)
                return (Block)structField.Value;
            return null;
        }

        /// <summary>
        /// Returns the field's group tag.
        /// </summary>
        /// <param name="field">The field to retrieve the group tag of.</param>
        /// <returns>A 32-bit signed integer if the field is of the type <see cref="FieldType.FieldTagReference"/>; otherwise 0.</returns>
        public static int GetGroupTag(this Field field)
        {
            if (field is Tag.Guerilla.TagReferenceField tagReferenceField)
                return BitConverter.ToInt32(Encoding.UTF8.GetBytes(tagReferenceField.GroupTag.PadRight(4, '\0')), 0);
            return 0;
        }
    }
}
