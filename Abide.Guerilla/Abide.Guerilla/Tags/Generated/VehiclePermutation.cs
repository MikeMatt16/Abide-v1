using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct VehiclePermutation
	{
		[Field("weight#relatively how likely this vehicle will be chosen", null)]
		public float Weight0;
		[Field("vehicle^#which vehicle to ", null)]
		public TagReference Vehicle1;
		[Field("variant name", null)]
		public StringId VariantName2;
	}
}
#pragma warning restore CS1591
