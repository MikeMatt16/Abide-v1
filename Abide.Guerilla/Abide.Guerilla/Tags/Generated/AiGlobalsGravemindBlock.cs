using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct AiGlobalsGravemindBlock
	{
		[Field("min retreat time:secs", null)]
		public float MinRetreatTime0;
		[Field("ideal retreat time:secs", null)]
		public float IdealRetreatTime1;
		[Field("max retreat time:secs", null)]
		public float MaxRetreatTime2;
	}
}
#pragma warning restore CS1591
