using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(44, 4)]
	public unsafe struct SoundEffectTemplatesBlock
	{
		public enum Flags3Options
		{
			UseHighLevelParameters_0 = 1,
			CustomParameters_1 = 2,
		}
		[Field("dsp effect", null)]
		public StringId DspEffect0;
		[Field("explanation", null)]
		[Data(65536)]
		public TagBlock Explanation2;
		[Field("flags", typeof(Flags3Options))]
		public int Flags3;
		[Field("", null)]
		public short _4;
		[Field("", null)]
		public short _5;
		[Field("parameters", null)]
		[Block("Sound Effect Template Parameter Block", 128, typeof(SoundEffectTemplateParameterBlock))]
		public TagBlock Parameters6;
	}
}
#pragma warning restore CS1591
