using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(64, 4)]
	public unsafe struct DontUseMeScenarioEnvironmentObjectBlock
	{
		[Field("BSP*", null)]
		public short BSP1;
		[Field("EMPTY STRING", null)]
		public short EMPTYSTRING2;
		[Field("Unique ID*", null)]
		public int UniqueID3;
		[Field("", null)]
		public fixed byte _4[4];
		[Field("Object Definition Tag*", null)]
		public Tag ObjectDefinitionTag5;
		[Field("Object*^", null)]
		public int Object6;
		[Field("", null)]
		public fixed byte _7[44];
	}
}
#pragma warning restore CS1591
