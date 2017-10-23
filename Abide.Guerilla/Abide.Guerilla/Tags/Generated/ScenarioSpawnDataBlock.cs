using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(108, 4)]
	public unsafe struct ScenarioSpawnDataBlock
	{
		[Field("Dynamic Spawn Lower Height", null)]
		public float DynamicSpawnLowerHeight1;
		[Field("Dynamic Spawn Upper Height", null)]
		public float DynamicSpawnUpperHeight2;
		[Field("Game Object Reset Height", null)]
		public float GameObjectResetHeight3;
		[Field("", null)]
		public fixed byte _4[60];
		[Field("Dynamic Spawn Overloads", null)]
		[Block("Dynamic Spawn Zone Overload Block", 32, typeof(DynamicSpawnZoneOverloadBlock))]
		public TagBlock DynamicSpawnOverloads5;
		[Field("Static Respawn Zones", null)]
		[Block("Static Spawn Zone Block", 128, typeof(StaticSpawnZoneBlock))]
		public TagBlock StaticRespawnZones6;
		[Field("Static Initial Spawn Zones", null)]
		[Block("Static Spawn Zone Block", 128, typeof(StaticSpawnZoneBlock))]
		public TagBlock StaticInitialSpawnZones7;
	}
}
#pragma warning restore CS1591
