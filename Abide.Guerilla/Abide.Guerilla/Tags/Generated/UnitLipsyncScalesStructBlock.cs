using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct UnitLipsyncScalesStructBlock
	{
		[Field("attack weight", null)]
		public float AttackWeight0;
		[Field("decay weight", null)]
		public float DecayWeight1;
	}
}
#pragma warning restore CS1591
