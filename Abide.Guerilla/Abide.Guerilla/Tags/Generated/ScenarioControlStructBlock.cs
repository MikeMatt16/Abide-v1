using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct ScenarioControlStructBlock
	{
		public enum Flags0Options
		{
			UsableFromBothSides_0 = 1,
		}
		[Field("Flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("*DON'T TOUCH THIS", null)]
		public short DONTTOUCHTHIS1;
		[Field("", null)]
		public fixed byte _2[2];
	}
}
#pragma warning restore CS1591
