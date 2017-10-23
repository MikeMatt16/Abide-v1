using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct NewHudDashlightDataStructBlock
	{
		[Field("low clip cutoff#the cutoff for showing the reload dashlight", null)]
		public short LowClipCutoff1;
		[Field("low ammo cutoff#the cutoff for showing the low ammo dashlight", null)]
		public short LowAmmoCutoff2;
		[Field("age cutoff#the age cutoff for showing the low battery dashlight", null)]
		public float AgeCutoff3;
	}
}
#pragma warning restore CS1591
