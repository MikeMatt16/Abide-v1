using Abide.HaloLibrary;
using System;
using System.IO;
using System.Text;

namespace Abide.Guerilla.H2Guerilla
{
    internal sealed class GuerillaBinaryReader : BinaryReader
    {
        /// <summary>
        /// Gets and returns this reader's H2alang Library instance.
        /// </summary>
        public Library LocalizationLibrary
        {
            get { return localizationLibrary; }
        }

        private readonly Library localizationLibrary;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuerillaBinaryReader"/> class using the supplied localization library file name, and stream.
        /// </summary>
        /// <param name="localizationLibraryFileName">The path to the localization library.</param>
        /// <param name="input">The input stream.</param>
        public GuerillaBinaryReader(string localizationLibraryFileName, Stream input) : base(input)
        {
            localizationLibrary = new Library(localizationLibraryFileName);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GuerillaBinaryReader"/> class using the supplied localization library, and stream.
        /// </summary>
        /// <param name="localizationLibrary">The localization library.</param>
        /// <param name="input">The input stream.</param>
        public GuerillaBinaryReader(Library localizationLibrary, Stream input) : base(input)
        {
            this.localizationLibrary = localizationLibrary;
        }

        /// <summary>
        /// Reads a Guerilla tag field structure from the current stream and advances the current position of the stream by sixteen bytes.
        /// </summary>
        /// <returns>A tag field structure.</returns>
        public TagField ReadTagField()
        {
            //Initialize
            TagField field = new TagField();

            //Read
            field.Type = (FieldType)ReadInt16();
            BaseStream.Seek(2, SeekOrigin.Current);
            field.NameAddress = ReadInt32();
            field.DefinitionAddress = ReadInt32();
            field.GroupTag = ReadInt32();

            //Return
            return field;
        }
        /// <summary>
        /// Reads a Guerilla tag group structure from the current stream and advances the current position of the stream by one-hundred-and-twelve bytes.
        /// </summary>
        /// <returns>A tag group instance.</returns>
        public TagGroup ReadTagGroup()
        {
            //Initialize
            TagGroup tagGroup = new TagGroup();
            tagGroup.ChildGroupTags = new int[16];

            //Read
            tagGroup.NameAddress = ReadInt32();
            tagGroup.Flags = ReadInt32();
            tagGroup.GroupTag = ReadTag();
            tagGroup.ParentGroupTag = ReadTag();
            tagGroup.Version = ReadInt16();
            tagGroup.Initialized = ReadByte();
            BaseStream.Seek(1, SeekOrigin.Current);
            tagGroup.PostProcessProcedure = ReadInt32();
            tagGroup.SavePostProcessProcedure = ReadInt32();
            tagGroup.PostprocessForSyncProcedure = ReadInt32();
            BaseStream.Seek(4, SeekOrigin.Current);
            tagGroup.DefinitionAddress = ReadInt32();
            for (int i = 0; i < 16; i++) tagGroup.ChildGroupTags[i] = ReadInt32();
            tagGroup.ChildsCount = ReadInt16();
            BaseStream.Seek(2, SeekOrigin.Current);
            tagGroup.DefaultTagPathAddress = ReadInt32();

            //Return
            return tagGroup;
        }
        /// <summary>
        /// Reads a Guerilla tag block definition structure from the current stream and advances the current position of the stream by fifty-two bytes.
        /// </summary>
        /// <returns>A tag block definition instance.</returns>
        public TagBlockDefinition ReadTagBlockDefinition()
        {
            //Initialize
            TagBlockDefinition tagBlock = new TagBlockDefinition();

            //Read
            tagBlock.DisplayNameAddress = ReadInt32();
            tagBlock.NameAddress = ReadInt32();
            tagBlock.Flags = ReadInt32();
            tagBlock.MaximumElementCount = ReadInt32();
            tagBlock.MaximumElementCountStringAddress = ReadInt32();
            tagBlock.FieldSetsAddress = ReadInt32();
            tagBlock.FieldSetCount = ReadInt32();
            tagBlock.FieldSetLatestAddress = ReadInt32();
            BaseStream.Seek(4, SeekOrigin.Current);
            tagBlock.PostProcessProcedure = ReadInt32();
            tagBlock.FormatProcedure = ReadInt32();
            tagBlock.GenerateDefaultProcedure = ReadInt32();
            tagBlock.DisposeElementProcedure = ReadInt32();
            
            //Return
            return tagBlock;
        }
        /// <summary>
        /// Reads a Guerilla tag field set version structure from the current stream and advances the current position of the stream by twenty bytes.
        /// </summary>
        /// <returns>A tag field set version instance.</returns>
        public TagFieldSetVersion ReadTagFieldSetVersion()
        {
            //Initialize
            TagFieldSetVersion fieldSetVersion = new TagFieldSetVersion();

            //Read
            fieldSetVersion.FieldsAddress = ReadInt32();
            fieldSetVersion.Index = ReadInt32();
            fieldSetVersion.UpgradeProcedure = ReadInt32();
            BaseStream.Seek(4, SeekOrigin.Current);
            fieldSetVersion.SizeOf = ReadInt32();

            //Return
            return fieldSetVersion;
        }
        /// <summary>
        /// Reads a Guerilla tag field set structure from the current stream and advances the current position of the stream by fourty bytes.
        /// </summary>
        /// <returns>A tag field set instance.</returns>
        public TagFieldSet ReadTagFieldSet()
        {
            //Initialize
            TagFieldSet fieldSet = new TagFieldSet();

            //Read
            fieldSet.Address = (int)(BaseStream.Position + Guerilla.BaseAddress);
            fieldSet.Version = ReadTagFieldSetVersion();
            fieldSet.Size = ReadInt32();
            fieldSet.AlignmentBit = ReadInt32();
            fieldSet.ParentVersionIndex = ReadInt32();
            fieldSet.FieldsAddress = ReadInt32();
            fieldSet.SizeStringAddress = ReadInt32();

            //Return
            return fieldSet;
        }
        /// <summary>
        /// Reads a four-character code tag from the current stream and advances the stream 4 bytes.
        /// </summary>
        /// <returns>A tag.</returns>
        public Tag ReadTag()
        {
            byte[] tag = ReadBytes(4); Array.Reverse(tag);
            return Encoding.UTF8.GetString(tag);
        }
        /// <summary>
        /// Reads a null-terminated string from the current stream and advances the stream in accordance with the Encoding used and specific string being read from the stream.
        /// </summary>
        /// <returns>A string.</returns>
        public string ReadNullTerminatedString()
        {
            //Prepare
            StringBuilder builder = new StringBuilder();
            char c = (char)0;

            //Read
            while((c = ReadChar()) != '\0')
                builder.Append(c);

            //Get string
            return builder.ToString();
        }
        /// <summary>
        /// Reads a localized string from either the underlying stream or the localization library.
        /// </summary>
        /// <param name="num">The address or resource identifier of the string.</param>
        /// <returns>A string.</returns>
        public string ReadLocalizedString(int num)
        {
            //Check
            if (num == 0) return string.Empty;

            //Prepare
            long position = BaseStream.Position;
            string s = string.Empty;

            //Check
            if (num < Guerilla.BaseAddress) try { s = localizationLibrary.LoadString(num); } catch { }
            else if (num > Guerilla.BaseAddress && (num - Guerilla.BaseAddress) < BaseStream.Length)
            {
                BaseStream.Seek(num - Guerilla.BaseAddress, SeekOrigin.Begin);
                s = ReadNullTerminatedString();
            }

            //Go back
            BaseStream.Seek(position, SeekOrigin.Begin);

            //Return
            return s;
        }
        /// <summary>
        /// Releases all resources used by this instance and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            localizationLibrary.Dispose();
        }
    }
}
