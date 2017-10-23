using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("stereo_system", "BooM", "����", typeof(StereoSystemBlock))]
	[FieldSet(4, 4)]
	public unsafe struct StereoSystemBlock
	{
		[Field("unused", null)]
		public int Unused0;
	}
}
#pragma warning restore CS1591
