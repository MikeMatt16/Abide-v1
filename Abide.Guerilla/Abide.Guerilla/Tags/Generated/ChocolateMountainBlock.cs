using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("chocolate_mountain", "gldf", "����", typeof(ChocolateMountainBlock))]
	[FieldSet(12, 4)]
	public unsafe struct ChocolateMountainBlock
	{
		[Field("lighting variables", null)]
		[Block("Lighting Variables Block", 13, typeof(LightingVariablesBlock))]
		public TagBlock LightingVariables0;
	}
}
#pragma warning restore CS1591
