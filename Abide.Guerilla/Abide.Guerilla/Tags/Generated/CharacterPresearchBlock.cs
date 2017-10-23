using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct CharacterPresearchBlock
	{
		public enum PreSearchFlags0Options
		{
			Flag1_0 = 1,
		}
		[Field("Pre-search flags", typeof(PreSearchFlags0Options))]
		public int PreSearchFlags0;
		[Field("min presearch time:seconds#If the min presearch time expires and the target is (actually) outside the min-certainty radius, presearch turns off", null)]
		public FloatBounds MinPresearchTime1;
		[Field("max presearch time:seconds#Presearch turns off after the given time", null)]
		public FloatBounds MaxPresearchTime2;
		[Field("Min certainty radius", null)]
		public float MinCertaintyRadius3;
		[Field("DEPRECATED", null)]
		public float DEPRECATED4;
		[Field("min suppressing time#if the min suppressing time expires and the target is outside the min-certainty radius, suppressing fire turns off", null)]
		public FloatBounds MinSuppressingTime5;
	}
}
#pragma warning restore CS1591
