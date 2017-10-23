using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct QuantizedOrientationStructBlock
	{
		[Field("rotation x*", null)]
		public short RotationX0;
		[Field("rotation y*", null)]
		public short RotationY1;
		[Field("rotation z*", null)]
		public short RotationZ2;
		[Field("rotation w*", null)]
		public short RotationW3;
		public Vector3 DefaultTranslation4;
		[Field("default scale*", null)]
		public float DefaultScale5;
	}
}
#pragma warning restore CS1591
