using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(40, 4)]
	public unsafe struct AiScriptReferenceBlock
	{
		[Field("script name^", null)]
		public String ScriptName0;
		[Field("", null)]
		public fixed byte _1[8];
	}
}
#pragma warning restore CS1591
