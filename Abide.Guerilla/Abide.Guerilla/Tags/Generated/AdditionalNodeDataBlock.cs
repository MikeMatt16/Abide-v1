using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(60, 4)]
	public unsafe struct AdditionalNodeDataBlock
	{
		[Field("node name^", null)]
		public StringId NodeName0;
		[Field("default rotation*", null)]
		public Quaternion DefaultRotation1;
		public Vector3 DefaultTranslation2;
		[Field("default scale*", null)]
		public float DefaultScale3;
		public Vector3 MinBounds4;
		public Vector3 MaxBounds5;
	}
}
#pragma warning restore CS1591
