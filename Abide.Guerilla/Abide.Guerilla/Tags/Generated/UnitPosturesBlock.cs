using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct UnitPosturesBlock
	{
		[Field("name^", null)]
		public StringId Name0;
		[Field("", null)]
		public fixed byte _1[24];
		[Field("pill offset", null)]
		public Vector3 PillOffset2;
	}
}
#pragma warning restore CS1591
