using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct CameraTrackControlPointBlock
	{
		[Field("position", null)]
		public Vector3 Position0;
		[Field("orientation", null)]
		public Quaternion Orientation1;
		[Field("", null)]
		public fixed byte _2[32];
	}
}
#pragma warning restore CS1591
