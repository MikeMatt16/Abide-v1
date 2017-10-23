using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(52, 4)]
	public unsafe struct CsPointSetBlock
	{
		public enum Flags4Options
		{
			ManualReferenceFrame_0 = 1,
			TurretDeployment_1 = 2,
		}
		[Field("name^", null)]
		public String Name0;
		[Field("points", null)]
		[Block("Cs Point Block", 20, typeof(CsPointBlock))]
		public TagBlock Points1;
		[Field("bsp index", null)]
		public short BspIndex2;
		[Field("manual reference frame", null)]
		public short ManualReferenceFrame3;
		[Field("flags", typeof(Flags4Options))]
		public int Flags4;
	}
}
#pragma warning restore CS1591
