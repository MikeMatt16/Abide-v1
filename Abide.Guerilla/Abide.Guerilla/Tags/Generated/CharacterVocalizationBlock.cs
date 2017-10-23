using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct CharacterVocalizationBlock
	{
		[Field("look comment time:s#How long does the player look at an AI before the AI responds?", null)]
		public float LookCommentTime0;
		[Field("look long comment time:s#How long does the player look at the AI before he responds with his 'long look' comment?", null)]
		public float LookLongCommentTime1;
	}
}
#pragma warning restore CS1591
