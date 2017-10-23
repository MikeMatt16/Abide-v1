using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(104, 4)]
	public unsafe struct InstantaneousDamageRepsonseBlock
	{
		public enum ResponseType0Options
		{
			ReceivesAllDamage_0 = 0,
			ReceivesAreaEffectDamage_1 = 1,
			ReceivesLocalDamage_2 = 2,
		}
		public enum ConstraintDamageType2Options
		{
			None_0 = 0,
			DestroyOneOfGroup_1 = 1,
			DestroyEntireGroup_2 = 2,
			LoosenOneOfGroup_3 = 3,
			LoosenEntireGroup_4 = 4,
		}
		public enum Flags4Options
		{
			KillsObject_0 = 1,
			InhibitsMeleeAttack_1 = 2,
			InhibitsWeaponAttack_2 = 4,
			InhibitsWalking_3 = 8,
			ForcesDropWeapon_4 = 16,
			KillsWeaponPrimaryTrigger_5 = 32,
			KillsWeaponSecondaryTrigger_6 = 64,
			DestroysObject_7 = 128,
			DamagesWeaponPrimaryTrigger_8 = 256,
			DamagesWeaponSecondaryTrigger_9 = 512,
			LightDamageLeftTurn_10 = 1024,
			MajorDamageLeftTurn_11 = 2048,
			LightDamageRightTurn_12 = 4096,
			MajorDamageRightTurn_13 = 8192,
			LightDamageEngine_14 = 16384,
			MajorDamageEngine_15 = 32768,
			KillsObjectNoPlayerSolo_16 = 65536,
			CausesDetonation_17 = 131072,
			DestroyAllGroupConstraints_18 = 262144,
			KillsVariantObjects_19 = 524288,
			ForceUnattachedEffects_20 = 1048576,
			FiresUnderThreshold_21 = 2097152,
			TriggersSpecialDeath_22 = 4194304,
			OnlyOnSpecialDeath_23 = 8388608,
			OnlyNOTOnSpecialDeath_24 = 16777216,
		}
		public enum NewState9Options
		{
			Default_0 = 0,
			MinorDamage_1 = 1,
			MediumDamage_2 = 2,
			MajorDamage_3 = 3,
			Destroyed_4 = 4,
		}
		[Field("response type", typeof(ResponseType0Options))]
		public short ResponseType0;
		[Field("constraint damage type", typeof(ConstraintDamageType2Options))]
		public short ConstraintDamageType2;
		[Field("flags", typeof(Flags4Options))]
		public int Flags4;
		[Field("damage threshold#repsonse fires after crossing this threshold.  1=full health", null)]
		public float DamageThreshold5;
		[Field("transition effect", null)]
		public TagReference TransitionEffect6;
		[Field("damage effect", typeof(InstantaneousResponseDamageEffectStructBlock))]
		[Block("Instantaneous Response Damage Effect Struct", 1, typeof(InstantaneousResponseDamageEffectStructBlock))]
		public InstantaneousResponseDamageEffectStructBlock DamageEffect7;
		[Field("region", null)]
		public StringId Region8;
		[Field("new state", typeof(NewState9Options))]
		public short NewState9;
		[Field("runtime region index*", null)]
		public short RuntimeRegionIndex10;
		[Field("effect marker name", null)]
		public StringId EffectMarkerName11;
		[Field("damage effect marker", typeof(InstantaneousResponseDamageEffectMarkerStructBlock))]
		[Block("Instantaneous Response Damage Effect Marker Struct", 1, typeof(InstantaneousResponseDamageEffectMarkerStructBlock))]
		public InstantaneousResponseDamageEffectMarkerStructBlock DamageEffectMarker12;
		[Field("response delay#in seconds", null)]
		public float ResponseDelay14;
		[Field("delay effect", null)]
		public TagReference DelayEffect15;
		[Field("delay effect marker name", null)]
		public StringId DelayEffectMarkerName16;
		[Field("constraint/group name", null)]
		public StringId ConstraintGroupName18;
		[Field("ejecting seat label", null)]
		public StringId EjectingSeatLabel20;
		[Field("skip fraction", null)]
		public float SkipFraction22;
		[Field("destroyed child object marker name", null)]
		public StringId DestroyedChildObjectMarkerName24;
		[Field("total damage threshold", null)]
		public float TotalDamageThreshold26;
	}
}
#pragma warning restore CS1591
