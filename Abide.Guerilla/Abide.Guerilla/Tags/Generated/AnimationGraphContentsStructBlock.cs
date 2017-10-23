using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(36, 4)]
	public unsafe struct AnimationGraphContentsStructBlock
	{
		[Field("modes|AABBCC", null)]
		[Block("Animation Mode Block", 512, typeof(AnimationModeBlock))]
		public TagBlock ModesAABBCC1;
		[Field("vehicle suspension|CCAABB", null)]
		[Block("Vehicle Suspension Block", 32, typeof(VehicleSuspensionBlock))]
		public TagBlock VehicleSuspensionCCAABB3;
		[Field("object overlays|CCAABB", null)]
		[Block("Object Animation Block", 32, typeof(ObjectAnimationBlock))]
		public TagBlock ObjectOverlaysCCAABB4;
	}
}
#pragma warning restore CS1591
