using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(84, 4)]
	public unsafe struct ScenarioStructureBspReferenceBlock
	{
		public enum Flags9Options
		{
			DefaultSkyEnabled_0 = 1,
		}
		[Field("", null)]
		public fixed byte _0[16];
		[Field("Structure BSP^", null)]
		public TagReference StructureBSP1;
		[Field("Structure Lightmap^", null)]
		public TagReference StructureLightmap2;
		[Field("", null)]
		public fixed byte _3[4];
		[Field("UNUSED radiance est. search distance", null)]
		public float UNUSEDRadianceEstSearchDistance4;
		[Field("", null)]
		public fixed byte _5[4];
		[Field("UNUSED luminels per world unit", null)]
		public float UNUSEDLuminelsPerWorldUnit6;
		[Field("UNUSED output white reference", null)]
		public float UNUSEDOutputWhiteReference7;
		[Field("", null)]
		public fixed byte _8[8];
		[Field("Flags", typeof(Flags9Options))]
		public short Flags9;
		[Field("", null)]
		public fixed byte _10[2];
		[Field("Default Sky", null)]
		public short DefaultSky11;
		[Field("", null)]
		public fixed byte _12[2];
	}
}
#pragma warning restore CS1591
