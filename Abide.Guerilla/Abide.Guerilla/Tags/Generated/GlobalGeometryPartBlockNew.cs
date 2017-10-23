using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(72, 4)]
	public unsafe struct GlobalGeometryPartBlockNew
	{
		public enum Type0Options
		{
			NotDrawn_0 = 0,
			OpaqueShadowOnly_1 = 1,
			OpaqueShadowCasting_2 = 2,
			OpaqueNonshadowing_3 = 3,
			Transparent_4 = 4,
			LightmapOnly_5 = 5,
		}
		public enum Flags1Options
		{
			Decalable_0 = 1,
			NewPartTypes_1 = 2,
			DislikesPhotons_2 = 4,
			OverrideTriangleList_3 = 8,
			IgnoredByLightmapper_4 = 16,
		}
		[Field("Type*", typeof(Type0Options))]
		public short Type0;
		[Field("Flags*", typeof(Flags1Options))]
		public short Flags1;
		[Field("Material*", null)]
		public short Material2;
		[Field("Strip Start Index*", null)]
		public short StripStartIndex3;
		[Field("Strip Length*", null)]
		public short StripLength4;
		[Field("First Subpart Index*", null)]
		public short FirstSubpartIndex5;
		[Field("Subpart Count*", null)]
		public short SubpartCount6;
		[Field("Max Nodes/Vertex*", null)]
		public int MaxNodesVertex7;
		[Field("Contributing Compound Node Count*", null)]
		public int ContributingCompoundNodeCount8;
		public Vector3 Position10;
		[Field("Node Index*", null)]
		public int NodeIndex12;
		[Field("Node Weight*", null)]
		public float NodeWeight15;
		[Field("lod mipmap magic number*", null)]
		public float LodMipmapMagicNumber17;
		[Field("", null)]
		public fixed byte _18[24];
	}
}
#pragma warning restore CS1591
