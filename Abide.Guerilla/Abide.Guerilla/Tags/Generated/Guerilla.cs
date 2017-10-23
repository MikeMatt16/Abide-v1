using Abide.HaloLibrary;
using System;
using System.Collections.Generic;

namespace Abide.Guerilla.Tags
{
	internal static class Guerilla
	{
		public static Dictionary<Tag, Type> TagGroupDictionary;
		
		static Guerilla()
		{
			// Initialize Dictionary
			TagGroupDictionary = new Dictionary<Tag, Type>();
			
			TagGroupDictionary.Add("hlmt", typeof(ModelBlock));
			TagGroupDictionary.Add("mode", typeof(RenderModelBlock));
			TagGroupDictionary.Add("coll", typeof(CollisionModelBlock));
			TagGroupDictionary.Add("phmo", typeof(PhysicsModelBlock));
			TagGroupDictionary.Add("bitm", typeof(BitmapBlock));
			TagGroupDictionary.Add("colo", typeof(ColorTableBlock));
			TagGroupDictionary.Add("unic", typeof(MultilingualUnicodeStringListBlock));
			TagGroupDictionary.Add("unit", typeof(UnitBlock));
			TagGroupDictionary.Add("bipd", typeof(BipedBlock));
			TagGroupDictionary.Add("vehi", typeof(VehicleBlock));
			TagGroupDictionary.Add("scen", typeof(SceneryBlock));
			TagGroupDictionary.Add("bloc", typeof(CrateBlock));
			TagGroupDictionary.Add("crea", typeof(CreatureBlock));
			TagGroupDictionary.Add("phys", typeof(PhysicsBlock));
			TagGroupDictionary.Add("obje", typeof(ObjectBlock));
			TagGroupDictionary.Add("cont", typeof(ContrailBlock));
			TagGroupDictionary.Add("weap", typeof(WeaponBlock));
			TagGroupDictionary.Add("ligh", typeof(LightBlock));
			TagGroupDictionary.Add("effe", typeof(EffectBlock));
			TagGroupDictionary.Add("prt3", typeof(ParticleBlock));
			TagGroupDictionary.Add("PRTM", typeof(ParticleModelBlock));
			TagGroupDictionary.Add("pmov", typeof(ParticlePhysicsBlock));
			TagGroupDictionary.Add("matg", typeof(GlobalsBlock));
			TagGroupDictionary.Add("snd!", typeof(SoundBlock));
			TagGroupDictionary.Add("lsnd", typeof(SoundLoopingBlock));
			TagGroupDictionary.Add("item", typeof(ItemBlock));
			TagGroupDictionary.Add("eqip", typeof(EquipmentBlock));
			TagGroupDictionary.Add("ant!", typeof(AntennaBlock));
			TagGroupDictionary.Add("MGS2", typeof(LightVolumeBlock));
			TagGroupDictionary.Add("tdtl", typeof(LiquidBlock));
			TagGroupDictionary.Add("devo", typeof(CellularAutomataBlock));
			TagGroupDictionary.Add("whip", typeof(CellularAutomata2dBlock));
			TagGroupDictionary.Add("BooM", typeof(StereoSystemBlock));
			TagGroupDictionary.Add("trak", typeof(CameraTrackBlock));
			TagGroupDictionary.Add("proj", typeof(ProjectileBlock));
			TagGroupDictionary.Add("devi", typeof(DeviceBlock));
			TagGroupDictionary.Add("mach", typeof(DeviceMachineBlock));
			TagGroupDictionary.Add("ctrl", typeof(DeviceControlBlock));
			TagGroupDictionary.Add("lifi", typeof(DeviceLightFixtureBlock));
			TagGroupDictionary.Add("pphy", typeof(PointPhysicsBlock));
			TagGroupDictionary.Add("ltmp", typeof(ScenarioStructureLightmapBlock));
			TagGroupDictionary.Add("sbsp", typeof(ScenarioStructureBspBlock));
			TagGroupDictionary.Add("scnr", typeof(ScenarioBlock));
			TagGroupDictionary.Add("shad", typeof(ShaderBlock));
			TagGroupDictionary.Add("stem", typeof(ShaderTemplateBlock));
			TagGroupDictionary.Add("slit", typeof(ShaderLightResponseBlock));
			TagGroupDictionary.Add("spas", typeof(ShaderPassBlock));
			TagGroupDictionary.Add("vrtx", typeof(VertexShaderBlock));
			TagGroupDictionary.Add("pixl", typeof(PixelShaderBlock));
			TagGroupDictionary.Add("DECR", typeof(DecoratorSetBlock));
			TagGroupDictionary.Add("DECP", typeof(DecoratorsBlock));
			TagGroupDictionary.Add("sky ", typeof(SkyBlock));
			TagGroupDictionary.Add("wind", typeof(WindBlock));
			TagGroupDictionary.Add("snde", typeof(SoundEnvironmentBlock));
			TagGroupDictionary.Add("lens", typeof(LensFlareBlock));
			TagGroupDictionary.Add("fog ", typeof(PlanarFogBlock));
			TagGroupDictionary.Add("fpch", typeof(PatchyFogBlock));
			TagGroupDictionary.Add("metr", typeof(MeterBlock));
			TagGroupDictionary.Add("deca", typeof(DecalBlock));
			TagGroupDictionary.Add("coln", typeof(ColonyBlock));
			TagGroupDictionary.Add("jpt!", typeof(DamageEffectBlock));
			TagGroupDictionary.Add("udlg", typeof(DialogueBlock));
			TagGroupDictionary.Add("itmc", typeof(ItemCollectionBlock));
			TagGroupDictionary.Add("vehc", typeof(VehicleCollectionBlock));
			TagGroupDictionary.Add("wphi", typeof(WeaponHudInterfaceBlock));
			TagGroupDictionary.Add("grhi", typeof(GrenadeHudInterfaceBlock));
			TagGroupDictionary.Add("unhi", typeof(UnitHudInterfaceBlock));
			TagGroupDictionary.Add("nhdt", typeof(NewHudDefinitionBlock));
			TagGroupDictionary.Add("hud#", typeof(HudNumberBlock));
			TagGroupDictionary.Add("hudg", typeof(HudGlobalsBlock));
			TagGroupDictionary.Add("mply", typeof(MultiplayerScenarioDescriptionBlock));
			TagGroupDictionary.Add("dobc", typeof(DetailObjectCollectionBlock));
			TagGroupDictionary.Add("ssce", typeof(SoundSceneryBlock));
			TagGroupDictionary.Add("hmt ", typeof(HudMessageTextBlock));
			TagGroupDictionary.Add("wgit", typeof(UserInterfaceScreenWidgetDefinitionBlock));
			TagGroupDictionary.Add("skin", typeof(UserInterfaceListSkinDefinitionBlock));
			TagGroupDictionary.Add("wgtz", typeof(UserInterfaceGlobalsDefinitionBlock));
			TagGroupDictionary.Add("wigl", typeof(UserInterfaceSharedGlobalsDefinitionBlock));
			TagGroupDictionary.Add("sily", typeof(TextValuePairDefinitionBlock));
			TagGroupDictionary.Add("goof", typeof(MultiplayerVariantSettingsInterfaceDefinitionBlock));
			TagGroupDictionary.Add("foot", typeof(MaterialEffectsBlock));
			TagGroupDictionary.Add("garb", typeof(GarbageBlock));
			TagGroupDictionary.Add("styl", typeof(StyleBlock));
			TagGroupDictionary.Add("char", typeof(CharacterBlock));
			TagGroupDictionary.Add("adlg", typeof(AiDialogueGlobalsBlock));
			TagGroupDictionary.Add("mdlg", typeof(AiMissionDialogueBlock));
			TagGroupDictionary.Add("*cen", typeof(ScenarioSceneryResourceBlock));
			TagGroupDictionary.Add("*ipd", typeof(ScenarioBipedsResourceBlock));
			TagGroupDictionary.Add("*ehi", typeof(ScenarioVehiclesResourceBlock));
			TagGroupDictionary.Add("*qip", typeof(ScenarioEquipmentResourceBlock));
			TagGroupDictionary.Add("*eap", typeof(ScenarioWeaponsResourceBlock));
			TagGroupDictionary.Add("*sce", typeof(ScenarioSoundSceneryResourceBlock));
			TagGroupDictionary.Add("*igh", typeof(ScenarioLightsResourceBlock));
			TagGroupDictionary.Add("dgr*", typeof(ScenarioDevicesResourceBlock));
			TagGroupDictionary.Add("dec*", typeof(ScenarioDecalsResourceBlock));
			TagGroupDictionary.Add("cin*", typeof(ScenarioCinematicsResourceBlock));
			TagGroupDictionary.Add("trg*", typeof(ScenarioTriggerVolumesResourceBlock));
			TagGroupDictionary.Add("clu*", typeof(ScenarioClusterDataResourceBlock));
			TagGroupDictionary.Add("*rea", typeof(ScenarioCreatureResourceBlock));
			TagGroupDictionary.Add("dc*s", typeof(ScenarioDecoratorsResourceBlock));
			TagGroupDictionary.Add("sslt", typeof(ScenarioStructureLightingResourceBlock));
			TagGroupDictionary.Add("hsc*", typeof(HsSourceFilesBlock));
			TagGroupDictionary.Add("ai**", typeof(ScenarioAiResourceBlock));
			TagGroupDictionary.Add("/**/", typeof(ScenarioCommentsResourceBlock));
			TagGroupDictionary.Add("bsdt", typeof(BreakableSurfaceBlock));
			TagGroupDictionary.Add("mpdt", typeof(MaterialPhysicsBlock));
			TagGroupDictionary.Add("sncl", typeof(SoundClassesBlock));
			TagGroupDictionary.Add("mulg", typeof(MultiplayerGlobalsBlock));
			TagGroupDictionary.Add("<fx>", typeof(SoundEffectTemplateBlock));
			TagGroupDictionary.Add("sfx+", typeof(SoundEffectCollectionBlock));
			TagGroupDictionary.Add("gldf", typeof(ChocolateMountainBlock));
			TagGroupDictionary.Add("jmad", typeof(ModelAnimationGraphBlock));
			TagGroupDictionary.Add("clwd", typeof(ClothBlock));
			TagGroupDictionary.Add("egor", typeof(ScreenEffectBlock));
			TagGroupDictionary.Add("weat", typeof(WeatherSystemBlock));
			TagGroupDictionary.Add("snmx", typeof(SoundMixBlock));
			TagGroupDictionary.Add("spk!", typeof(SoundDialogueConstantsBlock));
			TagGroupDictionary.Add("ugh!", typeof(SoundCacheFileGestaltBlock));
			TagGroupDictionary.Add("$#!+", typeof(CacheFileSoundBlock));
			TagGroupDictionary.Add("mcsr", typeof(MouseCursorDefinitionBlock));
		}
	}
}
