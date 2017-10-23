using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("wind", "wind", "����", typeof(WindBlock))]
	[FieldSet(64, 4)]
	public unsafe struct WindBlock
	{
		[Field("velocity:world units#the wind magnitude in the weather region scales the wind between these bounds", null)]
		public FloatBounds Velocity0;
		[Field("variation area#the wind direction varies inside a box defined by these angles on either side of the direction from the weather region.", null)]
		public Vector2 VariationArea1;
		[Field("local variation weight", null)]
		public float LocalVariationWeight2;
		[Field("local variation rate", null)]
		public float LocalVariationRate3;
		[Field("damping", null)]
		public float Damping4;
		[Field("", null)]
		public fixed byte _5[36];
	}
}
#pragma warning restore CS1591
