using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct SoundEffectTemplateAdditionalSoundInputBlock
	{
		[Field("dsp effect", null)]
		public StringId DspEffect0;
		[Field("low frequency sound", typeof(MappingFunctionBlock))]
		[Block("Mapping Function", 1, typeof(MappingFunctionBlock))]
		public MappingFunctionBlock LowFrequencySound2;
		[Field("time period: seconds", null)]
		public float TimePeriod3;
	}
}
#pragma warning restore CS1591
