using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(304, 4)]
	public unsafe struct EditorCommentBlock
	{
		public enum Type1Options
		{
			Generic_0 = 0,
		}
		public Vector3 Position0;
		[Field(")Type", typeof(Type1Options))]
		public int Type1;
		[Field("Name^", null)]
		public String Name2;
		[Field("Comment", null)]
		public LongString Comment3;
	}
}
#pragma warning restore CS1591
