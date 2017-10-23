using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct UserHintPointBlock
	{
		public Vector3 Point0;
		[Field("reference frame*", null)]
		public short ReferenceFrame1;
		[Field("", null)]
		public fixed byte _2[2];
	}
}
#pragma warning restore CS1591
