using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(44, 4)]
	public unsafe struct ScenarioScreenEffectReferenceBlock
	{
		[Field("", null)]
		public fixed byte _0[16];
		[Field("Screen Effect", null)]
		public TagReference ScreenEffect1;
		[Field("Primary Input:Interpolator", null)]
		public StringId PrimaryInput2;
		[Field("Secondary Input:Interpolator", null)]
		public StringId SecondaryInput3;
		[Field("", null)]
		public fixed byte _4[2];
		[Field("", null)]
		public fixed byte _5[2];
	}
}
#pragma warning restore CS1591
