using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct ClothLinksBlock
	{
		[Field("attachment bits*", null)]
		public int AttachmentBits0;
		[Field("index1*", null)]
		public short Index11;
		[Field("index2*", null)]
		public short Index22;
		[Field("default_distance*", null)]
		public float DefaultDistance3;
		[Field("damping_multiplier*", null)]
		public float DampingMultiplier4;
	}
}
#pragma warning restore CS1591
