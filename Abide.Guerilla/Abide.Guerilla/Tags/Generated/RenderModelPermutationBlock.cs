using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct RenderModelPermutationBlock
	{
		[Field("L1 section index*:(super low)", null)]
		public short L1SectionIndex1;
		[Field("L2 section index*:(low)", null)]
		public short L2SectionIndex2;
		[Field("L3 section index*:(medium)", null)]
		public short L3SectionIndex3;
		[Field("L4 section index*:(high)", null)]
		public short L4SectionIndex4;
		[Field("L5 section index*:(super high)", null)]
		public short L5SectionIndex5;
		[Field("L6 section index*:(hollywood)", null)]
		public short L6SectionIndex6;
	}
}
#pragma warning restore CS1591
