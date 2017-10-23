using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("sound_looping", "lsnd", "����", typeof(SoundLoopingBlock))]
	[FieldSet(60, 4)]
	public unsafe struct SoundLoopingBlock
	{
		public enum Flags0Options
		{
			DeafeningToAIsWhenUsedAsABackgroundStereoTrackCausesNearbyAIsToBeUnableToHear_0 = 1,
			NotALoopThisIsACollectionOfPermutationsStrungTogetherThatShouldPlayOnceThenStop_1 = 2,
			StopsMusicAllOtherMusicLoopsWillStopWhenThisOneStarts_2 = 4,
			AlwaysSpatializeAlwaysPlayAs3dSoundEvenInFirstPerson_3 = 8,
			SynchronizePlaybackSynchronizesPlaybackWithOtherLoopingSoundsAttachedToTheOwnerOfThisSound_4 = 16,
			SynchronizeTracks_5 = 32,
			FakeSpatializationWithDistance_6 = 64,
			CombineAll3dPlayback_7 = 128,
		}
		[Field("flags", typeof(Flags0Options))]
		public int Flags0;
		[Field("marty's music time: seconds", null)]
		public float MartySMusicTime1;
		[Field("", null)]
		public float _2;
		[Field("", null)]
		public fixed byte _3[8];
		[Field("", null)]
		public TagReference _4;
		[Field("tracks#tracks play in parallel and loop continuously for the duration of the looping sound.", null)]
		[Block("Looping Sound Track Block", 3, typeof(LoopingSoundTrackBlock))]
		public TagBlock Tracks5;
		[Field("detail sounds#detail sounds play at random throughout the duration of the looping sound.", null)]
		[Block("Looping Sound Detail Block", 12, typeof(LoopingSoundDetailBlock))]
		public TagBlock DetailSounds6;
	}
}
#pragma warning restore CS1591
