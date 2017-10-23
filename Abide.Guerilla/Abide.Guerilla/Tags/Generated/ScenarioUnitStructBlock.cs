using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct ScenarioUnitStructBlock
	{
		public enum Flags1Options
		{
			Dead_0 = 1,
			Closed_1 = 2,
			NotEnterableByPlayer_2 = 4,
		}
		[Field("Body Vitality:[0,1]", null)]
		public float BodyVitality0;
		[Field("Flags", typeof(Flags1Options))]
		public int Flags1;
	}
}
#pragma warning restore CS1591
