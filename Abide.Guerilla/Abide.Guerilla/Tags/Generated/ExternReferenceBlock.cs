using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(2, 4)]
	public unsafe struct ExternReferenceBlock
	{
		[Field("parameter index", null)]
		public int ParameterIndex0;
		[Field("extern index", null)]
		public int ExternIndex1;
	}
}
#pragma warning restore CS1591
