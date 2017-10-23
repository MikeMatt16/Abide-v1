using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct CharacterFiringPatternPropertiesBlock
	{
		[Field("weapon^", null)]
		public TagReference Weapon0;
		[Field("", null)]
		public fixed byte _1[24];
		[Field("firing patterns", null)]
		[Block("Character Firing Pattern Block", 2, typeof(CharacterFiringPatternBlock))]
		public TagBlock FiringPatterns2;
	}
}
#pragma warning restore CS1591
