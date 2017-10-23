using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct BspLeafBlock
	{
		[Field("cluster*", null)]
		public short Cluster0;
		[Field("surface reference count*", null)]
		public short SurfaceReferenceCount1;
		[Field("first surface reference index*", null)]
		public int FirstSurfaceReferenceIndex2;
	}
}
#pragma warning restore CS1591
