using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(228, 4)]
	public unsafe struct MaterialsSweetenersStructBlock
	{
		public enum SweetenerInheritanceFlags15Options
		{
			SoundSmall_0 = 1,
			SoundMedium_1 = 2,
			SoundLarge_2 = 4,
			SoundRolling_3 = 8,
			SoundGrinding_4 = 16,
			SoundMelee_5 = 32,
			__6 = 64,
			EffectSmall_7 = 128,
			EffectMedium_8 = 256,
			EffectLarge_9 = 512,
			EffectRolling_10 = 1024,
			EffectGrinding_11 = 2048,
			EffectMelee_12 = 4096,
			__13 = 8192,
		}
		[Field("sound sweetener (small)", null)]
		public TagReference SoundSweetenerSmall0;
		[Field("sound sweetener (medium)", null)]
		public TagReference SoundSweetenerMedium1;
		[Field("sound sweetener (large)", null)]
		public TagReference SoundSweetenerLarge2;
		[Field("sound sweetener rolling", null)]
		public TagReference SoundSweetenerRolling3;
		[Field("sound sweetener grinding", null)]
		public TagReference SoundSweetenerGrinding4;
		[Field("sound sweetener (melee)", null)]
		public TagReference SoundSweetenerMelee5;
		[Field("", null)]
		public TagReference _6;
		[Field("effect sweetener (small)", null)]
		public TagReference EffectSweetenerSmall7;
		[Field("effect sweetener (medium)", null)]
		public TagReference EffectSweetenerMedium8;
		[Field("effect sweetener (large)", null)]
		public TagReference EffectSweetenerLarge9;
		[Field("effect sweetener rolling", null)]
		public TagReference EffectSweetenerRolling10;
		[Field("effect sweetener grinding", null)]
		public TagReference EffectSweetenerGrinding11;
		[Field("effect sweetener (melee)", null)]
		public TagReference EffectSweetenerMelee12;
		[Field("", null)]
		public TagReference _13;
		[Field("sweetener inheritance flags", typeof(SweetenerInheritanceFlags15Options))]
		public int SweetenerInheritanceFlags15;
	}
}
#pragma warning restore CS1591
