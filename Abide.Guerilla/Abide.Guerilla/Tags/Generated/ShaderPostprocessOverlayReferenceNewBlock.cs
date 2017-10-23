using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct ShaderPostprocessOverlayReferenceNewBlock
	{
		[Field("overlay index", null)]
		public short OverlayIndex0;
		[Field("transform index", null)]
		public short TransformIndex1;
	}
}
#pragma warning restore CS1591
