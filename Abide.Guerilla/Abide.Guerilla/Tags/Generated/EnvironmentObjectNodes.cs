using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(4, 4)]
	public unsafe struct EnvironmentObjectNodes
	{
		public enum ProjectionSign2Options
		{
			ProjectionSign_0 = 1,
		}
		[Field("reference frame index", null)]
		public short ReferenceFrameIndex0;
		[Field("projection axis", null)]
		public int ProjectionAxis1;
		[Field("projection sign", typeof(ProjectionSign2Options))]
		public byte ProjectionSign2;
	}
}
#pragma warning restore CS1591
