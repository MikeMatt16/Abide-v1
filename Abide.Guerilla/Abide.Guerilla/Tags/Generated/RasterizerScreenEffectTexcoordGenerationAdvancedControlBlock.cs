using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(72, 4)]
	public unsafe struct RasterizerScreenEffectTexcoordGenerationAdvancedControlBlock
	{
		public enum Stage0Flags0Options
		{
			XyScaledByZFar_0 = 1,
		}
		public enum Stage1Flags1Options
		{
			XyScaledByZFar_0 = 1,
		}
		public enum Stage2Flags2Options
		{
			XyScaledByZFar_0 = 1,
		}
		public enum Stage3Flags3Options
		{
			XyScaledByZFar_0 = 1,
		}
		[Field("stage 0 flags", typeof(Stage0Flags0Options))]
		public short Stage0Flags0;
		[Field("stage 1 flags", typeof(Stage1Flags1Options))]
		public short Stage1Flags1;
		[Field("stage 2 flags", typeof(Stage2Flags2Options))]
		public short Stage2Flags2;
		[Field("stage 3 flags", typeof(Stage3Flags3Options))]
		public short Stage3Flags3;
		[Field("stage 0 offset", null)]
		public Plane3 Stage0Offset4;
		[Field("stage 1 offset", null)]
		public Plane3 Stage1Offset5;
		[Field("stage 2 offset", null)]
		public Plane3 Stage2Offset6;
		[Field("stage 3 offset", null)]
		public Plane3 Stage3Offset7;
	}
}
#pragma warning restore CS1591
