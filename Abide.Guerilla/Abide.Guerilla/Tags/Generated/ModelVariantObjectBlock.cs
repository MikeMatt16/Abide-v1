using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(24, 4)]
	public unsafe struct ModelVariantObjectBlock
	{
		[Field("parent marker^", null)]
		public StringId ParentMarker0;
		[Field("child marker", null)]
		public StringId ChildMarker1;
		[Field("child object", null)]
		public TagReference ChildObject2;
	}
}
#pragma warning restore CS1591
