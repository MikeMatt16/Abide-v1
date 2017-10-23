using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct ParticlePropertyScalarStructNewBlock
	{
		public enum InputVariable0Options
		{
			ParticleAge_0 = 0,
			ParticleEmitTime_1 = 1,
			ParticleRandom1_2 = 2,
			ParticleRandom2_3 = 3,
			EmitterAge_4 = 4,
			EmitterRandom1_5 = 5,
			EmitterRandom2_6 = 6,
			SystemLod_7 = 7,
			GameTime_8 = 8,
			EffectAScale_9 = 9,
			EffectBScale_10 = 10,
			ParticleRotation_11 = 11,
			ExplosionAnimation_12 = 12,
			ExplosionRotation_13 = 13,
			ParticleRandom3_14 = 14,
			ParticleRandom4_15 = 15,
			LocationRandom_16 = 16,
		}
		public enum RangeVariable1Options
		{
			ParticleAge_0 = 0,
			ParticleEmitTime_1 = 1,
			ParticleRandom1_2 = 2,
			ParticleRandom2_3 = 3,
			EmitterAge_4 = 4,
			EmitterRandom1_5 = 5,
			EmitterRandom2_6 = 6,
			SystemLod_7 = 7,
			GameTime_8 = 8,
			EffectAScale_9 = 9,
			EffectBScale_10 = 10,
			ParticleRotation_11 = 11,
			ExplosionAnimation_12 = 12,
			ExplosionRotation_13 = 13,
			ParticleRandom3_14 = 14,
			ParticleRandom4_15 = 15,
			LocationRandom_16 = 16,
		}
		public enum OutputModifier2Options
		{
			Empty_0 = 0,
			Plus_1 = 1,
			Times_2 = 2,
		}
		public enum OutputModifierInput3Options
		{
			ParticleAge_0 = 0,
			ParticleEmitTime_1 = 1,
			ParticleRandom1_2 = 2,
			ParticleRandom2_3 = 3,
			EmitterAge_4 = 4,
			EmitterRandom1_5 = 5,
			EmitterRandom2_6 = 6,
			SystemLod_7 = 7,
			GameTime_8 = 8,
			EffectAScale_9 = 9,
			EffectBScale_10 = 10,
			ParticleRotation_11 = 11,
			ExplosionAnimation_12 = 12,
			ExplosionRotation_13 = 13,
			ParticleRandom3_14 = 14,
			ParticleRandom4_15 = 15,
			LocationRandom_16 = 16,
		}
		[Field("Input Variable", typeof(InputVariable0Options))]
		public short InputVariable0;
		[Field("Range Variable", typeof(RangeVariable1Options))]
		public short RangeVariable1;
		[Field("Output Modifier", typeof(OutputModifier2Options))]
		public short OutputModifier2;
		[Field("Output Modifier Input", typeof(OutputModifierInput3Options))]
		public short OutputModifierInput3;
		[Field("Mapping", typeof(MappingFunctionBlock))]
		[Block("Mapping Function", 1, typeof(MappingFunctionBlock))]
		public MappingFunctionBlock Mapping5;
	}
}
#pragma warning restore CS1591
