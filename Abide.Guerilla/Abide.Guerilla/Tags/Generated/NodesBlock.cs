using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct NodesBlock
	{
		public enum Flags1Options
		{
			DoesNotAnimate_0 = 1,
		}
		[Field("name^*", null)]
		public StringId Name0;
		[Field("flags", typeof(Flags1Options))]
		public short Flags1;
		[Field("parent*", null)]
		public short Parent2;
		[Field("sibling*", null)]
		public short Sibling3;
		[Field("child*", null)]
		public short Child4;
	}
}
#pragma warning restore CS1591
