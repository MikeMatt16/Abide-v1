using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(20, 4)]
	public unsafe struct SyntaxDatumBlock
	{
		[Field("Datum Header", null)]
		public short DatumHeader0;
		[Field("Script Index/Function Index/Constant Type Union", null)]
		public short ScriptIndexFunctionIndexConstantTypeUnion1;
		[Field("Type", null)]
		public short Type2;
		[Field("Flags", null)]
		public short Flags3;
		[Field("Next Node Index", null)]
		public int NextNodeIndex4;
		[Field("Data", null)]
		public int Data5;
		[Field("source_offset", null)]
		public int SourceOffset6;
	}
}
#pragma warning restore CS1591
