using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct UserHintRayBlock
	{
		public Vector3 Point0;
		[Field("reference frame*", null)]
		public short ReferenceFrame1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("Vector", null)]
		public Vector3 Vector3;
	}
}
#pragma warning restore CS1591
