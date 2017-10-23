using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(16, 4)]
	public unsafe struct RenderModelCompoundNodeBlock
	{
		[Field("node index*", null)]
		public int NodeIndex1;
		[Field("node weight*", null)]
		public float NodeWeight4;
	}
}
#pragma warning restore CS1591
