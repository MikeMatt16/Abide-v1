using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(60, 4)]
	public unsafe struct StructureBspMarkerBlock
	{
		[Field("Name*", null)]
		public String Name0;
		[Field("Rotation", null)]
		public Quaternion Rotation1;
		public Vector3 Position2;
	}
}
#pragma warning restore CS1591
