using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct CharacterBossBlock
	{
		[Field("", null)]
		public fixed byte _0[4];
		[Field("", null)]
		public fixed byte _1[36];
		[Field("flurry damage threshold:[0..1]#when more than x damage is caused a juggernaut flurry is triggered", null)]
		public float FlurryDamageThreshold2;
		[Field("flurry time:seconds#flurry lasts this long", null)]
		public float FlurryTime3;
	}
}
#pragma warning restore CS1591
