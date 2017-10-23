using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct FlightReferenceBlock
	{
		[Field("flight hint index", null)]
		public short FlightHintIndex0;
		[Field("poit index", null)]
		public short PoitIndex1;
	}
}
#pragma warning restore CS1591
