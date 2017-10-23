using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct UserHintClimbBlock
	{
		public enum Flags0Options
		{
			Bidirectional_0 = 1,
			Closed_1 = 2,
		}
		[Field("Flags", typeof(Flags0Options))]
		public short Flags0;
		[Field("geometry index*", null)]
		public short GeometryIndex1;
	}
}
#pragma warning restore CS1591
