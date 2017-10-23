using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("cellular_automata", "devo", "����", typeof(CellularAutomataBlock))]
	[FieldSet(564, 4)]
	public unsafe struct CellularAutomataBlock
	{
		[Field("updates per second:Hz", null)]
		public short UpdatesPerSecond1;
		[Field("x (width):cells", null)]
		public short XWidth2;
		[Field("y (depth):cells", null)]
		public short YDepth3;
		[Field("z (height):cells", null)]
		public short ZHeight4;
		[Field("x (width):world units", null)]
		public float XWidth5;
		[Field("y (depth):world units", null)]
		public float YDepth6;
		[Field("z (height):world units", null)]
		public float ZHeight7;
		[Field("", null)]
		public fixed byte _8[32];
		[Field("cell birth chance:[0,1]", null)]
		public float CellBirthChance11;
		[Field("", null)]
		public fixed byte _12[32];
		[Field("cell gene mutates 1 in:times", null)]
		public int CellGeneMutates1In14;
		[Field("virus gene mutations 1 in:times", null)]
		public int VirusGeneMutations1In15;
		[Field("", null)]
		public fixed byte _16[32];
		[Field("infected cell lifespan:updates#the lifespan of a cell once infected", null)]
		public FloatBounds InfectedCellLifespan18;
		[Field("minimum infection age:updates#no cell can be infected before it has been alive this number of updates", null)]
		public short MinimumInfectionAge19;
		[Field("", null)]
		public fixed byte _20[2];
		[Field("cell infection chance:[0,1]", null)]
		public float CellInfectionChance21;
		[Field("infection threshold:[0,1]#0.0 is most difficult for the virus, 1.0 means any virus can infect any cell", null)]
		public float InfectionThreshold22;
		[Field("", null)]
		public fixed byte _23[32];
		[Field("new cell filled chance:[0,1]", null)]
		public float NewCellFilledChance25;
		[Field("new cell infected chance:[0,1]", null)]
		public float NewCellInfectedChance26;
		[Field("", null)]
		public fixed byte _27[32];
		[Field("detail texture change chance:[0,1]", null)]
		public float DetailTextureChangeChance29;
		[Field("", null)]
		public fixed byte _30[32];
		[Field("detail texture width:cells#the number of cells repeating across the detail texture in both dimensions", null)]
		public short DetailTextureWidth31;
		[Field("", null)]
		public fixed byte _32[2];
		[Field("detail texture", null)]
		public TagReference DetailTexture33;
		[Field("", null)]
		public fixed byte _35[32];
		[Field("mask bitmap", null)]
		public TagReference MaskBitmap36;
		[Field("", null)]
		public fixed byte _37[240];
	}
}
#pragma warning restore CS1591
