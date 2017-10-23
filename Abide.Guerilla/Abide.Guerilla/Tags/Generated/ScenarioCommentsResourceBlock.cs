using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("scenario_comments_resource", "/**/", "����", typeof(ScenarioCommentsResourceBlock))]
	[FieldSet(12, 4)]
	public unsafe struct ScenarioCommentsResourceBlock
	{
		[Field("Comments", null)]
		[Block("Editor Comment Block", 65536, typeof(EditorCommentBlock))]
		public TagBlock Comments0;
	}
}
#pragma warning restore CS1591
