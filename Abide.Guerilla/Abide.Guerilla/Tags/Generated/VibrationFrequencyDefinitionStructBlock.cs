using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct VibrationFrequencyDefinitionStructBlock
	{
		[Field("duration:seconds", null)]
		public float Duration0;
		[Field("dirty whore", typeof(MappingFunctionBlock))]
		[Block("Mapping Function", 1, typeof(MappingFunctionBlock))]
		public MappingFunctionBlock DirtyWhore2;
		[Field("", null)]
		public fixed byte _3[16];
	}
}
#pragma warning restore CS1591
