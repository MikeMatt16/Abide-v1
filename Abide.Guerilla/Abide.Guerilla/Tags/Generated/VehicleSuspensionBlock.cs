using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(40, 4)]
	public unsafe struct VehicleSuspensionBlock
	{
		[Field("label^", null)]
		public StringId Label0;
		[Field("animation*", typeof(AnimationIndexStructBlock))]
		[Block("Animation Index Struct", 1, typeof(AnimationIndexStructBlock))]
		public AnimationIndexStructBlock Animation1;
		[Field("marker name", null)]
		public StringId MarkerName2;
		[Field("mass point offset", null)]
		public float MassPointOffset3;
		[Field("full extension ground_depth", null)]
		public float FullExtensionGroundDepth4;
		[Field("full compression ground_depth", null)]
		public float FullCompressionGroundDepth5;
		[Field("region name", null)]
		public StringId RegionName7;
		[Field("destroyed mass point offset", null)]
		public float DestroyedMassPointOffset8;
		[Field("destroyed full extension ground_depth", null)]
		public float DestroyedFullExtensionGroundDepth9;
		[Field("destroyed full compression ground_depth", null)]
		public float DestroyedFullCompressionGroundDepth10;
	}
}
#pragma warning restore CS1591
