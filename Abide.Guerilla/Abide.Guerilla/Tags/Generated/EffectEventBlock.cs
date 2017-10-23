using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(72, 4)]
	public unsafe struct EffectEventBlock
	{
		public enum Flags0Options
		{
			DisabledForDebugging_0 = 1,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("skip fraction#chance that this event will be skipped entirely", null)]
		public float SkipFraction1;
		[Field("delay bounds:seconds#delay before this event takes place", null)]
		public FloatBounds DelayBounds2;
		[Field("duration bounds:seconds#duration of this event", null)]
		public FloatBounds DurationBounds3;
		[Field("parts", null)]
		[Block("Effect Part Block", 32, typeof(EffectPartBlock))]
		public TagBlock Parts4;
		[Field("", null)]
		public fixed byte _5[12];
		[Field("beams", null)]
		[Block("Beam Block", 1024, typeof(BeamBlock))]
		public TagBlock Beams6;
		[Field("accelerations", null)]
		[Block("Effect Accelerations Block", 32, typeof(EffectAccelerationsBlock))]
		public TagBlock Accelerations7;
		[Field("particle systems", null)]
		[Block("Particle System Definition Block New", 32, typeof(ParticleSystemDefinitionBlockNew))]
		public TagBlock ParticleSystems8;
	}
}
#pragma warning restore CS1591
