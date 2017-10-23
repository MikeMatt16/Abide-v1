using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(260, 4)]
	public unsafe struct GScenarioEditorFolderBlock
	{
		[Field("parent folder", null)]
		public int ParentFolder0;
		[Field("name^", null)]
		public LongString Name1;
	}
}
#pragma warning restore CS1591
