using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(64, 4)]
	public unsafe struct ScenarioCutsceneCameraPointBlock
	{
		public enum Flags0Options
		{
			EditAsRelative_0 = 1,
		}
		public enum Type1Options
		{
			Normal_0 = 0,
			IgnoreTargetOrientation_1 = 1,
			Dolly_2 = 2,
			IgnoreTargetUpdates_3 = 3,
		}
		[Field("Flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("Type", typeof(Type1Options))]
		public short Type1;
		[Field("Name^", null)]
		public String Name2;
		[Field("", null)]
		public fixed byte _4[4];
		public Vector3 Position5;
		[Field("Orientation", null)]
		public Vector3 Orientation6;
		[Field(")Unused", null)]
		public float Unused7;
		[Field("", null)]
		public fixed byte _8[36];
	}
}
#pragma warning restore CS1591
