using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("liquid", "tdtl", "����", typeof(LiquidBlock))]
	[FieldSet(116, 4)]
	public unsafe struct LiquidBlock
	{
		public enum Type2Options
		{
			Standard_0 = 0,
			WeaponToProjectile_1 = 1,
			ProjectileFromWeapon_2 = 2,
		}
		[Field("", null)]
		public fixed byte _1[2];
		[Field("type", typeof(Type2Options))]
		public short Type2;
		[Field("attachment marker name", null)]
		public StringId AttachmentMarkerName3;
		[Field("", null)]
		public fixed byte _4[56];
		[Field("falloff distance from camera:world units", null)]
		public float FalloffDistanceFromCamera5;
		[Field("cutoff distance from camera:world units", null)]
		public float CutoffDistanceFromCamera6;
		[Field("", null)]
		public fixed byte _7[32];
		[Field("arcs", null)]
		[Block("Arc", 3, typeof(LiquidArcBlock))]
		public TagBlock Arcs8;
	}
}
#pragma warning restore CS1591
