using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct GloalWindPrimitivesBlock
	{
		public enum WindPrimitiveType3Options
		{
			Vortex_0 = 0,
			Gust_1 = 1,
			Implosion_2 = 2,
			Explosion_3 = 3,
		}
		[Field("position", null)]
		public Vector3 Position0;
		[Field("radius", null)]
		public float Radius1;
		[Field("strength", null)]
		public float Strength2;
		[Field("wind primitive type", typeof(WindPrimitiveType3Options))]
		public short WindPrimitiveType3;
		[Field("", null)]
		public fixed byte _4[2];
	}
}
#pragma warning restore CS1591
