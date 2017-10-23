using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct TransparentPlanesBlock
	{
		[Field("Section Index", null)]
		public short SectionIndex0;
		[Field("Part Index", null)]
		public short PartIndex1;
		[Field("plane", null)]
		public Plane3 Plane2;
	}
}
#pragma warning restore CS1591
