using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(428, 4)]
	public unsafe struct RasterizerDataBlock
	{
		public enum Flags28Options
		{
			TintEdgeDensity_0 = 1,
		}
		[Field("distance attenuation", null)]
		public TagReference DistanceAttenuation1;
		[Field("vector normalization", null)]
		public TagReference VectorNormalization2;
		[Field("gradients", null)]
		public TagReference Gradients3;
		[Field("UNUSED", null)]
		public TagReference UNUSED4;
		[Field("UNUSED", null)]
		public TagReference UNUSED5;
		[Field("UNUSED", null)]
		public TagReference UNUSED6;
		[Field("glow", null)]
		public TagReference Glow7;
		[Field("UNUSED", null)]
		public TagReference UNUSED8;
		[Field("UNUSED", null)]
		public TagReference UNUSED9;
		[Field("", null)]
		public fixed byte _10[16];
		[Field("global vertex shaders", null)]
		[Block("Vertex Shader Reference Block", 32, typeof(VertexShaderReferenceBlock))]
		public TagBlock GlobalVertexShaders11;
		[Field("default 2D", null)]
		public TagReference Default2D13;
		[Field("default 3D", null)]
		public TagReference Default3D14;
		[Field("default cube map", null)]
		public TagReference DefaultCubeMap15;
		[Field("UNUSED", null)]
		public TagReference UNUSED17;
		[Field("UNUSED", null)]
		public TagReference UNUSED18;
		[Field("UNUSED", null)]
		public TagReference UNUSED19;
		[Field("UNUSED", null)]
		public TagReference UNUSED20;
		[Field("UNUSED", null)]
		public TagReference UNUSED22;
		[Field("UNUSED", null)]
		public TagReference UNUSED23;
		[Field("", null)]
		public fixed byte _24[36];
		[Field("global shader", null)]
		public TagReference GlobalShader26;
		[Field("flags", typeof(Flags28Options))]
		public short Flags28;
		[Field("", null)]
		public fixed byte _29[2];
		[Field("refraction amount:pixels", null)]
		public float RefractionAmount30;
		[Field("distance falloff", null)]
		public float DistanceFalloff31;
		[Field("tint color", null)]
		public ColorRgbF TintColor32;
		[Field("hyper-stealth refraction:pixels", null)]
		public float HyperStealthRefraction33;
		[Field("hyper-stealth distance falloff", null)]
		public float HyperStealthDistanceFalloff34;
		[Field("hyper-stealth tint color", null)]
		public ColorRgbF HyperStealthTintColor35;
		[Field("UNUSED", null)]
		public TagReference UNUSED37;
	}
}
#pragma warning restore CS1591
