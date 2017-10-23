using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(48, 4)]
	public unsafe struct StaticSpawnZoneBlock
	{
		[Field("Data", typeof(StaticSpawnZoneDataStructBlock))]
		[Block("Static Spawn Zone Data Struct", 1, typeof(StaticSpawnZoneDataStructBlock))]
		public StaticSpawnZoneDataStructBlock Data2;
		public Vector3 Position3;
		[Field("Lower Height", null)]
		public float LowerHeight4;
		[Field("Upper Height", null)]
		public float UpperHeight5;
		[Field("Inner Radius", null)]
		public float InnerRadius6;
		[Field("Outer Radius", null)]
		public float OuterRadius7;
		[Field("Weight", null)]
		public float Weight8;
	}
}
#pragma warning restore CS1591
