using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(480, 4)]
	public unsafe struct GlobalHudMultitextureOverlayDefinition
	{
		public enum FramebufferBlendFunc2Options
		{
			AlphaBlend_0 = 0,
			Multiply_1 = 1,
			DoubleMultiply_2 = 2,
			Add_3 = 3,
			Subtract_4 = 4,
			ComponentMin_5 = 5,
			ComponentMax_6 = 6,
			AlphaMultiplyAdd_7 = 7,
			ConstantColorBlend_8 = 8,
			InverseConstantColorBlend_9 = 9,
			None_10 = 10,
		}
		public enum PrimaryAnchor6Options
		{
			Texture_0 = 0,
			Screen_1 = 1,
		}
		public enum SecondaryAnchor7Options
		{
			Texture_0 = 0,
			Screen_1 = 1,
		}
		public enum TertiaryAnchor8Options
		{
			Texture_0 = 0,
			Screen_1 = 1,
		}
		public enum _0To1BlendFunc10Options
		{
			Add_0 = 0,
			Subtract_1 = 1,
			Multiply_2 = 2,
			Multiply2x_3 = 3,
			Dot_4 = 4,
		}
		public enum _1To2BlendFunc11Options
		{
			Add_0 = 0,
			Subtract_1 = 1,
			Multiply_2 = 2,
			Multiply2x_3 = 3,
			Dot_4 = 4,
		}
		public enum PrimaryWrapMode25Options
		{
			Clamp_0 = 0,
			Wrap_1 = 1,
		}
		public enum SecondaryWrapMode26Options
		{
			Clamp_0 = 0,
			Wrap_1 = 1,
		}
		public enum TertiaryWrapMode27Options
		{
			Clamp_0 = 0,
			Wrap_1 = 1,
		}
		[Field("", null)]
		public fixed byte _0[2];
		[Field("type", null)]
		public short Type1;
		[Field("framebuffer blend func", typeof(FramebufferBlendFunc2Options))]
		public short FramebufferBlendFunc2;
		[Field("", null)]
		public fixed byte _3[2];
		[Field("", null)]
		public fixed byte _4[32];
		[Field("primary anchor", typeof(PrimaryAnchor6Options))]
		public short PrimaryAnchor6;
		[Field("secondary anchor", typeof(SecondaryAnchor7Options))]
		public short SecondaryAnchor7;
		[Field("tertiary anchor", typeof(TertiaryAnchor8Options))]
		public short TertiaryAnchor8;
		[Field("0 to 1 blend func", typeof(_0To1BlendFunc10Options))]
		public short _0To1BlendFunc10;
		[Field("1 to 2 blend func", typeof(_1To2BlendFunc11Options))]
		public short _1To2BlendFunc11;
		[Field("", null)]
		public fixed byte _12[2];
		[Field("primary scale", null)]
		public Vector2 PrimaryScale14;
		[Field("secondary scale", null)]
		public Vector2 SecondaryScale15;
		[Field("tertiary scale", null)]
		public Vector2 TertiaryScale16;
		[Field("primary offset", null)]
		public Vector2 PrimaryOffset18;
		[Field("secondary offset", null)]
		public Vector2 SecondaryOffset19;
		[Field("tertiary offset", null)]
		public Vector2 TertiaryOffset20;
		[Field("primary", null)]
		public TagReference Primary22;
		[Field("secondary", null)]
		public TagReference Secondary23;
		[Field("tertiary", null)]
		public TagReference Tertiary24;
		[Field("primary wrap mode", typeof(PrimaryWrapMode25Options))]
		public short PrimaryWrapMode25;
		[Field("secondary wrap mode", typeof(SecondaryWrapMode26Options))]
		public short SecondaryWrapMode26;
		[Field("tertiary wrap mode", typeof(TertiaryWrapMode27Options))]
		public short TertiaryWrapMode27;
		[Field("", null)]
		public fixed byte _28[2];
		[Field("", null)]
		public fixed byte _29[184];
		[Field("effectors", null)]
		[Block("Global Hud Multitexture Overlay Effector Definition", 30, typeof(GlobalHudMultitextureOverlayEffectorDefinition))]
		public TagBlock Effectors30;
		[Field("", null)]
		public fixed byte _31[128];
	}
}
#pragma warning restore CS1591
