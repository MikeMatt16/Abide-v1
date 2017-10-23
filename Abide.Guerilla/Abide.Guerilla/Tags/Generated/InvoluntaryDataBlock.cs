using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct InvoluntaryDataBlock
	{
		[Field("involuntary vocalization index*", null)]
		public short InvoluntaryVocalizationIndex0;
		[Field("", null)]
		public fixed byte _1[2];
	}
}
#pragma warning restore CS1591
