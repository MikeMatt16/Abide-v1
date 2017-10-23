using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct ModelObjectDataBlock
	{
		public enum Type0Options
		{
			NotSet_0 = 0,
			UserDefined_1 = 1,
			AutoGenerated_2 = 2,
		}
		[Field("type*", typeof(Type0Options))]
		public short Type0;
		[Field("", null)]
		public fixed byte _1[2];
		public Vector3 Offset2;
		[Field("radius*", null)]
		public float Radius3;
	}
}
#pragma warning restore CS1591
