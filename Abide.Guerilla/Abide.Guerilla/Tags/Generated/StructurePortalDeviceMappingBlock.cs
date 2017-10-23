using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct StructurePortalDeviceMappingBlock
	{
		[Field("device portal associations", null)]
		[Block("Structure Device Portal Association Block", 128, typeof(StructureDevicePortalAssociationBlock))]
		public TagBlock DevicePortalAssociations0;
		[Field("game portal to portal map", null)]
		[Block("Game Portal To Portal Mapping Block", 128, typeof(GamePortalToPortalMappingBlock))]
		public TagBlock GamePortalToPortalMap1;
	}
}
#pragma warning restore CS1591
