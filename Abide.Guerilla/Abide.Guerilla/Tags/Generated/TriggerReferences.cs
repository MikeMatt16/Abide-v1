using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct TriggerReferences
	{
		public enum TriggerFlags0Options
		{
			Not_0 = 1,
		}
		[Field("Trigger flags", typeof(TriggerFlags0Options))]
		public int TriggerFlags0;
		[Field("trigger^", null)]
		public short Trigger1;
		[Field("", null)]
		public fixed byte _2[2];
	}
}
#pragma warning restore CS1591
