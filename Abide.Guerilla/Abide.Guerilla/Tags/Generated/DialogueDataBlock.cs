using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct DialogueDataBlock
	{
		[Field("start index (postprocess)*", null)]
		public short StartIndexPostprocess0;
		[Field("length (postprocess)*", null)]
		public short LengthPostprocess1;
	}
}
#pragma warning restore CS1591
