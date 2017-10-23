using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(64, 4)]
	public unsafe struct DecoratorProjectedDecalBlock
	{
		[Field("Decorator Set", null)]
		public byte DecoratorSet0;
		[Field("Decorator Class", null)]
		public int DecoratorClass1;
		[Field("Decorator Permutation", null)]
		public int DecoratorPermutation2;
		[Field("Sprite Index", null)]
		public int SpriteIndex3;
		public Vector3 Position4;
		[Field("Left", null)]
		public Vector3 Left5;
		[Field("Up", null)]
		public Vector3 Up6;
		[Field("Extents", null)]
		public Vector3 Extents7;
		public Vector3 PreviousPosition8;
	}
}
#pragma warning restore CS1591
