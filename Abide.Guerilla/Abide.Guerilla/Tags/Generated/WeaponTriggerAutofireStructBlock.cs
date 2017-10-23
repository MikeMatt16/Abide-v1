using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(12, 4)]
	public unsafe struct WeaponTriggerAutofireStructBlock
	{
		public enum SecondaryAction3Options
		{
			Fire_0 = 0,
			Charge_1 = 1,
			Track_2 = 2,
			FireOther_3 = 3,
		}
		public enum PrimaryAction4Options
		{
			Fire_0 = 0,
			Charge_1 = 1,
			Track_2 = 2,
			FireOther_3 = 3,
		}
		[Field("autofire time", null)]
		public float AutofireTime1;
		[Field("autofire throw", null)]
		public float AutofireThrow2;
		[Field("secondary action", typeof(SecondaryAction3Options))]
		public short SecondaryAction3;
		[Field("primary action", typeof(PrimaryAction4Options))]
		public short PrimaryAction4;
	}
}
#pragma warning restore CS1591
