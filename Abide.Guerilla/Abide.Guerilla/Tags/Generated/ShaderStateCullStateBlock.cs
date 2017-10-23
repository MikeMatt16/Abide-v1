using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct ShaderStateCullStateBlock
	{
		public enum Mode0Options
		{
			None_0 = 0,
			CW_1 = 1,
			CCW_2 = 2,
		}
		public enum FrontFace1Options
		{
			CW_0 = 0,
			CCW_1 = 1,
		}
		[Field("mode", typeof(Mode0Options))]
		public short Mode0;
		[Field("front face", typeof(FrontFace1Options))]
		public short FrontFace1;
	}
}
#pragma warning restore CS1591
