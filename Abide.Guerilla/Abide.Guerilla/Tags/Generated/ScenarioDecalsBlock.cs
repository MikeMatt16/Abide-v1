using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct ScenarioDecalsBlock
	{
		[Field("Decal Type^", null)]
		public short DecalType0;
		[Field("Yaw[-127,127]*", null)]
		public int Yaw1271271;
		[Field("Pitch[-127,127]*", null)]
		public int Pitch1271272;
		public Vector3 Position3;
	}
}
#pragma warning restore CS1591
