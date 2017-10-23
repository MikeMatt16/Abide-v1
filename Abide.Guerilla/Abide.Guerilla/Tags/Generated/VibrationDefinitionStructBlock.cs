using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(32, 4)]
	public unsafe struct VibrationDefinitionStructBlock
	{
		[Field("low frequency vibration", typeof(VibrationFrequencyDefinitionStructBlock))]
		[Block("Vibration Frequency Definition Struct", 1, typeof(VibrationFrequencyDefinitionStructBlock))]
		public VibrationFrequencyDefinitionStructBlock LowFrequencyVibration1;
		[Field("high frequency vibration", typeof(VibrationFrequencyDefinitionStructBlock))]
		[Block("Vibration Frequency Definition Struct", 1, typeof(VibrationFrequencyDefinitionStructBlock))]
		public VibrationFrequencyDefinitionStructBlock HighFrequencyVibration3;
		[Field("", null)]
		public fixed byte _4[16];
	}
}
#pragma warning restore CS1591
