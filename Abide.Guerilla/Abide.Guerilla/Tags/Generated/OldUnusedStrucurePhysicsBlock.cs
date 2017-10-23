using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(60, 4)]
	public unsafe struct OldUnusedStrucurePhysicsBlock
	{
		[Field("*mopp code", null)]
		[Data(1048576)]
		public TagBlock MoppCode0;
		[Field("*Evironment Object Identifiers", null)]
		[Block("Old Unused Object Identifiers Block", 2048, typeof(OldUnusedObjectIdentifiersBlock))]
		public TagBlock EvironmentObjectIdentifiers1;
		[Field("", null)]
		public fixed byte _2[4];
		public Vector3 MoppBoundsMin3;
		public Vector3 MoppBoundsMax4;
	}
}
#pragma warning restore CS1591
