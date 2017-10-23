using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct UserHintWellPointBlock
	{
		public enum Type0Options
		{
			Jump_0 = 0,
			Climb_1 = 1,
			Hoist_2 = 2,
		}
		[Field("type", typeof(Type0Options))]
		public short Type0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("point", null)]
		public Vector3 Point2;
		[Field("reference frame", null)]
		public short ReferenceFrame3;
		[Field("", null)]
		public fixed byte _4[2];
		[Field("sector index", null)]
		public int SectorIndex5;
		[Field("normal", null)]
		public Vector2 Normal6;
	}
}
#pragma warning restore CS1591
