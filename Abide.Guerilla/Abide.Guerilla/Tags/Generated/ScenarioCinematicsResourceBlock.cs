using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("scenario_cinematics_resource", "cin*", "����", typeof(ScenarioCinematicsResourceBlock))]
	[FieldSet(36, 4)]
	public unsafe struct ScenarioCinematicsResourceBlock
	{
		[Field("Flags", null)]
		[Block("Scenario Cutscene Flag Block", 512, typeof(ScenarioCutsceneFlagBlock))]
		public TagBlock Flags0;
		[Field("Camera Points", null)]
		[Block("Scenario Cutscene Camera Point Block", 512, typeof(ScenarioCutsceneCameraPointBlock))]
		public TagBlock CameraPoints1;
		[Field("Recorded Animations", null)]
		[Block("Recorded Animation Block", 1024, typeof(RecordedAnimationBlock))]
		public TagBlock RecordedAnimations2;
	}
}
#pragma warning restore CS1591
