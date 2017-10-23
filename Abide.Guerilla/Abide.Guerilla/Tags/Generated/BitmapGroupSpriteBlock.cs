using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct BitmapGroupSpriteBlock
	{
		[Field("Bitmap Index*", null)]
		public short BitmapIndex0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("", null)]
		public fixed byte _2[4];
		[Field("Left*", null)]
		public float Left3;
		[Field("Right*", null)]
		public float Right4;
		[Field("Top*", null)]
		public float Top5;
		[Field("Bottom*", null)]
		public float Bottom6;
		[Field("Registration Point*", null)]
		public Vector2 RegistrationPoint7;
	}
}
#pragma warning restore CS1591
