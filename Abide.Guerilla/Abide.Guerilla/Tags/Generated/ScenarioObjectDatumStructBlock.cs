using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(48, 4)]
	public unsafe struct ScenarioObjectDatumStructBlock
	{
		public enum PlacementFlags1Options
		{
			NotAutomatically_0 = 1,
			Unused_1 = 2,
			Unused_2 = 4,
			Unused_3 = 8,
			LockTypeToEnvObject_4 = 16,
			LockTransformToEnvObject_5 = 32,
			NeverPlaced_6 = 64,
			LockNameToEnvObject_7 = 128,
			CreateAtRest_8 = 256,
		}
		public enum TransformFlags5Options
		{
			Mirrored_0 = 1,
		}
		public enum BSPPolicy8Options
		{
			Default_0 = 0,
			AlwaysPlaced_1 = 1,
			ManualBSPPlacement_2 = 2,
		}
		[Field("Placement Flags", typeof(PlacementFlags1Options))]
		public int PlacementFlags1;
		public Vector3 Position2;
		[Field("Rotation", null)]
		public Vector3 Rotation3;
		[Field("Scale", null)]
		public float Scale4;
		[Field(")Transform Flags", typeof(TransformFlags5Options))]
		public short TransformFlags5;
		[Field("Object ID", typeof(ScenarioObjectIdStructBlock))]
		[Block("Scenario Object Id Struct", 1, typeof(ScenarioObjectIdStructBlock))]
		public ScenarioObjectIdStructBlock ObjectID7;
		[Field("BSP Policy", typeof(BSPPolicy8Options))]
		public byte BSPPolicy8;
		[Field("", null)]
		public fixed byte _9[1];
		[Field("Editor Folder", null)]
		public short EditorFolder10;
	}
}
#pragma warning restore CS1591
