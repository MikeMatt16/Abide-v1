using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[FieldSet(68, 4)]
	public unsafe struct GlobalScenarioLoadParametersBlock
	{
		[Field("scenario^", null)]
		public TagReference Scenario1;
		[Field("parameters", null)]
		[Data(65535)]
		public TagBlock Parameters2;
		[Field("", null)]
		public fixed byte _3[32];
	}
}
#pragma warning restore CS1591
