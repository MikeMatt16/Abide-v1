using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(56, 4)]
	public unsafe struct ScenarioCutsceneFlagBlock
	{
		[Field("", null)]
		public fixed byte _0[4];
		[Field("Name^", null)]
		public String Name1;
		public Vector3 Position2;
		[Field("Facing", null)]
		public Vector2 Facing3;
		[Field("", null)]
		public fixed byte _4[36];
	}
}
#pragma warning restore CS1591
