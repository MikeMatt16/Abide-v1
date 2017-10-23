using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct UnitCameraStructBlock
	{
		[Field("pitch auto-level", null)]
		public float PitchAutoLevel2;
		[Field("pitch range", null)]
		public FloatBounds PitchRange3;
		[Field("camera tracks", null)]
		[Block("Unit Camera Track Block", 2, typeof(UnitCameraTrackBlock))]
		public TagBlock CameraTracks4;
	}
}
#pragma warning restore CS1591
