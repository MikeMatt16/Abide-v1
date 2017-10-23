using Abide.Guerilla.Types;
using Abide.HaloLibrary;

namespace Abide.Guerilla.Tags
{
#pragma warning disable CS1591
	[TagGroup("model_animation_graph", "jmad", "����", typeof(ModelAnimationGraphBlock))]
	[FieldSet(236, 4)]
	public unsafe struct ModelAnimationGraphBlock
	{
		[Field("resources", typeof(AnimationGraphResourcesStructBlock))]
		[Block("Animation Graph Resources Struct", 1, typeof(AnimationGraphResourcesStructBlock))]
		public AnimationGraphResourcesStructBlock Resources0;
		[Field("content", typeof(AnimationGraphContentsStructBlock))]
		[Block("Animation Graph Contents Struct", 1, typeof(AnimationGraphContentsStructBlock))]
		public AnimationGraphContentsStructBlock Content1;
		[Field("run time data", typeof(ModelAnimationRuntimeDataStructBlock))]
		[Block("Model Animation Runtime Data Struct", 1, typeof(ModelAnimationRuntimeDataStructBlock))]
		public ModelAnimationRuntimeDataStructBlock RunTimeData2;
		[Field("last import results*", null)]
		[Data(131072)]
		public TagBlock LastImportResults4;
		[Field("additional node data", null)]
		[Block("Additional Node Data Block", 255, typeof(AdditionalNodeDataBlock))]
		public TagBlock AdditionalNodeData5;
	}
}
#pragma warning restore CS1591
