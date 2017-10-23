using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct MaterialPhysicsPropertiesStructBlock
	{
		[Field("", null)]
		public fixed byte _0[4];
		[Field("friction", null)]
		public float Friction1;
		[Field("restitution", null)]
		public float Restitution2;
		[Field("density:kg/m^3", null)]
		public float Density3;
	}
}
#pragma warning restore CS1591
