using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(88, 4)]
	public unsafe struct DamageEffectPlayerResponseBlock
	{
		public enum ResponseType0Options
		{
			Shielded_0 = 0,
			Unshielded_1 = 1,
			All_2 = 2,
		}
		[Field("response type", typeof(ResponseType0Options))]
		public short ResponseType0;
		[Field("", null)]
		public fixed byte _1[2];
		[Field("screen flash*", typeof(ScreenFlashDefinitionStructBlock))]
		[Block("Screen Flash Definition Struct", 1, typeof(ScreenFlashDefinitionStructBlock))]
		public ScreenFlashDefinitionStructBlock ScreenFlash3;
		[Field("vibration*", typeof(VibrationDefinitionStructBlock))]
		[Block("Vibration Definition Struct", 1, typeof(VibrationDefinitionStructBlock))]
		public VibrationDefinitionStructBlock Vibration5;
		[Field("sound effect", typeof(DamageEffectSoundEffectDefinitionBlock))]
		[Block("Damage Effect Sound Effect Definition", 1, typeof(DamageEffectSoundEffectDefinitionBlock))]
		public DamageEffectSoundEffectDefinitionBlock SoundEffect7;
		[Field("", null)]
		public fixed byte _8[24];
	}
}
#pragma warning restore CS1591
