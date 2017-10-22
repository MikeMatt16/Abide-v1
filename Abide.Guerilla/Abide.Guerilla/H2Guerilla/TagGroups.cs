using Abide.HaloLibrary;

namespace Abide.Guerilla.H2Guerilla
{
    internal struct TagGroup
    {
        public int NameAddress;
        public int Flags;
        public Tag GroupTag;
        public Tag ParentGroupTag;
        public short Version;
        public byte Initialized;
        public int PostProcessProcedure;
        public int SavePostProcessProcedure;
        public int PostprocessForSyncProcedure;
        public int DefinitionAddress;
        public int[] ChildGroupTags;
        public short ChildsCount;
        public int DefaultTagPathAddress;
        public string Name;
    }

    internal struct TagBlockDefinition
    {
        public int DisplayNameAddress;
        public int NameAddress;
        public int Flags;
        public int MaximumElementCount;
        public int MaximumElementCountStringAddress;
        public int FieldSetsAddress;
        public int FieldSetCount;
        public int FieldSetLatestAddress;
        public int PostProcessProcedure;
        public int FormatProcedure;
        public int GenerateDefaultProcedure;
        public int DisposeElementProcedure;
        public string DisplayName;
        public string Name;
        public string MaximumElementCountString;
    }

    internal struct TagFieldSetVersion
    {
        public int FieldsAddress;
        public int Index;
        public int UpgradeProcedure;
        public int SizeOf;
    }

    internal struct TagFieldSet
    {
        public TagFieldSetVersion Version;
        public int Size;
        public int AlignmentBit;
        public int ParentVersionIndex;
        public int FieldsAddress;
        public int SizeStringAddress;
        public int Address;
        public string SizeString;
    }
}
