using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct TextValuePairBlock
	{
		[Field("value pairs^", null)]
		public TagReference ValuePairs0;
	}
}
#pragma warning restore CS1591
