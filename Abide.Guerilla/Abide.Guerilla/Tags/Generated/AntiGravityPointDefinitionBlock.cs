using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(76, 4)]
	public unsafe struct AntiGravityPointDefinitionBlock
	{
		public enum Flags1Options
		{
			GetsDamageFromRegion_0 = 1,
		}
		[Field("marker name^", null)]
		public StringId MarkerName0;
		[Field("flags", typeof(Flags1Options))]
		public int Flags1;
		[Field("antigrav strength", null)]
		public float AntigravStrength2;
		[Field("antigrav offset", null)]
		public float AntigravOffset3;
		[Field("antigrav height", null)]
		public float AntigravHeight4;
		[Field("antigrav damp factor", null)]
		public float AntigravDampFactor5;
		[Field("antigrav normal k1", null)]
		public float AntigravNormalK16;
		[Field("antigrav normal k0", null)]
		public float AntigravNormalK07;
		[Field("radius", null)]
		public float Radius8;
		[Field("", null)]
		public fixed byte _9[12];
		[Field("", null)]
		public fixed byte _10[2];
		[Field("", null)]
		public fixed byte _11[2];
		[Field("damage source region name", null)]
		public StringId DamageSourceRegionName12;
		[Field("default state error", null)]
		public float DefaultStateError13;
		[Field("minor damage error", null)]
		public float MinorDamageError14;
		[Field("medium damage error", null)]
		public float MediumDamageError15;
		[Field("major damage error", null)]
		public float MajorDamageError16;
		[Field("destroyed state error", null)]
		public float DestroyedStateError17;
	}
}
#pragma warning restore CS1591
