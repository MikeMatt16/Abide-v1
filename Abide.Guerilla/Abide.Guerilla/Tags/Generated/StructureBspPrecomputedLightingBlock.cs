using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(96, 4)]
	public unsafe struct StructureBspPrecomputedLightingBlock
	{
		public enum LightType1Options
		{
			FreeStanding_0 = 0,
			AttachedToEditorObject_1 = 1,
			AttachedToStructureObject_2 = 2,
		}
		[Field("Index*", null)]
		public int Index0;
		[Field("Light Type*", typeof(LightType1Options))]
		public short LightType1;
		[Field("Attachment Index*", null)]
		public int AttachmentIndex2;
		[Field("Object Type*", null)]
		public int ObjectType3;
		[Field("Visibility*", typeof(VisibilityStructBlock))]
		[Block("Visibility Struct", 1, typeof(VisibilityStructBlock))]
		public VisibilityStructBlock Visibility4;
	}
}
#pragma warning restore CS1591
