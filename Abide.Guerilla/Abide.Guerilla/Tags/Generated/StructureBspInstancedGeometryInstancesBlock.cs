using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(88, 4)]
	public unsafe struct StructureBspInstancedGeometryInstancesBlock
	{
		public enum Flags6Options
		{
			NotInLightprobes_0 = 1,
		}
		public enum PathfindingPolicy12Options
		{
			Cutout_0 = 0,
			Static_1 = 1,
			None_2 = 2,
		}
		public enum LightmappingPolicy13Options
		{
			PerPixel_0 = 0,
			PerVertex_1 = 1,
		}
		[Field("Scale", null)]
		public float Scale0;
		[Field("Forward", null)]
		public Vector3 Forward1;
		[Field("Left", null)]
		public Vector3 Left2;
		[Field("Up", null)]
		public Vector3 Up3;
		public Vector3 Position4;
		[Field("Instance Definition*", null)]
		public short InstanceDefinition5;
		[Field("Flags", typeof(Flags6Options))]
		public short Flags6;
		[Field("", null)]
		public fixed byte _7[4];
		[Field("", null)]
		public fixed byte _8[12];
		[Field("", null)]
		public fixed byte _9[4];
		[Field("Checksum*", null)]
		public int Checksum10;
		[Field("Name*^", null)]
		public StringId Name11;
		[Field("Pathfinding Policy*", typeof(PathfindingPolicy12Options))]
		public short PathfindingPolicy12;
		[Field("Lightmapping Policy*", typeof(LightmappingPolicy13Options))]
		public short LightmappingPolicy13;
	}
}
#pragma warning restore CS1591
