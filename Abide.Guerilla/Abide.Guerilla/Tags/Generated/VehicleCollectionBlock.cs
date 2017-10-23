using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("vehicle_collection", "vehc", "����", typeof(VehicleCollectionBlock))]
	[FieldSet(16, 4)]
	public unsafe struct VehicleCollectionBlock
	{
		[Field("vehicle permutations", null)]
		[Block("Vehicle Permutation", 32, typeof(VehiclePermutation))]
		public TagBlock VehiclePermutations0;
		[Field("spawn time (in seconds, 0 = default)", null)]
		public short SpawnTimeInSeconds0Default1;
		[Field("", null)]
		public fixed byte _2[2];
	}
}
#pragma warning restore CS1591
