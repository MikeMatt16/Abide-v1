using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("screen_effect", "egor", "����", typeof(ScreenEffectBlock))]
	[FieldSet(156, 4)]
	public unsafe struct ScreenEffectBlock
	{
		[Field("", null)]
		public fixed byte _1[64];
		[Field("shader", null)]
		public TagReference Shader2;
		[Field("", null)]
		public fixed byte _3[64];
		[Field("pass references", null)]
		[Block("Pass Reference", 8, typeof(RasterizerScreenEffectPassReferenceBlock))]
		public TagBlock PassReferences4;
	}
}
#pragma warning restore CS1591
