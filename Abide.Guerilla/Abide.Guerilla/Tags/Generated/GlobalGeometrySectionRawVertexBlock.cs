using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(196, 4)]
	public unsafe struct GlobalGeometrySectionRawVertexBlock
	{
		public Vector3 Position0;
		[Field("Node Index (OLD)*", null)]
		public int NodeIndexOLD2;
		[Field("node_weight*", null)]
		public float NodeWeight5;
		[Field("Node Index (NEW)*", null)]
		public int NodeIndexNEW8;
		[Field("Use New Node Indices*", null)]
		public int UseNewNodeIndices10;
		[Field("Adjusted Compound Node Index*", null)]
		public int AdjustedCompoundNodeIndex11;
		[Field("texcoord*", null)]
		public Vector2 Texcoord12;
		[Field("Normal*", null)]
		public Vector3 Normal13;
		[Field("Binormal*", null)]
		public Vector3 Binormal14;
		[Field("Tangent*", null)]
		public Vector3 Tangent15;
		[Field("Anisotropic Binormal*", null)]
		public Vector3 AnisotropicBinormal16;
		[Field("Secondary texcoord*", null)]
		public Vector2 SecondaryTexcoord17;
		[Field("Primary Lightmap Color", null)]
		public ColorRgbF PrimaryLightmapColor18;
		[Field("Primary Lightmap texcoord", null)]
		public Vector2 PrimaryLightmapTexcoord19;
		[Field("Primary Lightmap Incident Direction", null)]
		public Vector3 PrimaryLightmapIncidentDirection20;
		[Field("", null)]
		public fixed byte _21[12];
		[Field("", null)]
		public fixed byte _22[8];
		[Field("", null)]
		public fixed byte _23[12];
	}
}
#pragma warning restore CS1591
