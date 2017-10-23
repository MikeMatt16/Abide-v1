using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(28, 4)]
	public unsafe struct PlayerTrainingEntryDataBlock
	{
		public enum Flags8Options
		{
			NotInMultiplayer_0 = 1,
		}
		[Field("display string#comes out of the HUD text globals", null)]
		public StringId DisplayString0;
		[Field("display string2#comes out of the HUD text globals, used for grouped prompt", null)]
		public StringId DisplayString21;
		[Field("display string3#comes out of the HUD text globals, used for ungrouped prompt", null)]
		public StringId DisplayString32;
		[Field("max display time#how long the message can be on screen before being hidden", null)]
		public short MaxDisplayTime3;
		[Field("display count#how many times a training message will get displayed (0-3 only!)", null)]
		public short DisplayCount4;
		[Field("dissapear delay#how long a displayed but untriggered message stays up", null)]
		public short DissapearDelay5;
		[Field("redisplay delay#how long after display this message will stay hidden", null)]
		public short RedisplayDelay6;
		[Field("display delay (s)#how long the event can be triggered before it's displayed", null)]
		public float DisplayDelayS7;
		[Field("flags", typeof(Flags8Options))]
		public short Flags8;
		[Field("", null)]
		public fixed byte _9[2];
	}
}
#pragma warning restore CS1591
