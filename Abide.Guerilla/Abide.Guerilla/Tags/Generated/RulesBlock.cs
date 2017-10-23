using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(88, 4)]
	public unsafe struct RulesBlock
	{
		[Field("name^", null)]
		public String Name0;
		[Field("tint color", null)]
		public ColorRgbF TintColor1;
		[Field("", null)]
		public fixed byte _2[32];
		[Field("states", null)]
		[Block("States Block", 16, typeof(StatesBlock))]
		public TagBlock States3;
	}
}
#pragma warning restore CS1591
