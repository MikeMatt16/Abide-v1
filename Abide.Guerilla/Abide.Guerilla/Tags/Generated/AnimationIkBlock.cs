using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(8, 4)]
	public unsafe struct AnimationIkBlock
	{
		[Field("marker#the marker name on the object being attached", null)]
		public StringId Marker0;
		[Field("attach to marker#the marker name object (weapon, vehicle, etc.) the above marker is being attached to", null)]
		public StringId AttachToMarker1;
	}
}
#pragma warning restore CS1591
