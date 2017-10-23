using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("camera_track", "trak", "����", typeof(CameraTrackBlock))]
	[FieldSet(16, 4)]
	public unsafe struct CameraTrackBlock
	{
		public enum Flags0Options
		{
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("control points", null)]
		[Block("Camera Track Control Point Block", 16, typeof(CameraTrackControlPointBlock))]
		public TagBlock ControlPoints1;
		[Field("", null)]
		public fixed byte _2[32];
	}
}
#pragma warning restore CS1591
