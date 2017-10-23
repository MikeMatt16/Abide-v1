using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct RenderModelMarkerBlock
	{
		[Field("region index*", null)]
		public int RegionIndex0;
		[Field("permutation index*", null)]
		public int PermutationIndex1;
		[Field("node index*", null)]
		public int NodeIndex2;
		[Field("", null)]
		public fixed byte _3[1];
		public Vector3 Translation4;
		[Field("rotation*", null)]
		public Quaternion Rotation5;
		[Field("scale", null)]
		public float Scale6;
	}
}
#pragma warning restore CS1591
