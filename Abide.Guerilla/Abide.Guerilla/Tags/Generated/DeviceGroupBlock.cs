using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(40, 4)]
	public unsafe struct DeviceGroupBlock
	{
		public enum Flags2Options
		{
			CanChangeOnlyOnce_0 = 1,
		}
		[Field("Name^", null)]
		public String Name0;
		[Field("Initial Value:[0,1]", null)]
		public float InitialValue1;
		[Field("Flags", typeof(Flags2Options))]
		public int Flags2;
		[Field("", null)]
		public fixed byte _3[12];
	}
}
#pragma warning restore CS1591
