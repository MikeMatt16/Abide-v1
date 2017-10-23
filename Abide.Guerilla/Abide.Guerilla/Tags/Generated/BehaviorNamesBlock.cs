using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct BehaviorNamesBlock
	{
		[Field("behavior name*^", null)]
		public String BehaviorName0;
	}
}
#pragma warning restore CS1591
