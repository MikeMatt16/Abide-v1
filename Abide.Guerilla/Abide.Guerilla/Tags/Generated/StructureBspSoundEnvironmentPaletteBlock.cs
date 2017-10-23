using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(80, 4)]
	public unsafe struct StructureBspSoundEnvironmentPaletteBlock
	{
		[Field("Name^", null)]
		public String Name0;
		[Field("Sound Environment", null)]
		public TagReference SoundEnvironment1;
		[Field("Cutoff Distance", null)]
		public float CutoffDistance2;
		[Field("Interpolation Speed:1/sec", null)]
		public float InterpolationSpeed3;
		[Field("", null)]
		public fixed byte _4[24];
	}
}
#pragma warning restore CS1591
