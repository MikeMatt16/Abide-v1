using Abide.Guerilla.H2Guerilla;
using System.Collections.Generic;
using System.IO;

namespace Abide.Guerilla.Managed
{
    /// <summary>
    /// Represents a tag field array definition.
    /// </summary>
    public sealed class TagFieldDefinitionArray : TagFieldDefinition
    {
        private const int startAndEndStructSize = 32;

        /// <summary>
        /// Gets the number to offset the address by to arrive at the end of the array.
        /// </summary>
        public override int Nudge
        {
            get { return startAndEndStructSize + (fieldList.Count * 16); }
        }
        /// <summary>
        /// Gets and returns the number of elements within this array.
        /// This value is equivalent to <see cref="TagFieldDefinition.DefinitionAddress"/>.
        /// </summary>
        public int ElementCount
        {
            get { return DefinitionAddress; }
        }
        /// <summary>
        /// Gets and returns the size of the array element.
        /// </summary>
        public int Size
        {
            get
            {
                return 0;
            }
        }

        private readonly List<TagFieldDefinition> fieldList;

        /// <summary>
        /// Inititalizes a new instance of the <see cref="TagFieldDefinitionArray"/> class.
        /// </summary>
        public TagFieldDefinitionArray() : base()
        {
            //Setup
            fieldList = new List<TagFieldDefinition>();
        }
        /// <summary>
        /// Gets a list of fields within this array.
        /// </summary>
        /// <returns>An array of <see cref="TagFieldDefinition"/> elements.</returns>
        public TagFieldDefinition[] GetFields()
        {
            return fieldList.ToArray();
        }
        /// <summary>
        /// Reads this array field using the supplied guerilla binary reader.
        /// </summary>
        /// <param name="reader">The guerilla reader.</param>
        internal override void Read(GuerillaBinaryReader reader)
        {
            //Prepare
            TagFieldDefinition field = new TagFieldDefinition();
            
            //Read
            base.Read(reader);

            //Loop
            do
            {
                //Store address
                long fieldAddress = reader.BaseStream.Position;

                //Read the field type
                switch ((FieldType)reader.ReadInt16())
                {
                    case FieldType.FieldTagReference: field = new TagReferenceDefinition(); break;
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
                    case FieldType.FieldArrayStart: field = new TagFieldDefinitionArray(); break;
                    default: field = new TagFieldDefinition(); break;
                }

                //Goto field
                reader.BaseStream.Seek(fieldAddress, SeekOrigin.Begin);
                field.Read(reader);

                //Add
                if (field.Type != FieldType.FieldArrayEnd) fieldList.Add(field);

                //Goto next field
                reader.BaseStream.Seek(fieldAddress + field.Nudge, SeekOrigin.Begin);
            }
            while (field.Type != FieldType.FieldArrayEnd);
        }
    }
}
