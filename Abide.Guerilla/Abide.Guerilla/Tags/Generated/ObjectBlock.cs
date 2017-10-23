using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("object", "obje", "����", typeof(ObjectBlock))]
	[FieldSet(256, 4)]
	public unsafe struct ObjectBlock
	{
		public enum Flags1Options
		{
			DoesNotCastShadow_0 = 1,
			SearchCardinalDirectionLightmapsOnFailure_1 = 2,
			Unused_2 = 4,
			NotAPathfindingObstacle_3 = 8,
			ExtensionOfParentObjectPassesAllFunctionValuesToParentAndUsesParentSMarkers_4 = 16,
			DoesNotCauseCollisionDamage_5 = 32,
			EarlyMover_6 = 64,
			EarlyMoverLocalizedPhysics_7 = 128,
			UseStaticMassiveLightmapSampleCastATonOfRaysOnceAndStoreTheResultsForLighting_8 = 256,
			ObjectScalesAttachments_9 = 512,
			InheritsPlayerSAppearance_10 = 1024,
			DeadBipedsCanTLocalize_11 = 2048,
			AttachToClustersByDynamicSphereUseThisForTheMacGunOnSpacestation_12 = 4096,
			EffectsCreatedByThisObjectDoNotSpawnObjectsInMultiplayer_13 = 8192,
		}
		public enum LightmapShadowMode6Options
		{
			Default_0 = 0,
			Never_1 = 1,
			Always_2 = 2,
		}
		public enum SweetenerSize7Options
		{
			Small_0 = 0,
			Medium_1 = 1,
			Large_2 = 2,
		}
		[Field("", null)]
		public fixed byte _0[2];
		[Field("flags", typeof(Flags1Options))]
		public short Flags1;
		[Field("bounding radius:world units", null)]
		public float BoundingRadius2;
		public Vector3 BoundingOffset3;
		[Field("", null)]
		public fixed byte _4[12];
		[Field("acceleration scale:[0,+inf]#marine 1.0, grunt 1.4, elite 0.9, hunter 0.5, etc.", null)]
		public float AccelerationScale5;
		[Field("lightmap shadow mode", typeof(LightmapShadowMode6Options))]
		public short LightmapShadowMode6;
		[Field("sweetener size", typeof(SweetenerSize7Options))]
		public byte SweetenerSize7;
		[Field("", null)]
		public fixed byte _8[1];
		[Field("", null)]
		public fixed byte _9[4];
		[Field("", null)]
		public fixed byte _10[32];
		[Field("dynamic light sphere radius#sphere to use for dynamic lights and shadows. only used if not 0", null)]
		public float DynamicLightSphereRadius11;
		public Vector3 DynamicLightSphereOffset12;
		[Field("default model variant", null)]
		public StringId DefaultModelVariant13;
		[Field("model", null)]
		public TagReference Model14;
		[Field("crate object", null)]
		public TagReference CrateObject15;
		[Field("", null)]
		public fixed byte _16[16];
		[Field("modifier shader", null)]
		public TagReference ModifierShader17;
		[Field("creation effect", null)]
		public TagReference CreationEffect18;
		[Field("material effects", null)]
		public TagReference MaterialEffects19;
		[Field("", null)]
		public fixed byte _20[24];
		[Field("ai properties", null)]
		[Block("Object Ai Properties Block", 1, typeof(ObjectAiPropertiesBlock))]
		public TagBlock AiProperties21;
		[Field("", null)]
		public fixed byte _22[24];
		[Field("functions", null)]
		[Block("Object Function Block", 256, typeof(ObjectFunctionBlock))]
		public TagBlock Functions23;
		[Field("", null)]
		public fixed byte _24[16];
		[Field("Apply collision damage scale#0 means 1.  1 is standard scale.  Some things may want to apply more damage", null)]
		public float ApplyCollisionDamageScale26;
		[Field("min game acc (default)#0-oo", null)]
		public float MinGameAccDefault28;
		[Field("max game acc (default)#0-oo", null)]
		public float MaxGameAccDefault29;
		[Field("min game scale (default)#0-1", null)]
		public float MinGameScaleDefault30;
		[Field("max game scale (default)#0-1", null)]
		public float MaxGameScaleDefault31;
		[Field("min abs acc (default)#0-oo", null)]
		public float MinAbsAccDefault33;
		[Field("max abs acc (default)#0-oo", null)]
		public float MaxAbsAccDefault34;
		[Field("min abs scale (default)#0-1", null)]
		public float MinAbsScaleDefault35;
		[Field("max abs scale (default)#0-1", null)]
		public float MaxAbsScaleDefault36;
		[Field("hud text message index", null)]
		public short HudTextMessageIndex37;
		[Field("", null)]
		public fixed byte _38[2];
		[Field("attachments", null)]
		[Block("Object Attachment Block", 16, typeof(ObjectAttachmentBlock))]
		public TagBlock Attachments39;
		[Field("widgets", null)]
		[Block("Object Widget Block", 4, typeof(ObjectWidgetBlock))]
		public TagBlock Widgets40;
		[Field("old functions", null)]
		[Block("Old Object Function Block", 4, typeof(OldObjectFunctionBlock))]
		public TagBlock OldFunctions41;
		[Field("change colors", null)]
		[Block("Object Change Colors", 4, typeof(ObjectChangeColors))]
		public TagBlock ChangeColors42;
		[Field("predicted resources*", null)]
		[Block("Predicted Resource Block", 2048, typeof(PredictedResourceBlock))]
		public TagBlock PredictedResources43;
	}
}
#pragma warning restore CS1591
