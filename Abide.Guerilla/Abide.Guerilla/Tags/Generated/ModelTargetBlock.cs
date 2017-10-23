using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct ModelTargetBlock
	{
		[Field("marker name^#multiple markers become multiple spheres of the same radius", null)]
		public StringId MarkerName0;
		[Field("size#sphere radius", null)]
		public float Size1;
		[Field("cone angle#the target is only visible when viewed within this angle of the marker's x axis", null)]
		public float ConeAngle2;
		[Field("damage section#the target is associated with this damage section", null)]
		public short DamageSection3;
		[Field("variant#the target will only appear with this variant", null)]
		public short Variant4;
		[Field("targeting relevance#higher relevances turn into stronger magnetisms", null)]
		public float TargetingRelevance5;
		[Field("lock-on data", typeof(ModelTargetLockOnDataStructBlock))]
		[Block("Model Target Lock On Data Struct", 1, typeof(ModelTargetLockOnDataStructBlock))]
		public ModelTargetLockOnDataStructBlock LockOnData6;
	}
}
#pragma warning restore CS1591
