using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(40, 4)]
	public unsafe struct HsScriptsBlock
	{
		public enum ScriptType1Options
		{
			Startup_0 = 0,
			Dormant_1 = 1,
			Continuous_2 = 2,
			Static_3 = 3,
			Stub_4 = 4,
			CommandScript_5 = 5,
		}
		public enum ReturnType2Options
		{
			Unparsed_0 = 0,
			SpecialForm_1 = 1,
			FunctionName_2 = 2,
			Passthrough_3 = 3,
			Void_4 = 4,
			Boolean_5 = 5,
			Real_6 = 6,
			Short_7 = 7,
			Long_8 = 8,
			String_9 = 9,
			Script_10 = 10,
			StringId_11 = 11,
			UnitSeatMapping_12 = 12,
			TriggerVolume_13 = 13,
			CutsceneFlag_14 = 14,
			CutsceneCameraPoint_15 = 15,
			CutsceneTitle_16 = 16,
			CutsceneRecording_17 = 17,
			DeviceGroup_18 = 18,
			Ai_19 = 19,
			AiCommandList_20 = 20,
			AiCommandScript_21 = 21,
			AiBehavior_22 = 22,
			AiOrders_23 = 23,
			StartingProfile_24 = 24,
			Conversation_25 = 25,
			StructureBsp_26 = 26,
			Navpoint_27 = 27,
			PointReference_28 = 28,
			Style_29 = 29,
			HudMessage_30 = 30,
			ObjectList_31 = 31,
			Sound_32 = 32,
			Effect_33 = 33,
			Damage_34 = 34,
			LoopingSound_35 = 35,
			AnimationGraph_36 = 36,
			DamageEffect_37 = 37,
			ObjectDefinition_38 = 38,
			Bitmap_39 = 39,
			Shader_40 = 40,
			RenderModel_41 = 41,
			StructureDefinition_42 = 42,
			LightmapDefinition_43 = 43,
			GameDifficulty_44 = 44,
			Team_45 = 45,
			ActorType_46 = 46,
			HudCorner_47 = 47,
			ModelState_48 = 48,
			NetworkEvent_49 = 49,
			Object_50 = 50,
			Unit_51 = 51,
			Vehicle_52 = 52,
			Weapon_53 = 53,
			Device_54 = 54,
			Scenery_55 = 55,
			ObjectName_56 = 56,
			UnitName_57 = 57,
			VehicleName_58 = 58,
			WeaponName_59 = 59,
			DeviceName_60 = 60,
			SceneryName_61 = 61,
		}
		[Field("name^*", null)]
		public String Name0;
		[Field("script type*", typeof(ScriptType1Options))]
		public short ScriptType1;
		[Field("return type*", typeof(ReturnType2Options))]
		public short ReturnType2;
		[Field("root expression index*", null)]
		public int RootExpressionIndex3;
		[Field("", null)]
		public fixed byte _4[52];
	}
}
#pragma warning restore CS1591
