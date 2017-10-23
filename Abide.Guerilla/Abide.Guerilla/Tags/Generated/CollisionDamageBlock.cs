using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(80, 4)]
	public unsafe struct CollisionDamageBlock
	{
		[Field("collision damage", null)]
		public TagReference CollisionDamage0;
		[Field("min game acc (default)#0-oo", null)]
		public float MinGameAccDefault1;
		[Field("max game acc (default)#0-oo", null)]
		public float MaxGameAccDefault2;
		[Field("min game scale (default)#0-1", null)]
		public float MinGameScaleDefault3;
		[Field("max game scale (default)#0-1", null)]
		public float MaxGameScaleDefault4;
		[Field("min abs acc (default)#0-oo", null)]
		public float MinAbsAccDefault5;
		[Field("max abs acc (default)#0-oo", null)]
		public float MaxAbsAccDefault6;
		[Field("min abs scale (default)#0-1", null)]
		public float MinAbsScaleDefault7;
		[Field("max abs scale (default)#0-1", null)]
		public float MaxAbsScaleDefault8;
		[Field("", null)]
		public fixed byte _9[32];
	}
}
#pragma warning restore CS1591
