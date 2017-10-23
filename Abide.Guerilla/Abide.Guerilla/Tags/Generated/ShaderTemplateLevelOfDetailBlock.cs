using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct ShaderTemplateLevelOfDetailBlock
	{
		[Field("Projected Diameter:pixels", null)]
		public float ProjectedDiameter0;
		[Field("Pass", null)]
		[Block("Pass", 16, typeof(ShaderTemplatePassReferenceBlock))]
		public TagBlock Pass1;
	}
}
#pragma warning restore CS1591
