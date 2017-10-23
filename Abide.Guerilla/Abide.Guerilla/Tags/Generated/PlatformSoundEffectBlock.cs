using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(40, 4)]
	public unsafe struct PlatformSoundEffectBlock
	{
		[Field("function inputs", null)]
		[Block("Platform Sound Effect Function Block", 16, typeof(PlatformSoundEffectFunctionBlock))]
		public TagBlock FunctionInputs0;
		[Field("constant inputs", null)]
		[Block("Platform Sound Effect Constant Block", 16, typeof(PlatformSoundEffectConstantBlock))]
		public TagBlock ConstantInputs1;
		[Field("template override descriptors", null)]
		[Block("Platform Sound Effect Override Descriptor Block", 16, typeof(PlatformSoundEffectOverrideDescriptorBlock))]
		public TagBlock TemplateOverrideDescriptors2;
		[Field("input overrides", null)]
		public int InputOverrides3;
	}
}
#pragma warning restore CS1591
