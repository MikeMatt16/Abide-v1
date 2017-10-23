using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("model", "hlmt", "����", typeof(ModelBlock))]
	[FieldSet(348, 4)]
	public unsafe struct ModelBlock
	{
		public enum ShadowFadeDistance17Options
		{
			FadeAtSuperHighDetailLevel_0 = 0,
			FadeAtHighDetailLevel_1 = 1,
			FadeAtMediumDetailLevel_2 = 2,
			FadeAtLowDetailLevel_3 = 3,
			FadeAtSuperLowDetailLevel_4 = 4,
			FadeNever_5 = 5,
		}
		public enum Flags30Options
		{
			ActiveCamoAlwaysOn_0 = 1,
			ActiveCamoAlwaysMerge_1 = 2,
			ActiveCamoNeverMerge_2 = 4,
		}
		public enum RuntimeFlags38Options
		{
			ContainsRunTimeNodes_0 = 1,
		}
		[Field("render model", null)]
		public TagReference RenderModel2;
		[Field("collision model", null)]
		public TagReference CollisionModel3;
		[Field("animation", null)]
		public TagReference Animation4;
		[Field("physics", null)]
		public TagReference Physics5;
		[Field("physics_model", null)]
		public TagReference PhysicsModel6;
		[Field("disappear distance:world units", null)]
		public float DisappearDistance8;
		[Field("begin fade distance:world units", null)]
		public float BeginFadeDistance9;
		[Field("", null)]
		public fixed byte _10[4];
		[Field("reduce to L1:world units (super low)", null)]
		public float ReduceToL111;
		[Field("reduce to L2:world units (low)", null)]
		public float ReduceToL212;
		[Field("reduce to L3:world units (medium)", null)]
		public float ReduceToL313;
		[Field("reduce to L4:world units (high)", null)]
		public float ReduceToL414;
		[Field("reduce to L5:world units (super high)", null)]
		public float ReduceToL515;
		[Field("", null)]
		public fixed byte _16[4];
		[Field("shadow fade distance", typeof(ShadowFadeDistance17Options))]
		public short ShadowFadeDistance17;
		[Field("", null)]
		public fixed byte _18[2];
		[Field("variants", null)]
		[Block("Variant", 64, typeof(ModelVariantBlock))]
		public TagBlock Variants19;
		[Field("materials", null)]
		[Block("Material", 32, typeof(ModelMaterialBlock))]
		public TagBlock Materials20;
		[Field("new damage info", null)]
		[Block("Global Damage Info Block", 1, typeof(GlobalDamageInfoBlock))]
		public TagBlock NewDamageInfo21;
		[Field("targets", null)]
		[Block("Model Target Block", 32, typeof(ModelTargetBlock))]
		public TagBlock Targets22;
		[Field("", null)]
		[Block("Model Region Block", 16, typeof(ModelRegionBlock))]
		public TagBlock _23;
		[Field("", null)]
		[Block("Model Node Block", 255, typeof(ModelNodeBlock))]
		public TagBlock _24;
		[Field("", null)]
		public fixed byte _25[4];
		[Field("model object data", null)]
		[Block("Model Object Data Block", 1, typeof(ModelObjectDataBlock))]
		public TagBlock ModelObjectData26;
		[Field("default dialogue#The default dialogue tag for this model (overriden by variants)", null)]
		public TagReference DefaultDialogue28;
		[Field("UNUSED*", null)]
		public TagReference UNUSED29;
		[Field("flags", typeof(Flags30Options))]
		public int Flags30;
		[Field("default dialogue effect#The default dialogue tag for this model (overriden by variants)", null)]
		public StringId DefaultDialogueEffect31;
		[Field("", null)]
		public int _33;
		[Field("", null)]
		public int _36;
		[Field("runtime flags*", typeof(RuntimeFlags38Options))]
		public int RuntimeFlags38;
		[Field("scenario load parameters", null)]
		[Block("Scenario Load Parameters", 32, typeof(GlobalScenarioLoadParametersBlock))]
		public TagBlock ScenarioLoadParameters39;
		[Field("hologram shader", null)]
		public TagReference HologramShader41;
		[Field("hologram control function", null)]
		public StringId HologramControlFunction42;
	}
}
#pragma warning restore CS1591
