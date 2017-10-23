using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct AnimationAimingScreenStructBlock
	{
		[Field("right yaw per frame", null)]
		public float RightYawPerFrame0;
		[Field("left yaw per frame", null)]
		public float LeftYawPerFrame1;
		[Field("right frame count", null)]
		public short RightFrameCount2;
		[Field("left frame count", null)]
		public short LeftFrameCount3;
		[Field("down pitch per frame", null)]
		public float DownPitchPerFrame4;
		[Field("up pitch per frame", null)]
		public float UpPitchPerFrame5;
		[Field("down pitch frame count", null)]
		public short DownPitchFrameCount6;
		[Field("up pitch frame count", null)]
		public short UpPitchFrameCount7;
	}
}
#pragma warning restore CS1591
