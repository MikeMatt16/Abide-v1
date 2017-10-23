using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct SoundPlaybackParameterDefinitionBlock
	{
		[Field("scale bounds", null)]
		public FloatBounds ScaleBounds0;
		[Field("random base and variance", null)]
		public FloatBounds RandomBaseAndVariance1;
	}
}
#pragma warning restore CS1591
