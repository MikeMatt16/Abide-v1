using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(68, 4)]
	public unsafe struct ParticleSystemDefinitionBlockNew
	{
		public enum CoordinateSystem2Options
		{
			World_0 = 0,
			Local_1 = 1,
			Parent_2 = 2,
		}
		public enum Environment3Options
		{
			AnyEnvironment_0 = 0,
			AirOnly_1 = 1,
			WaterOnly_2 = 2,
			SpaceOnly_3 = 3,
		}
		public enum Disposition4Options
		{
			EitherMode_0 = 0,
			ViolentModeOnly_1 = 1,
			NonviolentModeOnly_2 = 2,
		}
		public enum CameraMode5Options
		{
			IndependentOfCameraMode_0 = 0,
			OnlyInFirstPerson_1 = 1,
			OnlyInThirdPerson_2 = 2,
			BothFirstAndThird_3 = 3,
		}
		public enum Flags7Options
		{
			Glow_0 = 1,
			Cinematics_1 = 2,
			LoopingParticle_2 = 4,
			DisabledForDebugging_3 = 8,
			InheritEffectVelocity_4 = 16,
			DonTRenderSystem_5 = 32,
			RenderWhenZoomed_6 = 64,
			SpreadBetweenTicks_7 = 128,
			PersistentParticle_8 = 256,
			ExpensiveVisibility_9 = 512,
		}
		[Field("particle", null)]
		public TagReference Particle0;
		[Field("location", null)]
		public int Location1;
		[Field("coordinate system", typeof(CoordinateSystem2Options))]
		public short CoordinateSystem2;
		[Field("environment", typeof(Environment3Options))]
		public short Environment3;
		[Field("disposition", typeof(Disposition4Options))]
		public short Disposition4;
		[Field("camera mode", typeof(CameraMode5Options))]
		public short CameraMode5;
		[Field("sort bias#use values between -10 and 10 to move closer and farther from camera (positive is closer)", null)]
		public short SortBias6;
		[Field("flags", typeof(Flags7Options))]
		public short Flags7;
		[Field("LOD in distance#defaults to 0.0", null)]
		public float LODInDistance8;
		[Field("LOD feather in delta#defaults to 0.0", null)]
		public float LODFeatherInDelta9;
		[Field("", null)]
		public fixed byte _10[4];
		[Field("LOD out distance#defaults to 30.0", null)]
		public float LODOutDistance11;
		[Field("LOD feather out delta#defaults to 10.0", null)]
		public float LODFeatherOutDelta12;
		[Field("", null)]
		public fixed byte _13[4];
		[Field("emitters", null)]
		[Block("Particle System Emitter Definition Block", 8, typeof(ParticleSystemEmitterDefinitionBlock))]
		public TagBlock Emitters14;
	}
}
#pragma warning restore CS1591
