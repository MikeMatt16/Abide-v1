using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(7, 4)]
	public unsafe struct ShaderPassPostprocessConstantInfoNewBlock
	{
		[Field("parameter name", null)]
		public StringId ParameterName0;
		[Field("", null)]
		public fixed byte _1[3];
	}
}
#pragma warning restore CS1591
