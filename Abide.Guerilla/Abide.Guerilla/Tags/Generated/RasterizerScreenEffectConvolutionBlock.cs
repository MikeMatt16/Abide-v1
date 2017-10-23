using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(92, 4)]
	public unsafe struct RasterizerScreenEffectConvolutionBlock
	{
		public enum Flags1Options
		{
			OnlyWhenPrimaryIsActive_0 = 1,
			OnlyWhenSecondaryIsActive_1 = 2,
			PredatorZoom_2 = 4,
		}
		[Field("flags", typeof(Flags1Options))]
		public short Flags1;
		[Field("", null)]
		public fixed byte _2[2];
		[Field("", null)]
		public fixed byte _3[64];
		[Field("convolution amount:[0,+inf)", null)]
		public float ConvolutionAmount4;
		[Field("filter scale", null)]
		public float FilterScale5;
		[Field("filter box factor:[0,1] not used for zoom", null)]
		public float FilterBoxFactor6;
		[Field("zoom falloff radius", null)]
		public float ZoomFalloffRadius7;
		[Field("zoom cutoff radius", null)]
		public float ZoomCutoffRadius8;
		[Field("resolution scale:[0,1]", null)]
		public float ResolutionScale9;
	}
}
#pragma warning restore CS1591
