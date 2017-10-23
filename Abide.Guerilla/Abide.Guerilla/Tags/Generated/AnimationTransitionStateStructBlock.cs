using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct AnimationTransitionStateStructBlock
	{
		[Field("state name*#name of the state", null)]
		public StringId StateName0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("index a*#first level sub-index into state", null)]
		public int IndexA2;
		[Field("index b*#second level sub-index into state", null)]
		public int IndexB3;
	}
}
#pragma warning restore CS1591
