using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(80, 4)]
	public unsafe struct ScenarioSoundSceneryBlock
	{
		[Field("Type", null)]
		public short Type1;
		[Field("Name^", null)]
		public short Name3;
		[Field("Object Data", typeof(ScenarioObjectDatumStructBlock))]
		[Block("Scenario Object Datum Struct", 1, typeof(ScenarioObjectDatumStructBlock))]
		public ScenarioObjectDatumStructBlock ObjectData4;
		[Field("sound_scenery", typeof(SoundSceneryDatumStructBlock))]
		[Block("Sound Scenery Datum Struct", 1, typeof(SoundSceneryDatumStructBlock))]
		public SoundSceneryDatumStructBlock SoundScenery5;
	}
}
#pragma warning restore CS1591
