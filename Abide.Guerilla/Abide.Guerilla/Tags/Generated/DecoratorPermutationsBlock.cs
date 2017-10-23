using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(40, 4)]
	public unsafe struct DecoratorPermutationsBlock
	{
		public enum Flags3Options
		{
			AlignToNormal_0 = 1,
			OnlyOnGround_1 = 2,
			Upright_2 = 4,
		}
		public enum FadeDistance4Options
		{
			Close_0 = 0,
			Medium_1 = 1,
			Far_2 = 2,
		}
		[Field("name", null)]
		public StringId Name0;
		[Field("shader", null)]
		public byte Shader1;
		[Field("", null)]
		public fixed byte _2[3];
		[Field("flags", typeof(Flags3Options))]
		public byte Flags3;
		[Field("fade distance", typeof(FadeDistance4Options))]
		public byte FadeDistance4;
		[Field("index", null)]
		public int Index5;
		[Field("distribution weight", null)]
		public int DistributionWeight6;
		[Field("scale", null)]
		public FloatBounds Scale7;
		[Field("tint 1", null)]
		public ColorRgb Tint18;
		[Field("tint 2", null)]
		public ColorRgb Tint29;
		[Field("base map tint percentage", null)]
		public float BaseMapTintPercentage10;
		[Field("lightmap tint percentage", null)]
		public float LightmapTintPercentage11;
		[Field("wind scale", null)]
		public float WindScale12;
	}
}
#pragma warning restore CS1591
