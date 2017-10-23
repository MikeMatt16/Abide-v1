using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct PlatformSoundEffectTemplateComponentBlock
	{
		public enum ValueType0Options
		{
			Zero_0 = 0,
			Time_1 = 1,
			Scale_2 = 2,
			Rolloff_3 = 3,
		}
		[Field("value type", typeof(ValueType0Options))]
		public int ValueType0;
		[Field("default value", null)]
		public float DefaultValue1;
		[Field("minimum value", null)]
		public float MinimumValue2;
		[Field("maximum value", null)]
		public float MaximumValue3;
	}
}
#pragma warning restore CS1591
