using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(156, 4)]
	public unsafe struct StructureLightmapGroupBlock
	{
		public enum Type0Options
		{
			Normal_0 = 0,
		}
		public enum Flags1Options
		{
			Unused_0 = 1,
		}
		[Field("type", typeof(Type0Options))]
		public short Type0;
		[Field("flags", typeof(Flags1Options))]
		public short Flags1;
		[Field("structure checksum", null)]
		public int StructureChecksum2;
		[Field("section palette", null)]
		[Block("Structure Lightmap Palette Color Block", 128, typeof(StructureLightmapPaletteColorBlock))]
		public TagBlock SectionPalette3;
		[Field("writable palettes", null)]
		[Block("Structure Lightmap Palette Color Block", 128, typeof(StructureLightmapPaletteColorBlock))]
		public TagBlock WritablePalettes4;
		[Field("bitmap group", null)]
		public TagReference BitmapGroup5;
		[Field("clusters", null)]
		[Block("Lightmap Geometry Section Block", 512, typeof(LightmapGeometrySectionBlock))]
		public TagBlock Clusters6;
		[Field("cluster render info", null)]
		[Block("Lightmap Geometry Render Info Block", 1024, typeof(LightmapGeometryRenderInfoBlock))]
		public TagBlock ClusterRenderInfo7;
		[Field("poop definitions", null)]
		[Block("Lightmap Geometry Section Block", 512, typeof(LightmapGeometrySectionBlock))]
		public TagBlock PoopDefinitions8;
		[Field("lighting environments*", null)]
		[Block("Structure Lightmap Lighting Environment Block", 1024, typeof(StructureLightmapLightingEnvironmentBlock))]
		public TagBlock LightingEnvironments9;
		[Field("geometry buckets", null)]
		[Block("Lightmap Vertex Buffer Bucket Block", 1024, typeof(LightmapVertexBufferBucketBlock))]
		public TagBlock GeometryBuckets10;
		[Field("instance render info", null)]
		[Block("Lightmap Geometry Render Info Block", 1024, typeof(LightmapGeometryRenderInfoBlock))]
		public TagBlock InstanceRenderInfo11;
		[Field("instance bucket refs", null)]
		[Block("Lightmap Instance Bucket Reference Block", 2000, typeof(LightmapInstanceBucketReferenceBlock))]
		public TagBlock InstanceBucketRefs12;
		[Field("scenery object info", null)]
		[Block("Lightmap Scenery Object Info Block", 2000, typeof(LightmapSceneryObjectInfoBlock))]
		public TagBlock SceneryObjectInfo13;
		[Field("scenery object bucket refs", null)]
		[Block("Lightmap Instance Bucket Reference Block", 2000, typeof(LightmapInstanceBucketReferenceBlock))]
		public TagBlock SceneryObjectBucketRefs14;
	}
}
#pragma warning restore CS1591
