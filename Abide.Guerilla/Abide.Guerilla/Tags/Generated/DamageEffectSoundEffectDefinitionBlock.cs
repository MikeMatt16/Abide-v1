using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct DamageEffectSoundEffectDefinitionBlock
	{
		[Field("effect name", null)]
		public StringId EffectName0;
		[Field("duration:seconds", null)]
		public float Duration1;
		[Field("effect scale function", typeof(MappingFunctionBlock))]
		[Block("Mapping Function", 1, typeof(MappingFunctionBlock))]
		public MappingFunctionBlock EffectScaleFunction3;
	}
}
#pragma warning restore CS1591
